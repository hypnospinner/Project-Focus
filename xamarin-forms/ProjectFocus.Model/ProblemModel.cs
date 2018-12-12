using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectFocus.Model
{
    class ProblemModel : INotifyPropertyChanged
    {
        // Should we create keep data about user id and problem id in there
        // it seems that it's better to add user id when sending a data to sever
        // so we don't need it there
        // and it makes sense for problem to know it's id

        // fields are created according to Proplmen implementaion in back-end

        private DateTime _createdAt;
        private string _name;
        private string _description;
        private string _content;

        // we don't want to change it accidentally
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

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                NotifyPropertyChaged();
            }
        }

        public string Content
        {
            get => _content;
            set
            {
                _content = value;
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
