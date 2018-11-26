using Foundation;
using ProjectFocus.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Xamarin.Forms;

[assembly: Dependency(typeof(ProjectFocus.iOS.LocaleManager))]
namespace ProjectFocus.iOS
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
            if (NSLocale.PreferredLanguages.Length == 0)
                return new CultureInfo("en");

            var pref = NSLocale.PreferredLanguages[0];

            if (_netCultures.TryGetValue(pref, out var result))
                return result;

            try
            {
                result = new CultureInfo(iOSToCandidateDotnetLanguage(pref));
                _netCultures[pref] = result;
                return result;
            }
            catch (CultureNotFoundException) { }

            try
            {
                var dashIndex = pref.IndexOf("-", StringComparison.Ordinal);
                var languageCode = dashIndex > 0 ? pref.Split('-')[0] : pref;
                result = new CultureInfo(iOSLanguageCodeToCandidateDotnetFallbackLanguage(languageCode));
                _netCultures[pref] = result;
                return result;
            }
            catch (CultureNotFoundException) { }

            return new CultureInfo("en");
        }

        private string iOSToCandidateDotnetLanguage(string iOSLanguage)
        {
            var netLanguage = iOSLanguage;
            //certain languages need to be converted to CultureInfo equivalent
            switch (iOSLanguage)
            {
                case "ms-MY":   // "Malaysian (Malaysia)" not supported .NET culture
                case "ms-SG":    // "Malaysian (Singapore)" not supported .NET culture
                    netLanguage = "ms"; // closest supported
                    break;
                case "gsw-CH":  // "Schwiizertüütsch (Swiss German)" not supported .NET culture
                    netLanguage = "de-CH"; // closest supported
                    break;
                    // add more application-specific cases here (if required)
                    // ONLY use cultures that have been tested and known to work
            }
            return netLanguage;
        }

        private string iOSLanguageCodeToCandidateDotnetFallbackLanguage(string languageCode)
        {
            var netLanguage = languageCode; // use the first part of the identifier (two chars, usually);
            switch (languageCode)
            {
                case "pt":
                    netLanguage = "pt-PT"; // fallback to Portuguese (Portugal)
                    break;
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