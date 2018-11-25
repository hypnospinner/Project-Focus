using Microsoft.AppCenter.Analytics;
using ProjectFocus.Integration;
using ProjectFocus.Interface;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppCenterDevice = Microsoft.AppCenter.Device;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ProjectFocus
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // The instantiation could be transferred to XAML
            // code to solve this one in great style.
            // [Exercise][ToDo] Find a way to solve this in pure XAML.
            var navigationPage = new NavigationPage();
            MainPage = navigationPage;

            if (DesignMode.IsDesignModeEnabled) return;

            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
                //AppResources.Culture = ci; // set the RESX for resource localization
                DependencyService.Get<ILocalize>().SetLocale(ci); // set the Thread for locale-aware methods
            }

            new CompositionRoot().Compose(navigationPage);
        }

        protected override void OnStart()
        {
            Microsoft.AppCenter.AppCenter.Start("uwp=fd480248-b061-4b39-8554-a60bd945bf1e;" +
                  "android={Your Android App secret here}" +
                  "ios={Your iOS App secret here}",
                  typeof(Analytics));
       }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
