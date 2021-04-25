using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using FogOilAssistant.Components.Data.GlobalStorage;
using FogOilAssistant.Components.Data.MenuButton;

namespace FogOilAssistant.Components.Models.ToggleMenu
{
    public class ViewModelToggleMenu: System.ComponentModel.INotifyPropertyChanged
    {
        private List<ButtonMenu> buttonMenus;

        public event PropertyChangedEventHandler PropertyChanged;

        public List<ButtonMenu> ButtonMenus {
            get
            {
                return buttonMenus;
            }
            set
            {
                buttonMenus = value;
                OnPropertyChanged("ButtonMenus");
            }
        }

        private void ChangeMenu(object sender, NotifyCollectionChangedEventArgs e)
        {
            ButtonMenus = DataBaseData.getInstance().MenuList.ToList();
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public ViewModelToggleMenu()
        {
            ButtonMenus = DataBaseData.getInstance().MenuList.ToList();
            DataBaseData.getInstance().MenuList.CollectionChanged += ChangeMenu;
        }
    }
}
