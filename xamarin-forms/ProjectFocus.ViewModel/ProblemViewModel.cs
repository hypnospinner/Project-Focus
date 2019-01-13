using ProjectFocus.Interface;

namespace ProjectFocus.ViewModel
{
    public class ProblemViewModel : ViewModelBase, IProblemViewModel
    {
        private string _text = "Happy binding from problem!";
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                NotifyPropertyChanged();
            }
        }

        private float _importance;
        public float Importance
        {
            get
            {
                return _importance;
            }
            set
            {
                _importance = value;
                NotifyPropertyChanged();
            }
        }

        private float _urgency;
        public float Urgency
        {
            get
            {
                return _urgency;
            }
            set
            {
                _urgency = value;
                NotifyPropertyChanged();
            }
        }
    }
}
