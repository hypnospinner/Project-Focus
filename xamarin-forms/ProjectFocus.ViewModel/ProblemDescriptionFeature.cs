using ProjectFocus.Interface;

namespace ProjectFocus.ViewModel
{
    [FeatureMetadata(nameof(ProblemDescriptionFeature), FeatureScope.ProblemCreation)]
    class ProblemDescriptionFeature : FeatureViewModelBase, IProblemDescriptionFeature
    {
        public ProblemDescriptionFeature(INotification notification)
        {

        }

        private string problemHeader;
        private string problemDescription;
        public string ProblemHeader
        {
            get => problemHeader;
            set
            {
                problemHeader = value;
                NotifyPropertyChanged();
            }
        }
        public string ProblemDescription
        {
            get => problemDescription;
            set 
            {
                problemDescription = value;
                NotifyPropertyChanged();
            }
        }
    }
}
