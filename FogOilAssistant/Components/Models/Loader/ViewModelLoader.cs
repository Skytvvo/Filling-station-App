using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using FogOilAssistant.Components.Data;

namespace FogOilAssistant.Components.Models.Loader
{
    public delegate void ChangeStatus(string percents,string status, bool visibility);
    public class ViewModelLoader: INotifyPropertyChanged
    {
        


        private string percent = "0%";
        public string Percent
        {
            get => percent;
            set
            {
                percent = value;
                OnPropertyChanged("Percent");
            }
        }

        private bool visibility = false;
        public bool Visibility
        {
            get => visibility;
            set
            {
                visibility = value;
                OnPropertyChanged("Visibility");
            }
        }

        private string status = "Initialization";
        public string Status
        {
            get => status;
            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }

        event Action closeApp;

        public void closeLoader()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(FogOilAssistant.Components.View.LoadingWindow.Loader))
                    (window as View.LoadingWindow.Loader).Close();
            }
        }
        public CommandViewModel CloseApp { get => new CommandViewModel(() =>
         {
             //closeLoader();            
            closeApp();

         });
        }
     
        public void updateState(string percents, string status, bool visibility)
        {
            Status = status;
            Percent = percents;
            Visibility = visibility;
        }

        public ViewModelLoader(Action action,ref ChangeStatus changeStatusDel)
        {
            this.closeApp += action;
            changeStatusDel += updateState;

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
