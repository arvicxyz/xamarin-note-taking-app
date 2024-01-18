using Xamarin.Forms;

namespace NoteTakingApp.ViewModels
{
    public interface IBaseViewModel
    {
        Page Page { get; set; }

        INavigation Navigation { get; set; }
    }
}
