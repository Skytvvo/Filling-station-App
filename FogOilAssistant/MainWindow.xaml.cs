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
using FogOilAssistant.Components;

namespace FogOilAssistant
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

     
      


        private void toggle_menu_handler(object sender, RoutedEventArgs e)
        {
            if(this.toggle_menu.Visibility == Visibility.Visible)
            {
                this.toggle_menu.Visibility = Visibility.Collapsed;
                Grid.SetRow(this.toggle_menu_button, 3);
            }
            else 
            { 
                this.toggle_menu.Visibility = Visibility.Visible;
                Grid.SetRow(this.toggle_menu_button, 0);
            }
            
        }

        private void Toolbar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void close_app_button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void minimize_button_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;

        }
    }
}
