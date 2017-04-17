using System.Collections.Generic;
using System.Linq;
using FriendStorage.UI.Messages;
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
        private IMessenger _testMessenger;
        private List<Mock<IFriendEditViewModel>> _friendEditViewModelMocks;

        private readonly MockRepository _mockRepository = new MockRepository(MockBehavior.Loose);

        [SetUp]
        public void SetUp()
        {
            _navigationVmMock = new Mock<INavigationViewModel>();
            _friendEditViewModelMocks = new List<Mock<IFriendEditViewModel>>();
            _testMessenger = new Messenger();

            _viewModel = new MainViewModel(_navigationVmMock.Object, CreateFriendEditViewModel,
                _testMessenger);
        }

        [Test]
        public void ShouldCallTheLoadMethodOfTheNavigationViewModel()
        {
            _viewModel.Load();
            _navigationVmMock.Verify(vm => vm.Load(), Times.Once);
        }

        [Test]
        public void ShouldAddFriendEditViewModelAndLoadAndSelectIt()
        {
            const int friendId = 7;
            _testMessenger.Send(new OpenFriendEditViewMessage(friendId));

            Assert.AreEqual(1, _viewModel.FriendEditViewModels.Count);

            var friendEditVm = _viewModel.FriendEditViewModels.First();
            Assert.AreEqual(friendEditVm, _viewModel.SelectedFriendEditViewModel);

            _friendEditViewModelMocks.First().Verify(vm => vm.Load(friendId), Times.Once);
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
