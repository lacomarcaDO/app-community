using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.Clients.Models;
using Community.Clients.ViewModels.Home;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Community.Clients.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BlogDetailPage : ContentPage
    {
        BlogDetailViewModel viewModel = new BlogDetailViewModel();//=> App.Locator.DetailPageViewModel;

        public BlogDetailPage(ItemModel model)//, BlogDetailViewModel viewModel)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = this.viewModel;//= viewModel;
            viewModel.Initilize(model);
        }

    }
}