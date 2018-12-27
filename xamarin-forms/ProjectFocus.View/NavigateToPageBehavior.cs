using ProjectFocus.Interface;
using System;
using Xamarin.Forms;

namespace ProjectFocus.View
{
    public class NavigateToPageBehavior : BehaviorBase<ContentPage>
    {
        public static readonly BindableProperty PageTypeProperty =
            BindableProperty.CreateAttached("PageType", typeof(Type), typeof(NavigateToPageBehavior), null);

        public Type PageType
        {
            get { return (Type)GetValue(PageTypeProperty); }
            set { SetValue(PageTypeProperty, value); }
        }

        public static readonly BindableProperty NotificationProperty =
            BindableProperty.CreateAttached("Notification", typeof(INotification), typeof(NavigateToPageBehavior), null, propertyChanged: OnNotificationChanged);

        public INotification Notification
        {
            get { return (INotification)GetValue(NotificationProperty); }
            set { SetValue(NotificationProperty, value); }
        }

        protected override void OnDetachingFrom(BindableObject bindable)
        {
            Notification.Subscribe(null);
            base.OnDetachingFrom(bindable);
        }

        private static void OnNotificationChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(oldValue != null)
            {
                ((INotification)oldValue).Subscribe(null);
            }

            var notification = (INotification)newValue;
            notification.Subscribe(viewModel =>
            {
                var behavior = (NavigateToPageBehavior)bindable;
                var pageType = behavior.PageType;
                var page = (ContentPage)Activator.CreateInstance(pageType);
                page.BindingContext = viewModel;
                behavior.AssociatedObject.Navigation.PushAsync(page);
            });
        }
    }
}
