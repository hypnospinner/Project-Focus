using ProjectFocus.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Xamarin.Forms;

[assembly: Dependency(typeof(ProjectFocus.Droid.LocaleManager))]
namespace ProjectFocus.Droid
{
    public class LocaleManager : ILocaleManager
    {
        private static readonly Dictionary<string, CultureInfo> _netCultures
            = new Dictionary<string, CultureInfo>();

        public void SetLocale(CultureInfo ci)
        {
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }

        public CultureInfo GetCurrentCultureInfo()
        {
            var pref = Java.Util.Locale.Default.ToString();

            if (_netCultures.TryGetValue(pref, out var result))
                return result;

            var prefDash = pref.Replace('_', '-');

            try
            {
                result = new CultureInfo(androidToCandidateDotnetLanguage(prefDash));
                _netCultures[pref] = result;
                return result;
            }
            catch (CultureNotFoundException) { }

            try
            {
                var dashIndex = prefDash.IndexOf("-", StringComparison.Ordinal);
                var languageCode = dashIndex > 0 ? pref.Split('-')[0] : pref;
                result = new CultureInfo(androidLanguageCodeToCandidateDotnetFallbackLanguage(languageCode));
                _netCultures[pref] = result;
                return result;
            }
            catch (CultureNotFoundException) { }

            return new CultureInfo("en");
        }

        private string androidToCandidateDotnetLanguage(string androidLanguage)
        {
            var netLanguage = androidLanguage;
            //certain languages need to be converted to CultureInfo equivalent
            switch (androidLanguage)
            {
                case "ms-BN":   // "Malaysian (Brunei)" not supported .NET culture
                case "ms-MY":   // "Malaysian (Malaysia)" not supported .NET culture
                case "ms-SG":   // "Malaysian (Singapore)" not supported .NET culture
                    netLanguage = "ms"; // closest supported
                    break;
                case "in-ID":  // "Indonesian (Indonesia)" has different code in  .NET
                    netLanguage = "id-ID"; // correct code for .NET
                    break;
                case "gsw-CH":  // "Schwiizertüütsch (Swiss German)" not supported .NET culture
                    netLanguage = "de-CH"; // closest supported
                    break;
                    // add more application-specific cases here (if required)
                    // ONLY use cultures that have been tested and known to work
            }
            return netLanguage;
        }

        private string androidLanguageCodeToCandidateDotnetFallbackLanguage(string languageCode)
        {
            var netLanguage = languageCode; // use the first part of the identifier (two chars, usually);
            switch (languageCode)
            {
                case "gsw":
                    netLanguage = "de-CH"; // equivalent to German (Switzerland) for this app
                    break;
                    // add more application-specific cases here (if required)
                    // ONLY use cultures that have been tested and known to work
            }
            return netLanguage;
        }
    }
}