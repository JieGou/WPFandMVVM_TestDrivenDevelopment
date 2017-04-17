using System;
using FriendStorage.Model;
using FriendStorage.UI.DataProvider;

namespace FriendStorage.UI.ViewModel
{
    public interface IFriendEditViewModel
    {
        Friend Friend { get; }
        void Load(int friendId);
    }

    public class FriendEditViewModel : ViewModelBase, IFriendEditViewModel
    {
        #region Private Fields

        private IFriendDataProvider _dataProvider;
        private Friend _friend;

        #endregion

        #region Properties

        public Friend Friend
        {
            get { return _friend; }
            private set
            {
                _friend = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Constructor

        public FriendEditViewModel(IFriendDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        #endregion

        #region Public Methods

        public void Load(int friendId)
        {
            Friend = _dataProvider.GetFriendById(friendId);
        }

        #endregion
    }
}
