using System;
using System.Collections.Generic;
using SmartNews.ViewModels;
using Xamarin.Forms;

namespace SmartNews.Views
{
    public partial class RssDetailsPage : ContentPage
    {
        ItemDetailViewModel viewModel;
        public string Link { get; set; }
        public RssDetailsPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }
    }
}
