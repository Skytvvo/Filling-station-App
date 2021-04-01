using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FogOilAssistant.Components.Models.Pages
{


    public class ViewModelShop: INotifyPropertyChanged
    {

        private string _Path;
        private int ID = 1;

        public string Path
        {
            get => _Path;
            set
            {
                _Path = value;
                OnPropertyChanged("Path");
            }
        }

        private static Timer aTimer;

        public ViewModelShop()
        {
            this.Path = "/Components/Images/Slider/1.jpg";
            aTimer = new System.Timers.Timer();
            aTimer.Interval = 15000;
            aTimer.Elapsed += OnTimedEvent;
            aTimer.Enabled = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (ID == 3)
                ID = 0;
            this.Path = $"/Components/Images/Slider/{++ID}.jpg";
        }
    }


}
