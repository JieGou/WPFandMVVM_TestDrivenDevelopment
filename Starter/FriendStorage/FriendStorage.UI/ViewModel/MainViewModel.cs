namespace FriendStorage.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Constructor

        public MainViewModel(INavigationViewModel navigationViewModel)
        {
            NavigationViewModel = navigationViewModel;
        }

        #endregion

        #region Public Methods

        public void Load()
        {
            NavigationViewModel.Load();
        }

        #endregion

        #region Properties

        public INavigationViewModel NavigationViewModel { get; set; }

        #endregion
    }
}
