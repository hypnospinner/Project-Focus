using System.Collections.Generic;
using ProjectFocus.Interface;

namespace ProjectFocus.ViewModel.Mock
{
    public class ProblemViewModel : IProblemViewModel
    {
        public IEnumerable<IViewModelFeature> Features => throw new System.NotImplementedException();
    }
}
