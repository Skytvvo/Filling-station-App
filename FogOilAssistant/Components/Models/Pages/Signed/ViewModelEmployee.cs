using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FogOilAssistant.Components.Data;
using FogOilAssistant.Components.Data.GlobalStorage;

namespace FogOilAssistant.Components.Models.Pages.Signed
{
    

    public class ViewModelEmployee: INotifyPropertyChanged
    {

        #region Pages
        protected List<string> pages;


        protected string selectedPage = "/Components/View/Pages/EmployeeControls/Frames/ProductManagment.xaml";
        public string SelectedPage 
        { 
            get => selectedPage;
            set 
            {
                selectedPage = value;
                OnPropertyChanged("SelectedPage");
            }
        }


        #endregion

        #region Commands

        #endregion

        protected List<ArgRelayCommand> commandList;

        public List<ArgRelayCommand> CommandList
        {
            get => commandList;
            set
            {
                commandList = value;
                OnPropertyChanged("CommandList");
            }
        }
        #region getCommands
        protected List<ArgRelayCommand> GetCommands()
        {
            return new List<ArgRelayCommand>()
            {
                //refueling
                new ArgRelayCommand(){
                     Action = new RelayCommand((obj)=>{
                         SelectedPage = pages[0];
                     }),
                     Name = "Refueling"

                },
                //products
                new ArgRelayCommand(){
                     Action = new RelayCommand((obj)=>{
                         SelectedPage = pages[1];
                     }),
                     Name = "Products"

                },
                //orders
                new ArgRelayCommand(){
                     Action = new RelayCommand((obj)=>{
                         SelectedPage = pages[2];
                         }),
                     Name = "Orders"

                },
                new ArgRelayCommand(){
                     Action = new RelayCommand((obj)=>{
                         SelectedPage = pages[3];
                         }),
                     Name = "Cars"

                },
                 new ArgRelayCommand(){
                     Action = new RelayCommand((obj)=>{
                            DataBaseData.getInstance().Exit();
                            DataBaseData.getInstance().GoToShopPage();
                         }),
                     Name = "Exit"

                },
            };
        }
        #endregion

        public ViewModelEmployee()
        {
            pages = new List<string>()
            {
                "/Components/View/Pages/EmployeeControls/Frames/Refueling.xaml",
                "/Components/View/Pages/EmployeeControls/Frames/ProductManagment.xaml",
                "/Components/View/Pages/EmployeeControls/Frames/OrderManagment.xaml",
                "/Components/View/Pages/EmployeeControls/Frames/CarManagment.xaml",
                "/Components/View/Pages/EmployeeControls/Frames/LocationManagment.xaml",
                "/Components/View/Pages/EmployeeControls/Frames/UserManagment.xaml",
            };
            CommandList = GetCommands();
            SelectedPage = pages[0];
            
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
