using ProjectFocus.Interface;
using System;
using System.Globalization;
using System.Resources;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectFocus.View
{
    // You exclude the 'Extension' suffix when using in XAML
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        private readonly CultureInfo _cultureInfo = null;
        private const string _ResourceId = "ProjectFocus.View.AppResources";

        static readonly Lazy<ResourceManager> ResMgr = new Lazy<ResourceManager>(
            () => new ResourceManager(_ResourceId, typeof(TranslateExtension).Assembly));

        public string Text { get; set; }

        public TranslateExtension()
        {
            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
                // No way around service locator here.
                // TranslateExtension is instantiated directly, not injected (sic!)
                _cultureInfo = DependencyService.Get<ILocaleManager>().GetCurrentCultureInfo();
            else
                _cultureInfo = Thread.CurrentThread.CurrentUICulture;

        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return string.Empty;

            var translation = ResMgr.Value.GetString(Text, _cultureInfo);
            if (translation == null)
            {
#if DEBUG
                throw new ArgumentException(
                    string.Format("Key '{0}' was not found in resources '{1}' for culture '{2}'.", Text, _ResourceId, _cultureInfo.Name),
                    "Text");
#else
                translation = Text; // HACK: returns the key, which GETS DISPLAYED TO THE USER
#endif
            }
            return translation;
        }
    }
}
