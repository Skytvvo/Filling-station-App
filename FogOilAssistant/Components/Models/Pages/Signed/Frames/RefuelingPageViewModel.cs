using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FogOilAssistant.Components.Data;
using FogOilAssistant.Components.Data.Pages.Signed;
using FogOilAssistant.Components.Database;

namespace FogOilAssistant.Components.Models.Pages.Signed.Frames
{
    public class RefuelingPageViewModel : INotifyPropertyChanged
    {
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


        private string volume = "0";
        public string Volume 
        { 
            get => volume; 
            set
            {
                volume = value;
                OnPropertyChanged("Volume");
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
                    await Task.Run(() =>
                    {
                        userId = db.Users.First(_user => _user.Nick == Nick).UserId;
                        loadPurchase(userId);
                    });
                }
            }
            catch (Exception e)
            {
                toDeny();
            }
        }

        private async void loadPurchase(int id)
        {
            try
            {
                using (FogOilEntities db = new FogOilEntities())
                {
                    Purchase = (await db.Users.FindAsync(userId)).UserProducts
                            .Where(product => product.OrderStatu.Status == "Ready")
                            .Select((prod) => builder
                            .GetProduct(prod.UserProductsId, new HistoryBuilder(), prod.Product, prod.OrderStatu, prod.Location))
                            .ToList();
                }
                toAccess();
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


        #region Commands
        public CommandViewModel FindUser { get => new CommandViewModel(findUser); }
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
        }
        #endregion
    }
}
