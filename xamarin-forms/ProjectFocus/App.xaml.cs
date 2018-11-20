using System;

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

            var navigationPage = new NavigationPage();
            navigationPage.Title = "Strange";

            MainPage = navigationPage;
            new AppComposer().Compose(navigationPage);
        }

        protected override void OnStart()
        {
            AppCenter.Start("uwp=fd480248-b061-4b39-8554-a60bd945bf1e;" +
                  "android={Your Android App secret here}" +
                  "ios={Your iOS App secret here}",
                  typeof(Analytics));
            // Handle when your app starts
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
