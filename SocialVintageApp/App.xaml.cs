﻿using SocialVintageApp.Models;
using SocialVintageApp.Views;

namespace SocialVintageApp
{
    public partial class App : Application
    {
        public User? LoggedInUser { get; set; }
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            LoginView? v = serviceProvider.GetService<LoginView>();
            MainPage = new NavigationPage(v);

            
        }
    }
}
