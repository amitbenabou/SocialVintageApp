using SocialVintageApp.Models;
using SocialVintageApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Windows.Networking;
//using Windows.Networking;

namespace SocialVintageApp.ViewModels
{
    public class UserProfileViewModel : ViewModelBase
    {
        private SocialVintageWebAPIProxy proxy;
        public UserProfileViewModel(SocialVintageWebAPIProxy proxy)
        {
            this.proxy = proxy;
            this.SaveCommand = new Command(OnSave);
            ShowPasswordCommand = new Command(OnShowPassword);
            UploadPhotoCommand = new Command(OnUploadPhoto);
            PhotoURL = proxy.GetDefaultProfilePhotoUrl();
            LocalPhotoPath = "";
            IsPassword = true;
            NameError = "Name is required";
            AdressError = "Adress is required";
            EmailError = "Email is required";
            PasswordError = "Password must be at least 4 characters long and contain letters and numbers";
            User u = ((App)Application.Current).LoggedInUser;
            Name = u.UserName;
            Adress = u.UserAdress;
            Email = u.UserMail;
            Password = u.Pswrd;
            PhotoURL = proxy.GetImagesBaseAddress() + u.ProfileImagePath;
        }

        #region Name
        private bool showNameError;

        public bool ShowNameError
        {
            get => showNameError;
            set
            {
                showNameError = value;
                OnPropertyChanged("ShowNameError");
            }
        }

        private string name;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                ValidateName();
                OnPropertyChanged("Name");
            }
        }

        private string nameError;

        public string NameError
        {
            get => nameError;
            set
            {
                nameError = value;
                OnPropertyChanged("NameError");
            }
        }

        private void ValidateName()
        {
            this.ShowNameError = string.IsNullOrEmpty(Name);
        }
        #endregion


        #region Email
        private bool showEmailError;

        public bool ShowEmailError
        {
            get => showEmailError;
            set
            {
                showEmailError = value;
                OnPropertyChanged("ShowEmailError");
            }
        }

        private string email;

        public string Email
        {
            get => email;
            set
            {
                email = value;
                ValidateEmail();
                OnPropertyChanged("Email");
            }
        }

        private string emailError;

        public string EmailError
        {
            get => emailError;
            set
            {
                emailError = value;
                OnPropertyChanged("EmailError");
            }
        }

        private void ValidateEmail()
        {
            this.ShowEmailError = string.IsNullOrEmpty(Email);
            if (!ShowEmailError)
            {
                //check if email is in the correct format using regular expression
                if (!System.Text.RegularExpressions.Regex.IsMatch(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                {
                    EmailError = "Email is not valid";
                    ShowEmailError = true;
                }
                else
                {
                    EmailError = "";
                    ShowEmailError = false;
                }
            }
            else
            {
                EmailError = "Email is required";
            }
        }
        #endregion

        #region Password
        private bool showPasswordError;

        public bool ShowPasswordError
        {
            get => showPasswordError;
            set
            {
                showPasswordError = value;
                OnPropertyChanged("ShowPasswordError");
            }
        }

        private string password;

        public string Password
        {
            get => password;
            set
            {
                password = value;
                ValidatePassword();
                OnPropertyChanged("Password");
            }
        }

        private string passwordError;

        public string PasswordError
        {
            get => passwordError;
            set
            {
                passwordError = value;
                OnPropertyChanged("PasswordError");
            }
        }

        private void ValidatePassword()
        {
            //Password must include characters and numbers and be longer than 4 characters
            if (string.IsNullOrEmpty(password) ||
                password.Length < 4 ||
                !password.Any(char.IsDigit) ||
                !password.Any(char.IsLetter))
            {
                this.ShowPasswordError = true;
            }
            else
                this.ShowPasswordError = false;
        }

        //This property will indicate if the password entry is a password
        private bool isPassword = true;
        public bool IsPassword
        {
            get => isPassword;
            set
            {
                isPassword = value;
                OnPropertyChanged("IsPassword");
            }
        }
        //This command will trigger on pressing the password eye icon
        public Command ShowPasswordCommand { get; }
        //This method will be called when the password eye icon is pressed
        public void OnShowPassword()
        {
            //Toggle the password visibility
            IsPassword = !IsPassword;
        }
        #endregion


        #region Adress
        private bool showAdressError;

        public bool ShowAdressError
        {
            get => showAdressError;
            set
            {
                showAdressError = value;
                OnPropertyChanged("ShowAdressError");
            }
        }

        private string adress;

        public string Adress
        {
            get => adress;
            set
            {
                adress = value;
                ValidateAdress();
                OnPropertyChanged("Adress");
            }
        }

        private string adressError;

        public string AdressError
        {
            get => adressError;
            set
            {
                adressError = value;
                OnPropertyChanged("AdressError");
            }
        }

        private void ValidateAdress()
        {
            this.ShowAdressError = string.IsNullOrEmpty(Adress);
        }

        #endregion

        #region Photo

        private string photoURL;

        public string PhotoURL
        {
            get => photoURL;
            set
            {
                photoURL = value;
                OnPropertyChanged("PhotoURL");
            }
        }

        private string localPhotoPath;

        public string LocalPhotoPath
        {
            get => localPhotoPath;
            set
            {
                localPhotoPath = value;
                OnPropertyChanged("LocalPhotoPath");
            }
        }

        public Command UploadPhotoCommand { get; }
        //This method open the file picker to select a photo
        private async void OnUploadPhoto()
        {
            try
            {
                var result = await MediaPicker.Default.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = "Please select a photo",
                });

                if (result != null)
                {
                    // The user picked a file
                    this.LocalPhotoPath = result.FullPath;
                    this.PhotoURL = result.FullPath;
                }
            }
            catch (Exception ex)
            {
            }

        }

        private void UpdatePhotoURL(string virtualPath)
        {
            Random r = new Random();
            PhotoURL = proxy.GetImagesBaseAddress() + virtualPath + "?v=" + r.Next();
            LocalPhotoPath = "";
        }

        #endregion

        public Command SaveCommand { get; }
        public async void OnSave()
        {
            ValidateName();
            ValidateAdress();
            ValidateEmail();
            ValidatePassword();


            if (!ShowNameError && !ShowAdressError && !ShowEmailError && !ShowPasswordError)
            {

                //Update AppUser object with the data from the Edit form
                User theUser = ((App)App.Current).LoggedInUser;
                theUser.UserName = Name;
                theUser.UserAdress = Adress;
                theUser.UserMail = Email;
                theUser.Pswrd = Password;


                //Call the Register method on the proxy to register the new user
                InServerCall = true;
                bool success = await proxy.UpdateProfile(theUser);


                //If the save was successful, navigate to the login page
                if (success)
                {
                    //UPload profile imae if needed
                    if (!string.IsNullOrEmpty(LocalPhotoPath))
                    {
                        User? updatedUser = await proxy.UploadProfileImage(LocalPhotoPath);
                        if (updatedUser == null)
                        {
                            await Shell.Current.DisplayAlert("Save Profile", "User Data Was Saved BUT Profile image upload failed", "ok");
                        }
                        else
                        {
                            theUser.ProfileImagePath = updatedUser.ProfileImagePath;
                            UpdatePhotoURL(theUser.ProfileImagePath);
                        }

                    }
                    InServerCall = false;
                    await Shell.Current.DisplayAlert("Save Profile", "Profile saved successfully", "ok");
                }
                else
                {
                    InServerCall = false;
                    //If the registration failed, display an error message
                    string errorMsg = "Save Profile failed. Please try again.";
                    await Shell.Current.DisplayAlert("Save Profile", errorMsg, "ok");
                }
            }
        }
    }
}

