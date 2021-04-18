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


        #region Commands
        public CommandViewModel Switch_SignIn { get => new CommandViewModel(switch_signUp); }
        public CommandViewModel Switch_SignUp { get => new CommandViewModel(switch_signIn); }
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

        void switch_signIn()
        {

            SignUp_background = "#ff0000";
            SignUp_oapcity = 1;

            SignIn_background = "#535353";
            SignIn_oapcity = 0.5;

            Switch_text = "Sign up";

            Sign = new CommandViewModel(signUp);
        }
        //clicked on sign
        void switch_signUp()
        {
            
            SignIn_background = "#ff0000";
            SignIn_oapcity = 1;

            SignUp_background = "#535353";
            SignUp_oapcity = 0.5;

            Switch_text = "Sign in";
            Sign = new CommandViewModel(signIn);

        }
        #endregion


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
        //text for form-part log-action button (switch-action button)
        public string Switch_text {
            get => switch_text; 
            set
            {
                this.switch_text = value;
                OnPropertyChanged("Switch_text");
            }
        }

        //signIn mode style
        string signIn_background = "#ff0000";
        public string SignIn_background {
            get => signIn_background; 
            set
            {
                signIn_background = value;
                OnPropertyChanged("SignIn_background");
            }
        }
        double signIn_oapcity = 1;
        public double SignIn_oapcity
        {
            get => signIn_oapcity; 
            set
            {
                signIn_oapcity = value;
                OnPropertyChanged("SignIn_oapcity");
            } 
        }




        //signUp mode style
        string signUp_background = "#535353";
        public string SignUp_background
        {
            get => signUp_background;
            set
            {
                signUp_background = value;
                OnPropertyChanged("SignUp_background");
            }
        }
        double signUp_oapcity = 0.5;
        public double SignUp_oapcity
        {
            get => signUp_oapcity;
            set
            {
                signUp_oapcity = value;
                OnPropertyChanged("SignUp_oapcity");
            }
        }

        //login input
        string login = "";
        public string Login {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
                
        }

        //password input
        string password = "";
        public string Password {
            get => password; 
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }




        //checkbox

        bool isChecked = false;
        public bool IsChecked 
        { 
            get => isChecked;
            set
            {
                isChecked = value;
                OnPropertyChanged("IsChecked");

                if (value)
                    Blur = 0;
                else
                    Blur = 20;
                    
            } 
        }


        //blur
        double blur = 20;
        public double Blur 
        { 
            get => blur;
            set 
            {
                blur = value;
                OnPropertyChanged("Blur");
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

       


        void signIn()
        {
            MessageBox.Show((Login+Password));
        }
        
        void signUp()
        {
            MessageBox.Show(Login + Password);
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
