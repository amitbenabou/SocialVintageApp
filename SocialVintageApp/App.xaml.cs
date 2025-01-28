using SocialVintageApp.Models;
using SocialVintageApp.Services;
using SocialVintageApp.Views;

namespace SocialVintageApp
{
    public partial class App : Application
    {
        public User? LoggedInUser { get; set; }
        public BasicData? BasicData { get; set; }
        private SocialVintageWebAPIProxy proxy;
        public App(IServiceProvider serviceProvider, SocialVintageWebAPIProxy proxy)
        {
            this.proxy = proxy;
            InitializeComponent();
            LoginView? v = serviceProvider.GetService<LoginView>();
            MainPage = new NavigationPage(v);
            ReadBasicDataFromServer();
            
        }

        private async void ReadBasicDataFromServer()
        {
            this.BasicData = await proxy.GetBasicDataAsync();
        }
    }
}
