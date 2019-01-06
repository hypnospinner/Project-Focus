using System;

namespace ProjectFocus.Interface
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FeatureMetadataAttribute : Attribute
    {
        public string Name { get; set; }

        public FeatureScope[] SupportedScopes { get; set; }

        public FeatureMetadataAttribute()
        { }

        public FeatureMetadataAttribute(string name, params FeatureScope[] supportedScopes)
        {
            Name = name;
            SupportedScopes = supportedScopes;
        }
    }
}
