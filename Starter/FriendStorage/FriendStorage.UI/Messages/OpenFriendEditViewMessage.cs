namespace FriendStorage.UI.Messages
{
    public class OpenFriendEditViewMessage
    {
        #region Properties

        public int FriendId { get; private set; }
        
        #endregion

        #region Constructor

        public OpenFriendEditViewMessage(int friendId)
        {
            FriendId = friendId;
        }

        #endregion
    }
}
