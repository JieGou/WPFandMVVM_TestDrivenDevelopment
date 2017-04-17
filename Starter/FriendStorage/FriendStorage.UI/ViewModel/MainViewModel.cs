using System;
using System.Collections.ObjectModel;
using System.Linq;
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
            FriendEditViewModels = new ObservableCollection<IFriendEditViewModel>();
            _friendEditVmCreator = friendEditVmCreator;
            _messenger = messenger;

            MessageRegistration();
        }

        #endregion

        #region Public Methods

        public void Load()
        {
            NavigationViewModel.Load();
        }

        #endregion

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

            friendEditVm = _friendEditVmCreator();
            FriendEditViewModels.Add(friendEditVm);
            friendEditVm.Load(msg.FriendId);
            SelectedFriendEditViewModel = friendEditVm;
        }
    }
}
