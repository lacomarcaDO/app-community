using Community.Clients.Models;
using Community.Clients.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Community.Clients.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BlogPage : ContentPage
    {
        BlogViewModel viewModel = new BlogViewModel();

        public BlogPage()
        {
            InitializeComponent();
            BindingContext = viewModel;
            viewModel.Initlize();
            //  NavigationPage.SetHasNavigationBar(this, false);
            //MyLabel.Text = " <b>&nbsp;No te olvides de comentar nuevamente y compartirlo con tus amigos desafiandolos con el HashTag #JsMentales</b>";
            //MyLabel.TextType = TextType.Html;
        }
        public ItemModel itemModel;
        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            if (sender is Grid label)
            {
                if (label.BindingContext is ItemModel itemModel)
                {
                    viewModel.ListItemTapped(itemModel);
                }
            }
        }
    }
}