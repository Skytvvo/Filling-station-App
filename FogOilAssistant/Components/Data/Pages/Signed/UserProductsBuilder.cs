using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using FogOilAssistant.Components.Database;
namespace FogOilAssistant.Components.Data.Pages.Signed
{
    public class UserProductsBuilder
    {
        public ProductPresenter GetProduct(PresentedBuilder builder, Database.Product product, Database.OrderStatu status = null, Database.Location location = null)
        {
            builder.BuildProduct(product);
            builder.BuildStatus(status);
            builder.BuildLocation(location);
            builder.BuildColor();
            return builder.ProductPresenter;
        }
    }

    public class ProductPresenter
    {
       
        public string Description { get; set; }
        public string Price { get; set; }
        public string Img { get; set; }
        public string Name { get; set; }

        public double latitude { get; set; }
        public double longitude { get; set; }
        public string Addres { get; set; }

        public string Status { get; set; }
        public int StatusId { get; set; }



        public SolidColorBrush Color { get; set; }


    }


    public abstract class PresentedBuilder
    {

        public SolidColorBrush GetSolidColorBrush(byte R, byte G, byte B)
        {
            try
            {
                return new SolidColorBrush(Color.FromRgb(R, G, B));
            }
            catch
            {
                return new SolidColorBrush(Colors.White);
            }
        }

        public SolidColorBrush GetColor(int id)
        {

            switch (id)
            {
                case 1:
                    return GetSolidColorBrush(0, 255, 236);
                case 2:
                    return GetSolidColorBrush(255,0,0);
                case 3:
                    return GetSolidColorBrush(37,255,0);
                case 4:
                    return GetSolidColorBrush(255,255,0);
                case 5:
                    return GetSolidColorBrush(236,0,255);
                default: return GetSolidColorBrush(255,255,255);
            }
        }

        public abstract void BuildProduct(Database.Product product = null);
        public abstract void BuildStatus(Database.OrderStatu status = null);
        public abstract void BuildLocation(Database.Location location = null);
        public abstract void BuildColor(string color = "#fff");
        public ProductPresenter ProductPresenter { get; set; } = new ProductPresenter();
    }

    public class HistoryBuilder : PresentedBuilder
    {
        public override void BuildProduct(Database.Product product = null)
        {
            this.ProductPresenter.Img = product.Img;
            this.ProductPresenter.Name = product.Name;
            this.ProductPresenter.Description = product.Description;
            this.ProductPresenter.Price = product.Price + " BYN";

        }
        public override void BuildStatus(Database.OrderStatu status = null)
        {
            this.ProductPresenter.Status = status.Status;
            this.ProductPresenter.StatusId = status.StatusId;
        }
        public override void BuildLocation(Database.Location location = null)
        {
            this.ProductPresenter.latitude = location.Latitude;
            this.ProductPresenter.longitude = location.Longitude;
            this.ProductPresenter.Addres = location.Adress;
        }

        public override void BuildColor(string color = "#fff")
        {

            this.ProductPresenter.Color = GetColor(ProductPresenter.StatusId);
        }
    }

    public class OrderBuilder : PresentedBuilder
    {
        public override void BuildProduct(Database.Product product = null)
        {
            this.ProductPresenter.Img = product.Img;
            this.ProductPresenter.Name = product.Name;
            this.ProductPresenter.Description = product.Description;
            this.ProductPresenter.Price = product.Price + " BYN";
        }

        public override void BuildColor(string color = "#fff")
        {
            this.ProductPresenter.Color = GetColor(ProductPresenter.StatusId);
        }

        public override void BuildLocation(Location location = null)
        {
            this.ProductPresenter.latitude = location.Latitude;
            this.ProductPresenter.longitude = location.Longitude;
            this.ProductPresenter.Addres = location.Adress;

        }

        public override void BuildStatus(OrderStatu status = null)
        {
            if (status.StatusId == 1)
                this.ProductPresenter.Status = "Processing";
            else
                this.ProductPresenter.Status = "Delivering";

            this.ProductPresenter.StatusId = status.StatusId;
        }
    }

    public class DeliveredBuilder : PresentedBuilder
    {
        public override void BuildProduct(Database.Product product = null)
        {
            this.ProductPresenter.Img = product.Img;
            this.ProductPresenter.Name = product.Name;
            this.ProductPresenter.Description = product.Description;
            this.ProductPresenter.Price = product.Price + " BYN";
        }

        public override void BuildColor(string color = "#fff")
        {
            this.ProductPresenter.Color = GetColor(ProductPresenter.StatusId);
        }

        public override void BuildLocation(Location location = null)
        {
            this.ProductPresenter.latitude = location.Latitude;
            this.ProductPresenter.longitude = location.Longitude;
            this.ProductPresenter.Addres = location.Adress;

        }

        public override void BuildStatus(OrderStatu status = null)
        {
            this.ProductPresenter.Status =  "Delivered" ;
            this.ProductPresenter.StatusId = 3;
        }
    }

    public class BasketProductBuilder : PresentedBuilder
    {
        public override void BuildProduct(Database.Product product = null)
        {
            this.ProductPresenter.Img = product.Img;
            this.ProductPresenter.Name = product.Name;
            this.ProductPresenter.Description = product.Description;
            this.ProductPresenter.Price = product.Price + " BYN";
        }

        public override void BuildColor(string color = "#FFFFFF")
        {
            this.ProductPresenter.Color = GetSolidColorBrush(255,0,0);
        }

        public override void BuildLocation(Location location = null)
        {
            
        }

        public override void BuildStatus(OrderStatu status = null)
        {

        }
    }

}
