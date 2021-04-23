using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FogOilAssistant.Components.View
{
    /// <summary>
    /// Логика взаимодействия для BindablePasswordBox.xaml
    /// </summary>
    public partial class BindablePasswordBox : UserControl
    {
        private bool _isPasswordChanging;

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(BindablePasswordBox),
                new FrameworkPropertyMetadata(string.Empty, 
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    PasswordPropertyChanged,
                    null,
                    false,
                    UpdateSourceTrigger.PropertyChanged
                    ));

        private static void PasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BindablePasswordBox passwordbox)
            {
                passwordbox.updatePassword();
            }
        }

        private void updatePassword()
        {
            if (!_isPasswordChanging)
            {
                passwordbox.Password = Password;

            }
        }


        public BindablePasswordBox()
        {
            InitializeComponent();
        }

        private void Passwordbox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            
            _isPasswordChanging = true;
            Password = passwordbox.Password;
            _isPasswordChanging = false;
        }
    }
}
