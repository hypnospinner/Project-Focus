using ProjectFocus.Interface;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjectFocus.ViewModel
{

    public class ProblemViewModel : ViewModelBase, IProblemViewModel
    {
        private string _name;
        private string _isProblemCreated;   // simple vizualization for the result of problem creation        

        private ICommand _createProblemCommand;
        public ICommand CreateProblemCommand
        {
            get
            {
                if (_createProblemCommand == null)
                {
                    _createProblemCommand = new Command(
                        _ => 
                        {
                            if (!string.IsNullOrWhiteSpace(_name)) {
                                // creating and saving problem
                                // setting fields to defaults
                                Name = "";

                                IsProblemCreated = "Created successfully";
                            }
                            else
                                IsProblemCreated = "Failure";
                        });

                    return _createProblemCommand;
                }
                else return _createProblemCommand;
            }            
        }

        public string Name { get
            {
                return _name;
            }
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }

        public string IsProblemCreated
        {
            get
            {
                return _isProblemCreated;
            }
            set
            {
                _isProblemCreated = value;
                NotifyPropertyChanged();
            }
           
        }
    }
}
