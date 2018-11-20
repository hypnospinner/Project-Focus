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
    }
}
