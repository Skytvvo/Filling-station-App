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
using FogOilAssistant.Components.Data.Pages.Signed;

namespace FogOilAssistant.Components.View.Pages.EmployeeControls
{
    /// <summary>
    /// Логика взаимодействия для ModifinedTextBox.xaml
    /// </summary>
    public partial class ModifinedTextBox : UserControl, INotifyPropertyChanged
    {


        public string ModText
        {
            get
            {
              return  Description.Text;
            }
            set
            {
                Description.Text = value;
                OnPropertyChanged("ModText");
            }
        }
        public TextBoxProp Description
        {
            get { return (this.GetValue(DescriptionProperty) as TextBoxProp); }
            set 
            {
                this.SetValue(DescriptionProperty, value);
                Description.Text = value.Text;
                OnPropertyChanged("ModText");
            }
        }

        public static DependencyProperty DescriptionProperty = DependencyProperty.Register(
          "Description", typeof(TextBoxProp), typeof(ModifinedTextBox), new PropertyMetadata(null));
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                int index = my_t.CaretIndex;
                ModText = ModText.Insert(index++, "\n");
                this.my_t.CaretIndex = index;
                OnPropertyChanged("ModText");

            }
        }
        public ModifinedTextBox()
        {
            InitializeComponent();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ModText = Description.Text;
            OnPropertyChanged("ModText");

        }
    }
}
