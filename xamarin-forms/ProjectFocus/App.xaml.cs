using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

            new AppComposer().Compose(navigationPage);
        }

        protected override void OnStart()
        {
            AppCenter.Start("uwp=fd480248-b061-4b39-8554-a60bd945bf1e;" +
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
