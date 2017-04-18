using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using FriendStorage.UI.Command;
using FriendStorage.UI.Messages;
using GalaSoft.MvvmLight.Messaging;

namespace FriendStorage.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Private Fields

        private IFriendEditViewModel _selectedFriendEditViewModel;
        private Func<IFriendEditViewModel> _friendEditVmCreator;
        private IMessenger _messenger;

        #endregion

        #region Properties

        public INavigationViewModel NavigationViewModel { get; set; }
        public ObservableCollection<IFriendEditViewModel> FriendEditViewModels { get; private set; }
        public ICommand AddFriendCommand { get; private set; }
        public ICommand CloseFriendTabCommand { get; private set; }

        public IFriendEditViewModel SelectedFriendEditViewModel
        {
            get { return _selectedFriendEditViewModel; }
            set
            {
                _selectedFriendEditViewModel = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Constructor

        public MainViewModel(INavigationViewModel navigationViewModel, 
            Func<IFriendEditViewModel> friendEditVmCreator, IMessenger messenger)
        {
            NavigationViewModel = navigationViewModel;
            _friendEditVmCreator = friendEditVmCreator;
            _messenger = messenger;

            FriendEditViewModels = new ObservableCollection<IFriendEditViewModel>();
            CloseFriendTabCommand = new DelegateCommand(OnCloseFriendTabExecute);
            AddFriendCommand = new DelegateCommand(OnAddFriendExecute);

            MessageRegistration();
        }

        #endregion

        #region Public Methods

        public void Load()
        {
            NavigationViewModel.Load();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Handles all event registration for this view model.
        /// </summary>
        private void MessageRegistration()
        {
            _messenger.Register<OpenFriendEditViewMessage>(this, HandleOpenFriendEditViewMessageReceived);
        }

        private void HandleOpenFriendEditViewMessageReceived(OpenFriendEditViewMessage msg)
        {
            var friendEditVm = FriendEditViewModels.SingleOrDefault(vm => vm.Friend.Id == msg.FriendId);

            if (friendEditVm != null) return;

            SetSelectedFriendEditVm(msg.FriendId);
        }

        private void OnAddFriendExecute(object obj)
        {
            SetSelectedFriendEditVm(null);
        }

        private void OnCloseFriendTabExecute(object obj)
        {
            var friendEditVm = (IFriendEditViewModel) obj;
            FriendEditViewModels.Remove(friendEditVm);
        }

        private void SetSelectedFriendEditVm(int? friendId)
        {
            var friendEditVm = _friendEditVmCreator();
            FriendEditViewModels.Add(friendEditVm);

            if (friendId.HasValue)
            {
                friendEditVm.Load(friendId.Value);
            }
            else
            {
                friendEditVm.Load(null);
            }

            SelectedFriendEditViewModel = friendEditVm;
        }

        #endregion
    }
}
