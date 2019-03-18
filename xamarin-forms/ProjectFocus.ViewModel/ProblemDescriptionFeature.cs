using ProjectFocus.Interface;

namespace ProjectFocus.ViewModel
{
    [FeatureMetadata(nameof(ProblemDescriptionFeature), FeatureScope.ProblemCreation)]
    class ProblemDescriptionFeature : ViewModelBase, IProblemDescriptionFeature, IViewModelFeature
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
