using DryIoc;
using NoteTakingApp.Constants;
using NoteTakingApp.Core;
using NoteTakingApp.Localization;
using NoteTakingApp.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace NoteTakingApp
{
    public partial class App : Xamarin.Forms.Application
    {
        public static float ScreenWidth { get; set; }
        public static float ScreenHeight { get; set; }
        public static double StatusBarHeight { get; set; }

        public static Action ExitApplication;

        public static Rules DefaultRules => Rules.Default.WithConcreteTypeDynamicRegistrations(reuse: Reuse.Transient)
                                                         .With(Made.Of(FactoryMethod.ConstructorWithResolvableArguments))
                                                         .WithFuncAndLazyWithoutRegistration()
                                                         .WithTrackingDisposableTransients()
                                                         .WithFactorySelector(Rules.SelectLastRegisteredFactory());

        public App()
        {
            Xamarin.Forms.Application.Current
                .On<Xamarin.Forms.PlatformConfiguration.Android>()
                .UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            InitializeComponent();

            var config = Rules.Default;
            if (Device.RuntimePlatform == Device.iOS)
                config = DefaultRules.WithUseInterpretation();

            IContainer Container = new Container(config);
            IocManager.RegisterDependencies(Container);

            OnInitialized();
        }

        protected override async void OnStart()
        {
            base.OnStart();

            SetInitializeScreen();

            // TODO: Put anything that needs to load for iOS and Android here
            await Task.Delay(AppConstants.InitialAppLoading);

            var page = new MainPage();
            MainPage = new NavigationPage(page)
            {
                BarTextColor = Color.White,
                BarBackgroundColor = AppGlobal.ToolbarColor
            };

            Device.BeginInvokeOnMainThread(() =>
            {
                var color = AppGlobal.ToolbarColor;
                var statusbar = DependencyService.Get<IStatusBarPlatformSpecific>();
                statusbar.SetStatusBarColor(color, false);
            });

            OnResume();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private void OnInitialized()
        {
            Xamarin.Forms.Application.Current.UserAppTheme = OSAppTheme.Light;
            SetStatusBarHeight();
        }

        private void SetStatusBarHeight()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                if (StatusBarHeight <= 0)
                {
                    if (Device.Idiom == TargetIdiom.Tablet)
                    {
                        // iPad
                        StatusBarHeight = 24;
                    }
                    else if (Device.Idiom == TargetIdiom.Phone)
                    {
                        // iPhone
                        if (DeviceInfo.Model == "iPhone 14" ||
                            DeviceInfo.Model == "iPhone 14 Pro" ||
                            DeviceInfo.Model == "iPhone 14 Pro Max" ||
                            DeviceInfo.Model == "iPhone 13" ||
                            DeviceInfo.Model == "iPhone 13 Pro" ||
                            DeviceInfo.Model == "iPhone 13 Pro Max" ||
                            DeviceInfo.Model == "iPhone 12" ||
                            DeviceInfo.Model == "iPhone 12 Pro" ||
                            DeviceInfo.Model == "iPhone 12 Pro Max" ||
                            DeviceInfo.Model == "iPhone 11" ||
                            DeviceInfo.Model == "iPhone 11 Pro" ||
                            DeviceInfo.Model == "iPhone 11 Pro Max" ||
                            DeviceInfo.Model == "iPhone X" ||
                            DeviceInfo.Model == "iPhone XR" ||
                            DeviceInfo.Model == "iPhone XS" ||
                            DeviceInfo.Model == "iPhone XS Max")
                        {
                            StatusBarHeight = 44;
                        }
                        else
                        {
                            StatusBarHeight = 20;
                        }
                    }
                }
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                if (Device.Idiom == TargetIdiom.Tablet)
                {
                    // Android Tablet
                    StatusBarHeight = 0;
                }
                else if (Device.Idiom == TargetIdiom.Phone)
                {
                    // Android Phone
                    StatusBarHeight = 0;
                }
            }
        }

        private void SetInitializeScreen()
        {
            var loadingSize = (OnPlatform<double>)Current.Resources["ActivityIndicatorSize"];
            var loadingScale = (OnPlatform<double>)Current.Resources["ActivityIndicatorScale"];
            var fontFamily = (OnPlatform<string>)Current.Resources["RegularFontFamily"];
            var fontSize = (OnPlatform<double>)Current.Resources["TitleFontSize"];
            var loadingColor = (Color)Current.Resources["PrimaryTextColor"];
            var backgroundColor = Color.White;

            Device.BeginInvokeOnMainThread(() =>
            {
                var statusbar = DependencyService.Get<IStatusBarPlatformSpecific>();
                statusbar.SetStatusBarColor(backgroundColor, true);
            });
            MainPage = new ContentPage
            {
                BackgroundColor = backgroundColor,
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    Children =
                    {
                        new ActivityIndicator
                        {
                            WidthRequest = loadingSize,
                            HeightRequest = loadingSize,
                            Scale = loadingScale,
                            Color = loadingColor,
                            IsRunning = true
                        },
                        new Label
                        {
                            Text = "initializing".Translate(),
                            VerticalTextAlignment = TextAlignment.Center,
                            HorizontalTextAlignment = TextAlignment.Center,
                            FontFamily = fontFamily,
                            FontSize = fontSize,
                            TextColor = loadingColor
                        }
                    }
                }
            };
        }
    }
}
