using ProjectFocus.Interface;
using System.Collections.Generic;

namespace ProjectFocus.ViewModel.Mock
{
    public class MainViewModel : IMainViewModel
    {
        IEnumerable<IFeatureViewModelBase> IMainViewModel.Features => new[] { new NewProblemFeature() };
    }
}
