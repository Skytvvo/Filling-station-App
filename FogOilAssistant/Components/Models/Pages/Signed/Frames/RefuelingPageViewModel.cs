using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using FogOilAssistant.Components.Data;
using FogOilAssistant.Components.Data.GlobalStorage;
using FogOilAssistant.Components.Data.Pages.Signed;
using FogOilAssistant.Components.Data.UI;
using FogOilAssistant.Components.Database;

namespace FogOilAssistant.Components.Models.Pages.Signed.Frames
{

    public class FuelWrapper: INotifyPropertyChanged
    {
        public Fuel item
        {
            get;
            set;
        }

        public string Price
        {
            get => item.Price.ToString();
            set
            {
                if (value.Length == 0)
                {
                    item.Price = 0;
                    OnPropertyChanged("Price");
                    return;
                }
                if (Regex.IsMatch(value, @"^[0-9]*(?:\.[0-9]*)?$"))
                {

                    if (Convert.ToDouble(value) <= 100)
                    {
                        item.Price = Convert.ToDouble(value);
                        OnPropertyChanged("Price");
                        return;
                    }
                }

                DataBaseData.getInstance().CallNotify(new Data.Pages.Notify()
                {
                    Message = "Invalid value",
                    Color = UIData.GetColor(UIData.MessageColor.ERROR)
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class RefuelingPageViewModel : INotifyPropertyChanged
    {
        #region refuel callback


        private List<FuelWrapper> fuelList = new List<FuelWrapper>();
        public List<FuelWrapper> FuelList
        {
            get => fuelList;
            set
            {
                fuelList = value;
                OnPropertyChanged("FuelList");
            }
        }


        private bool fuelConfigVisibility = false;
        public bool FuelConfigVisibility
        {
            get => fuelConfigVisibility;
            set
            {
                fuelConfigVisibility = value;
                OnPropertyChanged("FuelConfigVisibility");
            }
        }

        private bool btnEnabled = false;
        public bool BtnEnabled
        {
            get => btnEnabled;
            set
            {
                btnEnabled = value;
                OnPropertyChanged("BtnEnabled");
            }
        }

        private double btnOpacity = 0.3;
        public double BtnOpacity
        {
            get => btnOpacity;
            set
            {
                btnOpacity = value;
                OnPropertyChanged("BtnOpacity");
            }
        }

        private void EnableButton()
        {
            BtnOpacity = 1;
            BtnEnabled = true;
        }
        private void DisableButton()
        {
            Volume = "";

            BtnOpacity = 0.3;
            BtnEnabled = false;
        }
        #endregion

        #region user prop
        private string nick;
        public string Nick 
        { 
            get => nick;
            set
            {
                nick = value;
                OnPropertyChanged("Nick");
            }
        }

        private int userId;

        private List<Fuel> fuels;
        public List<Fuel> Fuels 
        { 
            get => fuels;
            set
            {
                fuels = value;
                OnPropertyChanged("Fuels");
            }
        }


        private int selectedIndex = 0;
        public int SelectedIndex 
        { 
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                OnPropertyChanged("SelectedIndex");
            }
        }


        private string volume = "";
        public string Volume 
        { 
            get => volume; 
            set
            {
                if (Regex.IsMatch(value, @"^[0-9]*(?:\.[0-9]*)?$")) 
                {
                    volume = value;
                    EnableButton();
                    OnPropertyChanged("Volume");
                }
            }
        }


        #endregion

        #region queries
        private void getFuel()
        {
            try
            {
                using(FogOilEntities db = new FogOilEntities())
                {
                    Fuels = db.Fuels.ToList();
                }
            }
            catch(Exception e)
            {

            }
        }

        #region Find User
        private async void findUser()
        {
            try
            {
                using (FogOilEntities db = new FogOilEntities())
                {
                   
                        userId = db.Users.First(_user => _user.Nick == Nick).UserId;
                         
                    
                    //loadPurchase(userId);
                    await System.Windows.Threading.Dispatcher.CurrentDispatcher.InvokeAsync(() => {
                        Purchase = (db.Users.Find(userId)).UserProducts
                            .Where(product => product.OrderStatu.Status == "Ready" || product.OrderStatu.Status == "WishReject")
                            .Select((prod) => builder
                            .GetProduct(prod.UserProductsId, new HistoryBuilder(), prod.Product, prod.OrderStatu, prod.Location))
                            .ToList();
                    });
                    toAccess();

                }
            }
            catch (Exception e)
            {
                toDeny();
            }
        }

        

        
        #endregion

        #endregion


        #region Access methods
        private void toDeny()
        {
            FunctionsOpacity = 0.3;
            FunctionAccess = false;
        }
        private void toAccess()
        {
            FunctionsOpacity = 1;
            FunctionAccess = true;
        }
        #endregion


        #region Commands Methods
        private async void rejectProduct()
        {
            try
            {
                using (FogOilEntities db = new FogOilEntities())
                {
                    (await db.UserProducts.FindAsync(SelectedProduct.ID)).LastChangesDate = DateTime.Now;
                    (await db.UserProducts.FindAsync(SelectedProduct.ID)).Status = 2;
                    await db.SaveChangesAsync();
                    DataBaseData.getInstance().CallNotify(new Data.Pages.Notify()
                    {
                        Message = "Rejected",
                        Color = UIData.GetColor(UIData.MessageColor.DEFAULT)
                    });
                }
                closeAbout();
                findUser();
                
            }
            catch(Exception e)
            {
                DisableButton();
            }
        }

        private async void completeOrder()
        {
            try
            {
                using (FogOilEntities db = new FogOilEntities())
                {
                    (await db.UserProducts.FindAsync(SelectedProduct.ID)).LastChangesDate = DateTime.Now;
                    (await db.UserProducts.FindAsync(SelectedProduct.ID)).Status = 6;
                    await db.SaveChangesAsync();
                    DataBaseData.getInstance().CallNotify(new Data.Pages.Notify()
                    {
                        Message = "Order was completed",
                        Color = UIData.GetColor(UIData.MessageColor.SUCCESS)
                    });
                }
                closeAbout();
                findUser();

            }
            catch (Exception e)
            {
                DisableButton();
            }
        }
        #endregion
        private async void saveFuelItem(object obj)
        {
            FuelWrapper fuelWrapper = obj as FuelWrapper;
            if(fuelWrapper!= null)
            {
                try
                {
                    using(FogOilEntities db = new FogOilEntities())
                    {
                        (await db.Fuels.FindAsync(fuelWrapper.item.FuelId)).Price = fuelWrapper.item.Price;
                        await db.SaveChangesAsync();
                        DataBaseData.getInstance().CallNotify(new Data.Pages.Notify()
                        {
                            Message = "Changed",
                            Color = UIData.GetColor(UIData.MessageColor.SUCCESS)
                        });
                    }
                }
                catch
                {

                }
            }
        }
        #region Commands

        public RelayCommand SaveFuelItem { get => new RelayCommand(saveFuelItem); }

        public CommandViewModel FindUser { get => new CommandViewModel(findUser); }
        public CommandViewModel RefuelUser { get => new CommandViewModel(async ()=> {
            try
            {
                using(FogOilEntities db = new FogOilEntities())
                {
                    (await db.Users.FindAsync(userId)).Oil += Math.Round((Convert.ToDouble(volume)* Fuels[SelectedIndex].Price),2);
                    var result = (await db.Users.FindAsync(userId)).Oil;
                    //if (result > 5000)
                    //{
                    //    result -= 5000;
                    //}

                    (await db.Users.FindAsync(userId)).Bonus = (Math.Round((Convert.ToDouble(volume) * Fuels[SelectedIndex].Price), 2)+ ( await db.Users.FindAsync(userId)).Bonus);
                    await db.SaveChangesAsync();
                    DataBaseData.getInstance().CallNotify(new Data.Pages.Notify()
                    {
                        Message = "Refueled",
                        Color = UIData.GetColor(UIData.MessageColor.SUCCESS)
                    });


                }
                DisableButton();
            }
            catch(Exception e)
            {

            }
        }); }

        public RelayCommand ShowAbout { get => new RelayCommand((obj)=> {

            try
            {
                SelectedProduct = Purchase.First(item=> item.ID == (obj as ProductPresenter).ID);
                AboutVisibility = true;
                BtnVisibility = true;
                if((obj as ProductPresenter).StatusId == 5)
                {
                    //change color for border
                    //add event
                    ProductBtnText = "Reject";
                    ProductAction = new CommandViewModel(rejectProduct);

                }
                if ((obj as ProductPresenter).StatusId == 3)
                {
                    ProductBtnText = "Complete";
                    ProductAction = new CommandViewModel(completeOrder);
                }
                

            }
            catch (Exception e)
            {
                AboutVisibility = false;
                BtnVisibility = false;

            }
        }); }

        private CommandViewModel productAction;
        public CommandViewModel ProductAction {
            get
            {
                return productAction;
            }
            set
            {
                productAction = value;
                OnPropertyChanged("ProductAction");
            }
        
        }
        public CommandViewModel CloseAbout { get => new CommandViewModel(closeAbout); }
        private void closeAbout()
        {
            AboutVisibility = false;
            BtnVisibility = false;

        }

    #endregion

    #region oil prop

    private bool functionAccess = false;
        public bool FunctionAccess 
        { 
            get => functionAccess;
            set 
            {
                functionAccess = value;
                OnPropertyChanged("FunctionAccess");
            }

        }

        private double functionsOpacity = 0.3;
        public double FunctionsOpacity
        {
            get => functionsOpacity;
            set
            {
                functionsOpacity = value;
                OnPropertyChanged("FunctionsOpacity");
            }

        }


        #endregion

        #region user purchase
        private UserProductsBuilder builder = new UserProductsBuilder();
        
        //collection
        private List<ProductPresenter> purchase;
        public List<ProductPresenter> Purchase 
        { 
            get => purchase;
            set
            {
                purchase = value;
                OnPropertyChanged("Purchase");
            }
        }

        private ProductPresenter selectedProduct;
        public ProductPresenter SelectedProduct
        {
            get => selectedProduct;
            set
            {
                selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            }
        }

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

        private bool btnVisibility = false;
        public bool BtnVisibility
        {
            get => btnVisibility;
            set
            {
                btnVisibility = value;
                OnPropertyChanged("BtnVisibility");
            }
        }

        //private SolidColorBrush color = new SolidColorBrush(Colors.White);
        //public SolidColorBrush Color
        //{
        //    get
        //    {
        //        return color;
        //    }
        //    set
        //    {
        //        color = value;
        //        OnPropertyChanged("Color");
        //    }
        //}

        private string productBtnText = "Loading...";
        public string ProductBtnText
        {
            get
            {
                return productBtnText;
            }
            set
            {
                productBtnText = value;
                OnPropertyChanged("ProductBtnText");
            }
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
        #region constructor
        public RefuelingPageViewModel()
        {
            getFuel();
            InitialFuelConfig();
        }


        private async void InitialFuelConfig()
        {
            try
            {
                using(FogOilEntities db  = new FogOilEntities())
                {
                    if ((await db.Users.FindAsync(DataBaseData.getInstance().UserId))!= null)
                    {
                        if((await db.Users.FindAsync(DataBaseData.getInstance().UserId)).Root == 3)
                        {
                            FuelConfigVisibility = true;
                            FuelList = db.Fuels.Select(item=> new FuelWrapper() { item=item}).ToList();
                        }
                    }

                }
            }
            catch
            {

            }
        }
        #endregion
    }
}
