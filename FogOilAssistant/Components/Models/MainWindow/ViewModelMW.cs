using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FogOilAssistant.Components.Models.MainWindow
{
    //MW == MainWindow
    class ViewModelMW : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public ICommand MinimizeWindow { get => new WMICommand(Minimize); }
        public ICommand CloseAppWindow { get => new WMICommand(Close); }

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
