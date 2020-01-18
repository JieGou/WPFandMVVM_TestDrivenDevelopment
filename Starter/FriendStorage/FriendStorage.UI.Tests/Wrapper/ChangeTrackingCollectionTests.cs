using System.Collections.Generic;
using System.Linq;
using FriendStorage.Model;
using FriendStorage.UI.Wrapper;
using Xunit;

namespace FriendStorage.UI.Tests.Wrapper
{
    public class ChangeTrackingCollectionTests
    {
        public ChangeTrackingCollectionTests()
        {
            _emails = new List<FriendEmailWrapper>
            {
                new FriendEmailWrapper(new FriendEmail {Email = "thomas@thomasclaudiushuber.com"}),
                new FriendEmailWrapper(new FriendEmail {Email = "julia@juhu-design.com"})
            };
        }

        private readonly List<FriendEmailWrapper> _emails;

        [Fact]
        public void ShouldAcceptChanges()
        {
            var emailToModify = _emails.First();
            var emailToRemove = _emails.Skip(1).First();
            var emailToAdd = new FriendEmailWrapper(new FriendEmail {Email = "anotherOne@thomasclaudiushuber.com"});
            var c = new ChangeTrackingCollection<FriendEmailWrapper>(_emails) {emailToAdd};

            c.Remove(emailToRemove);
            emailToModify.Email = "modified@thomasclaudiushuber.com";

            Assert.Equal("thomas@thomasclaudiushuber.com", emailToModify.EmailOriginalValue);

            Assert.Equal(2, c.Count);
            Assert.Equal(1, c.AddedItems.Count);
            Assert.Equal(1, c.ModifiedItems.Count);
            Assert.Equal(1, c.RemovedItems.Count);

            c.AcceptChanges();

            Assert.Equal(2, c.Count);
            Assert.True(c.Contains(emailToModify));
            Assert.True(c.Contains(emailToAdd));

            Assert.Equal(0, c.AddedItems.Count);
            Assert.Equal(0, c.ModifiedItems.Count);
            Assert.Equal(0, c.RemovedItems.Count);

            Assert.False(emailToModify.IsChanged);
            Assert.Equal("modified@thomasclaudiushuber.com", emailToModify.Email);
            Assert.Equal("modified@thomasclaudiushuber.com", emailToModify.EmailOriginalValue);

            Assert.False(c.IsChanged);
        }

        [Fact]
        public void ShouldNotTrackAddedItemAsModified()
        {
            var emailToAdd = new FriendEmailWrapper(new FriendEmail());
            var c = new ChangeTrackingCollection<FriendEmailWrapper>(_emails) {emailToAdd};

            emailToAdd.Email = "modified@thomasclaudiushuber.com";

            Assert.True(emailToAdd.IsChanged);
            Assert.Equal(3, c.Count);
            Assert.Equal(1, c.AddedItems.Count);
            Assert.Equal(0, c.RemovedItems.Count);
            Assert.Equal(0, c.ModifiedItems.Count);
            Assert.True(c.IsChanged);
        }

        [Fact]
        public void ShouldNotTrackRemovedItemAsModified()
        {
            var emailToModifyAndRemove = _emails.First();
            var c = new ChangeTrackingCollection<FriendEmailWrapper>(_emails);

            emailToModifyAndRemove.Email = "modified@thomasclaudiushuber.com";

            Assert.Equal(2, c.Count);
            Assert.Equal(0, c.AddedItems.Count);
            Assert.Equal(0, c.RemovedItems.Count);
            Assert.Equal(1, c.ModifiedItems.Count);
            Assert.Equal(emailToModifyAndRemove, c.ModifiedItems.First());
            Assert.True(c.IsChanged);

            c.Remove(emailToModifyAndRemove);

            Assert.Equal(1, c.Count);
            Assert.Equal(0, c.AddedItems.Count);
            Assert.Equal(1, c.RemovedItems.Count);
            Assert.Equal(0, c.ModifiedItems.Count);
            Assert.Equal(emailToModifyAndRemove, c.RemovedItems.First());
            Assert.True(c.IsChanged);
        }

        [Fact]
        public void ShouldRejectChanges()
        {
            var emailToModify = _emails.First();
            var emailToRemove = _emails.Skip(1).First();
            var emailToAdd = new FriendEmailWrapper(new FriendEmail {Email = "anotherOne@thomasclaudiushuber.com"});
            var c = new ChangeTrackingCollection<FriendEmailWrapper>(_emails) {emailToAdd};

            c.Remove(emailToRemove);
            emailToModify.Email = "modified@thomasclaudiushuber.com";

            Assert.Equal("thomas@thomasclaudiushuber.com", emailToModify.EmailOriginalValue);

            Assert.Equal(2, c.Count);
            Assert.Equal(1, c.AddedItems.Count);
            Assert.Equal(1, c.ModifiedItems.Count);
            Assert.Equal(1, c.RemovedItems.Count);

            c.RejectChanges();

            Assert.Equal(2, c.Count);
            Assert.True(c.Contains(emailToModify));
            Assert.True(c.Contains(emailToRemove));

            Assert.Equal(0, c.AddedItems.Count);
            Assert.Equal(0, c.ModifiedItems.Count);
            Assert.Equal(0, c.RemovedItems.Count);

            Assert.False(emailToModify.IsChanged);
            Assert.Equal("thomas@thomasclaudiushuber.com", emailToModify.Email);
            Assert.Equal("thomas@thomasclaudiushuber.com", emailToModify.EmailOriginalValue);

            Assert.False(c.IsChanged);
        }


        [Fact]
        public void ShouldRejectChangesWithModifiedAndRemovedItem()
        {
            var email = _emails.First();
            var c = new ChangeTrackingCollection<FriendEmailWrapper>(_emails);

            email.Email = "modified@thomasclaudiushuber.com";
            c.Remove(email);

            Assert.Equal("thomas@thomasclaudiushuber.com", email.EmailOriginalValue);

            Assert.Equal(1, c.Count);
            Assert.Equal(0, c.AddedItems.Count);
            Assert.Equal(0, c.ModifiedItems.Count);
            Assert.Equal(1, c.RemovedItems.Count);

            c.RejectChanges();

            Assert.Equal(2, c.Count);
            Assert.True(c.Contains(email));

            Assert.Equal(0, c.AddedItems.Count);
            Assert.Equal(0, c.ModifiedItems.Count);
            Assert.Equal(0, c.RemovedItems.Count);

            Assert.False(email.IsChanged);
            Assert.Equal("thomas@thomasclaudiushuber.com", email.Email);
            Assert.Equal("thomas@thomasclaudiushuber.com", email.EmailOriginalValue);

            Assert.False(c.IsChanged);
        }

        [Fact]
        public void ShouldTrackAddedItems()
        {
            var emailToAdd = new FriendEmailWrapper(new FriendEmail());

            var c = new ChangeTrackingCollection<FriendEmailWrapper>(_emails);
            Assert.Equal(2, c.Count);
            Assert.False(c.IsChanged);

            c.Add(emailToAdd);

            Assert.Equal(3, c.Count);
            Assert.Equal(1, c.AddedItems.Count);
            Assert.Equal(0, c.RemovedItems.Count);
            Assert.Equal(0, c.ModifiedItems.Count);
            Assert.Equal(emailToAdd, c.AddedItems.First());
            Assert.True(c.IsChanged);

            c.Remove(emailToAdd);

            Assert.Equal(2, c.Count);
            Assert.Equal(0, c.AddedItems.Count);
            Assert.Equal(0, c.RemovedItems.Count);
            Assert.Equal(0, c.ModifiedItems.Count);
            Assert.False(c.IsChanged);
        }

        [Fact]
        public void ShouldTrackModifiedItem()
        {
            var emailToModify = _emails.First();
            var c = new ChangeTrackingCollection<FriendEmailWrapper>(_emails);
            Assert.Equal(2, c.Count);
            Assert.False(c.IsChanged);

            emailToModify.Email = "modified@thomasclaudiushuber.com";

            Assert.Equal(0, c.AddedItems.Count);
            Assert.Equal(1, c.ModifiedItems.Count);
            Assert.Equal(0, c.RemovedItems.Count);
            Assert.True(c.IsChanged);

            emailToModify.Email = "thomas@thomasclaudiushuber.com";

            Assert.Equal(0, c.AddedItems.Count);
            Assert.Equal(0, c.ModifiedItems.Count);
            Assert.Equal(0, c.RemovedItems.Count);
            Assert.False(c.IsChanged);
        }

        [Fact]
        public void ShouldTrackRemovedItems()
        {
            var emailToRemove = _emails.First();
            var c = new ChangeTrackingCollection<FriendEmailWrapper>(_emails);
            Assert.Equal(2, c.Count);
            Assert.False(c.IsChanged);

            c.Remove(emailToRemove);

            Assert.Equal(1, c.Count);
            Assert.Equal(0, c.AddedItems.Count);
            Assert.Equal(1, c.RemovedItems.Count);
            Assert.Equal(0, c.ModifiedItems.Count);
            Assert.Equal(emailToRemove, c.RemovedItems.First());
            Assert.True(c.IsChanged);

            c.Add(emailToRemove);

            Assert.Equal(2, c.Count);
            Assert.Equal(0, c.AddedItems.Count);
            Assert.Equal(0, c.RemovedItems.Count);
            Assert.Equal(0, c.ModifiedItems.Count);
            Assert.False(c.IsChanged);
        }
    }
}