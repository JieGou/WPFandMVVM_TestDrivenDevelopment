using FriendStorage.DataAccess;
using FriendStorage.Model;
using FriendStorage.UI.DataProvider;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Linq;
using FriendStorage.UI.Messages;

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

            MessageRegistration();
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

        /// <summary>
        /// Handle all Message registration for this viewmodel.
        /// </summary>
        private void MessageRegistration()
        {
            _messenger.Register<FriendSavedMessage>(this, HandleFriendSavedMessageReceived);
            _messenger.Register<FriendDeletedMessage>(this, HandleFriendDeletedMessageReceived);
        }

        private void HandleFriendSavedMessageReceived(FriendSavedMessage msg)
        {
            var friend = msg.Friend;
            var displayMember = $"{friend.FirstName} {friend.LastName}";
            var navigationItem = Friends.SingleOrDefault(n => n.Id == friend.Id);

            if (navigationItem != null)
            {
                navigationItem.DisplayMember = displayMember;
            }
            else
            {
                navigationItem = new NavigationItemViewModel(friend.Id, 
                    displayMember, _messenger);
                Friends.Add(navigationItem);
            }
        }

        private void HandleFriendDeletedMessageReceived(FriendDeletedMessage msg)
        {
            var navigationItem = Friends.SingleOrDefault(n => n.Id == msg.FriendId);
            Friends.Remove(navigationItem);
        }
    }
}
