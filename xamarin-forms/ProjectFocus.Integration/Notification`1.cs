using ProjectFocus.Interface;
using System;
using System.Collections.Generic;

namespace ProjectFocus.Integration
{
    public class Notification<T> : INotification<T>
    {
        public void Publish(T parameter)
        {
            foreach (var handler in subscriptions.Values)
            {
                handler(parameter);
            }
        }

        public Guid Subscribe(Action<T> handler)
        {
            var subscriptionId = Guid.NewGuid();
            subscriptions[subscriptionId] = handler;
            return subscriptionId;
        }

        public void Unsubscribe(Guid subscriptionId)
        {
            subscriptions.Remove(subscriptionId);
        }

        private Dictionary<Guid, Action<T>> subscriptions
            = new Dictionary<Guid, Action<T>>();
    }
}