using System;
using System.ComponentModel;
using System.Windows.Input;
using FriendStorage.Model;
using FriendStorage.UI.Command;
using FriendStorage.UI.DataProvider;
using FriendStorage.UI.Wrapper;

namespace FriendStorage.UI.ViewModel
{
    public interface IFriendEditViewModel
    {
        FriendWrapper Friend { get; }
        void Load(int friendId);
    }

    public class FriendEditViewModel : ViewModelBase, IFriendEditViewModel
    {
        #region Private Fields

        private IFriendDataProvider _dataProvider;
        private FriendWrapper _friend;

        #endregion

        #region Properties

        public FriendWrapper Friend
        {
            get { return _friend; }
            private set
            {
                _friend = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; private set; }

        #endregion

        #region Constructor

        public FriendEditViewModel(IFriendDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }

        #endregion

        #region Public Methods

        public void Load(int friendId)
        {
            var friend = _dataProvider.GetFriendById(friendId);
            Friend = new FriendWrapper(friend);

            Friend.PropertyChanged += Friend_PropertyChanged;
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        #endregion

        #region Private Methods

        private void OnSaveExecute(object obj)
        {
            _dataProvider.SaveFriend(Friend.Model);
            Friend.AcceptChanges();
        }

        private bool OnSaveCanExecute(object args)
        {
            return Friend != null && Friend.IsChanged;
        }

        private void Friend_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        #endregion
    }
}
