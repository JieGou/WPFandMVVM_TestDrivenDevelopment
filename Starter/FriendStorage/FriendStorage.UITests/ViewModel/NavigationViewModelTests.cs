using System;
using System.Collections.Generic;
using FriendStorage.Model;
using FriendStorage.UI.DataProvider;
using FriendStorage.UI.ViewModel;
using NUnit.Framework;

namespace FriendStorage.UITests.ViewModel
{
    public class NavigationViewModelTests
    {
        [Test]
        public void ShouldLoadFriends()
        {
            var viewModel = new NavigationViewModel(new NavigationDataProviderMock());
            viewModel.Load();
            Assert.AreEqual(2, viewModel.Friends.Count);
        }

        [Test]
        public void ShouldLoadFriendsOnlyOnce()
        {
            var viewModel = new NavigationViewModel(new NavigationDataProviderMock());
            viewModel.Load();
            viewModel.Load();
            Assert.AreEqual(2, viewModel.Friends.Count);
        }
    }

    public class NavigationDataProviderMock : INavigationDataProvider
    {
        #region Public Methods

        public IEnumerable<Friend> GetAllFriends()
        {
            yield return new Friend {Id = 1, FirstName = "Julia"};
            yield return new Friend {Id = 2, FirstName = "Thomas"};
        }

        #endregion
    }
}
