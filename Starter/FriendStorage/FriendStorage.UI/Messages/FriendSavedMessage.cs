using FriendStorage.Model;

namespace FriendStorage.UI.Messages
{
    public class FriendSavedMessage
    {
        public Friend Friend { get; private set; }

        public FriendSavedMessage(Friend friend)
        {
            Friend = friend;
        }
    }
}
