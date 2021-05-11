using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using FogOilAssistant.Components.Data;
using FogOilAssistant.Components.Data.GlobalStorage;
using FogOilAssistant.Components.Data.UI;
using FogOilAssistant.Components.Database;

namespace FogOilAssistant.Components.Models.Pages.Signed.Frames
{
    public class ButtonDescription
    {
        public string Text { get; set; }
        public CommandViewModel act { get; set; }
    }

    public class ViewModelLocation: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Locations
        private List<Location> locations;
        public List<Location> Locations 
        { 
            get => locations; 
            set
            {
                locations = value;
                OnPropertyChanged("Locations");
            }
        }

        private Location selectedLocation;
        public Location SelectedLocation
        {
            get => selectedLocation;
            set
            {
                selectedLocation = value;
                OnPropertyChanged("SelectedLocation");
            }
        }
        #endregion
        #region Location Parts

        private string addres;
        public string Addres
        {
            get => addres;
            set
            {
                addres = value;
                OnPropertyChanged("Addres");
            }
        }
        private string latitude;
        public string Latitude
        {
            get => latitude;
            set
            {
                if (Regex.IsMatch(value, @"^[0-9]*(?:\.[0-9]*)?$"))
                {
                    latitude = value;
                    OnPropertyChanged("Latitude");
                }
            }
        }
        private string longitude;
        public string Longitude
        {
            get => longitude;
            set
            {
                if (Regex.IsMatch(value, @"^[0-9]*(?:\.[0-9]*)?$"))
                {
                    longitude = value;
                    OnPropertyChanged("Longitude");
                }
            }
        }
        #endregion
        #region visibility
        private bool editorVisibility;
        public bool EditorVisibility
        {
            get => editorVisibility;
            set
            {
                editorVisibility = value;
                OnPropertyChanged("EditorVisibility");
            }
        }

        private void closeEditor()
        {
            EditorVisibility = false;
        }
        private void openEditor()
        {
            EditorVisibility = true;
        }

        #endregion

        #region methods
        private async void saveLocation()
        {
            try
            {
                using(FogOilEntities db = new FogOilEntities())
                {
                    if((await db.Locations.FindAsync(selectedLocation.LocationId)) == null)
                    {
                        parseData();
                        db.Locations.Add(SelectedLocation);
                        await db.SaveChangesAsync();
                        loadLocation();
                        closeEditor();

                        DataBaseData.getInstance().CallNotify(new Data.Pages.Notify()
                        {
                            Message = "Successfully created",
                            Color = UIData.GetColor(UIData.MessageColor.SUCCESS)
                        });

                        return;
                    }
                    (await db.Locations.FindAsync(selectedLocation.LocationId)).Latitude =
                        Convert.ToDouble(Latitude);
                    (await db.Locations.FindAsync(selectedLocation.LocationId)).Longitude =
                        Convert.ToDouble(Longitude);
                    (await db.Locations.FindAsync(selectedLocation.LocationId)).Adress =
                        Addres;
                    await db.SaveChangesAsync();
                    loadLocation();
                    closeEditor();
                    DataBaseData.getInstance().CallNotify(new Data.Pages.Notify()
                    {
                        Message = "Successfully saved",
                        Color = UIData.GetColor(UIData.MessageColor.SUCCESS)
                    });
                }
            }
            catch(Exception e)
            {
                closeEditor();
            }
           
        }

        private void parseData()
        {
            SelectedLocation.Latitude = Convert.ToDouble(Latitude);
            SelectedLocation.Longitude = Convert.ToDouble(Longitude);
            SelectedLocation.Adress =  Addres;
        }
        
        private async void deleteLocation()
        {
            try
            {
                using(FogOilEntities db = new FogOilEntities())
                {
                    var item = await db.Locations.FindAsync(SelectedLocation.LocationId);
                    int res = item.UserProducts.Count;
                    
                    if (res != 0)
                        throw new Exception("Error: can't remove addres");
                    db.Locations.Remove(item);
                    await db.SaveChangesAsync();
                    loadLocation();
                    closeEditor();
                    DataBaseData.getInstance().CallNotify(new Data.Pages.Notify()
                    {
                        Message = "Deleted",
                        Color = UIData.GetColor(UIData.MessageColor.ERROR)
                    });
                }
            }
            catch(Exception e)
            {
                closeEditor();
                MessageBox.Show(e.Message);
            }
        }
        #endregion

        #region Commands
        public RelayCommand SelectLocation { get => new RelayCommand((obj)=> { 
            try
            {
                SelectedLocation = (obj as Location);
                
                Longitude = SelectedLocation.Longitude.ToString();
                Latitude = SelectedLocation.Latitude.ToString();
                Addres = SelectedLocation.Adress;

                openEditor();
            }
            catch(Exception e)
            {
                closeEditor();
            }
            
        }); }

        public CommandViewModel NewAddres { get => new CommandViewModel(()=> {
            SelectedLocation = new Location();
            Addres = "";
            Longitude = "";
            Latitude = "";
            openEditor();
        }); }

        private List<ButtonDescription> locationActions;
        public List<ButtonDescription> LocationActions
        {
            get => locationActions;
            set
            {
                locationActions = value;
                OnPropertyChanged("LocationActions");
            }
        }

        #endregion

        #region Async Queries
        private async void loadLocation()
        {
            try
            {
                using(FogOilEntities db = new FogOilEntities())
                {
                    await Task.Run(() =>
                    {
                        Locations = db.Locations.ToList();
                    });
                }
            }
            catch(Exception e)
            {

            }
        }
        #endregion

        public ViewModelLocation()
        {
            loadLocation();
            LocationActions = new List<ButtonDescription>()
            {
                new ButtonDescription(){ Text="Close", act=new CommandViewModel(closeEditor)},
                new ButtonDescription(){ Text="Save", act=new CommandViewModel(saveLocation)},
                new ButtonDescription(){ Text="Delete", act=new CommandViewModel(deleteLocation)}
            };
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
