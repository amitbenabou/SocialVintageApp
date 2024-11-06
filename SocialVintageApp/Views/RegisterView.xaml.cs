using SocialVintageApp.ViewModels; 

namespace SocialVintageApp.Views;



public partial class RegisterView : ContentPage
{
	public RegisterView(RegisterViewModel vm)
	{
		this.BindingContext = vm;

		InitializeComponent();
	}
}