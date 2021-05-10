using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using FogOilAssistant.Components.Models.Pages;
using Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT;
using Microsoft.Toolkit.Wpf.UI.Controls;

namespace FogOilAssistant.Components.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для Refueling_map.xaml
    /// </summary>
    public partial class Refueling_map : Page
    {
        public Refueling_map()
        {
            InitializeComponent();
          //  this.DataContext = new ViewModelMap();
            
        }

        //private  async void map_container_Loaded(object sender, RoutedEventArgs e)
        //{
        //    BasicGeoposition basicGeoposition = new BasicGeoposition() { Latitude = 53.8144, Longitude = 28.0241 };
        //    var center = new Geopoint(basicGeoposition);
        //    await ((Microsoft.Toolkit.Wpf.UI.Controls.MapControl)sender).TrySetViewAsync(center, 5.8);
           
        //}
    }
}
