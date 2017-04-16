using System.Collections.Generic;
using FriendStorage.UI.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using Moq;
using NUnit.Framework;

namespace FriendStorage.UITests.ViewModel
{
    [TestFixture]
    public class MainViewModelTests
    {
        private Mock<INavigationViewModel> _navigationVmMock;
        private MainViewModel _viewModel;
        private Mock<IMessenger> _mockMessenger;
        private List<Mock<IFriendEditViewModel>> _friendEditViewModelMocks;

        private readonly MockRepository _mockRepository = new MockRepository(MockBehavior.Loose);

        [SetUp]
        public void SetUp()
        {
            _navigationVmMock = new Mock<INavigationViewModel>();
            _friendEditViewModelMocks = new List<Mock<IFriendEditViewModel>>();
            _mockMessenger = _mockRepository.Create<IMessenger>();

            _viewModel = new MainViewModel(_navigationVmMock.Object, CreateFriendEditViewModel,
                _mockMessenger.Object);
        }

        [Test]
        public void ShouldCallTheLoadMethodOfTheNavigationViewModel()
        {
            _viewModel.Load();
            _navigationVmMock.Verify(vm => vm.Load(), Times.Once);
        }

        #region Private Methods

        private IFriendEditViewModel CreateFriendEditViewModel()
        {
            var friendEditViewModelMock = new Mock<IFriendEditViewModel>();
            _friendEditViewModelMocks.Add(friendEditViewModelMock);
            return friendEditViewModelMock.Object;
        }

        #endregion
    }
}
