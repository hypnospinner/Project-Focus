using System;

using Xamarin.Forms;

namespace ProjectFocus.View
{
    public class FeatureTemplateSelector : DataTemplateSelector
    {
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var currentType = typeof(FeatureTemplateSelector);
            var assembly = currentType.Assembly;
            var result = new DataTemplate(() =>
                Activator.CreateInstance(assembly.GetType($"{currentType.Namespace}.{item.GetType().Name}View")));
            return result;
        }
    }
}
