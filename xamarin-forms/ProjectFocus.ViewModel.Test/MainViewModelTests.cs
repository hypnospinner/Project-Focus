using Moq;
using ProjectFocus.Interface;
using Xunit;

namespace ProjectFocus.ViewModel.Test
{
    public class MainViewModelTests
    {
        [Fact]
        public void NavigationTest()
        {
            var mockProblemViewModel = new Mock<IProblemViewModel>();
            var mockNotification = new Mock<INotification>();
            var sut = new MainViewModel();
            // Property injection
            sut.GetProblemViewModel = () => mockProblemViewModel.Object; // Stub
            sut.ProceedToCreateProblem = mockNotification.Object; // Mock

            // Problem command called from MainPage
            sut.ProblemCommand.Execute(null);

            // Verify ProblemPage navigation on command with the proper IProblemViewModel
            mockNotification.Verify(x => x.Publish(
                   It.Is<object>(o => o == mockProblemViewModel.Object)), Times.Exactly(1));
        }
    }
}
