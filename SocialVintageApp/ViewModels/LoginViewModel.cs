﻿using Microsoft.Extensions.DependencyInjection;
using SocialVintageApp.Models;
using SocialVintageApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SocialVintageApp.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private SocialVintageWebAPIProxy proxy;
        private IServiceProvider serviceProvider;

        public LoginViewModel(SocialVintageWebAPIProxy proxy, IServiceProvider serviceProvider) 
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            this.LoginCommand = new Command(OnLogin);
            this.RegisterCommand = new Command(OnRegister);
        }

        private string email;
        private string password;

        public string Email
        {
            get => email;
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        public string Password
        {
            get => password;
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        private string errorMsg;
        public string ErrorMsg
        {
            get => errorMsg;
            set
            {
                if (errorMsg != value)
                {
                    errorMsg = value;
                    OnPropertyChanged(nameof(ErrorMsg));
                }
            }
        }

        private bool rememberMe;
        public bool RememberMe
        {
            get => rememberMe;
            set
            {
                if (rememberMe != value)
                {
                    rememberMe = value;
                    OnPropertyChanged(nameof(RememberMe));
                }
            }
        }


        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        private async void OnLogin()
        {
            //Choose the way you want to blobk the page while indicating a server call
            InServerCall = true;
            ErrorMsg = "";
            //Call the server to login
            Login loginInfo = new Login { UserMail = Email, Pswrd = Password };
            User? u = await this.proxy.LoginAsync(loginInfo);

            InServerCall = false;

            //Set the application logged in user to be whatever user returned (null or real user)
            ((App)Application.Current).LoggedInUser = u;
            if (u == null)
            {
                ErrorMsg = "Invalid email or password";
            }
            else
            {
                ErrorMsg = "";
                //Navigate to the main page
                AppShell shell = serviceProvider.GetService<AppShell>();
                ((App)Application.Current).MainPage = shell;
                
            }
        }


        private async void OnRegister()
        {

        }
    }
}