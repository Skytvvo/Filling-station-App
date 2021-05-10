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
using System.Timers;
using FogOilAssistant.Components.Data.GlobalStorage;
using System.Collections.Specialized;

namespace FogOilAssistant.Components.Models.MainWindow
{
    //MW == MainWindow

    class ViewModelMW : INotifyPropertyChanged
    {


        //notify color
        private bool notifyVisssible = false;
        public bool NotifyVisssible
        {
            get => notifyVisssible;
            set
            {
                notifyVisssible = value;
                OnPropertyChanged("NotifyVisssible");
            }
        }


        //notify color
        private string notifyColor = "#ffffff";
        public string NotifyColor
        {
            get => notifyColor;
            set
            {
                notifyColor = value;
                OnPropertyChanged("NotifyColor");
            }
        }


        //notify message
        private string notifyMessage = "Welcome";
        public string NotifyMessage
        {
            get => notifyMessage;
            set
            {
                notifyMessage = value;
                OnPropertyChanged("NotifyMessage");
            }
        }

        private void incomingNotify(object sender, NotifyCollectionChangedEventArgs e)
        {
            try
            {
                NotifyMessage = DataBaseData.getInstance().Notifies.ToList()[0].Message;
                NotifyColor = DataBaseData.getInstance().Notifies.ToList()[0].Color;
                callNotify();
            }
            catch
            {

            }
        }

        public ViewModelMW()
        {
            notifyTimer = new Timer();
            notifyTimer.Interval = 3000;
            notifyTimer.Elapsed += showNotify;
            DataBaseData.getInstance().Notifies.CollectionChanged += this.incomingNotify;
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



        private void callNotify()
        {
            notifyTimer.Stop();
            this.NotifyVisssible = true;
            notifyTimer.Start();
        }
        private static Timer notifyTimer;
        private void showNotify(Object source, System.Timers.ElapsedEventArgs e)
        {
            this.NotifyVisssible = false;
            notifyTimer.Stop();
        }
    }


    

}
