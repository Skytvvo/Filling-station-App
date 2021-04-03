using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FogOilAssistant.Components.Data.MenuButton
{
    public class ButtonMenu : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public string text;
        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                text = value;
                OnPropertyChanged("Text");
            }
        }

        public string path;
        public string Path
        {
            get
            {
                return this.path;
            }
            set
            {
                this.path = value;
                OnPropertyChanged("Path");
            }
        }

        public ICommand Command { get; }


        public void Execute()
        {
            if (!String.IsNullOrEmpty(this.Text))
                navigateToPage(this.Text);
        }

        private void navigateToPage(string text)
        {
            foreach(System.Windows.Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).Frame.Navigate(new Uri(string.Format("{0}{1}{2}", "/Components/View/Pages/", text, ".xaml"), UriKind.RelativeOrAbsolute));
                }
            }
        }


        public ButtonMenu()
        {
            this.Command = new CommandViewModel(Execute);
        }
    }



   
}
