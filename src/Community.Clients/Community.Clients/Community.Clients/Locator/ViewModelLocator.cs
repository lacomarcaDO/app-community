using Community.Clients.ViewModels;
using Community.Clients.ViewModels.Home;

namespace Community.Clients.Locator
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            //  ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            //SimpleIoc.Default.Register<BlogViewModel>();
            //    SimpleIoc.Default.Register<BlogDetailViewModel>();
        }
        public BlogViewModel BlogViewModel => new BlogViewModel();
        public BlogDetailViewModel BlogDetailViewModel => new BlogDetailViewModel();
        //public BlogViewModel BlogViewModel => ServiceLocator.Current.GetInstance<BlogViewModel>();
        //public BlogDetailViewModel BlogDetailViewModel => ServiceLocator.Current.GetInstance<BlogDetailViewModel>();
    }
}
