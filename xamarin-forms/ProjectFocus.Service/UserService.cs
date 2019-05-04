using System;

using ProjectFocus.Interface;
using ProjectFocus.Model;

namespace ProjectFocus.Service
{
    public class UserService : IUserService
    {
        public User GetCurrentUser()
        {
            return new User
            {
                Id = new Guid("f30b92ef-13b6-403b-a32a-bb3f688d58da")
            };
        }

        //[ToDo] : In real implementation this will be a list of selected features
        //from user configuration.
        public string[] GetEnabledFeatureKeys(FeatureScope featureScope)
            => featureScope == FeatureScope.MainSelection ? 
            new[] { "GoToProblemFeature" } : new[] {
                "SubmitProblemFeature" ,
                "ProblemDescriptionFeature" };

        public void SaveEnabledFeatures(FeatureScope featureScope, string[] userSelection)
        {
            throw new NotImplementedException();
        }
    }
}
