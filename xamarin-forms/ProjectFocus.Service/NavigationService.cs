﻿using Autofac.Features.Indexed;
using ProjectFocus.Interface;
using Xamarin.Forms;

namespace ProjectFocus.Service
{
    public class NavigationService : INavigationService
    {
        NavigationPage _navigation;

        // This is an automatic keyed resolution of all page types
        // brought to us by Autofac.
        IIndex<string, ContentPage> _pageIndex;

        public NavigationService(IIndex<string, ContentPage> pageIndex, NavigationPage navigation)
        {
            _pageIndex = pageIndex;
            _navigation = navigation;
        }

        public async void Navigate(PageKey target, object viewModel)
        {
            var page = _pageIndex[target + "Page"];
            page.BindingContext = viewModel;
            // [Engineering][ToDo] Animation support might be needed here in future.
            await _navigation.PushAsync(page, false);
        }

        public async void Back()
        {
            await _navigation.PopAsync();
        }

        public async void Home()
        {
            await _navigation.PopToRootAsync();
        }
    }
}