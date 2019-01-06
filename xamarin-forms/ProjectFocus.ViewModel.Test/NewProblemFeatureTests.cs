using System;

using Moq;
using Xunit;

using ProjectFocus.Interface;

namespace ProjectFocus.ViewModel.Test
{
    public class NewProblemFeatureTests
    {
        [Fact]
        public void NewProblemFeatureMetadataTest()
        {
            var attributes = typeof(NewProblemFeature).GetCustomAttributes(typeof(FeatureScopeAttribute), true);
            Assert.Single(attributes);
            var attribute = (FeatureScopeAttribute)attributes[0];
            Assert.Equal("NewProblemFeature", attribute.Name);
            Assert.Single(attribute.SupportedScopes);
            Assert.Equal(FeatureScope.MainSelection, attribute.SupportedScopes[0]);
        }

        [Fact]
        public void ProceedToCreateProblemTest()
        {
            var mockNotification = new Mock<INotification>();
            var mockCommandFactory = new Mock<ICommandFactory>();
            var mockCommand = new Mock<IRelayCommand>();

            var stubProblemViewModel = new Mock<IProblemViewModel>().Object;

            mockCommandFactory.Setup(x => x.Create(It.IsAny<Action>(), It.IsAny<Func<bool>>()))
                .Callback<Action, Func<bool>>((execute, canExecute) => 
                    {
                        mockCommand.Setup(x => x.Execute(It.IsAny<object>())).Callback(() => execute());
                        mockCommand.Setup(x => x.CanExecute(It.IsAny<object>())).Returns(true);
                    })
                .Returns(mockCommand.Object);

            var sut = new NewProblemFeature();

            sut.CommandFactory = mockCommandFactory.Object;
            sut.GetProblemViewModel = () => stubProblemViewModel;
            sut.ProceedToCreateProblem = mockNotification.Object;

            var canExecuteResult = sut.ProblemCommand.CanExecute(null);
            sut.ProblemCommand.Execute(null);

            Assert.True(canExecuteResult);

            mockCommandFactory.Verify(x => x.Create(It.IsAny<Action>(), It.IsAny<Func<bool>>()), Times.Once);
            mockCommand.Verify(x => x.Execute(It.IsAny<object>()), Times.Once);
            mockCommand.Verify(x => x.CanExecute(It.IsAny<object>()), Times.Once);

            mockNotification.Verify(x => x.Publish(
                   It.Is<object>(y => y == stubProblemViewModel)), Times.Once);
        }
    }
}
