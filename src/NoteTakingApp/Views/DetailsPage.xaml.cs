using NoteTakingApp.ViewModels;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteTakingApp.Views
{
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : ContentPage
    {
        private DetailsPageViewModel _viewModel;

        public DetailsPage()
        {
            InitializeComponent();
            Toolbar.Padding = new Thickness(0, App.StatusBarHeight, 0, 0);
            _viewModel = (DetailsPageViewModel)BindingContext;
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
