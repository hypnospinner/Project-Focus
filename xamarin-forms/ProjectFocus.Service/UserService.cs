using System;

using ProjectFocus.Interface;

namespace ProjectFocus.Service
{
    public class UserService : IUserService
    {
        //[ToDo] : In real implementation this will be a list of selected features
        //from user configuration.
        public string[] GetEnabledFeatureKeys(FeatureScope featureScope)
            => featureScope == FeatureScope.MainSelection ? 
            new[] { "NewProblemFeature" } : new[] { "ProblemDescriptionFeature" };

        public void SaveEnabledFeatures(FeatureScope featureScope, string[] userSelection)
        {
            throw new NotImplementedException();
        }
    }
}
