using SocialVintageApp.ViewModels;

namespace SocialVintageApp.Views;

public partial class UserProfileView : ContentPage
{
	public UserProfileView(UserProfileViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}