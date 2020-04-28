using SmartNews.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SmartNews.Views
{
    public partial class SettingPage : ContentPage
    {
        public IList<string> ItemfontFamily = new List<string>() { "Arial", "Times New Roman", "UTM Avo", "UTM AvoBold", "UTM Beautiful Caps", "UTM Diana", "UTM Sarah" };
        public IList<int> ItemfontSize = new List<int>() { 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
        INotificationManager notificationManager;
        int notificationNumber = 0;
        public SettingPage()
        {
            InitializeComponent();
            foreach (var item in ItemfontSize)
            {
                fontsizePiker.Items.Add(item.ToString());
            }
            foreach (var item1 in ItemfontFamily)
            {
                fontNamePiker.Items.Add(item1);
            }
            UpdateStyleItem();
        }

        void OnScheduleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMessage.Text))
            {
                DependencyService.Get<INotificationManager>().ReceiveNotification("Local Notification", txtMessage.Text);
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Message null", "Message not null push notification", "Ok");
            }
        }

        #region Setting FontSize, Font Family
        public void UpdateStyleItem()
        {
            try
            {
                if (fontNamePiker.SelectedItem != null || fontsizePiker.SelectedItem != null || !checkFs.IsToggled)
                {
                    fontNamePiker.SelectedItem = Application.Current.Properties["Family"].ToString();
                    fontsizePiker.SelectedItem = Application.Current.Properties["Size"].ToString();
                    checkFs.IsToggled = Convert.ToBoolean(Application.Current.Properties["Mode"].ToString());
                }
                else
                {
                    fontNamePiker.SelectedItem = "Arial";
                    fontsizePiker.SelectedItem = "16";
                    checkFs.IsToggled = false;
                }
            }
            catch (Exception)
            {
                fontNamePiker.SelectedItem = "Arial";
                fontsizePiker.SelectedItem = "16";
                checkFs.IsToggled = false;
            }
        }

        void OnToggled(object sender, ToggledEventArgs e)
        {
            Application.Current.Properties["Mode"] = checkFs.IsToggled;
            if (e.Value)
            {
                Application.Current.Resources["backgroundColor"] = Color.FromHex("#333333");
                Application.Current.Resources["textColor"] = Color.FromHex("#FFFFFF");
            }
            else
            {
                Application.Current.Resources["backgroundColor"] = Color.FromHex("#FFFFFF");
                Application.Current.Resources["textColor"] = Color.FromHex("#333333");
            }
        }

        void fontFamily_SelectedIndexChanged(object sender, EventArgs args)
        {
            Application.Current.Resources["fontFamily"] = fontNamePiker.SelectedItem;
            Application.Current.Properties["Family"] = fontNamePiker.SelectedItem;
        }

        void fontsize_SelectedIndexChanged(object sender, EventArgs args)
        {
            Application.Current.Resources["InputFontsize"] = Convert.ToInt32(fontsizePiker.SelectedItem != null ? fontsizePiker.SelectedItem : 16);
            Application.Current.Properties["Size"] = Convert.ToInt32(fontsizePiker.SelectedItem != null ? fontsizePiker.SelectedItem : 16);
        }
        #endregion
    }
}
