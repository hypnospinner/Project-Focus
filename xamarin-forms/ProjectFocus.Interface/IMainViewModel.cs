using System.Collections.Generic;

namespace ProjectFocus.Interface
{
    public interface IMainViewModel
    {
        IEnumerable<IFeatureViewModelBase> Features { get; }
    }
}
