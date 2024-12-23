using SocialVintageApp.Models;
using SocialVintageApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialVintageApp.ViewModels
{
    public class AddStoreViewModel : ViewModelBase
    {
        private SocialVintageWebAPIProxy proxy;
        public AddStoreViewModel(SocialVintageWebAPIProxy proxy)
        {
            this.proxy = proxy;
            OpenStoreCommand = new Command(OnOpenStore);
            CancelCommand = new Command(OnCancel);
            UploadPhotoCommand = new Command(OnUploadPhoto);
            PhotoURL = proxy.GetDefaultProfilePhotoUrl();
            LocalPhotoPath = "";
            StoreNameError = "Name is required";
            AdressError = "Adress is required";
            
        }

        #region StoreName
        private bool showStoreNameError;

        public bool ShowStoreNameError
        {
            get => showStoreNameError;
            set
            {
                showStoreNameError = value;
                OnPropertyChanged("ShowStoreNameError");
            }
        }

        private string storeName;

        public string StoreName
        {
            get => storeName;
            set
            {
                storeName = value;
                ValidateStoreName();
                OnPropertyChanged("StoreName");
            }
        }

        private string storeNameError;

        public string StoreNameError
        {
            get => storeNameError;
            set
            {
                storeNameError = value;
                OnPropertyChanged("StoreNameError");
            }
        }

        private void ValidateStoreName()
        {
            this.ShowStoreNameError = string.IsNullOrEmpty(StoreName);
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

        #region deliveryOption

        private int option;

        public int Option
        {
            get => option;
            set
            {
                option = value;
                OnPropertyChanged("Option");
            }
        }

        #endregion

        #region clothesCatagory

        private int catagory;

        public int Catagory
        {
            get => catagory;
            set
            {
                catagory = value;
                OnPropertyChanged("Catagory");
            }
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

        //Define a command for the register button
        public Command OpenStoreCommand { get; }
        public Command CancelCommand { get; }

        //Define a method that will be called when the register button is clicked
        public async void OnOpenStore()
        {
            ValidateStoreName();
            ValidateAdress();

            if (!ShowStoreNameError && !ShowAdressError)
            {
                //Create a new AppUser object with the data from the registration form
                var newStore = new Store
                {
                    StoreId = ((App)Application.Current).LoggedInUser.UserId,
                    StoreName = StoreName,
                    Adress = Adress,
                    OptionId = Option,
                    CatagoryId = Catagory
                    
                };

                //Call the Register method on the proxy to register the new user
                InServerCall = true;
                newStore = await proxy.OpenStore(newStore);
                InServerCall = false;

                //If the registration was successful, navigate to the login page
                if (newStore != null)
                {
                    ((App)Application.Current).LoggedInUser.HasStore = true;
                    OnPropertyChanged();
                    //UPload profile imae if needed
                    if (!string.IsNullOrEmpty(LocalPhotoPath))
                    {
                        Store? updatedUser = await proxy.UploadStoreLogo(LocalPhotoPath);
                        if (updatedUser == null)
                        {
                            InServerCall = false;
                            await Application.Current.MainPage.DisplayAlert("Opening store", "Store Data Was Saved BUT Profile image upload failed", "ok");
                        }
                    }
                    InServerCall = false;

                    ((App)(Application.Current)).MainPage.Navigation.PopAsync();
                }
                else
                {

                    //If the registration failed, display an error message
                    string errorMsg = "Opening store failed. Please try again.";
                    await Application.Current.MainPage.DisplayAlert("Open store", errorMsg, "ok");
                }
            }
        }

        //Define a method that will be called upon pressing the cancel button
        public void OnCancel()
        {
            //Navigate back to the login page
            ((App)(Application.Current)).MainPage.Navigation.PopAsync();
        }
    }

}
