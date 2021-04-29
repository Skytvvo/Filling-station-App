using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using FogOilAssistant.Components.Data;
using FogOilAssistant.Components.Data.Convertor;
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

        private string productDescription;
        public string ProductDescription
        {
            get
            {
                return productDescription;
            }
            set
            {
                productDescription = value;
                OnPropertyChanged("ProductDescription");
            }
        }

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

        private void updateProducts()
        {
            try
            {
                using (FogOilEntities db = new FogOilEntities())
                {
                    Products = db.Products.ToList();
                }
            }
            catch(Exception e)
            {

            }
        }

        #region
        public RelayCommand EditProduct { get => new RelayCommand((obj)=> {
            SelectedProduct = (obj as Product);
            ProductDescription = SelectedProduct.Description;
            ProductImage = ImageConvertor.ConvertByteArrayToBitMapImage(SelectedProduct.ImgCode);
            ProductName = SelectedProduct.Name;
            ProductPrice = SelectedProduct.Price.ToString();

            EditVisibility = true;
        }); 
        }
        public CommandViewModel UploadImage { get => new CommandViewModel(()=> {

        }); }

        public CommandViewModel CloseEditor { get => new CommandViewModel(closeEditor);}
        public CommandViewModel SaveSelected { get => new CommandViewModel(saveSelected); }

        public CommandViewModel DeleteSelected { get => new CommandViewModel(deleteSelected); }

        public CommandViewModel EnteringEnter { get => new CommandViewModel(() => {
            ProductDescription = ProductDescription + '\n';
        }); }

        #endregion

        #endregion


        #region Elements Props Methods
        private void closeEditor()
        {
            EditVisibility = false;
        }

        private void saveSelected()
        {
            MessageBox.Show("SAVE");
        }
        private void deleteSelected()
        {
            MessageBox.Show("DELETE");
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
