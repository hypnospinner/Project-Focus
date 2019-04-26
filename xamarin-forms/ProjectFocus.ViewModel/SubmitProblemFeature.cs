using ProjectFocus.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ProjectFocus.ViewModel
{
    [FeatureMetadata(nameof(SubmitProblemFeature),FeatureScope.ProblemCreation)]
    public class SubmitProblemFeature : FeatureViewModelBase, ISubmitProblemFeature
    { 
        private ICommand _submitCommand;
        public ICommandFactory CommandFactory { get; set; }
        public ICommand SubmitCommand
        {
            get
            {
                if(_submitCommand == null)
                {
                    // mock for _submit command
                    _submitCommand = CommandFactory.Create(
                        () => Console.WriteLine("Submit Command"),
                        () => true);
                }

                return _submitCommand;
            }
        }
        protected override void SubscribeToNotifications()
        {
            base.SubscribeToNotifications();
        }
    }
}
