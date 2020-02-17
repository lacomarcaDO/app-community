using Community.Clients.Models;
using Xamarin.Forms;

namespace Community.Clients
{

    public class ContentDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TextDataTemplate { get; set; }
        public DataTemplate ImageDataTemplate { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is ContentModel contentModel)
            {
                return contentModel.IsImage ? ImageDataTemplate : TextDataTemplate;
            }
            return TextDataTemplate;
        }
    }
}
