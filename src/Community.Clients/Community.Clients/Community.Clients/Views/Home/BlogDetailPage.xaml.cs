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
        BlogDetailViewModel viewModel = new BlogDetailViewModel();

        public BlogDetailPage(ItemModel model)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = this.viewModel;
            viewModel.Initilize(model);
        }

    }
}