using FriendStorage.UI.Messages;
using FriendStorage.UI.ViewModel;
using FriendStorage.UITests.Extensions;
using GalaSoft.MvvmLight.Messaging;
using Moq;
using NUnit.Framework;

namespace FriendStorage.UITests.ViewModel
{
    [TestFixture]
    public class NavigationItemViewModelTest
    {
        private const int _friendId = 7;
        private MockRepository _mockRepository;
        private Mock<IMessenger> _mockMessenger;
        private NavigationItemViewModel _viewModel;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new MockRepository(MockBehavior.Loose);
            _mockMessenger = _mockRepository.Create<IMessenger>();
            _viewModel = new NavigationItemViewModel(_friendId, "Dicky", _mockMessenger.Object);
        }

        [Test]
        public void ShouldPublishOpenFriendEditViewEvent()
        {
            _viewModel.OpenFriendEditViewCommand.Execute(null);
            _mockMessenger.Verify(p => p.Send(It.Is<OpenFriendEditViewMessage>(m => m.FriendId == _friendId)), 
                Times.Once);
        }

        [Test]
        public void ShouldRaisePropertyChangedEventForDisplayMember()
        {
            var fired = _viewModel.IsPropertyChangedFired(() =>
            {
                _viewModel.DisplayMember = "Changed";
            }, nameof(_viewModel.DisplayMember));

            Assert.IsTrue(fired);
        }
    }
}
