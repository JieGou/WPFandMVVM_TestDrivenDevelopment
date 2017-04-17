using Autofac;
using FriendStorage.DataAccess;
using FriendStorage.UI.DataProvider;
using FriendStorage.UI.View;
using FriendStorage.UI.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace FriendStorage.UI.Startup
{
    public class BootStrapper
    {
        public IContainer BootStrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();

            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<NavigationDataProvider>().As<INavigationDataProvider>();
            builder.RegisterType<FileDataService>().As<IDataService>();
            builder.RegisterType<Messenger>().As<IMessenger>().SingleInstance();
            builder.RegisterType<FriendDataProvider>().As<IFriendDataProvider>();
            builder.RegisterType<FriendEditViewModel>().As<IFriendEditViewModel>();

            return builder.Build();
        }
    }
}
