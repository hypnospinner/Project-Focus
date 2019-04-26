using System;

namespace ProjectFocus.Interface
{
    public interface INotification<T>
    {
        Guid Subscribe(Action<T> handler);
        void Unsubscribe(Guid subscriptionId);
        void Publish(T parameter);
    }
}
