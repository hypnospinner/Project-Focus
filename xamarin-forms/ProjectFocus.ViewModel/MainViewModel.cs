using ProjectFocus.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectFocus.ViewModel
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        // fields
        private IEnumerable<IFeatureViewModelBase> features;
        // properties
        public IFeatureProvider FeatureProvider {get;set;}
        public IUserService UserService { get; set; }
        public IEnumerable<IFeatureViewModelBase> Features
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
