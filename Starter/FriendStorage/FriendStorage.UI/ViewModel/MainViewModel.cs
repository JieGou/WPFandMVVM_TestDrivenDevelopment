using System;
using FriendStorage.DataAccess;
using FriendStorage.UI.DataProvider;

namespace FriendStorage.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Constructor

        public MainViewModel()
        {
            var provider = new NavigationDataProvider(() => new FileDataService());
            NavigationViewModel = new NavigationViewModel(provider);
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
