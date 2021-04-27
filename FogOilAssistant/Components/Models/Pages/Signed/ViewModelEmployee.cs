using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FogOilAssistant.Components.Data;

namespace FogOilAssistant.Components.Models.Pages.Signed
{
    
    public class ViewModelEmployee: INotifyPropertyChanged
    {
        private List<RelayCommand> commandList;

        public List<RelayCommand> CommandList
        {
            get => commandList;
            set
            {
                commandList = value;
                OnPropertyChanged("CommandList");
            }
        }
        public ViewModelEmployee()
        {
            CommandList = new List<RelayCommand>()
            {
                new RelayCommand((obj) =>
                {
                    MessageBox.Show("1");
                }),
                new RelayCommand((obj) =>
                {
                    MessageBox.Show("2");

                }),
                new RelayCommand((obj) =>
                {
                    MessageBox.Show("3");

                })
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
