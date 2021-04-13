using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using FogOilAssistant.Components.View.Pages;
using Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT;

namespace FogOilAssistant.Components.Models.Pages
{
    public class ViewModelMap : INotifyPropertyChanged
    {
        

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion

        #region Fields and Props

        #endregion

        #region DOM Handlers
        private Refueling_map getPage()
        {


            try
            {
                
                var frame_content = (Application.Current.MainWindow as FogOilAssistant.MainWindow).Frame.Content;
                if (frame_content.GetType() == typeof(Refueling_map))
                {
                    return (frame_content as Refueling_map);
                }

                return null;
            }  
          catch(Exception e)
            {
                return null;
            }
                   
        }
        #endregion

        public ViewModelMap()
        {
         try
            {
                


                    //53.99758615428855, 27.869467488059716

            }
            catch(Exception e)
            {
                MessageBox.Show("Map error");
            }
        }

    }
}
