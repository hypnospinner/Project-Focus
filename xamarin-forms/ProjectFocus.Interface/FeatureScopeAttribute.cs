using System;

namespace ProjectFocus.Interface
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FeatureScopeAttribute : Attribute
    {
        public string Name { get; set; }

        public FeatureScope[] SupportedScopes { get; set; }

        public FeatureScopeAttribute()
        { }

        public FeatureScopeAttribute(string name, params FeatureScope[] supportedScopes)
        {
            Name = name;
            SupportedScopes = supportedScopes;
        }
    }
}
