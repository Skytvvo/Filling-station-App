using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
                    UsedProducts = (await db.CarObjects.FindAsync(SelectedCar.CarId)).Products.ToList();
                    
                    List<Product> prod = new List<Product>(db.Products);
                    var res = prod.Except(UsedProducts);
                    UnusedProducts = res.ToList();
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
        #endregion
        public ViewModelCarManagment()
        {
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
