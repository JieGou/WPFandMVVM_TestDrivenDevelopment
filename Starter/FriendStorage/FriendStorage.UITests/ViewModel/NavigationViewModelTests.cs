using System;
using System.Collections.Generic;
using FriendStorage.Model;
using FriendStorage.UI.DataProvider;
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

        [SetUp]
        public void SetUp()
        {
            var messenger = new Mock<IMessenger>();
            _navigationDataProviderMock = new Mock<INavigationDataProvider>();
            _navigationDataProviderMock.Setup(dp => dp.GetAllFriends())
                .Returns(new List<LookupItem>
                {
                    new LookupItem {Id = 1, DisplayMember = "Julia"},
                    new LookupItem {Id = 2, DisplayMember = "Thomas"}
                });

            _viewModel = new NavigationViewModel(
                _navigationDataProviderMock.Object, messenger.Object);
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
    }
}
