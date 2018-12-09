using Autofac.Features.Indexed;
using ProjectFocus.Interface;
using Xamarin.Forms;

namespace ProjectFocus.Service
{
    public class NavigationService : INavigationService
    {
        public NavigationPage Navigation { get; set; }

        // This is an automatic keyed resolution of all page types
        // brought to us by Autofac.
        public IIndex<string, ContentPage> PageIndex { get; set; }

        public async void Navigate(PageKey target, object viewModel)
        {
            var page = PageIndex[target + "Page"];
            page.BindingContext = viewModel;
            // [Engineering][ToDo] Animation support might be needed here in future.
            await Navigation.PushAsync(page, false);
        }

        public async void Back()
        {
            await Navigation.PopAsync();
        }

        public async void Home()
        {
            await Navigation.PopToRootAsync();
        }
    }
}
