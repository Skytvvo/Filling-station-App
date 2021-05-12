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
using FogOilAssistant.Components.Database;
using System.Threading;
using FogOilAssistant.Components.Models.Loader;

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

        private void incomingNotify()
        {
            try
            {
                NotifyMessage = DataBaseData.getInstance().currentNotify.Message;
                NotifyColor = DataBaseData.getInstance().currentNotify.Color;
                callNotify();
            }
            catch
            {

            }
        }

        private void checkConnection()
        {
            try
            {
                using (FogOilEntities db = new FogOilEntities())
                {
                    //ChangeStatus("50%",  "Trying to connect to the database");
                    changeStatus.Invoke("50%", "Trying to connect to the database",true);
                    Thread.Sleep(2000);

                    if (!db.Database.Exists())
                    {
                        //ChangeStatus("50%", "Cannot connect to the database. Trying to reconnect");
                        changeStatus.Invoke("50%", "Cannot connect to the database",true);
                        Thread.Sleep(2000);
                        checkConnection();
                    }
                    else
                    {
                        changeStatus.Invoke("100%", "Welcome",false);
                        Thread.Sleep(2000);
                        closeLoader();
                    }
                }
            }
            catch
            {

            }
        }

        Thread main;
        Thread newVisualWorldThread;
        void closeApp()
        {

            try
            {
            //    Close();
                main.Abort();
                Thread.CurrentThread.Abort();
            }
            catch(Exception e)
            {

            }
        }

        private void closeLoader()
        {
            newVisualWorldThread.Abort();
        }

        ChangeStatus changeStatus;

        private void runLoader()
        {
            main = Thread.CurrentThread;
            newVisualWorldThread = new Thread(ThreadStartingPoint);
            newVisualWorldThread.SetApartmentState(ApartmentState.STA);
            newVisualWorldThread.IsBackground = true;
            newVisualWorldThread.Start();

        }
        View.LoadingWindow.Loader loader;
        private void ThreadStartingPoint()
        {
            loader = new View.LoadingWindow.Loader();
            loader.DataContext = new ViewModelLoader(closeApp,ref changeStatus);
            loader.Show();
            System.Windows.Threading.Dispatcher.Run();
           
        }
        public ViewModelMW()
        {
            runLoader();
            checkConnection();
             notifyTimer = new System.Timers.Timer();
            notifyTimer.Interval = 3000;
            notifyTimer.Elapsed += showNotify;
            DataBaseData.getInstance().onNotify += this.incomingNotify;
            WindowCommand = new CommandViewModel(fullscreen);
            normal();
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
        private ICommand windowCommand;
        public ICommand WindowCommand
        {
            get => windowCommand;
            set
            {
                windowCommand = value;
                OnPropertyChanged("WindowCommand");
            }
        }

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
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(FogOilAssistant.MainWindow))
                    (window as FogOilAssistant.MainWindow).WindowState = WindowState.Minimized;
            }
        }

        private void normal()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(FogOilAssistant.MainWindow))
                    (window as FogOilAssistant.MainWindow).WindowState = WindowState.Normal;
            }
            WindowCommand = new CommandViewModel(fullscreen);

        }

        private void fullscreen()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(FogOilAssistant.MainWindow))
                    (window as FogOilAssistant.MainWindow).WindowState = WindowState.Maximized;
            }
            WindowCommand = new CommandViewModel(normal);

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
        private static System.Timers.Timer notifyTimer;
        private void showNotify(Object source, System.Timers.ElapsedEventArgs e)
        {
            this.NotifyVisssible = false;
            notifyTimer.Stop();
        }
    }


    

}
