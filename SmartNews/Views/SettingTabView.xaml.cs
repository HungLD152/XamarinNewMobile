using SmartNews.ViewModels;
using Syncfusion.DataSource.Extensions;
using Syncfusion.ListView.XForms;
using Syncfusion.ListView.XForms.Control.Helpers;
using System;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartNews.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingTabView : ContentPage
    {
        private RssItemViewModel viewModel = new RssItemViewModel();
        VisualContainer container;
        ExtendedScrollView scrollview;
        double scrollOffet;
        double previousOffset;

        public SettingTabView()
        {
            InitializeComponent();
            BindingContext = viewModel;
            UpdateSettingItem();
            viewModel.heightImages = 150;
            this.listView.DragDropController.UpdateSource = true;
            container = listView.GetVisualContainer();
            scrollview = listView.GetScrollView();
            scrollview.Scrolled += Scrollview_Scrolled;
        }

        private void Scrollview_Scrolled(object sender, ScrolledEventArgs e)
        {
            scrollOffet = (double)container.GetType().GetRuntimeProperties().First(p => p.Name == "ScrollOffset").GetValue(container);
            if (e.ScrollY == 0)
                return;
            if (previousOffset >= e.ScrollY)
            {
                // Up direction  
                viewModel.heightImages = 150 - e.ScrollY;
            }
            else
            {
                //Down direction 
                viewModel.heightImages = 150 - e.ScrollY;
            }
            previousOffset = e.ScrollY;
        }

        private void ListView_ItemDragging(object sender, ItemDraggingEventArgs e)
        {
            if (e.Action == DragAction.Drop)
            {
                viewModel.ItemTabBar.MoveTo(1, 5);
            }
        }

        public void UpdateSettingItem()
        {
            if (Application.Current.Properties.ContainsKey("TabItem"))
            {
                ShowTabItem.IsToggled = Convert.ToBoolean(Application.Current.Properties["TabItem"].ToString());
            }
        }

        void OnToggled(object sender, ToggledEventArgs e)
        {
            Application.Current.Properties["TabItem"] = ShowTabItem.IsToggled;
            Application.Current.SavePropertiesAsync();
        }
    }
}