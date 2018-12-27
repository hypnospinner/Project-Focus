using ProjectFocus.Interface;
using System;

namespace ProjectFocus.Integration
{
    public class Notification : INotification
    {
        public void Publish(object parameter)
        {
            action(parameter);
        }

        public void Subscribe(Action<object> handler)
        {
            action = handler;
        }

        private Action<object> action;
    }
}
