using ProjectFocus.Interface;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace ProjectFocus.ViewModel
{
    public class ProblemViewModel : ViewModelBase, IProblemViewModel
    {
        public IFeatureProvider FeatureProvider { get; set; }
        public IUserService UserService { get; set; }

        private IEnumerable<IViewModelFeature> features;
        public IEnumerable<IViewModelFeature> Features
        {
            get
            {
                if (features == null)
                    features = FeatureProvider.GetEnabledFeatures(FeatureScope.ProblemCreation,
                                          UserService.GetEnabledFeatureKeys(FeatureScope.ProblemCreation))
                              .Select(getFeature => getFeature());
                return features;
            }
        }
    }
}
