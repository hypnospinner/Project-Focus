using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectFocus.Model
{
    public class ProblemModel : INotifyPropertyChanged
    {
        private DateTime _createdAt;
        private string _name;

        public ProblemModel(string name)
        {
            _createdAt = DateTime.Now;
            _name = name;
        }

        public DateTime CreatedAt
        {
            get => _createdAt;
            set
            {
                if (_createdAt != null)
                {
                    _createdAt = value;
                    NotifyPropertyChaged();
                }
            }
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChaged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
