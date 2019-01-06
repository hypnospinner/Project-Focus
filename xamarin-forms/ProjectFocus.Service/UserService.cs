using System;

using ProjectFocus.Interface;

namespace ProjectFocus.Service
{
    public class UserService : IUserService
    {
        public string[] GetEnabledFeatureKeys(FeatureScope featureScope)
        {
            //[ToDo] : In real implementation this will be a list of selected features
            //from user configuration.
            return new[] { "NewProblemFeature" };
        }

        public void SaveEnabledFeatures(FeatureScope featureScope, string[] userSelection)
        {
            throw new NotImplementedException();
        }
    }
}
