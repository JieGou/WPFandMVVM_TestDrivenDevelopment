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

            var mockRepository = new MockRepository(MockBehavior.Loose);
            var mockMessenger = mockRepository.Create<IMessenger>();
            var viewModel = new NavigationItemViewModel(friendId, "Dicky", mockMessenger.Object);

            viewModel.OpenFriendEditViewCommand.Execute(null);

            mockMessenger.Verify(p => p.Send(It.Is<OpenFriendEditViewMessage>(m => m.FriendId == friendId)), 
                Times.Once);
        }
    }
}
