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
                using (FogOilAssistant.Components.Database.FogOilEntities DB = new FogOilAssistant.Components.Database.FogOilEntities())
                {
                    this.Products = DB.Products.ToList<Database.Product>();
                    this.basketProducts = new ObservableCollection<Database.Product>();
                    

                    this.CarTypes = DB.CarTypes.ToList<CarType>();
                    this.CarBrands = DB.CarBrands.ToList<CarBrand>();
                    this.CarModels = DB.CarModels.ToList<CarModel>();

                    this.CarObjects = DB.CarObjects.ToList<CarObject>();
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
