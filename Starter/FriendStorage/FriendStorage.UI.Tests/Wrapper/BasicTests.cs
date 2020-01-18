using System;
using System.Collections.Generic;
using FriendStorage.Model;
using FriendStorage.UI.Wrapper;
using Xunit;

namespace FriendStorage.UI.Tests.Wrapper
{
    public class BasicTests
    {
        public BasicTests()
        {
            _friend = new Friend
            {
                FirstName = "Thomas",
                Address = new Address(),
                Emails = new List<FriendEmail>()
            };
        }

        private readonly Friend _friend;

        [Fact]
        public void ShouldContainModelInModelProperty()
        {
            var wrapper = new FriendWrapper(_friend);

            Assert.Equal(_friend, wrapper.Model);
        }

        [Fact]
        public void ShouldGetValueOfUnderlyingModelProperty()
        {
            var wrapper = new FriendWrapper(_friend);

            Assert.Equal(_friend.FirstName, wrapper.FirstName);
        }

        [Fact]
        public void ShouldSetValueOfUnderlyingModelProperty()
        {
            var wrapper = new FriendWrapper(_friend) {FirstName = "Julia"};

            Assert.Equal("Julia", _friend.FirstName);
        }

        [Fact]
        public void ShouldThrowArgumentExceptionIfAddressIsNull()
        {
            _friend.Address = null;

            var exception = Assert.Throws<ArgumentException>(() => new FriendWrapper(_friend));

            Assert.Equal("Address cannot be null", exception.Message);
        }

        [Fact]
        public void ShouldThrowArgumentExceptionIfEmailsCollectionIsNull()
        {
            _friend.Emails = null;

            var exception = Assert.Throws<ArgumentException>(() => new FriendWrapper(_friend));

            Assert.Equal("Emails cannot be null", exception.Message);
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionIfModelIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new FriendWrapper(null));

            Assert.Equal("model", exception.ParamName);
        }
    }
}