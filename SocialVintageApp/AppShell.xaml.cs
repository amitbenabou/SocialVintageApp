using SocialVintageApp.ViewModels;

namespace SocialVintageApp
{
    public partial class AppShell : Shell
    {
        public AppShell(AppShellViewModel vm)
        {
            this.BindingContext = vm;
            InitializeComponent();
        }
    }
}
