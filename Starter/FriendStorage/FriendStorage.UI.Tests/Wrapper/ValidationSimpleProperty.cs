using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FriendStorage.Model;
using FriendStorage.UI.Wrapper;
using Xunit;

namespace FriendStorage.UI.Tests.Wrapper
{
    public class ValidationSimpleProperty
    {
        public ValidationSimpleProperty()
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
        public void ShouldRaiseErrorsChangedEventWhenFirstNameIsSetToEmptyAndBack()
        {
            var fired = false;
            var wrapper = new FriendWrapper(_friend);

            wrapper.ErrorsChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(wrapper.FirstName)) fired = true;
            };

            wrapper.FirstName = "";
            Assert.True(fired);

            fired = false;
            wrapper.FirstName = "Julia";
            Assert.True(fired);
        }

        [Fact]
        public void ShouldRaisePropertyChangedEventForIsValid()
        {
            var fired = false;
            var wrapper = new FriendWrapper(_friend);

            wrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(wrapper.IsValid)) fired = true;
            };

            wrapper.FirstName = "";
            Assert.True(fired);

            fired = false;
            wrapper.FirstName = "Julia";
            Assert.True(fired);
        }

        [Fact]
        public void ShouldRefreshErrorsAndIsValidWhenRejectingChanges()
        {
            var wrapper = new FriendWrapper(_friend);
            Assert.True(wrapper.IsValid);
            Assert.False(wrapper.HasErrors);

            wrapper.FirstName = "";

            Assert.False(wrapper.IsValid);
            Assert.True(wrapper.HasErrors);

            wrapper.RejectChanges();

            Assert.True(wrapper.IsValid);
            Assert.False(wrapper.HasErrors);
        }

        [Fact]
        public void ShouldReturnValidationErrorIfFirstNameIsEmpty()
        {
            var wrapper = new FriendWrapper(_friend);
            Assert.False(wrapper.HasErrors);

            wrapper.FirstName = "";
            Assert.True(wrapper.HasErrors);

            var errors = wrapper.GetErrors(nameof(wrapper.FirstName)).Cast<string>().ToList();
            Assert.Equal(1, errors.Count);
            Assert.Equal("Firstname is required", errors.First());

            wrapper.FirstName = "Julia";
            Assert.False(wrapper.HasErrors);
        }

        [Fact]
        public void ShouldSetErrorsAndIsValidAfterInitialization()
        {
            _friend.FirstName = "";
            var wrapper = new FriendWrapper(_friend);

            Assert.False(wrapper.IsValid);
            Assert.True(wrapper.HasErrors);

            var errors = wrapper.GetErrors(nameof(wrapper.FirstName)).Cast<string>().ToList();
            Assert.Equal(1, errors.Count);
            Assert.Equal("Firstname is required", errors.First());
        }

        [Fact]
        public void ShouldSetIsValid()
        {
            var wrapper = new FriendWrapper(_friend);
            Assert.True(wrapper.IsValid);

            wrapper.FirstName = "";
            Assert.False(wrapper.IsValid);

            wrapper.FirstName = "Julia";
            Assert.True(wrapper.IsValid);
        }

        [Fact]
        public void ShouldReturnValidationErrorIfEmailAddressIsNotValid()
        {
            var email = new FriendEmail();
            var wrapper = new FriendEmailWrapper(email);
            Assert.True(wrapper.HasErrors);
            
            wrapper.Email = "a";
            Assert.True(wrapper.HasErrors);

            var errors = wrapper.GetErrors(nameof(wrapper.Email)).Cast<string>().ToList();
            Assert.Equal(1, errors.Count);
            Assert.Equal("Email is not a valid email address", errors.First());

            wrapper.Email = "a@b.com";
            Assert.False(wrapper.HasErrors);
        }
    }
}