using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using FogOilAssistant.Components.Data;
using FogOilAssistant.Components.Data.Convertor;
using FogOilAssistant.Components.Data.Pages.Signed;
using FogOilAssistant.Components.Database;

namespace FogOilAssistant.Components.Models.Pages.Signed.Frames
{
    public class ViewModelProductManagment : INotifyPropertyChanged
    {

        #region Selected
        private Product selectedProduct;
        public Product SelectedProduct
        {
            get => selectedProduct;
            set
            {
                selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            }
        }

        private bool editVisibility = false;
        public bool EditVisibility
        {
            get => editVisibility;
            set
            {
                editVisibility = value;
                OnPropertyChanged("EditVisibility");
            }
        }

        private string productName;
        public string ProductName
        {
            get
            {
                return productName;
            }
            set
            {
                productName = value;
                OnPropertyChanged("ProductName");
            }
        }
        //description
       
        private TextBoxProp description = new TextBoxProp();
        public TextBoxProp Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        //==============
        public double productPrice;
        public string ProductPrice
        {
            get
            {
                return productPrice.ToString();
            }
            set
            {
                if(Regex.IsMatch(value, @"^[0-9]*(?:\.[0-9]*)?$"))
                {
                    try
                    {

                        Double.TryParse(value,out productPrice);
                    }
                    catch
                    {

                    }
                }

                OnPropertyChanged("ProductDescription");
            }
        }

        private BitmapImage productImage;
        public BitmapImage ProductImage
        {
            get
            {
                return productImage;
            }
            set
            {
                productImage = value;
                OnPropertyChanged("ProductImage");
            }
        }

        private int caretIndex = 0;
        public int DescIndex
        {
            get
            {
                return caretIndex;
            }
            set
            {
                caretIndex = value;
                OnPropertyChanged("DescIndex");
            }
        }
        #endregion

        #region Products
        private List<Product> products;
        public List<Product> Products
        {
            get => products;
            set
            {
                products = value;
                OnPropertyChanged("Products");
            }
        }
        #endregion


        public ViewModelProductManagment()
        {
            updateProducts();
        }


        #region Queries

        private async void updateProducts()
        {
            try
            {
                await Task.Run(() =>
                {
                    using (FogOilEntities db = new FogOilEntities())
                    {
                        Products = db.Products.ToList();
                    }
                });
            }
            catch(Exception e)
            {

            }
        }

        #region
        public RelayCommand EditProduct { get => new RelayCommand((obj)=> {
            SelectedProduct = (obj as Product);
            ProductImage = ImageConvertor.ConvertByteArrayToBitMapImage(SelectedProduct.ImgCode);
            ProductName = SelectedProduct.Name;
            productPrice = SelectedProduct.Price;
            OnPropertyChanged("ProductPrice");
            
            Description = new TextBoxProp() { Index = 0, Text = SelectedProduct.Description };
            EditVisibility = true;
        }); 
        }
        public CommandViewModel UploadImage { get => new CommandViewModel(()=> {
            var dialog = new OpenFileDialog();
            dialog.Filter = "(Png files *.png)|*.png";
            var result = dialog.ShowDialog();
            if (result != DialogResult.OK)
                return;
            byte[] buffer = File.ReadAllBytes(dialog.FileName);
            ProductImage = ImageConvertor.ConvertByteArrayToBitMapImage(buffer);
        }); }

        public CommandViewModel CloseEditor { get => new CommandViewModel(closeEditor);}
        public CommandViewModel SaveSelected { get => new CommandViewModel(saveSelected); 
            
        }

        public CommandViewModel DeleteSelected { get => new CommandViewModel(deleteSelected); }

        

        #endregion

        #endregion


        #region Elements Props Methods
        private void closeEditor()
        {
            EditVisibility = false;
        }

        private async void saveSelected()
        {
            try
            {
                using(FogOilEntities db = new FogOilEntities())
                {
                    closeEditor();
                    (await db.Products.FindAsync(SelectedProduct.ProductId)).Description = Description.Text;
                    (await db.Products.FindAsync(SelectedProduct.ProductId)).Price = productPrice;
                    (await db.Products.FindAsync(SelectedProduct.ProductId)).Name = ProductName;
                   
                    using(var stream = new MemoryStream())
                    {
                        var encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(ProductImage));
                        encoder.Save(stream);

                        (await db.Products.FindAsync(SelectedProduct.ProductId)).ImgCode = 
                        stream.ToArray();
                    }
                    await db.SaveChangesAsync();
                    await Task.Run(() =>
                    {
                        Products = db.Products.ToList();

                    });
                }
            }
            catch(Exception e)
            {

            }
        }
        private void deleteSelected()
        {
            System.Windows.MessageBox.Show("DELETE");
        }


        #endregion
        #region notify changed
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }
}
