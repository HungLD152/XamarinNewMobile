using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace SmartNews.Views
{
    public partial class SettingPage : ContentPage
    {
        Dictionary<string, FontAttributes> fontAttribute = new Dictionary<string, FontAttributes>
        {
            {"Normal", FontAttributes.None },
            {"Bold", FontAttributes.Bold },
            {"Italic", FontAttributes.Italic },
        };

        Dictionary<string, NamedSize> fontSize = new Dictionary<string, NamedSize>
        {
            {"default" , NamedSize.Default },
            {"Small" , NamedSize.Small },
            {"Medium" , NamedSize.Medium },
            {"Large" , NamedSize.Large },
        };

        Dictionary<string, NamedSize> nameToFont = new Dictionary<string, NamedSize>
        {
            {"default" , NamedSize.Default },
            {"Small" , NamedSize.Small },
            {"Medium" , NamedSize.Medium },
            {"Large" , NamedSize.Large },
        };
        public SettingPage()
        {
            InitializeComponent();
        }
    }
}
