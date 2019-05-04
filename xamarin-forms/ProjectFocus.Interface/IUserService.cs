using ProjectFocus.Model;

namespace ProjectFocus.Interface
{
    public interface IUserService
    {
        User GetCurrentUser();

        string[] GetEnabledFeatureKeys(FeatureScope featureScope);

        void SaveEnabledFeatures(FeatureScope featureScope, string[] userSelection);
    }
}
