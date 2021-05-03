using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using FogOilAssistant.Components.Data;
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

        #region Async
        private async void loadUsers()
        {
            try
            {
                using(FogOilEntities db = new FogOilEntities())
                {
                    await Task.Run(()=> {
                        Users =  db.Users.ToList();
                        Root = db.Roots.Select(item => item.Name).ToList();

                    });
                }
            }
            catch(Exception e)
            {

            }

        }

        
        #endregion

        #region Methods
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

                EditorVisibility = true;
            }
            catch(Exception e)
            {
                closeEditor();
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
                using(FogOilEntities db = new FogOilEntities())
                {
                    (await db.Users.FindAsync(SelectedUserId)).Root = SelectedIndex+1;
                    (await db.Users.FindAsync(SelectedUserId)).Nick = Nick;
                    (await db.Users.FindAsync(SelectedUserId)).Bonus = Convert.ToDouble(Bonus);
                    (await db.Users.FindAsync(SelectedUserId)).Oil = Convert.ToDouble(Oils);
                    await db.SaveChangesAsync();

                }
                loadUsers();
                closeEditor();
            }
            catch(Exception e)
            {
                closeEditor();
            }
        }
        #endregion

        #region Commands


        public RelayCommand OpenEditor { get => new RelayCommand(openEditor); }


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
