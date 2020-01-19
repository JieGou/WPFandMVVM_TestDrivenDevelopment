using System.Collections.Generic;
using System.Linq;
using FriendStorage.Model;
using FriendStorage.UI.Wrapper;
using Xunit;

namespace FriendStorage.UI.Tests.Wrapper
{
    public class ValidationCollectionProperty
    {
        public ValidationCollectionProperty()
        {
            _friend = new Friend
            {
                FirstName = "Thomas",
                Address = new Address {City = "Müllheim"},
                Emails = new List<FriendEmail>
                {
                    new FriendEmail {Email = "thomas@thomasclaudiushuber.com"},
                    new FriendEmail {Email = "julia@juhu-design.com"}
                }
            };
        }

        private readonly Friend _friend;

        [Fact]
        public void ShouldRaisePropertyChangedEventForIsValidOfRoot()
        {
            var fired = false;
            var wrapper = new FriendWrapper(_friend);
            wrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "IsValid") fired = true;
            };
            wrapper.Emails.First().Email = "";
            Assert.True(fired);

            fired = false;
            wrapper.Emails.First().Email = "thomas@thomasclaudiushuber.com";
            Assert.True(fired);
        }

        [Fact]
        public void ShouldRaisePropertyChangedEventForIsValidOfRootWhenAddingInvalidItem()
        {
            var fired = false;
            var wrapper = new FriendWrapper(_friend);
            wrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "IsValid") fired = true;
            };

            var emailToAdd = new FriendEmailWrapper(new FriendEmail());
            wrapper.Emails.Add(emailToAdd);
            Assert.True(fired);

            fired = false;
            emailToAdd.Email = "thomas@thomasclaudiushuber.com";
            Assert.True(fired);
        }

        [Fact]
        public void ShouldRaisePropertyChangedEventForIsValidOfRootWhenRemovingInvalidItem()
        {
            var fired = false;
            var wrapper = new FriendWrapper(_friend);
            wrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "IsValid") fired = true;
            };
            wrapper.Emails.First().Email = "";
            Assert.True(fired);

            fired = false;
            wrapper.Emails.Remove(wrapper.Emails.First());
            Assert.True(fired);
        }

        [Fact]
        public void ShouldSetIsValidOfRoot()
        {
            var wrapper = new FriendWrapper(_friend);
            Assert.True(wrapper.IsValid);

            wrapper.Emails.First().Email = "";
            Assert.False(wrapper.IsValid);

            wrapper.Emails.First().Email = "thomas@thomasclaudiushuber.com";
            Assert.True(wrapper.IsValid);
        }

        [Fact]
        public void ShouldSetIsValidOfRootWhenAddingInvalidItem()
        {
            var emailToAdd = new FriendEmailWrapper(new FriendEmail());
            var wrapper = new FriendWrapper(_friend);
            Assert.True(wrapper.IsValid);
            ;
            wrapper.Emails.Add(emailToAdd);
            Assert.False(wrapper.IsValid);
            emailToAdd.Email = "thomas@thomasclaudiushuber.com";
            Assert.True(wrapper.IsValid);
        }

        [Fact]
        public void ShouldSetIsValidOfRootWhenInitializing()
        {
            _friend.Emails.First().Email = "";
            var wrapper = new FriendWrapper(_friend);
            Assert.False(wrapper.IsValid);
            Assert.False(wrapper.HasErrors);
            Assert.True(wrapper.Emails.First().HasErrors);
        }

        [Fact]
        public void ShouldSetIsValidOfRootWhenRemovingInvalidItem()
        {
            var wrapper = new FriendWrapper(_friend);
            Assert.True(wrapper.IsValid);

            wrapper.Emails.First().Email = "";
            Assert.False(wrapper.IsValid);

            wrapper.Emails.Remove(wrapper.Emails.First());
            Assert.True(wrapper.IsValid);
        }
    }
}