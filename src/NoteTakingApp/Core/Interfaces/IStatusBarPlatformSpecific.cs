using Xamarin.Forms;

namespace NoteTakingApp.Core
{
    public interface IStatusBarPlatformSpecific
    {
        void SetStatusBarColor(Color color, bool isLightTheme);
    }
}
