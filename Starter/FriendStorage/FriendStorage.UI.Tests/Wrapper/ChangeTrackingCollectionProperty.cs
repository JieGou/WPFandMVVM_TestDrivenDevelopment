using System.Collections.Generic;
using System.Linq;
using FriendStorage.Model;
using FriendStorage.UI.Wrapper;
using Xunit;

namespace FriendStorage.UI.Tests.Wrapper
{
    public class ChangeTrackingCollectionProperty
    {
        public ChangeTrackingCollectionProperty()
        {
            _friend = new Friend
            {
                FirstName = "Thomas",
                Address = new Address(),
                Emails = new List<FriendEmail>
                {
                    new FriendEmail {Email = "thomas@thomasclaudiushuber.com"},
                    new FriendEmail {Email = "julia@juhu-design.com"}
                }
            };
        }

        private readonly Friend _friend;

        [Fact]
        public void ShouldAcceptChanges()
        {
            var wrapper = new FriendWrapper(_friend);

            var emailToModify = wrapper.Emails.First();
            emailToModify.Email = "modified@thomasclaudiushuber.com";

            Assert.True(wrapper.IsChanged);

            wrapper.AcceptChanges();

            Assert.False(wrapper.IsChanged);
            Assert.Equal("modified@thomasclaudiushuber.com", emailToModify.Email);
            Assert.Equal("modified@thomasclaudiushuber.com", emailToModify.EmailOriginalValue);
        }

        [Fact]
        public void ShouldRaisePropertyChangedEventForIsChangedPropertyOfFriendWrapper()
        {
            var fired = false;
            var wrapper = new FriendWrapper(_friend);
            wrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(wrapper.IsChanged)) fired = true;
            };

            wrapper.Emails.First().Email = "modified@thomasclaudiushuber.com";

            Assert.True(fired);
        }

        [Fact]
        public void ShouldRejectChanges()
        {
            var wrapper = new FriendWrapper(_friend);
            var emailToModify = wrapper.Emails.First();

            emailToModify.Email = "modified@thomasclaudiushuber.com";

            Assert.True(wrapper.IsChanged);

            wrapper.RejectChanges();

            Assert.False(wrapper.IsChanged);
            Assert.Equal("thomas@thomasclaudiushuber.com", emailToModify.Email);
            Assert.Equal("thomas@thomasclaudiushuber.com", emailToModify.EmailOriginalValue);
        }

        [Fact]
        public void ShouldSetIsChangedOfFriendWrapper()
        {
            var wrapper = new FriendWrapper(_friend);
            var emailToModify = wrapper.Emails.First();

            emailToModify.Email = "modified@thomasclaudiushuber.com";
            Assert.True(wrapper.IsChanged);

            emailToModify.Email = "thomas@thomasclaudiushuber.com";
            Assert.False(wrapper.IsChanged);
        }
    }
}