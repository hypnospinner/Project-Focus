using System.Windows.Input;

namespace ProjectFocus.Interface
{
    /// <summary>
    /// Presentation logic for submitting a problem
    /// </summary>
    public interface ISubmitProblemFeature
    {
        IRelayCommand SubmitCommand { get; }
    }
}
