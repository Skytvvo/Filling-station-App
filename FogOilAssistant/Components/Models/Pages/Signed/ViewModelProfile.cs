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
using FogOilAssistant.Components.Data.Pages.Signed;
using System.Threading;

namespace FogOilAssistant.Components.Models.Pages.Signed
{

    public class ViewModelProfile : INotifyPropertyChanged
    {
        #region Props
        static object locker = new object();

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


        //Selected collection
        private List<ProductPresenter> products;
        public List<ProductPresenter>  Products
        { 
            get => products;
            set
            {
                products = value;
                OnPropertyChanged("Products");
            }
        }

        private UserProductsBuilder CollectionControl;

        //Slected product
        private ProductPresenter selectedProduct;
        public ProductPresenter SelectedProduct
        { 
            get => selectedProduct; 
            set
            {
                selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            }
        }
        //about action => container
        private bool aboutVisibility = false;
        public bool AboutVisibility
        {
            get => aboutVisibility;
            set
            {
                aboutVisibility = value;
                OnPropertyChanged("AboutVisibility");
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

        public RelayCommand AboutAction { get => new RelayCommand(obj=>
        {
            try
            {
                AboutVisibility = true;
                SelectedProduct = obj as ProductPresenter;
            }
            catch(Exception e)
            {
                AboutVisibility = false;
            }

        }); }

        #region Commands Methods
        private async void showBassket()
        {
            try
            {
                using(FogOilEntities db = new FogOilEntities())
                {
                  
                    var userData = (await db.Users.FindAsync(DataBaseData.getInstance().UserId));
                    Products = userData.Baskets
                        .Select((prod) => CollectionControl
                        .GetProduct(new BasketProductBuilder(), prod.Product))
                        .ToList();
                    BasketProductsInfo = $"Basket({Products.Count()})";
                }
            }
            catch(Exception e)
            {

            }
        }

        private async void showOrders()
        {
            try
            {
                using (FogOilEntities db = new FogOilEntities())
                {
                    var userData = (await db.Users.FindAsync(DataBaseData.getInstance().UserId));
                    Products = userData.UserProducts
                        .Where(prod => prod.Status == 1 || prod.Status == 4)
                        .Select((prod) => CollectionControl
                        .GetProduct(new OrderBuilder(), prod.Product, prod.OrderStatu, prod.Location))
                        .ToList();
                    OrdersInfo = $"Orders({Products.Count()})";

                }
            }
            catch (Exception e)
            {

            }
        }

        private async void showHistory()
        {
            try
            {
                using (FogOilEntities db = new FogOilEntities())
                {
                   
                    Products = (await db.Users.FindAsync(DataBaseData.getInstance().UserId)).UserProducts
                        .Select((prod) => CollectionControl
                        .GetProduct(new HistoryBuilder(), prod.Product, prod.OrderStatu, prod.Location))
                        .ToList();
                    HistoryInfo = $"History({Products.Count()})";

                }
            }
            catch (Exception e)
            {

            }
        }

        private async void showDelivered()
        {
            try
            {
                using (FogOilEntities db = new FogOilEntities())
                {
                    var userData = (await db.Users.FindAsync(DataBaseData.getInstance().UserId));
                    Products = userData.UserProducts
                        .Where(prod => prod.Status == 3)
                        .Select((prod) => CollectionControl
                        .GetProduct(new DeliveredBuilder(), prod.Product, prod.OrderStatu, prod.Location))
                        .ToList();
                    DeliveredInfo = $"Delivered({Products.Count()})";

                }
            }
            catch (Exception e)
            {

            }
        }

        private void aboutBonus()
        {
            MessageBox.Show("Bonus");
        }
        
        private void totalOil()
        {
            
        }
        #endregion

        #endregion

        #region assync methods
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
            this.CollectionControl = new UserProductsBuilder();
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
