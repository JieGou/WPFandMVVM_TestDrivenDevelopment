using FriendStorage.UI.ViewModel;
using NUnit.Framework;

namespace FriendStorage.UITests.ViewModel
{
    public class MainViewModelTests
    {
        [Test]
        public void ShouldCallTheLoadMethodOfTheNavigationViewModel()
        {
            var navigationVmMock = new NavigationViewModelMock();
            var viewModel = new MainViewModel(navigationVmMock);
            viewModel.Load();

            Assert.True(navigationVmMock.LoadHasBeenCalled);
        }
    }

    public class NavigationViewModelMock : INavigationViewModel
    {
        #region Properties

        public bool LoadHasBeenCalled { get; set; }
        
        #endregion

        #region Public Methods

        public void Load()
        {
            LoadHasBeenCalled = true;
        }

        #endregion
    }
}
