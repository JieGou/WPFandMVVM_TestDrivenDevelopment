using System;

namespace FriendStorage.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Constructor

        public MainViewModel()
        {
            NavigationViewModel = new NavigationViewModel();
        }

        #endregion

        #region Public Methods

        public void Load()
        {
            NavigationViewModel.Load();
        }

        #endregion

        #region Properties

        public NavigationViewModel NavigationViewModel { get; set; }

        #endregion
    }
}
