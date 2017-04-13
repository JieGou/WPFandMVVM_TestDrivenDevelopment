using FriendStorage.DataAccess;
using FriendStorage.Model;
using System;
using System.Collections.Generic;

namespace FriendStorage.UI.DataProvider
{
    public class NavigationDataProvider : INavigationDataProvider
    {
        #region Private Fields

        private readonly Func<IDataService> _dataServiceCreator;
        
        #endregion

        #region Constructor

        public NavigationDataProvider(Func<IDataService> dataServiceCreator)
        {
            _dataServiceCreator = dataServiceCreator;
        }

        #endregion

        #region Public Methods

        public IEnumerable<Friend> GetAllFriends()
        {
            using (var dataService = _dataServiceCreator())
            {
                return dataService.GetAllFriends();
            }
        }

        #endregion
    }
}
