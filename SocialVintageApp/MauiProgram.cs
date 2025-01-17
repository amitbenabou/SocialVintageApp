﻿using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using SocialVintageApp.Services;
using SocialVintageApp.ViewModels;
using SocialVintageApp.Views;
using CommunityToolkit.Maui;

namespace SocialVintageApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Garet-Book.ttf", "GaretBook");
                    fonts.AddFont("Lostar.ttf", "Lostar");
                    fonts.AddFont("Narnia.ttf", "Narnia");
                    fonts.AddFont("Star Vintage - (Demo) hanscostudio.com.ttf", "StarVintage");
                })
                .RegisterDataServices()
                .RegisterPages()
                .RegisterViewModels();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
        public static MauiAppBuilder RegisterPages(this MauiAppBuilder builder)
        {

            builder.Services.AddTransient<AppShell>();
            builder.Services.AddTransient<LoginView>();
            builder.Services.AddTransient<RegisterView>();
            builder.Services.AddTransient<AddItemView>();
            builder.Services.AddTransient<AddStoreView>();
            builder.Services.AddTransient<AllStoresView>();
            builder.Services.AddTransient<HomePageView>();
            builder.Services.AddTransient<ShoppingBagView>();
            builder.Services.AddTransient<UserProfileView>();
            builder.Services.AddTransient<StoreProfileView>();

            return builder;
        }

        public static MauiAppBuilder RegisterDataServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<SocialVintageWebAPIProxy>();
            return builder;
        }
        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<ViewModelBase>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<AddItemViewModel>();
            builder.Services.AddTransient<AddStoreViewModel>();
            builder.Services.AddTransient<AllStoresViewModel>();
            builder.Services.AddTransient<HomePageViewModel>();
            builder.Services.AddTransient<ShoppingBagViewModel>();
            builder.Services.AddTransient<UserProfileViewModel>();
            builder.Services.AddTransient<AppShellViewModel>();
            builder.Services.AddTransient<StoreProfileViewModel>();
            return builder;
        }
    }
}
