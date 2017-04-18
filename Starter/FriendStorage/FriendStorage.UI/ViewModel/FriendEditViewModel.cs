using System;
using System.ComponentModel;
using System.Windows.Input;
using FriendStorage.Model;
using FriendStorage.UI.Command;
using FriendStorage.UI.DataProvider;
using FriendStorage.UI.Messages;
using FriendStorage.UI.Wrapper;
using GalaSoft.MvvmLight.Messaging;

namespace FriendStorage.UI.ViewModel
{
    public interface IFriendEditViewModel
    {
        FriendWrapper Friend { get; }
        void Load(int? friendId);
    }

    public class FriendEditViewModel : ViewModelBase, IFriendEditViewModel
    {
        #region Private Fields

        private IFriendDataProvider _dataProvider;
        private FriendWrapper _friend;
        private IMessenger _messenger;

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

        public ICommand DeleteCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }

        #endregion

        #region Constructor

        public FriendEditViewModel(IFriendDataProvider dataProvider, IMessenger messenger)
        {
            _dataProvider = dataProvider;
            _messenger = messenger;

            DeleteCommand = new DelegateCommand(OnDeleteExecute, OnDeleteCanExecute);
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }

        #endregion

        #region Public Methods

        public void Load(int? friendId)
        {
            var friend = friendId.HasValue
                ? _dataProvider.GetFriendById(friendId.Value)
                : new Friend();

            Friend = new FriendWrapper(friend);

            Friend.PropertyChanged += Friend_PropertyChanged;
            InvalidateCommands();
        }

        #endregion

        #region Private Methods

        private void OnDeleteExecute(object obj)
        {
            _dataProvider.DeleteFriend(Friend.Id);
            _messenger.Send(new FriendDeletedMessage(Friend.Id));
        }

        private bool OnDeleteCanExecute(object args)
        {
            // friend Id > 0 means that friend is not new
            return Friend != null && Friend.Id > 0;
        }

        private void OnSaveExecute(object obj)
        {
            _dataProvider.SaveFriend(Friend.Model);
            Friend.AcceptChanges();
            _messenger.Send(new FriendSavedMessage(Friend.Model));
        }

        private bool OnSaveCanExecute(object args)
        {
            return Friend != null && Friend.IsChanged;
        }

        private void Friend_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            InvalidateCommands();
        }

        private void InvalidateCommands()
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)DeleteCommand).RaiseCanExecuteChanged();
        }

        #endregion
    }
}
