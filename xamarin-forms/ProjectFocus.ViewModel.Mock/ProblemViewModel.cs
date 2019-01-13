using ProjectFocus.Interface;

namespace ProjectFocus.ViewModel.Mock
{
    public class ProblemViewModel : IProblemViewModel
    {
        public string Text { get; set; } = "Test Problem View Model";

        public float Importance { get; set; } = 0.5f;

        public float Urgency { get; set; } = 0.5f;
    }
}
