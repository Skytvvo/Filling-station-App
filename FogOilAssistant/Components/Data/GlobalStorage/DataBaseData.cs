using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FogOilAssistant.Annotations;
using FogOilAssistant.Components.Database;

namespace FogOilAssistant.Components.Data.GlobalStorage
{
    
    public class DataBaseData: INotifyPropertyChanged
    {
        private static DataBaseData instance;
        

        public List<Database.Product> Products = new List<Database.Product>();
        
        public List<CarBrand> CarBrands = new List<CarBrand>();
        public List<CarModel> CarModels = new List<CarModel>();
        public List<CarType> CarTypes = new List<CarType>();

        public List<CarObject> CarObjects = new List<CarObject>();


        public ObservableCollection<Database.Product> basketProducts = new ObservableCollection<Database.Product>();
        
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

    

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
