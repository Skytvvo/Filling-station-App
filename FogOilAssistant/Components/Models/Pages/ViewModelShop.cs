using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using FogOilAssistant.Components.Data.Product;

namespace FogOilAssistant.Components.Models.Pages
{


    public class ViewModelShop: INotifyPropertyChanged
    {

        private string _Path;
        private int ID = 1;

        public string Path
        {
            get => _Path;
            set
            {
                _Path = value;
                OnPropertyChanged("Path");
            }
        }
        private List<Product> products;
        public List<Product> Products
        {
            get
            {   
                return this.products;
            }
            set
            {
                this.products = value;
                OnPropertyChanged("Products");
            }
        }

        public ViewModelShop()
        {
            this.Path = "/Components/Images/Slider/1.jpg";
     
            aTimer = new System.Timers.Timer(); 
            aTimer.Interval = 7000;
            aTimer.Elapsed += OnTimedEvent;
            aTimer.Enabled = true;

            this.Products = new List<Product>()
            {
                new Product( "Oil", "79 BYN", "Petroleum engineering is a field of engineering concerned with the activities related to the production of Hydrocarbons, which can be either crude oil or natural gas.[1] Exploration and production are deemed to fall within the upstream sector of the oil and gas industry. Exploration, by earth scientists, and petroleum engineering are the oil and gas industry's two main subsurface disciplines, which focus on maximizing economic recovery of hydrocarbons from subsurface reservoirs. Petroleum geology and geophysics focus on provision of a static description of the hydrocarbon reservoir rock, while petroleum engineering focuses on estimation of the recoverable volume of this resource using a detailed understanding of the physical behavior of oil, water and gas within porous rock at very high pressure.","/Components/Images/Products/oil.png", 0),
                new Product( "Oil", "79 BYN", "Oil for your engine","/Components/Images/Products/oil.png", 0),
                new Product( "Oil", "79 BYN", "Oil for your engine","/Components/Images/Products/oil.png", 0),
                new Product( "Oil", "79 BYN", "Oil for your engine","/Components/Images/Products/oil.png", 0),
                new Product( "Oil", "79 BYN", "Oil for your engine","/Components/Images/Products/oil.png", 0),
                new Product( "Oil", "79 BYN", "Oil for your engine","/Components/Images/Products/oil.png", 0)
            };               

        }

        #region Events

        private static Timer aTimer;
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (ID == 3)
                ID = 0;
            this.Path = $"/Components/Images/Slider/{++ID}.jpg";
        }
        #endregion
    }


}
