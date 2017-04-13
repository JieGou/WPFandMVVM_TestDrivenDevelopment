using FriendStorage.DataAccess;
using FriendStorage.Model;
using FriendStorage.UI.DataProvider;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;

namespace FriendStorage.UI.ViewModel
{
    public interface INavigationViewModel
    {
        void Load();
    }

    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        #region Private Fields

        private INavigationDataProvider _dataProvider;
        private IMessenger _messenger;

        #endregion

        #region Properties

        public ObservableCollection<NavigationItemViewModel> Friends { get; private set; }

        #endregion

        #region Constructor

        public NavigationViewModel(INavigationDataProvider dataProvider, IMessenger messenger)
        {
            Friends = new ObservableCollection<NavigationItemViewModel>();
            _dataProvider = dataProvider;
            _messenger = messenger;
        }

        #endregion

        #region Public Methods

        public void Load()
        {
            Friends.Clear();
            foreach (var f in _dataProvider.GetAllFriends())
            {
                Friends.Add(new NavigationItemViewModel(f.Id, f.DisplayMember, _messenger));
            }
        }

        #endregion
    }
}
