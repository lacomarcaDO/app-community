using Community.Clients.ViewModels;
using Xamarin.Forms;

namespace Community.Clients.Models
{
    public class ContentModel : BaseViewModel
    {
        public string Text { get; set; }
        public bool IsImage { get; set; }

        private FontAttributes _fontAttributes = FontAttributes.None;

        public FontAttributes FontAttributes
        {
            get => _fontAttributes;
            set => SetProperty(ref _fontAttributes, value);
        }
    }
}
