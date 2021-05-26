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
using GMap;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using GMap.NET;
using FogOilAssistant.Components.Database;
using System.Windows.Media.Imaging;
using System.IO;

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

        private void map_Loaded(object sender, RoutedEventArgs e)
        {
            this.map.Bearing = 0;
            this.map.Zoom = 7;
            this.map.MaxZoom = 7;
            this.map.MinZoom = 7;
            this.map.CanDragMap = true;
            this.map.DragButton = System.Windows.Input.MouseButton.Left;

            this.map.ShowCenter = false;

            GMaps.Instance.Mode = AccessMode.ServerAndCache;

            this.map.MapProvider = GMapProviders.YandexMap;

            this.map.Position = new PointLatLng(53.901932854227674, 27.557406908267073);

            using(FogOilEntities db = new FogOilEntities())
            {
                var locations = db.Locations.ToList();

                foreach(var loc in locations)
                {
                    GMapMarker marker = new GMapMarker(new PointLatLng(loc.Longitude, loc.Latitude));
                    marker.Shape = new Image {
                        Source = new BitmapImage(new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/FogOilAssistant/Components/Images/logo_transparent.png")),
                        Height = 50,
                        Width = 50,
                        ToolTip = loc.Adress
                        };
                    this.map.Markers.Add(marker);
                }
            }
        }
    }
}
