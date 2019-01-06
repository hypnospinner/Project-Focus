using System;
using System.Collections.Generic;
using System.Linq;

using Autofac.Features.Metadata;

using ProjectFocus.Interface;

namespace ProjectFocus.Integration
{
    public class FeatureProvider : IFeatureProvider
    {
        public Lazy<IEnumerable<Meta<Func<IViewModelFeature>, FeatureScopeAttribute>>> TaggedFeatureSources { get; set; }

        public FeatureProvider()
        {
        }

        public string[] GetAllFeatureKeys(FeatureScope scope)
        {
            return TaggedFeatureSources.Value.SelectMany(feature => feature.Metadata.SupportedScopes.Contains(scope)
                                ? new[] { feature.Metadata.Name }
                                : Enumerable.Empty<string>())
                            .ToArray();
        }

        public Func<IViewModelFeature>[] GetEnabledFeatures(FeatureScope scope, string[] enabledFeatureKeys)
        {
            return TaggedFeatureSources.Value.SelectMany(feature =>
                                enabledFeatureKeys.Contains(feature.Metadata.Name)
                                && feature.Metadata.SupportedScopes.Contains(scope)
                                ? new[] { feature.Value }
                                : Enumerable.Empty<Func<IViewModelFeature>>())
                            .ToArray();
        }
    }
}
