using System.Collections.Generic;
using FriendStorage.Model;
using FriendStorage.UI.Wrapper;
using Xunit;

namespace FriendStorage.UI.Tests.Wrapper
{
    public class ChangeNotificationComplexProperty
    {
        public ChangeNotificationComplexProperty()
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
        public void ShouldInitializeAddressProperty()
        {
            var wrapper = new FriendWrapper(_friend);

            Assert.NotNull(wrapper.Address);
            Assert.Equal(_friend.Address, wrapper.Address.Model);
        }
    }
}