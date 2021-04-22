using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FogOilAssistant.Annotations;
using FogOilAssistant.Components.Data;
using FogOilAssistant.Components.Data.GlobalStorage;
using FogOilAssistant.Components.Database;

namespace FogOilAssistant.Components.Models.Basket
{
    public class ViewModelBasketProducts : INotifyPropertyChanged
    {

        #region Props
        //purchase
        private List<Product> baskedProducts;
        public List<Product> BaskedProducts
        {
            get => baskedProducts;
            set
            {
                try
                {
                    this.baskedProducts = value;
                    this.Total = 0;
                    this.baskedProducts.ForEach(item =>
                    {
                        this.Total += item.Price;
                    });
                    OnPropertyChanged("BaskedProducts");
                }
                catch (Exception e)
                {
                    this.BaskedProducts = DataBaseData.getInstance().basketProducts.ToList();
                }
               
            }
        }

        //total
        private double total = 0;

        public double Total
        {
            get => total;
            set
            {
                total = value;
                OnPropertyChanged("Total");
            }
        }
        #endregion


        #region Constructor
       
        public ViewModelBasketProducts()
        {
            this.BaskedProducts = DataBaseData.getInstance().basketProducts.ToList();
            DataBaseData.getInstance().basketProducts.CollectionChanged += this.Basket_CollectionChanged;
            
        }

        #endregion

        #region Commands
        public RelayCommand basket_remove
        {
            get => new RelayCommand(Remove);
        }
        public CommandViewModel BuyItems
        {
            get => new CommandViewModel(Buy);
        }

        #endregion

        #region Commands methods

        private void Buy()
        {
            //operations for db or message for user

            //

            //clear basket
            clear_basket();
        }

        private void Remove(object item_id)
        {
            //use DB COMMANDS FOR BACK
            
            DataBaseData.getInstance().basketProducts.Remove(
                //remove element, where productId equals item_id
                this.BaskedProducts.First(item => item.ProductId == (int) item_id)
            );

        }

        #endregion

        #region Methods

        private void clear_basket()
        {
            DataBaseData.getInstance().basketProducts.Clear();
        }

        #endregion
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }


            void Basket_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
            {
                this.BaskedProducts = DataBaseData.getInstance().basketProducts.ToList();
            }
        #endregion

    }
}
