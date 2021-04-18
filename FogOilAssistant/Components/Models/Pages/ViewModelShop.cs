﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using FogOilAssistant.Components.Data;
using FogOilAssistant.Components.Data.GlobalStorage;
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

            this.Products = DataBaseData.getInstance().Products;

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
