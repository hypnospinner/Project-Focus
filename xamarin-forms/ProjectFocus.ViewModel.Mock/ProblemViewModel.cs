using System.Collections.Generic;
using ProjectFocus.Interface;

namespace ProjectFocus.ViewModel.Mock
{
    public class ProblemViewModel : IProblemViewModel
    {
        public IEnumerable<IFeatureViewModelBase> Features => throw new System.NotImplementedException();
    }
}
