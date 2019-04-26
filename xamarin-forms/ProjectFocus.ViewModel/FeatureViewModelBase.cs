using ProjectFocus.Interface;

namespace ProjectFocus.ViewModel
{
    public class FeatureViewModelBase : ViewModelBase, IFeatureViewModelBase
    {
        protected INotification Notification { get; private set; }
        public void SetNotificationChanel(INotification notification)
        {
            Notification = notification;
            SubscribeToNotifications();
        }
        protected virtual void SubscribeToNotifications()
        { }
    }
}
