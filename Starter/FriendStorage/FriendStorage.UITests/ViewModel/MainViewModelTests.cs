using FriendStorage.UI.ViewModel;
using Moq;
using NUnit.Framework;

namespace FriendStorage.UITests.ViewModel
{
    [TestFixture]
    public class MainViewModelTests
    {
        private Mock<INavigationViewModel> _navigationVmMock;
        private MainViewModel _viewModel;

        [SetUp]
        public void SetUp()
        {
            _navigationVmMock = new Mock<INavigationViewModel>();
            _viewModel = new MainViewModel(_navigationVmMock.Object);
        }

        [Test]
        public void ShouldCallTheLoadMethodOfTheNavigationViewModel()
        {
            _viewModel.Load();
            _navigationVmMock.Verify(vm => vm.Load(), Times.Once);
        }
    }
}
