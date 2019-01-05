using System;
using Xamarin.Forms;

using ProjectFocus.Interface;

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

        private Guid subscriptionId;

        public void Unsubscribe(INotification notification)
        {
            if (notification == null)
                return;

            notification.Unsubscribe(subscriptionId);
        }

        public void Subscribe(INotification notification)
        {
            if (notification == null)
                return;

            subscriptionId = notification.Subscribe(viewModel =>
            {
                var page = (ContentPage)Activator.CreateInstance(PageType);
                page.BindingContext = viewModel;
                AssociatedObject.Navigation.PushAsync(page);
            });
        }

        protected override void OnDetachingFrom(BindableObject bindable)
        {
            Unsubscribe(Notification);
            base.OnDetachingFrom(bindable);
        }

        private static void OnNotificationChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var behavior = (NavigateToPageBehavior)bindable;
            behavior.Unsubscribe((INotification)oldValue);
            behavior.Subscribe((INotification)newValue);
        }
    }
}
