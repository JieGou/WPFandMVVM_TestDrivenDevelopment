using System.Collections.Generic;
using System.Linq;
using FriendStorage.Model;
using FriendStorage.UI.Wrapper;
using Xunit;

namespace FriendStorage.UI.Tests.Wrapper
{
    public class ChangeNotificationCollectionProperty
    {
        public ChangeNotificationCollectionProperty()
        {
            _friendEmail = new FriendEmail {Email = "thomas@thomasclaudiushuber.com"};
            _friend = new Friend
            {
                FirstName = "Thomas",
                Address = new Address(),
                Emails = new List<FriendEmail>
                {
                    new FriendEmail {Email = "julia@juhu-design.com"},
                    _friendEmail
                }
            };
        }

        private readonly Friend _friend;
        private readonly FriendEmail _friendEmail;

        private void CheckIfModelEmailsCollectionIsInSync(FriendWrapper wrapper)
        {
            Assert.Equal(_friend.Emails.Count, wrapper.Emails.Count);
            Assert.True(_friend.Emails.All(e =>
                wrapper.Emails.Any(we => we.Model == e)));
        }

        [Fact]
        public void ShouldBeInSyncAfterAddingEmail()
        {
            _friend.Emails.Remove(_friendEmail);
            var wrapper = new FriendWrapper(_friend);

            wrapper.Emails.Add(new FriendEmailWrapper(_friendEmail));

            CheckIfModelEmailsCollectionIsInSync(wrapper);
        }

        [Fact]
        public void ShouldBeInSyncAfterRemovingEmail()
        {
            var wrapper = new FriendWrapper(_friend);

            var emailToRemove = wrapper.Emails.Single(ew => ew.Model == _friendEmail);
            wrapper.Emails.Remove(emailToRemove);

            CheckIfModelEmailsCollectionIsInSync(wrapper);
        }

        [Fact]
        public void ShouldInitializeEmailsProperty()
        {
            var wrapper = new FriendWrapper(_friend);

            Assert.NotNull(wrapper.Emails);
            CheckIfModelEmailsCollectionIsInSync(wrapper);
        }
    }
}