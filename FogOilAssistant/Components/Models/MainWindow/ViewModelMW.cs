using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FogOilAssistant.Components.Data;

namespace FogOilAssistant.Components.Models.MainWindow
{
    //MW == MainWindow

    class ViewModelMW : INotifyPropertyChanged
    {
       
        
        public ViewModelMW()
        {
        }
        private Visibility basketVisibility = Visibility.Collapsed;
        public Visibility BasketVisibility
        {
            get => basketVisibility;
            set
            {
                basketVisibility = value;
                OnPropertyChanged("BasketVisibility");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public ICommand MinimizeWindow { get => new WMICommand(Minimize); }
        public ICommand CloseAppWindow { get => new WMICommand(Close); }
        public ICommand OnBasket { get => new CommandViewModel(onBasket); }

        private void onBasket()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(FogOilAssistant.MainWindow))
                {


                    switch (this.BasketVisibility)
                    {
                        case (Visibility.Collapsed):
                        {
                            this.BasketVisibility = Visibility.Visible;
                            break;
                        }
                        case (Visibility.Visible):
                        {
                            this.BasketVisibility = Visibility.Collapsed;
                            break;
                        }
                    }
                    (window as FogOilAssistant.MainWindow).BasketProducts.Visibility = this.BasketVisibility;

                }
            }
        }


        private void Minimize()
        {
            foreach(Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(FogOilAssistant.MainWindow))
                    (window as FogOilAssistant.MainWindow).WindowState = WindowState.Minimized;
            }
        }


        private void Close()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(FogOilAssistant.MainWindow))
                    (window as FogOilAssistant.MainWindow).Close();
            }
        }
       
    }




}
