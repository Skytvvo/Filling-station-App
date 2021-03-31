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
using FogOilAssistant.Components.Data.MenuButton;
using FogOilAssistant.Components.Models.ToggleMenu;

namespace FogOilAssistant.Components.View
{
    /// <summary>
    /// Логика взаимодействия для ToggleMenu.xaml
    /// </summary>
    public partial class ToggleMenu : UserControl
    {
        public ToggleMenu()
        {
            InitializeComponent();

        }
         

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
