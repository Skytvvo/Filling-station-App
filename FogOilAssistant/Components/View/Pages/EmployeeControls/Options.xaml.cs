using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using FogOilAssistant.Components.Data.GlobalStorage;
using FogOilAssistant.Components.Database;

namespace FogOilAssistant.Components.View.Pages.EmployeeControls
{
    /// <summary>
    /// Логика взаимодействия для Options.xaml
    /// </summary>
    public partial class Options : UserControl, INotifyPropertyChanged
    {
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public List<ArgRelayCommand> ActionProp
        {
            get { return (this.GetValue(ActionPropProperty) as List<ArgRelayCommand>); }
            set { this.SetValue(ActionPropProperty, value); }
        }

        public static DependencyProperty ActionPropProperty = DependencyProperty.Register(
          "ActionProp", typeof(List<ArgRelayCommand>), typeof(Options), new PropertyMetadata(null));


        public CommandViewModel Exit { get => new CommandViewModel(() => {
            DataBaseData.getInstance().Exit();
            DataBaseData.getInstance().GoToShopPage();
        }); 
        }

        public string Nick { get => DataBaseData.getInstance().Login; }

        private string root = "Loading...";

        public event PropertyChangedEventHandler PropertyChanged;

        public string Root 
        { 
            get => root;
            set
            { 
                root = value;
                OnPropertyChanged("Root");
            }
        }


        private string svgPath = "/Components/Images/ViewModels/Signed/admin.svg";
        public string SvgPath 
        { 
            get => svgPath;
            set
            { 
                svgPath = value;
                OnPropertyChanged("SvgPath");
            }
        }

        private async void PreLoad()
        {
            try
            {
                using(FogOilEntities db = new FogOilEntities())
                {
                    Root = (await db.Users.FindAsync(DataBaseData.getInstance().UserId)).Root1.Name;
                    SvgPath = GetPathById((await db.Users.FindAsync(DataBaseData.getInstance().UserId)).Root);
                }
            }
            catch(Exception e)
            {

            }
        }
        private string GetPathById(int id)
        {
            if (id == 2 )
                return "/Components/Images/ViewModels/Signed/employee.svg";
            return "/Components/Images/ViewModels/Signed/admin.svg";
        }
        public Options()
        {
            InitializeComponent();
            PreLoad();
        }

       
        
    }
}
