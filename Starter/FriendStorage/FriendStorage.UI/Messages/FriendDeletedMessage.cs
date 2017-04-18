using FriendStorage.Model;

namespace FriendStorage.UI.Messages
{
    public class FriendDeletedMessage
    {
        public int FriendId { get; private set; }

        public FriendDeletedMessage(int friendId)
        {
            FriendId = friendId;
        }
    }
}
