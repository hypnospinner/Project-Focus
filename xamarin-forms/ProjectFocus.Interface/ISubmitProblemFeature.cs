using System.Windows.Input;

namespace ProjectFocus.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISubmitProblemFeature
    {
        ICommand SubmitCommand { get; }
    }
}
