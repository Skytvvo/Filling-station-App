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
        
        private FogOilAssistant.Components.Database.FogOilEntities DB = new FogOilAssistant.Components.Database.FogOilEntities(); 

        public List<Database.Product> Products = new List<Database.Product>();
        
        public List<CarBrand> CarBrands = new List<CarBrand>();
        public List<CarModel> CarModels = new List<CarModel>();
        public List<CarType> CarTypes = new List<CarType>();

        public List<CarObject> CarObjects = new List<CarObject>();

        private DataBaseData()
        {
            this.Products = DB.Products.ToList<Database.Product>();
        }

        public static DataBaseData getInstance()
        {
            if (instance == null)
                instance = new DataBaseData();
            return instance;
        }
    }
}
