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
using FogOilAssistant.Components.Data;

namespace FogOilAssistant.Components.View.Pages.EmployeeControls
{
    /// <summary>
    /// Логика взаимодействия для Options.xaml
    /// </summary>
    public partial class Options : UserControl
    {
        public List<RelayCommand> ActionProp
        {
            get { return (this.GetValue(ActionPropProperty) as List<RelayCommand>); }
            set { this.SetValue(ActionPropProperty, value); }
        }

        public static DependencyProperty ActionPropProperty = DependencyProperty.Register(
          "ActionProp", typeof(List<RelayCommand>), typeof(Options), new PropertyMetadata(null));


        public Options()
        {
            InitializeComponent();
        }

       
        
    }
}
