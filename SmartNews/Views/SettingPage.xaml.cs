using System;
using System.Collections.Generic;
using SmartNews.ViewModels;
using Xamarin.Forms;

namespace SmartNews.Views
{
    public partial class SettingPage : ContentPage
    {
        public IList<string> ItemfontFamily = new List<string>() { "Arial", "Times New Roman", "UTM Avo Regular", "UTM AvoBold Regular", "UTM Beautiful Caps Regular", "UTM Diana Regular", "UTM Sarah Regular" };
        public IList<int> ItemfontSize = new List<int>() { 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
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
        }

        void OnToggled(object sender, ToggledEventArgs e)
        {
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
        }
        void fontsize_SelectedIndexChanged(object sender, EventArgs args)
        {
            Application.Current.Resources["InputFontsize"] = Convert.ToInt32(fontsizePiker.SelectedItem != null ? fontsizePiker.SelectedItem : 16);
        }
    }
}
