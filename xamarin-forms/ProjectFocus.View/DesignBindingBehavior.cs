using System;

using Xamarin.Forms;

namespace ProjectFocus.View
{
    /// <summary>
    /// A design-time-only behavior that binds its
    /// associated ContentPage to an instance the given type.
    /// </summary>
    public class DesignBindingBehavior : BehaviorBase<ContentPage>
    {
        public static readonly BindableProperty ObjectTypeProperty =
            BindableProperty.CreateAttached("ObjectType", typeof(Type), typeof(DesignBindingBehavior), null, propertyChanged: OnObjectTypeChanged);

        public Type ObjectType
        {
            get { return (Type)GetValue(ObjectTypeProperty); }
            set { SetValue(ObjectTypeProperty, value); }
        }

        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);

            if (DesignMode.IsDesignModeEnabled)
            {
                InstantiateAndBind(ObjectType, bindable);
            }
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
