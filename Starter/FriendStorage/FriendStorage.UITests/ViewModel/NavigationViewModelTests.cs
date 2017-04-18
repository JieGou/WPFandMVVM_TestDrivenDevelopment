using System;
using System.Collections.Generic;
using System.Linq;
using FriendStorage.Model;
using FriendStorage.UI.DataProvider;
using FriendStorage.UI.Messages;
using FriendStorage.UI.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using Moq;
using NUnit.Framework;

namespace FriendStorage.UITests.ViewModel
{
    [TestFixture]
    public class NavigationViewModelTests
    {
        private Mock<INavigationDataProvider> _navigationDataProviderMock;
        private NavigationViewModel _viewModel;
        private Messenger _testMessenger;

        [SetUp]
        public void SetUp()
        {
            _testMessenger = new Messenger();
                
            _navigationDataProviderMock = new Mock<INavigationDataProvider>();
            _navigationDataProviderMock.Setup(dp => dp.GetAllFriends())
                .Returns(new List<LookupItem>
                {
                    new LookupItem {Id = 1, DisplayMember = "Julia"},
                    new LookupItem {Id = 2, DisplayMember = "Thomas"}
                });

            _viewModel = new NavigationViewModel(
                _navigationDataProviderMock.Object, _testMessenger);
        }

        [Test]
        public void ShouldLoadFriends()
        {
            _viewModel.Load();
            Assert.AreEqual(2, _viewModel.Friends.Count);
        }

        [Test]
        public void ShouldLoadFriendsOnlyOnce()
        {
            _viewModel.Load();
            _viewModel.Load();
            Assert.AreEqual(2, _viewModel.Friends.Count);
        }

        [Test]
        public void ShouldUpdateNavigationItemWhenFriendIsSaved()
        {
            _viewModel.Load();
            var navigationItem = _viewModel.Friends.First();

            var friendId = navigationItem.Id;

            _testMessenger.Send(new FriendSavedMessage(new Friend
            {
                Id = friendId,
                FirstName = "Anna",
                LastName = "Banana"
            }));

            Assert.AreEqual("Anna Banana", navigationItem.DisplayMember);
        }
    }
}
