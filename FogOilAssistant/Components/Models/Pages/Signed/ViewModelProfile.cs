using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FogOilAssistant.Components.Data;
using FogOilAssistant.Components.Data.GlobalStorage;
using FogOilAssistant.Components.Database;

namespace FogOilAssistant.Components.Models.Pages.Signed
{

    public class ViewModelProfile : INotifyPropertyChanged
    {
        #region Props

        private static string defaultValue = "Loading...";

        //basket
        private string basketProductsInfo = defaultValue;
        public string BasketProductsInfo 
        { 
            get => basketProductsInfo; 
            set
            {
                basketProductsInfo = value;
                OnPropertyChanged("BasketProductsInfo");
            }
        }

        //orders
        private string ordersInfo = defaultValue;
        public string OrdersInfo
        {
            get => ordersInfo;
            set
            {
                ordersInfo = value;
                OnPropertyChanged("OrdersInfo");
            }
        }

        //history
        private string historyInfo = defaultValue;
        public string HistoryInfo
        {
            get => historyInfo;
            set
            {
                historyInfo = value;
                OnPropertyChanged("HistoryInfo");
            }
        }

        //delivered
        private string deliveredInfo = defaultValue;
        public string DeliveredInfo
        {
            get => deliveredInfo;
            set
            {
                deliveredInfo = value;
                OnPropertyChanged("DeliveredInfo");
            }
        }

        //bonus
        private string bonusInfo = defaultValue;
        public string BonusInfo
        {
            get => bonusInfo;
            set
            {
                bonusInfo = value;
                OnPropertyChanged("BonusInfo");
            }
        }

        //total oil
        private string totalOilInfo = defaultValue;
        public string TotalOilInfo
        {
            get => totalOilInfo;
            set
            {
                totalOilInfo = value;
                OnPropertyChanged("TotalOilInfo");
            }
        }

        //nick
        private string nickkInfo = defaultValue;
        public string NickInfo
        {
            get => nickkInfo;
            set
            {
                nickkInfo = value;
                OnPropertyChanged("NickInfo");
            }
        }

        //root
        private string rootInfo = defaultValue;
        public string RootInfo
        {
            get => rootInfo;
            set
            {
                rootInfo = value;
                OnPropertyChanged("RootInfo");
            }
        }

        #endregion




        #region Commands
        public CommandViewModel ShowBasket { get => new CommandViewModel(showBassket); }
        public CommandViewModel ShowOrders { get => new CommandViewModel(showOrders); }
        public CommandViewModel ShowHistory { get => new CommandViewModel(showHistory); }
        public CommandViewModel ShowDelivered { get => new CommandViewModel(showDelivered); }
        public CommandViewModel AboutBonus { get => new CommandViewModel(aboutBonus); }
        public CommandViewModel TotallOil { get => new CommandViewModel(totalOil); }


        #region Commands Methods
        private void showBassket()
        {
            MessageBox.Show("Basket");
        }

        private void showOrders()
        {
            MessageBox.Show("orders");
        }

        private void showHistory()
        {
            MessageBox.Show("history");
        }

        private void showDelivered()
        {
            MessageBox.Show("Delivered");
        }

        private void aboutBonus()
        {
            MessageBox.Show("Bonus");
        }
        
        private void totalOil()
        { 
            MessageBox.Show("totaloil");
        }
        #endregion

        #endregion

        #region assync methods
        //private void callPreloader()
        //{
           
        //}
        //private Task usePreloader(int id)
        //{
        //    try
        //    {
                
        //    }
        //    catch(Exception e)
        //    {
        //        return false;
        //    }

        //    return true;
        //}


        private async void preloadAll(int id)
        {
            using (FogOilEntities db = new FogOilEntities())
            {
                var user = await db.Users.FindAsync(id);
                NickInfo = $"Nickname: {user.Nick}";
                HistoryInfo = $"History({user.UserProducts.Count})";
                RootInfo = $"Root: {user.Root1.Name}";
                OrdersInfo = $"Orders({user.UserProducts.Where(item => item.Status == 1 || item.Status == 4).Count()})";
                DeliveredInfo = $"Delivered({user.UserProducts.Where(item => item.Status == 3).Count()})";
                BonusInfo = $"Discount({Math.Round(user.Bonus,2)}%)";
                BasketProductsInfo = $"Basket({user.Baskets.Count()})";
                TotalOilInfo = $"{Math.Round(user.Oil,2)} L";
            }
        }
        #endregion

        #region Constructor
        public ViewModelProfile()
        {
            //callPreloader();
            this.preloadAll(DataBaseData.getInstance().UserId);
        }



        #endregion

        #region events
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        #endregion
    }
}
