using System.Collections.Generic;
using FriendStorage.Model;
using FriendStorage.UI.Wrapper;
using Xunit;

namespace FriendStorage.UI.Tests.Wrapper
{
    public class ChangeTrackingComplexProperty
    {
        public ChangeTrackingComplexProperty()
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
        public void ShouldAcceptChanges()
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            var wrapper = new FriendWrapper(_friend);
            wrapper.Address.City = "Salt Lake City";
            Assert.Equal("Müllheim", wrapper.Address.CityOriginalValue);

            wrapper.AcceptChanges();

            Assert.False(wrapper.IsChanged);
            Assert.Equal("Salt Lake City", wrapper.Address.City);
            Assert.Equal("Salt Lake City", wrapper.Address.CityOriginalValue);
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

            wrapper.Address.City = "Salt Lake City";

            Assert.True(fired);
        }

        [Fact]
        public void ShouldRejectChanges()
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            var wrapper = new FriendWrapper(_friend);
            wrapper.Address.City = "Salt Lake City";
            Assert.Equal("Müllheim", wrapper.Address.CityOriginalValue);

            wrapper.RejectChanges();

            Assert.False(wrapper.IsChanged);
            Assert.Equal("Müllheim", wrapper.Address.City);
            Assert.Equal("Müllheim", wrapper.Address.CityOriginalValue);
        }

        [Fact]
        public void ShouldSetIsChangedOfFriendWrapper()
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            var wrapper = new FriendWrapper(_friend);
            wrapper.Address.City = "Salt Lake City";

            Assert.True(wrapper.IsChanged);

            wrapper.Address.City = @"Müllheim";
            Assert.False(wrapper.IsChanged);
        }
    }
}