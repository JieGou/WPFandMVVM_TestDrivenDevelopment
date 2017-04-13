using Autofac;
using FriendStorage.DataAccess;
using FriendStorage.UI.View;
using FriendStorage.UI.ViewModel;
using FriendStorage.UI.DataProvider;
using FriendStorage.UI.Startup;
using System;
using System.Windows;

namespace FriendStorage.UI
{
    public partial class App : Application
    {
        #region Protected Methods

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var bootStrapper = new BootStrapper();
            var container = bootStrapper.BootStrap();

            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }

        #endregion
    }
}
