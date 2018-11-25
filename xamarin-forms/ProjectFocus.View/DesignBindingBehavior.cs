using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProjectFocus.View
{
    /// <summary>
    /// A design-time-only behavior that binds its
    /// associated ContentPage to an instance the given type.
    /// </summary>
    public class DesignBindingBehavior : Behavior<ContentPage>
    {
        public static readonly BindableProperty ObjectTypeProperty =
            BindableProperty.CreateAttached("ObjectType", typeof(Type), typeof(DesignBindingBehavior), null, propertyChanged: OnObjectTypeChanged);

        public static Type GetObjectType(BindableObject view)
        {
            return (Type)view.GetValue(ObjectTypeProperty);
        }

        public static void SetObjectType(BindableObject view, Type value)
        {
            view.SetValue(ObjectTypeProperty, value);
        }

        public ContentPage AssociatedObject { get; private set; }

        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;

            if (DesignMode.IsDesignModeEnabled)
            {
                InstantiateAndBind(GetObjectType(this), bindable);
            }
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            base.OnDetachingFrom(bindable);
            AssociatedObject = null;
        }

        private static void OnObjectTypeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (DesignMode.IsDesignModeEnabled)
            {
                var objectType = newValue as Type;
                var behavior = (DesignBindingBehavior)bindable;
                InstantiateAndBind(objectType, behavior.AssociatedObject);
            }
        }

        private static void InstantiateAndBind(Type contextType, ContentPage bindable)
        {
            if (bindable == null || contextType == null) return;

            var contextInstance = Activator.CreateInstance(contextType);
            bindable.BindingContext = contextInstance;
        }
    }
}
