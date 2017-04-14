using FriendStorage.UI.Messages;
using FriendStorage.UI.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using Moq;
using NUnit.Framework;

namespace FriendStorage.UITests.ViewModel
{
    [TestFixture]
    public class NavigationItemViewModelTest
    {
        [Test]
        public void ShouldPublishOpenFriendEditViewEvent()
        {
            const int friendId = 7;

            var messengerMock = new Mock<IMessenger>();
            var viewModel = new NavigationItemViewModel(friendId, "Thomas", messengerMock.Object);
            var message = new OpenFriendEditViewMessage(friendId);

            viewModel.OpenFriendEditViewCommand.Execute(null);

            messengerMock.Verify(m => m.Send(message), Times.Once);
        }
    }
}
