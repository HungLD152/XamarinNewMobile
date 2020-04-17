using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace SmartNews.Views
{
    public partial class TabItem : StackLayout
    {
        public event EventHandler<string> OnTabItemClicked;

        public TabItem()
        {
            InitializeComponent();
        }

        private void OnContentTapped(object sender, EventArgs e)
        {
            OnTabItemClicked?.Invoke(this, (BindingContext as TabBarItemModel).Url);
        }
    }
}
