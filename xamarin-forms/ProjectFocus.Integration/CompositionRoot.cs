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
        public IMainViewModel Compose(NavigationPage navigation)
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

            var serviceAssembly = typeof(NavigationService).Assembly;

            builder.RegisterAssemblyTypes(serviceAssembly)
                   .Where(t => t.Name.EndsWith("Service", StringComparison.InvariantCultureIgnoreCase))
                   .AsImplementedInterfaces()
                   .PropertiesAutowired()
                   .SingleInstance();

            builder.RegisterInstance(navigation)
                   .As<NavigationPage>();

            var container = builder.Build();

            // [Think][ToDo] Do we really have to resolve this one manually?
            return container.Resolve<IMainViewModel>();
        }
    }
}
