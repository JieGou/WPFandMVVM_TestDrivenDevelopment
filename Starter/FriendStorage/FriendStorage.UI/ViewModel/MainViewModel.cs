using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;

namespace FriendStorage.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Private Fields

        private FriendEditViewModel _selectedFriendEditViewModel;
        private Func<IFriendEditViewModel> _friendEditVmCreator;
        private IMessenger _messenger;

        #endregion

        #region Properties

        public INavigationViewModel NavigationViewModel { get; set; }
        public ObservableCollection<IFriendEditViewModel> FriendEditViewModels { get; private set; }

        public FriendEditViewModel SelectedFriendEditViewModel
        {
            get { return _selectedFriendEditViewModel; }
            set { _selectedFriendEditViewModel = value; }
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
        }

        #endregion

        #region Public Methods

        public void Load()
        {
            NavigationViewModel.Load();
        }

        #endregion
    }
}
