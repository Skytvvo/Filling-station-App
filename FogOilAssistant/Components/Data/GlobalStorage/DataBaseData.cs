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
        
        private FogOilAssistant.Components.Database.FogOilEntities DB = new FogOilAssistant.Components.Database.FogOilEntities(); 

        public List<Database.Product> Products = new List<Database.Product>();
        
        public List<CarBrand> CarBrands = new List<CarBrand>();
        public List<CarModel> CarModels = new List<CarModel>();
        public List<CarType> CarTypes = new List<CarType>();

        public List<CarObject> CarObjects = new List<CarObject>();

        public ObservableCollection<Database.Product> basketProducts = new ObservableCollection<Database.Product>();
        
       


        private DataBaseData()
        {
            this.Products = DB.Products.ToList<Database.Product>();
            this.basketProducts = new ObservableCollection<Database.Product>(DB.Products);


            this.CarTypes = DB.CarTypes.ToList<CarType>();
            this.CarBrands = DB.CarBrands.ToList<CarBrand>();
            this.CarModels = DB.CarModels.ToList<CarModel>();

            this.CarObjects = DB.CarObjects.ToList<CarObject>();
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
