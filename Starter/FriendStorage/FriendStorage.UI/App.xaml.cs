using System;
using System.Runtime.InteropServices;
using FriendStorage.DataAccess;
using FriendStorage.UI.View;
using FriendStorage.UI.ViewModel;
using System.Windows;
using FriendStorage.UI.DataProvider;

namespace FriendStorage.UI
{
    public partial class App : Application
    {
        #region Protected Methods

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Func<FileDataService> dataService = () => new FileDataService();
            var navigationDataProvider = new NavigationDataProvider(dataService);
            var navigationVM = new NavigationViewModel(navigationDataProvider);
            var mainVM = new MainViewModel(navigationVM);

            var mainWindow = new MainWindow(mainVM);
            mainWindow.Show();
        }

        #endregion
    }
}
