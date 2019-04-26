using System;
using System.Linq;

using Moq;
using Xunit;

using ProjectFocus.Interface;

namespace ProjectFocus.ViewModel.Test
{
    public class MainViewModelTests
    {
        [Fact]
        public void FeaturesLoadingTest()
        {
            var sut = new MainViewModel();

            var mockUserService = new Mock<IUserService>();
            var mockFeatureProvider = new Mock<IFeatureProvider>();
            var stubFeature = new Mock<IFeatureViewModelBase>().Object;
            var mockFeatures = new Func<IFeatureViewModelBase>[] { () => stubFeature };
            var stubFeatureKeys = new[] { "key1", "key2", "key3" };

            mockUserService.Setup(x => x.GetEnabledFeatureKeys(It.IsAny<FeatureScope>())).Returns(stubFeatureKeys);
            mockFeatureProvider.Setup(x => x.GetEnabledFeatures(It.IsAny<FeatureScope>(), It.IsAny<string[]>())).Returns(mockFeatures);

            sut.UserService = mockUserService.Object;
            sut.FeatureProvider = mockFeatureProvider.Object;

            var result = sut.Features.ToArray();

            Assert.Single(result);
            Assert.Equal(stubFeature, result[0]);

            mockUserService.Verify(
                x => x.GetEnabledFeatureKeys(
                    It.Is<FeatureScope>(y => y == FeatureScope.MainSelection)),
                Times.Once);

            mockFeatureProvider.Verify(x => x.GetEnabledFeatures(
                It.Is<FeatureScope>(y => y == FeatureScope.MainSelection),
                It.Is<string[]>(y => y == stubFeatureKeys)), Times.Once);
        }
    }
}
