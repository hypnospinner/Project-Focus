using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFocus.Interface
{
    public interface INotification
    {
        void Subscribe(Action<object> handler);

        void Publish(object parameter);
    }
}
