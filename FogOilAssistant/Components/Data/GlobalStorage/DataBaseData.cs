using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FogOilAssistant.Components.Data.Pages.Oil;

namespace FogOilAssistant.Components.Data.GlobalStorage
{
    
    public class DataBaseData
    {
        private static DataBaseData instance;
        
        
        public List<Product.Product> Products = new List<Product.Product>();
        
        public List<CarBrand> CarBrands;
        public List<CarModel> CarModels;
        public List<CarType> CarTypes;

        public List<CarObject> CarObjects;

        private DataBaseData()
        {
            
            this.CarObjects = new List<CarObject>();

            this.CarBrands = new List<CarBrand>();
            this.CarTypes = new List<CarType>();
            this.CarModels = new List<CarModel>();
        }

        public static DataBaseData getInstance()
        {
            if (instance == null)
                instance = new DataBaseData();
            return instance;
        }
    }
}
