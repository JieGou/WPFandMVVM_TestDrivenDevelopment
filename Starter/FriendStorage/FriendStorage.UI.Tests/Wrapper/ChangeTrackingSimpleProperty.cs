using System.Collections.Generic;
using FriendStorage.Model;
using FriendStorage.UI.Wrapper;
using Xunit;

namespace FriendStorage.UI.Tests.Wrapper
{
    public class ChangeTrackingSimpleProperty
    {
        public ChangeTrackingSimpleProperty()
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
        public void ShouldAcceptChanges()
        {
            var wrapper = new FriendWrapper(_friend) {FirstName = "Julia"};

            Assert.Equal("Julia", wrapper.FirstName);
            Assert.Equal("Thomas", wrapper.FirstNameOriginalValue);
            Assert.True(wrapper.FirstNameIsChanged);
            Assert.True(wrapper.IsChanged);

            wrapper.AcceptChanges();

            Assert.Equal("Julia", wrapper.FirstName);
            Assert.Equal("Julia", wrapper.FirstNameOriginalValue);
            Assert.False(wrapper.FirstNameIsChanged);
            Assert.False(wrapper.IsChanged);
        }

        [Fact]
        public void ShouldRaisePropertyChangedEventForFirstNameIsChanged()
        {
            var fired = false;
            var wrapper = new FriendWrapper(_friend);
            wrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(wrapper.FirstNameIsChanged)) fired = true;
            };

            wrapper.FirstName = "Julia";

            Assert.True(fired);
        }

        [Fact]
        public void ShouldRaisePropertyChangedEventForIsChanged()
        {
            var fired = false;
            var wrapper = new FriendWrapper(_friend);
            wrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(wrapper.IsChanged)) fired = true;
            };

            wrapper.FirstName = "Julia";

            Assert.True(fired);
        }

        [Fact]
        public void ShouldRejectChanges()
        {
            var wrapper = new FriendWrapper(_friend) {FirstName = "Julia"};

            Assert.Equal("Julia", wrapper.FirstName);
            Assert.Equal("Thomas", wrapper.FirstNameOriginalValue);
            Assert.True(wrapper.FirstNameIsChanged);
            Assert.True(wrapper.IsChanged);

            wrapper.RejectChanges();

            Assert.Equal("Thomas", wrapper.FirstName);
            Assert.Equal("Thomas", wrapper.FirstNameOriginalValue);
            Assert.False(wrapper.FirstNameIsChanged);
            Assert.False(wrapper.IsChanged);
        }

        [Fact]
        public void ShouldSetIsChanged()
        {
            var wrapper = new FriendWrapper(_friend);
            Assert.False(wrapper.FirstNameIsChanged);
            Assert.False(wrapper.IsChanged);

            wrapper.FirstName = "Julia";
            Assert.True(wrapper.FirstNameIsChanged);
            Assert.True(wrapper.IsChanged);

            wrapper.FirstName = "Thomas";
            Assert.False(wrapper.FirstNameIsChanged);
            Assert.False(wrapper.IsChanged);
        }

        [Fact]
        public void ShouldStoreOriginalValue()
        {
            var wrapper = new FriendWrapper(_friend);
            Assert.Equal("Thomas", wrapper.FirstNameOriginalValue);

            wrapper.FirstName = "Julia";
            Assert.Equal("Thomas", wrapper.FirstNameOriginalValue);
        }
    }
}