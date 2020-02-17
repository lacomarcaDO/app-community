using Community.Clients.Resources;
using Community.Utils.Interfaces;
using Xamarin.Forms;

namespace Community.Clients.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Accept => Resource.Accept;

        public static string Error => Resource.Error;

        public static string LaComarcaDo => Resource.LaComarcaDo;

        public static string JoinToCommunity => Resource.JoinToCommunity;

        public static string FollowUs => Resource.FollowUs;

        public static string VisitOurWeb => Resource.VisitOurWeb;

        public static string Main => Resource.Main;

        public static string Blog => Resource.Blog;

        public static string Events => Resource.Events;

        public static string Info => Resource.Info;

        public static string Loading => Resource.Loading;

        public static string CheckInternet => Resource.CheckInternet;

    }
}
