using Xamarin.Essentials;
using Xamarin.Forms;

namespace NoteTakingApp.Constants
{
    public static class AppGlobal
    {
        public static string AppName
        {
            get { return (string)App.Current.Resources["AppNameString"]; }
        }

        public static string FavouritesText
        {
            get { return (string)App.Current.Resources["FavouritesText"]; }
        }

        public static Color ToolbarColor
        {
            get { return (Color)App.Current.Resources["PrimaryColor"]; }
        }

        public static Color SearchBarColor
        {
            get { return (Color)App.Current.Resources["MainLightBgColor"]; }
        }

        public static bool IsOnline
        {
            get { return Connectivity.NetworkAccess == NetworkAccess.Internet; }
        }
    }
}
