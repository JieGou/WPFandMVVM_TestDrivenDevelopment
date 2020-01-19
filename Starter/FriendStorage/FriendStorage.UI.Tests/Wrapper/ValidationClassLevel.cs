using System.Collections.Generic;
using System.Linq;
using FriendStorage.Model;
using FriendStorage.UI.Wrapper;
using Xunit;

namespace FriendStorage.UI.Tests.Wrapper
{
    public class ValidationClassLevel
    {
        public ValidationClassLevel()
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
        public void ShouldBeValidAgainWhenEmailIsAdded()
        {
            var wrapper = new FriendWrapper(_friend);
            wrapper.Emails.Clear();
            Assert.False(wrapper.IsDeveloper);
            Assert.True(wrapper.IsValid);

            wrapper.IsDeveloper = true;
            Assert.False(wrapper.IsValid);

            wrapper.Emails.Add(new FriendEmailWrapper(new FriendEmail {Email = "thomas@thomasclaudiushuber.com"}));
            Assert.True(wrapper.IsValid);

            var emailsErrors = wrapper.GetErrors(nameof(wrapper.Emails)).Cast<string>().ToList();
            Assert.Equal(0, emailsErrors.Count);

            var isDeveloperErrors = wrapper.GetErrors(nameof(wrapper.IsDeveloper)).Cast<string>().ToList();
            Assert.Equal(0, isDeveloperErrors.Count);
        }

        [Fact]
        public void ShouldBeValidAgainWhenIsDeveloperIsSetBackToFalse()
        {
            var wrapper = new FriendWrapper(_friend);
            wrapper.Emails.Clear();
            Assert.False(wrapper.IsDeveloper);
            Assert.True(wrapper.IsValid);

            wrapper.IsDeveloper = true;
            Assert.False(wrapper.IsValid);


            wrapper.IsDeveloper = false;
            Assert.True(wrapper.IsValid);

            var emailsErrors = wrapper.GetErrors(nameof(wrapper.Emails)).Cast<string>().ToList();
            Assert.Equal(0, emailsErrors.Count);

            var isDeveloperErrors = wrapper.GetErrors(nameof(wrapper.IsDeveloper)).Cast<string>().ToList();
            Assert.Equal(0, isDeveloperErrors.Count);
        }


        [Fact]
        public void ShouldHaveErrorsAndNotBeValidWhenIsDeveloperIsTrueAndNoEmailExists()
        {
            var expectedError = "A developer must have an email-address";

            var wrapper = new FriendWrapper(_friend);
            wrapper.Emails.Clear();
            Assert.False(wrapper.IsDeveloper);
            Assert.True(wrapper.IsValid);

            wrapper.IsDeveloper = true;
            Assert.False(wrapper.IsValid);

            var emailsErrors = wrapper.GetErrors(nameof(wrapper.Emails)).Cast<string>().ToList();
            Assert.Equal(1, emailsErrors.Count);
            Assert.Equal(expectedError, emailsErrors.Single());

            var isDeveloperErrors = wrapper.GetErrors(nameof(wrapper.IsDeveloper)).Cast<string>().ToList();
            Assert.Equal(1, isDeveloperErrors.Count);
            Assert.Equal(expectedError, isDeveloperErrors.Single());
        }

        [Fact]
        public void ShouldIntializeWithoutProblems()
        {
            _friend.IsDeveloper = true;
            var wrapper = new FriendWrapper(_friend);
            Assert.True(wrapper.IsValid);
        }
    }
}