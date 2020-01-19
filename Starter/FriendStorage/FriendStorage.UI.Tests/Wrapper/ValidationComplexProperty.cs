using System.Collections.Generic;
using FriendStorage.Model;
using FriendStorage.UI.Wrapper;
using Xunit;

namespace FriendStorage.UI.Tests.Wrapper
{
    public class ValidationComplexProperty
    {
        public ValidationComplexProperty()
        {
            _friend = new Friend
            {
                FirstName = "Thomas",
                Address = new Address {City = "Müllheim"},
                Emails = new List<FriendEmail>()
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
                if (e.PropertyName == nameof(wrapper.IsValid)) fired = true;
            };
            wrapper.Address.City = "";
            Assert.True(fired);

            fired = false;
            wrapper.Address.City = "Salt Lake City";
            Assert.True(fired);
        }


        [Fact]
        public void ShouldSetIsValidOfRoot()
        {
            var wrapper = new FriendWrapper(_friend);
            Assert.True(wrapper.IsValid);

            wrapper.Address.City = "";
            Assert.False(wrapper.IsValid);

            wrapper.Address.City = "Salt Lake City";
            Assert.True(wrapper.IsValid);
        }

        [Fact]
        public void ShouldSetIsValidOfRootAfterInitialization()
        {
            _friend.Address.City = "";
            var wrapper = new FriendWrapper(_friend);
            Assert.False(wrapper.IsValid);

            wrapper.Address.City = "Salt Lake City";
            Assert.True(wrapper.IsValid);
        }
    }
}