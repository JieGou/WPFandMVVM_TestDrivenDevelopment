using System;
using FriendStorage.Model;

namespace FriendStorage.UI.ViewModel
{
    public interface IFriendEditViewModel
    {
        Friend Friend { get; }
        void Load(int friendId);
    }

    public class FriendEditViewModel : ViewModelBase, IFriendEditViewModel
    {
        #region Properties

        public Friend Friend { get; }
        
        #endregion

        #region Public Methods

        public void Load(int friendId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
