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
            var mockNavigationService = new Mock<INavigationService>();
            var sut = new MainViewModel(mockNavigationService.Object, () => mockProblemViewModel.Object);

            // Verify MainPage navigation with MainViewModel on creation
            mockNavigationService.Verify(x => x.Navigate(
                It.Is<PageKey>(k => k == PageKey.Main),
                It.Is<object>(vm => vm == sut)), Times.Exactly(1));

            // Problem command called from MainPage
            sut.ProblemCommand.Execute(null);

            // Verify ProblemPage navigation on command with the proper IProblemViewModel
            mockNavigationService.Verify(x => x.Navigate(
                It.Is<PageKey>(k => k == PageKey.Problem),
                It.Is<object>(vm => vm == mockProblemViewModel.Object)), Times.Exactly(1));
        }
    }
}
