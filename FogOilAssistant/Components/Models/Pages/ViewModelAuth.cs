using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using FogOilAssistant.Components.Data;

namespace FogOilAssistant.Components.Models.Pages
{
    public class ViewModelAuth: INotifyPropertyChanged
    {

        #region FIELDS
        //counter for images
        int counter = 0;

        //path for images
        string imgPath;

        public string ImgPath { 
            get => imgPath;
            set
            {
                imgPath = value;
                OnPropertyChanged("ImgPath");
            } 
        }

        //text for log-action button
        string switch_text;
        public string Switch_text {
            get => switch_text; 
            set
            {
                this.switch_text = value;
                OnPropertyChanged("Switch_text");
            }
        }


        //login input
        string login = "";
        public string Login {
            get => login;
            set
            {
                login = value;
                MessageBox.Show(value);
                OnPropertyChanged("Login");
            }
                
        }   



        #endregion
        public ViewModelAuth()
        {
            this.ImgPath = "/Components/Images/Auth/auth_1.jpg";
            this.Switch_text = "Sign in";
            this.Sign = new CommandViewModel(signIn);

            Timer = new System.Timers.Timer();
            Timer.Interval = 5000;
            Timer.Elapsed += OnTimedEvent;
            Timer.Enabled = true;
        }


        #region Events

        CommandViewModel sign;
        public CommandViewModel Sign
        {
            get => sign;
            set
            {
                sign = value;
                OnPropertyChanged("Sign");
            }
        }


        void signIn()
        {
            MessageBox.Show("Sing in");
        }
        
        void signUp()
        {
            MessageBox.Show("Sing up");
        }

        private static Timer Timer;
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (counter == 3)
                counter = 0;
            this.ImgPath = $"/Components/Images/Auth/auth_{++counter}.jpg";
        }
        #endregion
    }
}
