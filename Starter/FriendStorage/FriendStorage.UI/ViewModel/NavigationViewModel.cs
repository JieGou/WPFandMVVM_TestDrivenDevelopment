using FriendStorage.DataAccess;
using FriendStorage.Model;
using System;
using System.Collections.ObjectModel;

namespace FriendStorage.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase
    {
        #region Properties

        public ObservableCollection<Friend> Friends { get; private set; }

        #endregion

        #region Constructor

        public NavigationViewModel()
        {
            Friends = new ObservableCollection<Friend>();   
        }

        #endregion

        #region Public Methods

        public void Load()
        {
            var dataService = new FileDataService();
            foreach (var friend in dataService.GetAllFriends())
            {
                Friends.Add(friend);
            }
        }

        #endregion
    }
}
