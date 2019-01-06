using ProjectFocus.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectFocus.ViewModel
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        public IFeatureProvider FeatureProvider {get;set;}
        public IUserService UserService { get; set; }

        private IEnumerable<IViewModelFeature> features;
        public IEnumerable<IViewModelFeature> Features
        {
            get
            {
                if (features == null)
                    features = FeatureProvider.GetEnabledFeatures(FeatureScope.MainSelection,
                                          UserService.GetEnabledFeatureKeys(FeatureScope.MainSelection))
                              .Select(getFeature => getFeature());
                return features;
            }
        }
    }
}
