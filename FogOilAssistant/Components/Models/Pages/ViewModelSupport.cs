using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FogOilAssistant.Components.Data.Pages.Support;

namespace FogOilAssistant.Components.Models.Pages
{
    public class ViewModelSupport : INotifyPropertyChanged
    {
        List<SupportItem> supportItems;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public List<SupportItem> SupportItems { get => supportItems;
            set {
                this.supportItems = value;
                OnPropertyChanged("SupportItems");
            }
        }

        public ViewModelSupport()
        {
            this.SupportItems = new List<SupportItem>()
            {
                new SupportItem(){ Color="#E31E8F", Link="#", Name="Feedback", Text="Your feedback will be processe by our technical support."},
                new SupportItem(){Color="#00E5FF", Link="#", Name="Consultant", Text="Online consultation with our employee"}
               
            };
        }
    }
}
