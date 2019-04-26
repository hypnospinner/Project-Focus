using System.Collections.Generic;

namespace ProjectFocus.Interface
{
    public interface IProblemViewModel
    {
        IEnumerable<IFeatureViewModelBase> Features { get; }
    }
}
