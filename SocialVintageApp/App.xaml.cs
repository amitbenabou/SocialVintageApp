using SocialVintageApp.Models;

namespace SocialVintageApp
{
    public partial class App : Application
    {
        public User? LoggedInUser { get; set; }
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
