using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FogOilAssistant.Components.Data;
using FogOilAssistant.Components.Data.Pages.Oil;
using FogOilAssistant.Components.Data.Product;

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
        List<CarType> carTypes;

        public List<CarType> CarTypes { 
            get => carTypes;
            set
            {
                this.carTypes = value;
                OnPropertyChanged("CarTypes");
            } 
        }


        List<CarBrand> carBrands;
        public List<CarBrand> CarBrands {
            get => carBrands;
            set
            {
                carBrands = value;
                OnPropertyChanged("CarBrands");
            }
        }

      


        List<CarModel> carModels;
        public List<CarModel> CarModels { 
            get => carModels;
            set
            {
                carModels = value;
                OnPropertyChanged("CarModels");
            }
        }

        #endregion

        #region Results (Products, Select partial)
        List<Product> products; 
        public List<Product> Products { 
            get => products;
            set
            {
                products = value;
                OnPropertyChanged("Products");
            }
        }

        private CarObject carObject;
        #endregion

        #region Commands
        public CommandViewModel Select { get => new CommandViewModel(select); }
        
        
        private void select()
        {
            MessageBox.Show("ok");
        }
       


        #endregion

        #region Selected Car Type

        private CarType carType;
        public CarType CarType {
            get => carType;
            set 
            {
                carType = value;
                OnPropertyChanged("CarType");
                OnPropertyChanged("IsCar");
                OnPropertyChanged("IsBus");
                OnPropertyChanged("IsMotorcycle");
                OnPropertyChanged("IsTruck");

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
              
                MessageBox.Show(value.ToString());
            }
        }
        public int SelectedBrandIndex { 
            get => selectedBrandIndex;
            set
            {
                this.selectedBrandIndex = value;
                OnPropertyChanged("SelectedBrandIndex");
             
                MessageBox.Show(value.ToString());
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
            this.CarTypes = new List<CarType>()
            {
                new CarType(){ ID=0, Name="Car"},
                new CarType(){ ID=1, Name="Bus"},
                new CarType(){ ID=2, Name="Motorcycle"},
                new CarType(){ ID=3, Name="Truck"}
            };
            this.carBrands = new List<CarBrand>()
            {
                new CarBrand(){  ID=0, Name="Volkswagen" },
                new CarBrand(){ ID=1, Name="Toyota" },
                new CarBrand(){ ID=2, Name="Subaru" },
                new CarBrand(){ ID=3, Name="Honda" }
                };
            this.CarModels = new List<CarModel>()
            {
                new CarModel(){ ID=1, Name="asd"}
            };
            this.Products = new List<Product>()
            {
                
            };
            this.carObject = new CarObject();

        }
    }
}
