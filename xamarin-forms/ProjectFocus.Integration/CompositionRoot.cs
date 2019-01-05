using Autofac;
using ProjectFocus.Interface;
using ProjectFocus.Service;
using ProjectFocus.View;
using ProjectFocus.ViewModel;
using System;
using Xamarin.Forms;

namespace ProjectFocus.Integration
{
    public class CompositionRoot
    {
        public void RunApplication(Application app)
        {
            var builder = new ContainerBuilder();

            var viewAssembly = typeof(MainPage).Assembly;

            builder.RegisterAssemblyTypes(viewAssembly)
                   .Where(t => t.Name.EndsWith("Page", StringComparison.InvariantCultureIgnoreCase))
                   .Keyed<ContentPage>(t => t.Name);

            var viewModelAssembly = typeof(MainViewModel).Assembly;

            builder.RegisterAssemblyTypes(viewModelAssembly)
                   .Where(t => t.Name.EndsWith("ViewModel", StringComparison.InvariantCultureIgnoreCase))
                   .AsImplementedInterfaces()
                   .PropertiesAutowired();

            var serviceAssembly = typeof(ProblemService).Assembly;

            builder.RegisterAssemblyTypes(serviceAssembly)
                   .Where(t => t.Name.EndsWith("Service", StringComparison.InvariantCultureIgnoreCase))
                   .AsImplementedInterfaces()
                   .PropertiesAutowired()
                   .SingleInstance();

            builder.RegisterType<Notification>().AsImplementedInterfaces();

            var container = builder.Build();

            // This here is platform-specific localization integration code
            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                var localeManager = DependencyService.Get<ILocaleManager>();
                var cultureInfo = localeManager.GetCurrentCultureInfo();
                // set the RESX for resource localization
                AppResources.Culture = cultureInfo;
                // set the Thread for locale-aware methods
                localeManager.SetLocale(cultureInfo);
            }

            var mainViewModel = container.Resolve<IMainViewModel>();
            var navigationPage = new NavigationPage(new MainPage() { BindingContext = mainViewModel });
            app.MainPage = navigationPage;
        }
    }
}
