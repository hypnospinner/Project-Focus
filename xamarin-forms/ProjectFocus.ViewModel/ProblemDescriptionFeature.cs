using ProjectFocus.Interface;
using ProjectFocus.ViewModel.Event;

namespace ProjectFocus.ViewModel
{
    [FeatureMetadata(nameof(ProblemDescriptionFeature), FeatureScope.ProblemCreation)]
    class ProblemDescriptionFeature : FeatureViewModelBase, IProblemDescriptionFeature
    {
        private string problemHeader;
        private string problemDescription;

        public string ProblemHeader
        {
            get => problemHeader;
            set
            {
                problemHeader = value;
                NotifyPropertyChanged();
                Notification.Publish(new ProblemHeaderChanged { NewHeader = value });
            }
        }
        public string ProblemDescription
        {
            get => problemDescription;
            set 
            {
                problemDescription = value;
                NotifyPropertyChanged();
                Notification.Publish(new ProblemDescriptionChanged { NewDescription = value });
            }
        }
    }
}
