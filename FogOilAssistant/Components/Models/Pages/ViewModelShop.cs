using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using FogOilAssistant.Components.Data;
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

        #region Commands
        public CommandViewModel SortDesc { get => new CommandViewModel(sort_desc); }
        public void sort_desc()
        {
            var SortedList = this.Products.OrderByDescending(item => item.Price).Cast<Product>();
            List<Product> finish = new List<Product>();
            foreach (Product product in SortedList)
                finish.Add(product);
            this.Products = finish;
        }


        public CommandViewModel Sort { get => new CommandViewModel(sort); }
        public void sort()
        {
            var SortedList = this.Products.OrderBy(item => item.Price).Cast<Product>();
            List<Product> finish = new List<Product>();
            foreach (Product product in SortedList)
                finish.Add(product);
            this.Products = finish;
        }

        public CommandViewModel SortByName { get => new CommandViewModel(sortByName); }
        public void sortByName()
        {
            var SortedList = this.Products.OrderBy(item => item.Name).Cast<Product>();
            List<Product> finish = new List<Product>();
            foreach (Product product in SortedList)
                finish.Add(product);
            this.Products = finish;
        }

        public CommandViewModel SortByNameDesc { get => new CommandViewModel(sortByNameDesc); }
        public void sortByNameDesc()
        {
            var SortedList = this.Products.OrderByDescending(item => item.Name).Cast<Product>();
            List<Product> finish = new List<Product>();
            foreach (Product product in SortedList)
                finish.Add(product);
            this.Products = finish;
        }
        #endregion

        public ViewModelShop()
        {
            this.Path = "/Components/Images/Slider/1.jpg";
     
            aTimer = new System.Timers.Timer(); 
            aTimer.Interval = 7000;
            aTimer.Elapsed += OnTimedEvent;
            aTimer.Enabled = true;

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
