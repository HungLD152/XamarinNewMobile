using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SmartNews.ViewModels;
using Xamarin.Forms;

namespace SmartNews.Views
{
    public partial class ControlTabBar : ScrollView
    {
        public string TitleBar { get; set; }
        public string Parameter { get; set; }
        private TabBarItemModel selectedItem;
        public TabBarItemModel SelectedItem { get { return selectedItem; } set { selectedItem = value; OnPropertyChanged("SelectedItem"); } }
        public event EventHandler<string> OnTabBarClicked;
        public static BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IList<TabBarItemModel>), typeof(ControlTabBar), null, BindingMode.TwoWay);
        public static BindableProperty ItemsSelectedProperty = BindableProperty.Create(nameof(ItemSelected), typeof(object), typeof(ControlTabBar), null, BindingMode.TwoWay);
        public IList<TabBarItemModel> ItemsSource
        {
            get { return (IList<TabBarItemModel>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        public object ItemSelected
        {
            get { return GetValue(ItemsSelectedProperty); }
            set { SetValue(ItemsSelectedProperty, value); }
        }

        public ControlTabBar()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == ItemsSourceProperty.PropertyName)
            {
                Container.Children.Clear();
                if (ItemsSource?.Count > 0)
                    foreach (var data in ItemsSource)
                    {
                        var item = new TabItem { BindingContext = data };
                        item.OnTabItemClicked += Item_OnTabItemClicked;
                        Container.Children.Add(item);
                    }
            }
            if (propertyName == ItemsSelectedProperty.PropertyName)
            {

            }
        }

        private void Item_OnTabItemClicked(object sender, string e)
        {
            var senderObj = (TabItem)sender;
            foreach (var item in Container.Children)
            {
                (item.BindingContext as TabBarItemModel).IsSelected = false;
                item.Margin = new Thickness(0, 10, 0, 0);
            }
            (senderObj.BindingContext as TabBarItemModel).IsSelected = true;
            if ((senderObj.BindingContext as TabBarItemModel).IsSelected)
            {
                senderObj.Margin = new Thickness(0, 7, 0, 1);
                senderObj.Padding = new Thickness(0, 0, 0, 1);
                senderObj.HeightRequest = 35;
                BottomColor.BackgroundColor = (senderObj.BindingContext as TabBarItemModel).ItemColor;
                BottomColor.Margin = new Thickness(0, -13, 0, 0);
            }
            OnTabBarClicked?.Invoke(this, e);
        }
    }
    public class TabBarItemModel
    {
        public string TitleBar { get; set; }
        public string Url { get; set; }
        public Color ItemColor { get; set; }
        public bool IsSelected { get; set; }
    }
}
