using System;

namespace ProjectFocus.Interface
{
    public interface IFeatureProvider
    {
        Func<IViewModelFeature>[] GetEnabledFeatures(FeatureScope scope, string[] enabledFeatureKeys);

        string[] GetAllFeatureKeys(FeatureScope scope);
    }
}
