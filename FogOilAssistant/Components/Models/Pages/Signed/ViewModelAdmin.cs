using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FogOilAssistant.Components.Data;
using FogOilAssistant.Components.Data.GlobalStorage;

namespace FogOilAssistant.Components.Models.Pages.Signed
{
    public class ViewModelAdmin: ViewModelEmployee
    {
        
        public ViewModelAdmin():base()
        {
            CommandList.Insert(3, new ArgRelayCommand() {
                Action = new RelayCommand((obj) => {
                    SelectedPage = pages[3];
                }),
                Name = "Locations"
            });
            CommandList.Insert(4, new ArgRelayCommand()
            {
                Action = new RelayCommand((obj) => {
                    SelectedPage = pages[4];
                }),
                Name = "Users"
            });

            OnPropertyChanged("CommandList");
        }

      
    }
}
