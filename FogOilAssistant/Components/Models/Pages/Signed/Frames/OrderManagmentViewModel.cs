using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FogOilAssistant.Components.Data;
using FogOilAssistant.Components.Data.Pages.Signed;
using FogOilAssistant.Components.Database;

namespace FogOilAssistant.Components.Models.Pages.Signed.Frames
{
    public class OrderManagmentViewModel : INotifyPropertyChanged
    {
        #region orders
        private bool aboutVisibility = false;
        public bool AboutVisibility
        {
            get => aboutVisibility;
            set
            {
                aboutVisibility = value;
                OnPropertyChanged("AboutVisibility");
            }
        }

        private ProductPresenter selectedOrder;
        public ProductPresenter SelectedOrder
        {
            get => selectedOrder;
            set
            {
                selectedOrder = value;
                OnPropertyChanged("SelectedOrder");
            }
        }

        private UserProductsBuilder Builder = new UserProductsBuilder();

        private List<ProductPresenter> products;
        public List<ProductPresenter> Products 
        {
            get => products;
            set
            {
                products = value;
                OnPropertyChanged("Products");
            }
        }
        #endregion

        #region UI methods
        private void closeEditor()
        {
            AboutVisibility = false;
        }
        private void showEditor()
        {
            AboutVisibility = true;
        }
        #endregion 

        #region async
        private async void UpdateProduct()
        {
            try
            {
                using(FogOilEntities db = new FogOilEntities())
                {
                    await System.Windows.Threading.Dispatcher.CurrentDispatcher.InvokeAsync(() => {
                        var res = db.UserProducts.ToList();
                        Products = res.Where(item => item.Status == 1 || item.Status == 4)
                        .Select(item=> Builder.GetProduct(item.UserProductsId,new EmployeeOrder(), item.Product, item.OrderStatu, item.Location)).ToList();
                    });
                   
                }
            }
            catch(Exception e)
            {

            }
        }

        private async void rejectSelected()
        {
            try
            {
                using (FogOilEntities db = new FogOilEntities())
                {
                    db.UserProducts.Remove(await db.UserProducts.FindAsync(selectedOrder.ID));
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {

            }
            UpdateProduct();
            closeEditor();
        }

        private async void toDelivering(object obj)
        {
            try
            {
                using (FogOilEntities db = new FogOilEntities())
                {
                    (await db.UserProducts.FindAsync(selectedOrder.ID)).Status = 4;
                    await db.SaveChangesAsync();
                }
            }
            catch(Exception e)
            {

            }
            UpdateProduct();
            closeEditor();
        }
        private async void toDelivered(object obj)
        {
            try
            {
                using (FogOilEntities db = new FogOilEntities())
                {
                    (await db.UserProducts.FindAsync(selectedOrder.ID)).Status = 3;
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {

            }
            UpdateProduct();
            closeEditor();

        }
        #endregion


        #region Commands
        public RelayCommand ProcessOrder { get => new RelayCommand((obj)=> { 
            try
            {
                SelectedOrder = (obj as ProductPresenter);
                if (SelectedOrder.StatusId == 1)
                {
                    ImplementAction = new RelayCommand(toDelivering);
                    BtnText = "Accept";
                }
                else
                {
                    ImplementAction = new RelayCommand(toDelivered);
                    BtnText = "Confirm";
                }
                showEditor();

            }
            catch (Exception e)
            {

            }
        }); }
        public CommandViewModel CloseEditor { get => new CommandViewModel(closeEditor); }

        public CommandViewModel RejectSelected { get => new CommandViewModel(rejectSelected); }
        private RelayCommand implementAction = null;
        public RelayCommand ImplementAction {
            get => implementAction; 
            set
            {
                implementAction = value;
                OnPropertyChanged("ImplementAction");
            }
        }

        private string btnText = "";
        public string BtnText
        {
            get => btnText;
            set
            {
                btnText = value;
                OnPropertyChanged("BtnText");
            }
        }
        

        #endregion

        public OrderManagmentViewModel()
        {
            UpdateProduct();
        }

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
