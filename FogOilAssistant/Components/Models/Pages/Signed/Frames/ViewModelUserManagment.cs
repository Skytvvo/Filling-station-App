using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using FogOilAssistant.Components.Data;
using FogOilAssistant.Components.Data.GlobalStorage;
using FogOilAssistant.Components.Data.Pages.Signed;
using FogOilAssistant.Components.Data.UI;
using FogOilAssistant.Components.Database;

namespace FogOilAssistant.Components.Models.Pages.Signed.Frames
{
    public class ViewModelUserManagment : INotifyPropertyChanged
    {
        #region users
        private List<User> users;
        public List<User> Users
        {
            get => users;
            set
            {
                users = value;
                OnPropertyChanged("Users");
            }
        }

        #endregion

        public string GetHashPAss(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

            return Convert.ToBase64String(hash);
        }

        #region UI props
        private bool editorVisibility; 
        public bool EditorVisibility
        {
            get => editorVisibility;
            set
            {
                editorVisibility = value;
                OnPropertyChanged("EditorVisibility");
            }
        }

        private bool aboutVisibility;
        public bool AboutVisibility
        {
            get => aboutVisibility;
            set
            {
                aboutVisibility = value;
                OnPropertyChanged("AboutVisibility");
            }
        }

        private void closeAbout()
        {
            AboutVisibility = false;
        }
        #endregion

        #region Selected User
        private int selectedUserId;
        public int SelectedUserId
        {
            get => selectedUserId;
            set
            {
                selectedUserId = value;
                OnPropertyChanged("SelectedUserId");
            }
        }

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


        private int selectedIndex;
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                OnPropertyChanged("SelectedIndex");
            }
        }
        private List<string> root;
        public List<string> Root
        {
            get => root;
            set
            {
                root = value;
                OnPropertyChanged("Root");
            }
        }

        private string oils;
        public string Oils
        {
            get => oils;
            set
            {
                if (Regex.IsMatch(value, @"^[0-9]*(?:\.[0-9]*)?$"))
                {
                    oils = value;
                    OnPropertyChanged("Oils");
                }
            }
        }


        private string newPassword = "";
        public string NewPassword
        {
            get => newPassword;
            set
            {
                newPassword = value;
                OnPropertyChanged("NewPassword");
            }
        }

        private string bonus;
        public string Bonus
        {
            get => bonus;
            set
            {
                if (Regex.IsMatch(value, @"^[0-9]*(?:\.[0-9]*)?$"))
                {
                    bonus = value;
                    OnPropertyChanged("Bonus");
                }
            }
        }


        #endregion

        #region History of User

        private UserProductsBuilder UserProductsBuilder = new UserProductsBuilder();

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

        #region Async
        private async void loadUsers()
        {
            try
            {
                using (FogOilEntities db = new FogOilEntities())
                {
                    await Task.Run(() => {
                        var id = DataBaseData.getInstance().UserId;
                        var res = db.Users;
                        Users = res.Where(item => item.UserId != id).ToList();
                        Root = db.Roots.Select(item => item.Name).ToList();

                    });
                }
            }
            catch (Exception e)
            {

            }

        }


        #endregion

        #region About panel
        private string writtenUser;
        public string WrittenUser
        {
            get => writtenUser;
            set
            {
                writtenUser = value;
                OnPropertyChanged("WrittenUser");
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


        #endregion

        #region Sort
        private int selectedTypeOfSort;
        public int SelectedTypeOfSort
        {
            get => selectedTypeOfSort;
            set
            {
                selectedTypeOfSort = value;
                reloadProducts(value);
                OnPropertyChanged("SelectedTypeOfSort");
            }
        }

        private List<string> typesOfSort;
        public List<string> TypesOfSort
        {
            get => typesOfSort;
            set
            {
                typesOfSort = value;

                OnPropertyChanged("TypesOfSort");
            }
        }

        private void reloadProducts(int selected)
        {
            switch (selected)
            {
                case 0:
                    loadUserProducts();
                    break;
                case 1:
                    sortByStatus();
                    break;
                case 2:
                    sortByDate();
                    break;
                case 3:
                    sortById();
                    break;
            }
        }

        private async void sortByStatus()
        {
            await Task.Run(()=> { 
                Products = Products.OrderBy(item => item.StatusId).ToList();
            });
        }
        private async void sortById()
        {
            await Task.Run(()=> { 
            Products = Products.OrderBy(item => item.ID).ToList();
            });
        }
        private async void sortByDate()
        {
            await Task.Run(() => {
                Products = Products.OrderBy(item => item.OrderDate).ToList();
            });
        }

        #endregion
        #region Methods

        private void createNewUser()
        {
            SelectedIndex = 0;
            Nick = "New User";
            Bonus = "0";
            Oils = "0";
            SelectedUserId = 0;

            Products = new List<ProductPresenter>();

            EditorVisibility = true;
        }
        private void openEditor(object obj)
        {
            try
            {
                var user = obj as User;
                if (user == null) throw new Exception();

                SelectedIndex = user.Root - 1;
                Nick = user.Nick;
                Bonus = user.Bonus.ToString();
                Oils = user.Oil.ToString();
                SelectedUserId = user.UserId;
                loadUserProducts();
                EditorVisibility = true;
            }
            catch (Exception e)
            {
                closeEditor();
            }
        }

        private async void loadUserProducts()
        {
            try
            {
                using (FogOilEntities db = new FogOilEntities())
                {
                    if (selectedUserId == 0)
                        return;
                    Products = (await db.Users.FindAsync(SelectedUserId)).UserProducts
                         .Select(item =>
                         UserProductsBuilder.GetProduct(item.UserProductsId,
                         new AllOrdersBuilder(),
                         item.Product,
                         item.OrderStatu,
                         item.Location)).ToList();
                }
            }
            catch (Exception e)
            {

            }
        }
        private void closeEditor()
        {
            EditorVisibility = false;
        }

        private async void RemoveUser()
        {
            try
            {
                using (FogOilEntities db = new FogOilEntities())
                {
                    var user = await db.Users.FindAsync(SelectedUserId);
                    if (user.Baskets.Count != 0 || (user.Baskets != null))
                    {
                        var removingUserBasket = db.Baskets.Where(item => item.UserId == SelectedUserId);
                        db.Baskets.RemoveRange(removingUserBasket);
                        await db.SaveChangesAsync();
                    }
                    if (user.UserProducts.Count != 0 || user.UserProducts != null)
                    {
                        var removingUserPurchase = db.UserProducts.Where(item => item.UserId == SelectedUserId);
                        db.UserProducts.RemoveRange(removingUserPurchase);
                        await db.SaveChangesAsync();
                    }
                    db.Users.Remove(user);
                    DataBaseData.getInstance().CallNotify(new Data.Pages.Notify()
                    {
                        Message = "Removed",
                        Color = UIData.GetColor(UIData.MessageColor.ERROR)
                    });
                    await db.SaveChangesAsync();
                }
                loadUsers();
                closeEditor();
            }
            catch (Exception e)
            {
                closeEditor();
            }
        }
        private async void SaveChanges()
        {
            try
            {
                using (FogOilEntities db = new FogOilEntities())
                {

                    if(db.Users.Where(item=>(item.Nick == Nick) && item.UserId != SelectedUserId).ToList().Count != 0)
                    {
                        DataBaseData.getInstance().CallNotify(new Data.Pages.Notify()
                        {
                            Message = "User exists",
                            Color = UIData.GetColor(UIData.MessageColor.ERROR)
                        });
                        NewPassword = "";
                        return;
                    }
                    if((await db.Users.FindAsync(selectedUserId))==null)
                    {
                        

                        if(NewPassword.Length <4)
                        {
                            DataBaseData.getInstance().CallNotify(new Data.Pages.Notify()
                            {
                                Message = "Password must consist min 4 symbols",
                                Color = UIData.GetColor(UIData.MessageColor.ERROR)
                            });
                            NewPassword = "";

                            return;
                        }
                        User newUser = new User() { Root = (SelectedIndex + 1), Nick = Nick, Bonus = Convert.ToDouble(Bonus), Oil =  Convert.ToDouble(Oils), Password = GetHashPAss(NewPassword) };
                        db.Users.Add(newUser);
                        await db.SaveChangesAsync();
                        DataBaseData.getInstance().CallNotify(new Data.Pages.Notify()
                        {
                            Message = "User was created",
                            Color = UIData.GetColor(UIData.MessageColor.SUCCESS)
                        });
                        NewPassword = "";

                        loadUsers();
                        closeEditor();
                        return;
                    }

                    //todo: check on exisitng user
                    (await db.Users.FindAsync(SelectedUserId)).Root = SelectedIndex + 1;
                    (await db.Users.FindAsync(SelectedUserId)).Nick = Nick;
                    (await db.Users.FindAsync(SelectedUserId)).Bonus = Convert.ToDouble(Bonus);
                    (await db.Users.FindAsync(SelectedUserId)).Oil = Convert.ToDouble(Oils);
                    if (NewPassword.Length != 0)
                    {
                        if (!(NewPassword.Length < 4))
                            (await db.Users.FindAsync(SelectedUserId)).Password = GetHashPAss(newPassword);
                        else
                        {
                            DataBaseData.getInstance().CallNotify(new Data.Pages.Notify()
                            {
                                Message = "Password must consist min 4 symbols",
                                Color = UIData.GetColor(UIData.MessageColor.ERROR)
                            });
                            NewPassword = "";
                            return;
                        }
                    }
                    await db.SaveChangesAsync();

                }
                DataBaseData.getInstance().CallNotify(new Data.Pages.Notify()
                {
                    Message = "Saved",
                    Color = UIData.GetColor(UIData.MessageColor.SUCCESS)
                });
                loadUsers();
                NewPassword = "";

                closeEditor();
            }
            catch (Exception e)
            {
                closeEditor();
            }
        }
        #endregion

        #region Commands


        public RelayCommand SelectProduct { get => new RelayCommand((obj) => { 
            try
            {
                SelectedProduct = obj as ProductPresenter;
                AboutVisibility = true;
            }
            catch(Exception e)
            {
                closeAbout();
            }
        }); }
        public RelayCommand OpenEditor { get => new RelayCommand(openEditor); }
        public CommandViewModel CreateNewUser { get => new CommandViewModel(createNewUser); }
        public CommandViewModel CloseSelectedAbout { get => new CommandViewModel(() => {
            closeAbout();
        }); }
        public CommandViewModel FindUser { get => new CommandViewModel(async ()=> {
            try
            {
                using (FogOilEntities db = new FogOilEntities())
                {
                    await Task.Run(() => {
                        var users = db.Users;
                        Users = users.Where(item => item.Nick.StartsWith(WrittenUser) && !item.Nick.Equals(Nick)).ToList();
                        WrittenUser = "";
                    });
                }
            }
            catch(Exception e)
            {

            }
        }); }
        private List<ButtonDescription> actionsList;
        public List<ButtonDescription> ActionsList
        {
            get => actionsList;
            set
            {
                actionsList = value;
                OnPropertyChanged("ActionsList");
            }
        }

        #endregion
        public ViewModelUserManagment()
        {
            TypesOfSort = new List<string>()
            {
                "Default",
                "Status",
                "Date",
                "ID"
            };
            ActionsList = new List<ButtonDescription>()
            {
              new ButtonDescription(){ act = new CommandViewModel(closeEditor), Text="Cancel"},
              new ButtonDescription(){ act = new CommandViewModel(SaveChanges), Text="Save"},
              new ButtonDescription(){ act = new CommandViewModel(RemoveUser), Text="Remove"},
            };

            loadUsers();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
