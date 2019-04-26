using ProjectFocus.Interface;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace ProjectFocus.ViewModel
{
    public class ProblemViewModel : ViewModelBase, IProblemViewModel
    {
        public INotification Notifcation { get; set; }
        public IFeatureProvider FeatureProvider { get; set; }
        public IUserService UserService { get; set; }

        private IEnumerable<IFeatureViewModelBase> features;
        public IEnumerable<IFeatureViewModelBase> Features
        {
            get
            {
                if (features == null)
                    features = FeatureProvider.GetEnabledFeatures(FeatureScope.ProblemCreation,
                                          UserService.GetEnabledFeatureKeys(FeatureScope.ProblemCreation))
                              .Select(getFeature => 
                              {
                                  var feature = getFeature();
                                  feature.SetNotificationChanel(Notifcation);
                                  return feature;
                              });

                return features;
            }
        }
    }
}
