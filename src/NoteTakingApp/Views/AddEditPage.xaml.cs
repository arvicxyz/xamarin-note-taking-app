using NoteTakingApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteTakingApp.Views
{
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEditPage : ContentPage
    {
        private AddEditPageViewModel _viewModel;

        public AddEditPage()
        {
            InitializeComponent();
            Toolbar.Padding = new Thickness(0, App.StatusBarHeight, 0, 0);
            _viewModel = (AddEditPageViewModel)BindingContext;
        }

        protected override void OnAppearing()
        {
            _viewModel.Init();
        }
    }
}
