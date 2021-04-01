using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FogOilAssistant.Components.Data.Product
{
    public class Product
    {
       private string _name;
       private string _price;
       private string _description;
       private string _img;
       private int _id;

       public Product(string name, string price, string description, string img, int id)
        {
            Name = name;
            Price = price;
            Description = description;
            Id = id;
            Img = img;
        }

        public string Name { get => _name; set => _name = value; }
        public string Price { get => _price; set => _price = value; }
        public string Description { get => _description; set => _description = value; }
        public string Img { get => _img; set => _img = value; }
        public int Id { get => _id; set => _id = value; }
    }
}
