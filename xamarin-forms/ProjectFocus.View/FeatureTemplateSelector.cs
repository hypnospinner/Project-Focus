using System;

using Xamarin.Forms;

using ProjectFocus.Interface;

namespace ProjectFocus.View
{
    public class FeatureTemplateSelector : DataTemplateSelector
    {
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var currentType = typeof(FeatureTemplateSelector);
            var assembly = currentType.Assembly;
            var result = new DataTemplate(() =>
                Activator.CreateInstance(assembly.GetType($"{currentType.Namespace}.{((FeatureMetadataAttribute)item.GetType().GetCustomAttributes(typeof(FeatureMetadataAttribute), true)[0]).Name}View")));
            return result;
        }
    }
}
