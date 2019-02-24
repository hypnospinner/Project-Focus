using Autofac;
using ProjectFocus.Interface;
using ProjectFocus.Service;
using ProjectFocus.View;
using ProjectFocus.ViewModel;
using System;
using System.Linq;
using Xamarin.Forms;

namespace ProjectFocus.Integration
{
    public class CompositionRoot
    {
        public void RunApplication(Application app)
        {
            var builder = new ContainerBuilder();

            var viewAssembly = typeof(MainPage).Assembly;

            var viewModelAssembly = typeof(MainViewModel).Assembly;

            builder.RegisterType<Notification>()
                   .As<INotification>();

            builder.RegisterType<CommandFactory>()
                   .As<ICommandFactory>()
                   .SingleInstance();

            builder.RegisterAssemblyTypes(viewModelAssembly)
                   .Where(t => t.Name.EndsWith("ViewModel", StringComparison.InvariantCultureIgnoreCase))
                   .AsImplementedInterfaces()
                   .PropertiesAutowired();

            builder.RegisterAssemblyTypes(viewModelAssembly)
                   .Where(t => t.Name.EndsWith("Feature", StringComparison.InvariantCultureIgnoreCase))
                   .WithMetadataFrom<FeatureMetadataAttribute>()
                   .As<IViewModelFeature>()
                   .PropertiesAutowired();

            builder.RegisterType<FeatureProvider>()
                   .As<IFeatureProvider>()
                   .SingleInstance()
                   .PropertiesAutowired();

            var serviceAssembly = typeof(ProblemService).Assembly;

            builder.RegisterAssemblyTypes(serviceAssembly)
                   .Where(t => t.Name.EndsWith("Service", StringComparison.InvariantCultureIgnoreCase))
                   .AsImplementedInterfaces()
                   .PropertiesAutowired()
                   .SingleInstance();

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
