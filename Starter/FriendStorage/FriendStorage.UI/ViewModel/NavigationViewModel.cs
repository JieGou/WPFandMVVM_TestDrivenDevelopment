using FriendStorage.DataAccess;
using FriendStorage.Model;
using FriendStorage.UI.DataProvider;
using System.Collections.ObjectModel;

namespace FriendStorage.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase
    {
        #region Private Fields

        private INavigationDataProvider _dataProvider;

        #endregion

        #region Properties

        public ObservableCollection<Friend> Friends { get; private set; }

        #endregion

        #region Constructor

        public NavigationViewModel(INavigationDataProvider dataProvider)
        {
            Friends = new ObservableCollection<Friend>();
            _dataProvider = dataProvider;
        }

        #endregion

        #region Public Methods

        public void Load()
        {
            Friends.Clear();
            foreach (var friend in _dataProvider.GetAllFriends())
            {
                Friends.Add(friend);
            }
        }

        #endregion
    }
}
