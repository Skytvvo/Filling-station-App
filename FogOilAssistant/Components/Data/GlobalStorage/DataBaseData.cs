using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using FogOilAssistant.Annotations;
using FogOilAssistant.Components.Data.MenuButton;
using FogOilAssistant.Components.Database;

namespace FogOilAssistant.Components.Data.GlobalStorage
{
    
    public class DataBaseData: INotifyPropertyChanged
    {
        private static DataBaseData instance;
        

        public List<Database.Product> Products;
        
        public List<CarBrand> CarBrands;
        public List<CarModel> CarModels;
        public List<CarType> CarTypes;

        public List<CarObject> CarObjects;

        public ObservableCollection<ButtonMenu> MenuList;

        public ObservableCollection<Database.Product> basketProducts;
        
        public string Login { get; set; }
        public int UserId { get; set; }
        private DataBaseData()
        {
            try
            {
                using (FogOilEntities DB = new FogOilEntities())
                {
                    this.Products = DB.Products.ToList();
                    this.basketProducts = new ObservableCollection<Database.Product>();


                    this.MenuList = new ObservableCollection<ButtonMenu>(){
                    new ButtonMenu { Path = "/Components/Images/support.svg", Text = "Support" },
                    new ButtonMenu { Path = "/Components/Images/sHOP.svg", Text = "Shop" },
                    new ButtonMenu { Path = "/Components/Images/oil-bottle.svg", Text = "Oil" },
                    new ButtonMenu { Path = "/Components/Images/location.svg", Text = "Refueling map" },
                    new ButtonMenu { Path = "/Components/Images/login.svg", Text = "Login" }
                    };
                    

                    this.CarTypes = DB.CarTypes.ToList();
                    this.CarBrands = DB.CarBrands.ToList();
                    this.CarModels = DB.CarModels.ToList();

                    this.CarObjects = DB.CarObjects.ToList();
                }
            }
            catch(Exception e)
            {

            }
            
        }

        public static DataBaseData getInstance()
        {
            if (instance == null)
                instance = new DataBaseData();
            return instance;
        }

        public bool userAuthChecker()
        {
            if (UserId != 0 && Login != null)
                return true;
            return false;
        }

        // only UI basket
        private bool addToAppBasket(int id)
        {
            var product = Products.FirstOrDefault(item => item.ProductId == id);
            if (product != null)
            {
                var result = basketProducts.FirstOrDefault(item => item.ProductId > id);
                if (result == null)
                    basketProducts.Add(product);
                else
                    basketProducts.Insert(basketProducts.IndexOf(result), product);
                return true;
            }
            else
                return false;
        }

        // to UI and DB baskets
        public bool AddToUserBasket(int id)
        {
            try
            {
                using(FogOilEntities db = new FogOilEntities())
                {
                    if(!addToAppBasket(id))
                        throw new Exception("Unknown productId");

                    if (!userAuthChecker())
                        throw new Exception("Unsigned user");

                    db.Users.Find(UserId).Baskets.Add(new Basket() { ProductId = id });
                    db.SaveChanges();
                }

                return true;
            }
            catch( Exception e)
            {
                return false;
            }
        }

        public void Exit()
        {
            this.MenuList.Remove(MenuList.Last());
            MenuList.Add(new ButtonMenu { Path = "/Components/Images/login.svg", Text = "Login" });
            UserId = 0;
            Login = null;
            basketProducts.Clear();
        }


        public void GoToShopPage()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(FogOilAssistant.MainWindow))
                {
                    (window as FogOilAssistant.MainWindow).Frame.Navigate(new Uri(string.Format("{0}{1}{2}", "/Components/View/Pages/", "Shop", ".xaml"), UriKind.RelativeOrAbsolute));

                }
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
