using System.ComponentModel;
using System.Windows.Input;
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
        void Load(int friendId);
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

        public ICommand SaveCommand { get; private set; }

        #endregion

        #region Constructor

        public FriendEditViewModel(IFriendDataProvider dataProvider, IMessenger messenger)
        {
            _dataProvider = dataProvider;
            _messenger = messenger;
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
            _messenger.Send(new FriendSavedMessage(Friend.Model));
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
