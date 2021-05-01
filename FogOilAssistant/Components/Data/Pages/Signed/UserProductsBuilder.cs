using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using FogOilAssistant.Components.Data.GlobalStorage;
using FogOilAssistant.Components.Database;
namespace FogOilAssistant.Components.Data.Pages.Signed
{
    public class UserProductsBuilder
    {
        public ProductPresenter GetProduct(int ID,PresentedBuilder builder, Database.Product product, Database.OrderStatu status = null, Database.Location location = null)
        {
            builder.BuildProduct(product,ID);
            builder.BuildStatus(status);
            builder.BuildLocation(location);
            builder.BuildColor();
            return builder.ProductPresenter;
        }
    }

    public class ProductPresenter
    {
        public int userID { get; set; }
        public string userNick { get; set; }

        public int tempID { get; set; }

        public int ID { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public Byte[] ImgCode { get; set; }
        public string Name { get; set; }

        public double latitude { get; set; }
        public double longitude { get; set; }
        public string Addres { get; set; }

        public string Status { get; set; }
        public int StatusId { get; set; }

   
        public RelayCommand ActionCommand { get; set; }

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

        public abstract void BuildProduct(Database.Product product = null, int ID = 0);
        public abstract void BuildStatus(Database.OrderStatu status = null);
        public abstract void BuildLocation(Database.Location location = null);
        public abstract void BuildColor(string color = "#fff");
        public ProductPresenter ProductPresenter { get; set; } = new ProductPresenter();
    }

    public class HistoryBuilder : PresentedBuilder
    {
        public override void BuildProduct(Database.Product product = null, int ID = 0)
        {
            this.ProductPresenter.Name = product.Name;
            this.ProductPresenter.Description = product.Description;
            this.ProductPresenter.Price = product.Price + " BYN";
            this.ProductPresenter.ID = ID;
            this.ProductPresenter.ImgCode = product.ImgCode;

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
            this.ProductPresenter.ActionCommand = new RelayCommand(async (obj) =>
            {
                try
                {
                    using (FogOilEntities db = new FogOilEntities())
                    {
                        (await db.UserProducts.FindAsync((int)obj)).Status = 1;
                        await db.SaveChangesAsync();
                    }
                }
                catch (Exception E)
                {

                }
            });

        }
    }

    public class OrderBuilder : PresentedBuilder
    {
        public override void BuildProduct(Database.Product product = null, int ID = 0)
        {
            this.ProductPresenter.ImgCode = product.ImgCode;
            this.ProductPresenter.Name = product.Name;
            this.ProductPresenter.Description = product.Description;
            this.ProductPresenter.Price = product.Price + " BYN";
            this.ProductPresenter.ID = ID;
        }

        public override void BuildColor(string color = "#fff")
        {
            this.ProductPresenter.Color = GetColor(ProductPresenter.StatusId);
            this.ProductPresenter.ActionCommand = new RelayCommand(async (obj) =>
            {
                try
                {
                    using(FogOilEntities db = new FogOilEntities())
                    {
                        (await db.UserProducts.FindAsync((int)obj)).Status = 5;
                        await db.SaveChangesAsync();
                    }
                }
                catch(Exception E)
                {

                }
            });
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

    public class EmployeeOrder : OrderBuilder
    {
        public override void BuildStatus(OrderStatu status = null)
        {
            if (status.StatusId == 1)
                this.ProductPresenter.Status = "Processing";
            else
                this.ProductPresenter.Status = "Delivering";
            try
            {
                this.ProductPresenter.userID = status.UserProducts.FirstOrDefault().UserId;
                this.ProductPresenter.userNick = status.UserProducts.FirstOrDefault().User.Nick;
            }
            catch
            {

            }
            this.ProductPresenter.StatusId = status.StatusId;
        }
        public override void BuildProduct(Database.Product product = null, int ID = 0)
        {
            this.ProductPresenter.ImgCode = product.ImgCode;
            this.ProductPresenter.Name = product.Name;
            this.ProductPresenter.Description = product.Description;
            this.ProductPresenter.Price = product.Price + " BYN";
            this.ProductPresenter.ID = ID;
            this.ProductPresenter.tempID = product.ProductId;
        }
    }
    public class DeliveredBuilder : PresentedBuilder
    {
        public override void BuildProduct(Database.Product product = null, int ID = 0)
        {
            this.ProductPresenter.ImgCode = product.ImgCode;
            this.ProductPresenter.Name = product.Name;
            this.ProductPresenter.Description = product.Description;
            this.ProductPresenter.Price = product.Price + " BYN";
            this.ProductPresenter.ID = ID;
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
        public override void BuildProduct(Database.Product product = null, int ID = 0)
        {
            this.ProductPresenter.ImgCode = product.ImgCode;
            this.ProductPresenter.Name = product.Name;
            this.ProductPresenter.Description = product.Description;
            this.ProductPresenter.Price = product.Price + " BYN";
            this.ProductPresenter.ID = ID;
        }

        public override void BuildColor(string color = "#FFFFFF")
        {
            this.ProductPresenter.Color = GetSolidColorBrush(255,0,0);
            this.ProductPresenter.ActionCommand = new RelayCommand( async (obj) =>
            {
                try
                {
                    using(FogOilEntities db = new FogOilEntities())
                    {
                        db.Baskets.Remove(
                            (await db.Baskets.FindAsync((int)obj))
                            );
                    
                        await db.SaveChangesAsync();
                    }
                }
                catch(Exception e)
                {

                }
            });
        }

        public override void BuildLocation(Location location = null)
        {
            
        }

        public override void BuildStatus(OrderStatu status = null)
        {

        }
    }

}
