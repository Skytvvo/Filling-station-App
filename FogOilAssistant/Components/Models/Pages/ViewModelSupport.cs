using System.ComponentModel;
using System.Runtime.CompilerServices;
using FogOilAssistant.Components.Data;
using FogOilAssistant.Components.Data.Pages.Support;
using System.Windows;
using System.Windows.Controls;
using System.Net.Mail;
using System.Net;
using FogOilAssistant.Components.Data.GlobalStorage;
using System;
using FogOilAssistant.Components.Database;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using FogOilAssistant.Components.Data.UI;

namespace FogOilAssistant.Components.Models.Pages
{
    public class FBs : INotifyPropertyChanged
    {
        public Fuel FuelItem { get; set; }

        private string summary;
        public string Summary
        {
            get => summary;
            set
            {
                if(value.Length == 0)
                {
                    summary = "0";
                    OnPropertyChanged("Summary");
                    return;
                }
                if (Regex.IsMatch(value, @"^[0-9]*(?:\.[0-9]*)?$"))
                {
                    
                    if (Convert.ToDouble(value) <= 100)
                    {
                        summary = Convert.ToDouble(value).ToString();
                        OnPropertyChanged("Summary");
                    }
                }
            }

        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class ViewModelSupport : INotifyPropertyChanged
    {
        SupportItem supportItems;


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public SupportItem SupportItems { get => supportItems;
            set {
                this.supportItems = value;
                OnPropertyChanged("SupportItems");
            }
        }



        

        private List<FBs> fuel_list = new List<FBs>();
        public List<FBs> Fuel_list
        {
            get => fuel_list;
            set
            {
                fuel_list = value;
                OnPropertyChanged("Fuel_list");
            }
        }

        private string text;
        public string Text
        {
            get => text;
            set
            {
                text = value;
                OnPropertyChanged("Text");
            }
        }

        public ViewModelSupport()
        {
            this.SupportItems = new SupportItem() { Color = "#00E5FF", Link = "#", Name = "Feedback", Text = "Your feedback will be processe by our technical support." };

            List<FBs> newList = new List<FBs>();
            using (FogOilEntities db = new FogOilEntities())
            {
                if (db.Users.Find(DataBaseData.getInstance().UserId) != null)
                {
                    if(db.Users.Find(DataBaseData.getInstance().UserId).Root == 1)
                    foreach (var item in db.Fuels)
                    {
                        newList.Add(new FBs() { FuelItem = item, Summary = "0" });
                    }
                }
            }

            Fuel_list = newList;
            
        }

        public CommandViewModel SendFeedback { get => new CommandViewModel(sendFeedback); }
        public CommandViewModel ToggleButton { get => new CommandViewModel(toggleButton); }
        public string CollapsePanel { get => "/Components/Images/exp_btn.svg"; }


        public RelayCommand BuyFuel { get => new RelayCommand(buyFuel); }
        public async void buyFuel(object obj)
        {
            try
            {

                FBs current = (obj as FBs);
                using (FogOilEntities db = new FogOilEntities())
                {
                    double price = Convert.ToDouble(current.Summary) * current.FuelItem.Price;
                    double userBonus = (await db.Users.FindAsync(DataBaseData.getInstance().UserId)).Bonus;
                    if (userBonus < price)
                    {
                        DataBaseData.getInstance().CallNotify(new Data.Pages.Notify()
                        {
                            Message = "Not enought bonuses",
                            Color = UIData.GetColor(UIData.MessageColor.ERROR)
                        });
                    }
                    else
                    {
                        (await db.Users.FindAsync(DataBaseData.getInstance().UserId)).Bonus = userBonus - price;
                    await db.SaveChangesAsync();

                        DataBaseData.getInstance().CallNotify(new Data.Pages.Notify()
                        {
                            Message = "Success",
                            Color = UIData.GetColor(UIData.MessageColor.SUCCESS)
                        });
                    }
                    
                    if(fuel_list.Find(item => item.FuelItem.FuelId == current.FuelItem.FuelId)!= null)
                    fuel_list.Find(item => item.FuelItem.FuelId == current.FuelItem.FuelId).Summary = "0";

                   

                }
            }
            catch
            {

            }
        }

        public async void sendFeedback()
        {

            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(FogOilAssistant.MainWindow))
                {
                    var content = (window as FogOilAssistant.MainWindow).Frame.Content;
                    if (content.GetType() == typeof(FogOilAssistant.Components.View.Pages.Support))
                    {
                        var support_content = content as FogOilAssistant.Components.View.Pages.Support;
                        Feedback feedback = new Feedback();
                        foreach(var child in (support_content.choosen_radiobuttons as Grid).Children)
                        {
                            if(child.GetType() == typeof(RadioButton) && (bool)(child as RadioButton).IsChecked)
                            {
                                feedback.Header = (child as RadioButton).Content.ToString();
                            }
                        }
                        if (feedback.Header == null || support_content.feedback_message.Text.Length<10)
                            return;
                        feedback.Message = support_content.feedback_message.Text;

                        try
                        {
                            MailAddress from = new MailAddress("fogoilsender@mail.ru", DataBaseData.getInstance().Login);
                            MailAddress to = new MailAddress("fogoilsender@mail.ru");
                            MailMessage m = new MailMessage(from, to);
                            m.Subject = feedback.Header;
                            m.Body = text;
                            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 2525);
                            smtp.Credentials = new NetworkCredential("fogoilsender@mail.ru", "RrP_ttuAEp32");
                            smtp.EnableSsl = true;
                            await smtp.SendMailAsync(m);
                        }
                        catch(Exception e)
                        {

                        }
                    }
                }
            }
        }
        public void toggleButton()
        {
           
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(FogOilAssistant.MainWindow))
                {
                    var content = (window as FogOilAssistant.MainWindow).Frame.Content;
                    if (content.GetType() == typeof(FogOilAssistant.Components.View.Pages.Support))
                    {
                        var support_content = content as FogOilAssistant.Components.View.Pages.Support;
                        if (support_content.expanded__sub_panel.Visibility == Visibility.Collapsed)
                        {
                            support_content.expanded__sub_panel.Visibility = Visibility.Visible;
                        }
                        else
                           support_content.expanded__sub_panel.Visibility = Visibility.Collapsed;

                        support_content.collapsed_angle.Angle += 180;
                    }
                }
            }
        }
    }
}
