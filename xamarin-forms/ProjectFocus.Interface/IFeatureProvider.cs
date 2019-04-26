using System;

namespace ProjectFocus.Interface
{
    public interface IFeatureProvider
    {
        Func<IFeatureViewModelBase>[] GetEnabledFeatures(FeatureScope scope, string[] enabledFeatureKeys);

        string[] GetAllFeatureKeys(FeatureScope scope);
    }
}
