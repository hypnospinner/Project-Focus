using System;

namespace ProjectFocus.Interface
{
    public interface INotification
    {
        Guid Subscribe(Action<object> handler);

        void Unsubscribe(Guid subscriptionId);

        void Publish(object parameter);
    }
}
