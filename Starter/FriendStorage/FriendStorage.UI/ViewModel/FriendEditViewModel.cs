using System;
using FriendStorage.Model;
using FriendStorage.UI.DataProvider;

namespace FriendStorage.UI.ViewModel
{
    public interface IFriendEditViewModel
    {
        Friend Friend { get; }
        void Load(int friendId);
    }

    public class FriendEditViewModel : ViewModelBase, IFriendEditViewModel
    {
        private IFriendDataProvider _dataProvider;

        #region Properties

        public Friend Friend { get; }
        
        #endregion

        public FriendEditViewModel(IFriendDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        #region Public Methods

        public void Load(int friendId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
