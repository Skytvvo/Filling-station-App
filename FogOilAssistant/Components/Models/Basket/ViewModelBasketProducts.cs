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
                        if(item!=null)
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
            if (DataBaseData.getInstance().UserId != 0 && DataBaseData.getInstance().Login != null)
                if (moveBasketToPurchase())
                    clear_basket();
            /*
             Description:
                1) if user is signing in or up => 2
                2) if products from basket was moved to purchase table => 3
                3) then clear basket (from app and database)
             */
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
        private bool moveBasketToPurchase()
        {
            try
            {
                using (FogOilEntities db = new FogOilEntities())
                {
                    foreach (var item in db.Users.Find(DataBaseData.getInstance().UserId).Baskets)
                        db.Users.Find(DataBaseData.getInstance().UserId).UserProducts.Add(new UserProduct() { ProductId = item.ProductId, LocationId = 1 });
                    db.SaveChanges();
                }
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        private bool clear_basket()
        {
           try
            {
                using (FogOilEntities db = new FogOilEntities())
                {
                    DataBaseData.getInstance().basketProducts.Clear();
                    db.Baskets.RemoveRange(db.Users.Find(DataBaseData.getInstance().UserId).Baskets);
                    db.SaveChanges();
                }
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
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
                MessageBox.Show("Collection was updated");
            }
        #endregion

    }
}
