namespace ProjectFocus.Interface
{
    public interface IUserService
    {
        string[] GetEnabledFeatureKeys(FeatureScope featureScope);

        void SaveEnabledFeatures(FeatureScope featureScope, string[] userSelection);
    }
}
