using System.Collections.Generic;

namespace ProjectFocus.Interface
{
    public interface IMainViewModel
    {
        IEnumerable<IViewModelFeature> Features { get; }
    }
}
