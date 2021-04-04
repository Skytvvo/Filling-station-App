using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FogOilAssistant.Components.Data.Pages.Oil;
using FogOilAssistant.Components.Data.Product;

namespace FogOilAssistant.Components.Models.Pages
{
    public class ViewModelOil : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

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


        List<Product> products; 
        public List<Product> Products { 
            get => products;
            set
            {
                products = value;
                OnPropertyChanged("Products");
            }
        }

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
                new CarBrand(){  ID=0, Name="Volkswagen"},
                new CarBrand(){ ID=1, Name="Toyota"},
                new CarBrand(){ ID=2, Name="Subaru"},
                new CarBrand(){ ID=3, Name="Honda"}
            };
            this.CarModels = new List<CarModel>()
            {
                new CarModel(){ ID=0, Name="ASD"},
                new CarModel(){ ID=0, Name="ASD"},
                new CarModel(){ ID=0, Name="ASD"},
                new CarModel(){ ID=0, Name="ASD"}
            };
            this.Products = new List<Product>()
            {
                new Product( "Joil", 12, "Petroleum engineering is a field of engineering concerned with the activities related to the production of Hydrocarbons, which can be either crude oil or natural gas.[1] Exploration and production are deemed to fall within the upstream sector of the oil and gas industry. Exploration, by earth scientists, and petroleum engineering are the oil and gas industry's two main subsurface disciplines, which focus on maximizing economic recovery of hydrocarbons from subsurface reservoirs. Petroleum geology and geophysics focus on provision of a static description of the hydrocarbon reservoir rock, while petroleum engineering focuses on estimation of the recoverable volume of this resource using a detailed understanding of the physical behavior of oil, water and gas within porous rock at very high pressure.","/Components/Images/Products/oil.png", 0),
                new Product( "Smart oil", 25, "Oil for your engine","/Components/Images/Products/oil.png", 0),
                new Product( "Eco oil", 67, "Oil for your engine","/Components/Images/Products/oil.png", 0),
                new Product( "Oil", 44, "Oil for your engine","/Components/Images/Products/oil.png", 0),
                new Product( "Palm oil", 129, "Oil for your engine","/Components/Images/Products/oil.png", 0),
                new Product( "Oil", 5, "Oil for your engine","/Components/Images/Products/oil.png", 0)
            };
        }
    }
}
