using NoteTakingApp.ViewModels;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteTakingApp.Views
{
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();
            Toolbar.Padding = new Thickness(0, App.StatusBarHeight, 0, 0);
            _viewModel = (MainPageViewModel)BindingContext;
        }

        protected override async void OnAppearing()
        {
            await Task.Run(async () =>
            {
                await _viewModel.Init();
            });
        }
    }
}
