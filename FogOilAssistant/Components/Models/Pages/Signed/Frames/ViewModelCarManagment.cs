using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using FogOilAssistant.Components.Data;
using FogOilAssistant.Components.Database;

namespace FogOilAssistant.Components.Models.Pages.Signed.Frames
{
    public class ViewModelCarManagment : INotifyPropertyChanged
    {
        #region search
        private string searchInput;

        public string SearchInput
        {
            get => searchInput;
            set
            {
                searchInput = value;
                OnPropertyChanged("SearchInput");
            }
        }

        #endregion
        #region cars
        private List<CarObject> cars;

        public List<CarObject> Cars
        {
            get => cars;
            set
            {
                cars = value;
                OnPropertyChanged("Cars");
            }
        }

        #endregion
        #region Selected Car

        private CarObject selectedCar;
        public CarObject SelectedCar
        {
            get => selectedCar;
            set
            {
                selectedCar = value;
                OnPropertyChanged("SelectedCar");
            }
        }
        #endregion
        #region Cars loading
        private async void loadCars()
        {
            try
            {
                using (FogOilEntities db = new FogOilEntities())
                {
                    await Task.Run(() => {
                        Cars = db.CarObjects.ToList();
                        foreach (var item in Cars)
                            // don't touch this, if you remove this then binding will be failure
                            _ = item.CarModel1.Name;
                        CarTypes = db.CarTypes.ToList();
                        CarBrands = db.CarBrands.ToList();
                    });
                }
            }
            catch (Exception e)
            {

            }
        }
        #endregion

        #region UI Car parts
        private int selectedBrand;
        public int SelectedBrand
        {
            get => selectedBrand;
            set
            {
                selectedBrand = value;
                OnPropertyChanged("SelectedBrand");
            }
        }

        private int selectedType;
        public int SelectedType
        {
            get => selectedType;
            set
            {
                selectedType = value;
                OnPropertyChanged("SelectedType");
            }
        }

        private string selectedModel;
        public string SelectedModel
        {
            get => selectedModel;
            set
            {
                selectedModel = value;
                OnPropertyChanged("SelectedModel");
            }
        }
        private List<CarType> carTypes;
        public List<CarType> CarTypes
        {
            get => carTypes;
            set
            {
                carTypes = value;
                OnPropertyChanged("CarTypes");
            }
        }
        private List<CarBrand> carBrands;
        public List<CarBrand> CarBrands
        {
            get => carBrands;
            set
            {
                carBrands = value;
                OnPropertyChanged("CarBrands");
            }
        }

        private bool editorVisibility;
        public bool EditorVisibility
        {
            get => editorVisibility;
            set
            {
                editorVisibility = value;
                OnPropertyChanged("EditorVisibility");
            }
        }


        #endregion

        #region UI Collection Switch
        private double usedCollectionOpacity = 0.3;
        public double UsedCollectionOpacity
        {
            get => usedCollectionOpacity;
            set
            {
                usedCollectionOpacity = value;
                OnPropertyChanged("UsedCollectionOpacity");
            }
        }
        private double unusedCollectionOpacity = 0.3;
        public double UnusedCollectionOpacity
        {
            get => unusedCollectionOpacity;
            set
            {
                unusedCollectionOpacity = value;
                OnPropertyChanged("UnusedCollectionOpacity");
            }
        }

        private ObservableCollection<Product> collectionUI;
        public ObservableCollection<Product> CollectionUI
        {
            get => collectionUI;
            set
            {
                collectionUI = value;
                OnPropertyChanged("CollectionUI");
            }
        }

        private async void switchToUsed()
        {
            UsedCollectionOpacity = 1;
            UnusedCollectionOpacity = 0.3;
            ProductAction = new RelayCommand(removeProduct);
            UICollectionBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            await Task.Run(()=> { 
                CollectionUI = new ObservableCollection<Product> (usedProducts);

            });
        }

        private async void switchToUnused()
        {
            UnusedCollectionOpacity = 1;
            UsedCollectionOpacity = 0.3;
            ProductAction = new RelayCommand(addProduct);
            UICollectionBrush = new SolidColorBrush(Color.FromRgb(37, 255, 0));
            await Task.Run(() =>
            {
                CollectionUI = new ObservableCollection<Product>( unusedProducts);
            });
        }

        private SolidColorBrush uiCollectionBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
        public SolidColorBrush UICollectionBrush
        {
            get => uiCollectionBrush;
            set
            {
                uiCollectionBrush = value;
                OnPropertyChanged("UICollectionBrush");
            }
        }
        #endregion


        #region Products
        private List<Product> unusedProducts;
        public List<Product> UnusedProducts
        {
            get => unusedProducts;
            set
            {
                unusedProducts = value;
                OnPropertyChanged("UnusedProducts");
            }
        }
        private List<Product> usedProducts;
        public List<Product> UsedProducts
        {
            get => usedProducts;
            set
            {
                usedProducts = value;
                OnPropertyChanged("UsedProducts");
            }
        }
        #endregion

        #region Commands
        public RelayCommand OpenEditor { get => new RelayCommand(openEditor); }
        public CommandViewModel CloseEditor { get => new CommandViewModel(closeEditor); }
        public CommandViewModel SaveChanges { get => new CommandViewModel(saveChanges); }
        public CommandViewModel RemoveCar { get => new CommandViewModel(removeCar); }
        public CommandViewModel SwitchToUnused { get => new CommandViewModel(switchToUnused); }
        public CommandViewModel SwitchToUsed { get => new CommandViewModel(switchToUsed); }

        private RelayCommand productAction;
        public RelayCommand ProductAction
        {
            get => productAction;
            set
            {
                productAction = value;
                OnPropertyChanged("ProductAction");
            }
        }
        #endregion

        #region Command methods
        private async void openEditor(object obj)
        {
            try
            {
                SelectedCar = obj as CarObject;
                SelectedBrand = CarBrands.FindIndex(item=>item.BrandId == SelectedCar.CarBrand);
                SelectedModel = SelectedCar.CarModel1.Name;
                SelectedType = CarTypes.FindIndex(item => item.TypeId == SelectedCar.CarType);
                using (FogOilEntities db = new FogOilEntities())
                {
                    UsedProducts = (await db.CarObjects.FindAsync(SelectedCar.CarId)).CarProducts.Select(pr=>pr.Product1).ToList();

                    await Task.Run(() =>
                    {
                        List<Product> prod = new List<Product>(db.Products);
                        var res = prod.Except(UsedProducts);
                        UnusedProducts = res.ToList();
                    });
                }
               

                EditorVisibility = true;
            }
            catch(Exception  e)
            {

            }
        }
        private void closeEditor()
        {
            EditorVisibility = false;
        }

        private async void saveChanges()
        {
            try
            {
                using(FogOilEntities db = new FogOilEntities())
                {
                    (await db.CarModels.FindAsync(SelectedCar.CarModel)).Name = SelectedModel;
                    (await db.CarObjects.FindAsync(selectedCar.CarId)).CarBrand = CarBrands[SelectedBrand].BrandId;
                    (await db.CarObjects.FindAsync(selectedCar.CarId)).CarType = CarTypes[SelectedType].TypeId;
                    var res = UsedProducts.Select(prod => new CarProduct() { Car = SelectedCar.CarId, Product = prod.ProductId });
                    (await db.CarObjects.FindAsync(selectedCar.CarId)).CarProducts = res.ToList();


                    await db.SaveChangesAsync();
                    loadCars();
                    closeEditor();
                }
            }
            catch(Exception e)
            {
                closeEditor();
            }
        }
        private async void removeCar()
        {
            try
            {
                using (FogOilEntities db = new FogOilEntities())
                {
                    
                    db.CarObjects.Remove(await db.CarObjects.FindAsync(SelectedCar.CarId));
                    await db.SaveChangesAsync();
                    loadCars();
                    closeEditor();
                }
            }
            catch (Exception e)
            {
                closeEditor();
            }
        }

        private void removeProduct(object product)
        {
            try
            {
                var tenmpProduct = usedProducts.Find(item => item.ProductId == (product as Product).ProductId);
                var awnser =  usedProducts.Remove(tenmpProduct);
                unusedProducts.Add(tenmpProduct);
                //CollectionUI = CollectionUI;

                if (UnusedCollectionOpacity == 1)
                    CollectionUI = new ObservableCollection<Product> (unusedProducts);
                else
                    CollectionUI = new ObservableCollection<Product>( usedProducts);
            }
            catch (Exception e)
            {

            }
        }
        private void addProduct(object product)
        {
            try
            {
                var tenmpProduct = unusedProducts.Find(item => item.ProductId == (product as Product).ProductId);
                var awnser = unusedProducts.Remove(tenmpProduct);
                usedProducts.Add(tenmpProduct);
                if (UnusedCollectionOpacity == 1)
                    CollectionUI = new ObservableCollection<Product>( unusedProducts);
                else
                    CollectionUI = new ObservableCollection<Product>( usedProducts);

            }
            catch (Exception e)
            {

            }
        }
        #endregion
        public ViewModelCarManagment()
        {
            ProductAction = new RelayCommand(removeProduct);
            loadCars();
        }

        

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
