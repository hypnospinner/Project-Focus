using System.Globalization;

namespace ProjectFocus.Interface
{
    public interface ILocaleManager
    {
        CultureInfo GetCurrentCultureInfo();

        void SetLocale(CultureInfo ci);
    }
}
