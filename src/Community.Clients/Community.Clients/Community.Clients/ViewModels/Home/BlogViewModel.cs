using Community.Clients.Models;
using Community.Clients.Views;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Community.Clients.Helpers;
using Community.Clients.Services;
using Xamarin.Forms.Internals;
using XF.Material.Forms.UI.Dialogs;

namespace Community.Clients.ViewModels
{
    public class BlogViewModel : BaseViewModel
    {
        private ObservableCollection<ItemModel> _items;
        public ObservableCollection<ItemModel> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        private ItemModel _selectedItem;

        public ItemModel SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        private async Task<List<ItemModel>> GetBloggerListAsync(string url)
        {
            var list = new List<ItemModel>();

            var httpclient = new HttpClient();
            var html = await httpclient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var htmlContent = htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("id", "")
                    .Equals("main")).ToList();

            var blogsList = htmlContent[0].Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                    .Equals("post hentry")).ToList();

            Console.WriteLine($@"total  items {blogsList.Count()}");
            Console.WriteLine();

            foreach (var productListItem in blogsList)
            {
                var itemModel = new ItemModel();

                var textNodeList = productListItem.Descendants("h3").ToList().Where(node => node.GetAttributeValue("class", "")
                     .Equals("post-title")).ToList();

                var imgNodeList = productListItem.Descendants("img").ToList();

                var contentNodeList = productListItem.Descendants("div").ToList().Where(node => node.GetAttributeValue("class", "")
                     .Equals("post-body entry-content")).ToList();

                if (textNodeList.Any())
                {
                    var textNode = textNodeList[0];
                    var url1 = textNode.Descendants("a").ToList()[0].Attributes["href"].Value; // [0].Attributes["href"].Value;
                    var title = textNode.Descendants("a").ToList()[0].Attributes["title"].Value;
                    Console.WriteLine(url1);
                    Console.WriteLine(title);
                    itemModel.PostTitle = title;
                    itemModel.Url = url1;
                }

                if (imgNodeList.Any())
                {
                    var imgNode = imgNodeList[0];
                    var img = imgNode.Attributes["src"].Value;
                    Console.WriteLine(img);
                    itemModel.Image = img;
                }

                if (contentNodeList.Any())
                {
                    var contentNode = contentNodeList[0];
                    var content = contentNode.Descendants("div").ToList()[0].FirstChild.InnerText.Trim('\r', '\n', '\t');
                    itemModel.Content = content;
                    Console.WriteLine(content);
                }

                var urlList = url.Split('.');
                var lastItem = urlList.LastOrDefault();
                var secondLastItem = urlList[urlList.Length - 2];

                itemModel.From = secondLastItem + "." + lastItem;
                itemModel.IsBlogger = true;
                Console.WriteLine(blogsList.IndexOf(productListItem));
                Console.WriteLine();

                list.Add(itemModel);
            }

            return list;
        }

        private async Task<List<ItemModel>> GetWordpressListAsync(string url)
        {
            var list = new List<ItemModel>();

            //var url = "http://notasti.com/";
            var httpclient = new HttpClient();
            var html = await httpclient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var htmlContent = htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("id", "")
                    .Equals("content") && node.GetAttributeValue("role", "").Equals("main")).ToList();

            var blogsList = htmlContent[0].Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                    .Equals("article-wrapper list-style clearfix")).ToList();

            Console.WriteLine(@"total  items  " + blogsList.Count());
            Console.WriteLine();
            foreach (var productListItem in blogsList)
            {
                var itemModel = new ItemModel();

                var textNodeList = productListItem.Descendants("h1").ToList().Where(node => node.GetAttributeValue("class", "")
                     .Equals("entry-title")).ToList();

                var imgNodeList = productListItem.Descendants("img").Where(node => node.GetAttributeValue("class", "")
                .Equals("img-responsive")).ToList();

                var dateNodeList = productListItem.Descendants("div").ToList().Where(s => s.GetAttributeValue("class", "").Equals("entry-date")).ToList();

                var contentNodeList = productListItem.Descendants("p").ToList();//.Where(s => s.GetAttributeValue("class", "").Equals("entry-date")).ToList();

                if (textNodeList.Any())
                {
                    var textNode = textNodeList[0];
                    var url1 = textNode.Descendants("a").ToList()[0].Attributes["href"].Value;
                    var title = textNode.InnerText.Trim('\r', '\n', '\t');
                    Console.WriteLine(url1);
                    Console.WriteLine(title);
                    itemModel.PostTitle = title;
                    itemModel.Url = url1;
                }
                if (imgNodeList.Any())
                {
                    var imgNode = imgNodeList[0];
                    var img = imgNode.Attributes["src"].Value;
                    Console.WriteLine(img);
                    itemModel.Image = img;
                }
                if (dateNodeList.Any())
                {
                    var dateNode = dateNodeList[0];
                    var dateDay = dateNode.Descendants("span").ToList().Where(s => s.GetAttributeValue("class", "").Equals("date")).ToList()[0].InnerText.Trim('\r', '\n', '\t');
                    var monthYear = dateNode.Descendants("span").ToList().Where(s => s.GetAttributeValue("class", "").Equals("month-year")).ToList()[0].InnerText.Trim('\r', '\n', '\t');
                    var completeDate = dateDay + "/" + monthYear.Split(',')[0] + "/" + monthYear.Split(',')[1];
                    Console.WriteLine(completeDate);
                    itemModel.Date = completeDate;
                }
                if (contentNodeList.Count >= 2)
                {
                    var contentNode = contentNodeList[1];
                    var content = contentNode.InnerText.Trim('\r', '\n', '\t');
                    Console.WriteLine(content);
                    itemModel.Content = content;
                }

                var urlList = url.Split('.');
                var lastItem = urlList.LastOrDefault();
                var secondLastItem = urlList[urlList.Length - 2];

                itemModel.From = secondLastItem + "." + lastItem;
                itemModel.IsBlogger = false;

                Console.WriteLine(blogsList.IndexOf(productListItem));
                Console.WriteLine();

                list.Add(itemModel);
            }
            return list;
        }

        public BlogViewModel()
        {
        }

        internal async void Initlize()
        {
            var _apiService = new ApiService();
            var url = "google.com";// App.Current.Resources["UrlApi"].ToString();
            var connection = await _apiService.CheckConnection(url);
            if (!connection)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.CheckInternet, Languages.Accept);
                return;
            }

            var loader = await MaterialDialog.Instance.LoadingDialogAsync(Languages.Loading);
            string jsonFileName = "blogsList.json";
            var assembly = typeof(BlogPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{jsonFileName}");
            using (var reader = new System.IO.StreamReader(stream ?? throw new InvalidOperationException()))
            {
                var jsonString = reader.ReadToEnd();
                var jsonList = JsonConvert.DeserializeObject<ObservableCollection<JsonModel>>(jsonString);

                Items = new ObservableCollection<ItemModel>();
                foreach (var item in jsonList)
                {
                    if (item.Type == "blogger")
                    {
                        var list = await GetBloggerListAsync(item.Page);
                        var itemsnewList = list.Take(item.PostLimit);
                        itemsnewList.ForEach(s => Items.Add(s));
                    }
                    else if (item.Type == "wordpress")
                    {
                        var list = await GetWordpressListAsync(item.Page);
                        var itemsnewList = list.Take(item.PostLimit);
                        itemsnewList.ForEach(s => Items.Add(s));
                    }
                }

            }

            var itemsnewListx = Items.OrderBy(p=>p.PostTitle);
            Items = new ObservableCollection<ItemModel>();
            itemsnewListx.ForEach(s => Items.Add(s));
          
            await loader.DismissAsync();
        }

        internal void ListItemTapped(ItemModel itemModel)
        {
            App.Current.MainPage.Navigation.PushAsync(new BlogDetailPage(itemModel));
        }
    }
}
