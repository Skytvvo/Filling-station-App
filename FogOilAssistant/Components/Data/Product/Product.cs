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
       private double _price;
       private string _description;
       private string _img;
       private int _id;
        private string _gov_price;

        public Product(string name, double price, string description, string img, int id)
        {
            Name = name;
            Price = price;
            Description = description;
            Id = id;
            Img = img;
            Gov_price = price + " BYN";
        }

        public string Name { get => _name; set => _name = value; }
        public double Price { get => _price; set => _price = value; }
        public string Description { get => _description; set => _description = value; }
        public string Img { get => _img; set => _img = value; }
        public int Id { get => _id; set => _id = value; }
        public string Gov_price { get => _gov_price; set => _gov_price = value; }
    }
}
