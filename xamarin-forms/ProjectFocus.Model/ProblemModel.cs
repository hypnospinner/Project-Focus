using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectFocus.Model
{
    public class ProblemModel : INotifyPropertyChanged
    {
        private string _name;
        private string _description;

        public ProblemModel(string name)
        {
            _name = name;
        }

        public string Name
        {
            get => _name;
            set 
            {
                _name = value;
                NotifyPropertyChaged();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                NotifyPropertyChaged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChaged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
