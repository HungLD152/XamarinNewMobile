using SmartNews.ViewModels;
using Syncfusion.DataSource.Extensions;
using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartNews.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingTabView : ContentPage
    {
        private RssItemViewModel viewModel = new RssItemViewModel();
        public SettingTabView()
        {
            InitializeComponent();
            BindingContext = viewModel;
            this.listView.DragDropController.UpdateSource = true;
            UpdateSettingItem();
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