using ProjectFocus.Interface;
using ProjectFocus.Model;
using ProjectFocus.ViewModel.Event;
using System;

namespace ProjectFocus.ViewModel
{
    [FeatureMetadata(nameof(SubmitProblemFeature),FeatureScope.ProblemCreation)]
    public class SubmitProblemFeature : FeatureViewModelBase, ISubmitProblemFeature
    {
        private bool _changed;
        private Problem _problemModel;
        private IRelayCommand _submitCommand;
        public ICommandFactory CommandFactory { get; set; }
        public IProblemService ProblemService { get; set; }
        public IUserService UserService { get; set; }

        private bool Changed
        {
            get => _changed;
            set
            {
                _changed = value;
                _submitCommand.NotifyCanExecuteChanged();
            }
        }

        public IRelayCommand SubmitCommand
        {
            get
            {
                if(_submitCommand == null)
                {
                    _submitCommand = CommandFactory.Create(
                        () =>
                        {
                            ProblemService.CreateProblem(_problemModel, UserService.GetCurrentUser());
                            Changed = false;
                        },
                        () => Changed);
                }

                return _submitCommand;
            }
        }

        protected override void SubscribeToNotifications()
        {
            Notification.Subscribe(featureEvent =>
            {
                switch (featureEvent)
                {
                    case ProblemRead read:
                        _problemModel = read.ProblemModel;
                        Changed = false;
                        break;
                    case ProblemHeaderChanged headerChanged:
                        if (_problemModel == null) _problemModel = new Problem { Name = headerChanged.NewHeader };
                        else _problemModel.Name = headerChanged.NewHeader;
                        Changed = true;
                        break;
                    case ProblemDescriptionChanged descriptionChanged:
                        if (_problemModel == null) _problemModel = new Problem { Description = descriptionChanged.NewDescription };
                        else _problemModel.Description = descriptionChanged.NewDescription;
                        Changed = true;
                        break;
                }
            });
        }
    }
}
