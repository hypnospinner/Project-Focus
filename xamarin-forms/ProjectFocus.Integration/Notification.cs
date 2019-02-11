using ProjectFocus.Interface;
using System;
using System.Collections.Generic;

namespace ProjectFocus.Integration
{
    public class Notification : INotification
    {
        public void Publish(object parameter)
        {
            foreach (var handler in subscriptions.Values)
            {
                handler(parameter);
            }
        }

        public Guid Subscribe(Action<object> handler)
        {
            var subscriptionId = Guid.NewGuid();
            subscriptions[subscriptionId] = handler;
            return subscriptionId;
        }

        public void Unsubscribe(Guid subscriptionId)
        {
            subscriptions.Remove(subscriptionId);
        }

        private Dictionary<Guid, Action<object>> subscriptions
            = new Dictionary<Guid, Action<object>>();
    }
}
