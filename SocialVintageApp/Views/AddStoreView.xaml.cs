using SocialVintageApp.ViewModels;

namespace SocialVintageApp.Views;

public partial class AddStoreView : ContentPage
{
	public AddStoreView(AddStoreViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}