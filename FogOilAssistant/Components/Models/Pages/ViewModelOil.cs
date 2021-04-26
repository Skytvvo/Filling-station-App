using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using FogOilAssistant.Components.Data;
using FogOilAssistant.Components.Data.GlobalStorage;
using FogOilAssistant.Components.Database;
using Application = System.Windows.Application;
using CarBrand = FogOilAssistant.Components.Database.CarBrand;
using CarModel = FogOilAssistant.Components.Database.CarModel;
using MessageBox = System.Windows.MessageBox;
using Product = FogOilAssistant.Components.Database.Product;

namespace FogOilAssistant.Components.Models.Pages
{
    public class ViewModelOil : INotifyPropertyChanged
    {

        #region Oil Page
        FogOilAssistant.Components.View.Pages.Oil oil;
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        #endregion
            
        #region Select parts
        List<FogOilAssistant.Components.Database.CarType> carTypes;

        public List<FogOilAssistant.Components.Database.CarType> CarTypes { 
            get => carTypes;
            set
            {
                this.carTypes = value;
                OnPropertyChanged("CarTypes");
            } 
        }


        //Brands List
        List<Database.CarBrand> carBrands;
        public List<Database.CarBrand> CarBrands {
            get => carBrands;
            set
            {
                carBrands = value;
                OnPropertyChanged("CarBrands");
            }
        }

      

        //Models List
        List<Database.CarModel> carModels;
        public List<Database.CarModel> CarModels { 
            get => carModels;
            set
            {
                carModels = value;
                OnPropertyChanged("CarModels");
            }
        }

        #endregion

        #region Results (Products, Select partial)
        List<Database.Product> products; 
        public List<Database.Product> Products { 
            get => products;
            set
            {
                products = value;
                OnPropertyChanged("Products");
            }
        }

        private Database.CarObject carObject;
        #endregion

        #region Commands
        public CommandViewModel Select { get => new CommandViewModel(select); }
        
        
        private async void select()
        {

            try
            {
                using(FogOilEntities db = new FogOilEntities())
                {
                    foreach (var item in DataBaseData.getInstance().CarObjects)
                    {
                        //this shit doesn't want to work with linq 
                        var result = db.CarObjects.Find(item.CarId);
                        if (result.CarType == this.CarType.TypeId)
                            if (result.CarBrand == this.CarBrands[SelectedBrandIndex].BrandId)
                                if (result.CarModel == this.CarModels[SelectedModelIndex].ModelId)
                                    this.Products = new List<Product>(result.Products);
                    }
                }
            }
            catch (Exception e)
            {

            }
          
        }


        public RelayCommand BuyProduct
        {
            get => new RelayCommand(productId =>
            {
                  DataBaseData.getInstance().AddToUserBasket((int)productId);
            });
        }
        #endregion

        #region Selected Car Type

        private Database.CarType carType;
        public Database.CarType CarType {
            get => carType;
            set 
            {
                carType = value;
                OnPropertyChanged("CarType");
                OnPropertyChanged("IsCar");
                OnPropertyChanged("IsBus");
                OnPropertyChanged("IsMotorcycle");
                OnPropertyChanged("IsTruck");

                this.CarBrands  =  DataBaseData.getInstance().CarObjects.FindAll(item => item.CarType == value.TypeId)
                    .Select(item => item.CarBrand1).Distinct().ToList() as List<CarBrand>;
                if (this.CarBrands != null && this.CarBrands.Count != 0)
                    SelectedBrandIndex = 0;
                else
                    SelectedBrandIndex = -1;
                this.getPage();
                 

            }
        }

     
        public bool IsCar
        {
            get { return this.CarType == CarTypes[0]; }
            set
            {
                this.CarType = value ? CarTypes[0] : this.CarType;
            }
        }

        public bool IsBus
        {
            get { return this.CarType == CarTypes[1]; }
            set
            {
                this.CarType = value ? CarTypes[1] : this.CarType;
            }
        }
        public bool IsMotorcycle
        {
            get { return this.CarType == CarTypes[2]; }
            set
            {
                this.CarType = value ? CarTypes[2] : this.CarType;
            }
        }
        public bool IsTruck
        {
            get { return this.CarType == CarTypes[3]; }
            set
            {
                this.CarType = value ? CarTypes[3] : this.CarType;
            }
        }
        #endregion


        #region Selected Car parts

        private int selectedModelIndex = 0;
        private int selectedBrandIndex = 0;

        public int SelectedModelIndex { 
            get => selectedModelIndex;
            set
            {
                this.selectedModelIndex = value;
                
                OnPropertyChanged("SelectedModelIndex");
              
            }
        }
        public int SelectedBrandIndex { 
            get => selectedBrandIndex;
            set
            {
                this.selectedBrandIndex = value;

                try
                {
                    this.CarModels = DataBaseData.getInstance().CarObjects.FindAll(item => item.CarBrand == CarBrands[value].BrandId)
                        .Select(item => item.CarModel1).ToList() as List<CarModel>;
                }
                catch (Exception e)
                {
                    SelectedModelIndex = -1;
                    CarModels = new List<CarModel>();
                }
             
                if (this.carModels != null && this.CarModels.Count != 0)
                    SelectedModelIndex = 0;
                else
                    SelectedModelIndex = -1;

                OnPropertyChanged("SelectedBrandIndex");
            } 
        }


       
        #endregion

        #region DOM ELEMENTS QUERIES

        private void getPage()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(FogOilAssistant.MainWindow))
                {
                    var frame_content = (window as FogOilAssistant.MainWindow).Frame.Content;
                    if(frame_content.GetType() == typeof(FogOilAssistant.Components.View.Pages.Oil))
                    {
                        this.oil = frame_content as FogOilAssistant.Components.View.Pages.Oil;

                    }

                }
            }
        }



        #endregion

        public ViewModelOil()
        {
            this.CarTypes = DataBaseData.getInstance().CarTypes;
           
            

        }
    }
}
