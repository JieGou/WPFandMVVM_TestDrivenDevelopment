﻿using System;
using System.Threading.Tasks;
using FriendStorage.Model;
using FriendStorage.UI.DataProvider;
using FriendStorage.UI.Messages;
using FriendStorage.UI.ViewModel;
using FriendStorage.UITests.Extensions;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Moq;
using NUnit.Framework;

namespace FriendStorage.UITests.ViewModel
{
    [TestFixture]
    public class FriendEditViewModelTests
    {
        private const int _friendId = 5;

        private Mock<IFriendDataProvider> _dataProviderMock;
        private Mock<IDialogService> _dialogServiceMock;
        private FriendEditViewModel _viewModel;
        private Messenger _testMessenger;

        [SetUp]
        public void SetUp()
        {
            _dataProviderMock = new Mock<IFriendDataProvider>();
            _dataProviderMock.Setup(dp => dp.GetFriendById(_friendId))
                .Returns(new Friend {Id = _friendId, FirstName = "Johnny"});

            _dialogServiceMock = new Mock<IDialogService>();

            _testMessenger = new Messenger();

            _viewModel = new FriendEditViewModel(_dataProviderMock.Object, _testMessenger,
                _dialogServiceMock.Object);
        }

        [Test]
        public void ShouldLoadFriend()
        {
            _viewModel.Load(_friendId);

            Assert.NotNull(_viewModel.Friend);
            Assert.AreEqual(_friendId, _viewModel.Friend.Id);

            _dataProviderMock.Verify(dp => dp.GetFriendById(_friendId), Times.Once);
        }

        [Test]
        public void ShouldRaisePropertyChangedEventForFriend()
        {
            var fired = _viewModel.IsPropertyChangedFired(() =>
            {
                _viewModel.Load(_friendId);
            }, nameof(_viewModel.Friend));

            Assert.True(fired);
        }

        [Test]
        public void ShouldDisableSaveCommandWhenFriendIsLoaded()
        {
            _viewModel.Load(_friendId);
            Assert.False(_viewModel.SaveCommand.CanExecute(null));
        }

        [Test]
        public void ShouldEnableSaveCommandWhenFriendIsChanged()
        {
            _viewModel.Load(_friendId);
            _viewModel.Friend.FirstName = "Changed";

            Assert.True(_viewModel.SaveCommand.CanExecute(null));
        }

        [Test]
        public void ShouldDisableSaveCommandWithoutLoad()
        {
            Assert.False(_viewModel.SaveCommand.CanExecute(null));
        }

        [Test]
        public void ShouldRaiseCanExecuteChangedForSaveCommandWhenFriendIsChanged()
        {
            _viewModel.Load(_friendId);
            var fired = false;
            _viewModel.SaveCommand.CanExecuteChanged += (s, e) => fired = true;
            _viewModel.Friend.FirstName = "Changed";
            Assert.True(fired);
        }

        [Test]
        public void ShouldRaiseCanExecuteChangedForSaveCommandAfterLoad()
        {
            var fired = false;
            _viewModel.SaveCommand.CanExecuteChanged += (s, e) => fired = true;
            _viewModel.Load(_friendId);
            Assert.True(fired);
        }

        [Test]
        public void ShouldCallSaveMethodOfDataProviderWhenSaveCommandIsExecuted()
        {
            _viewModel.Load(_friendId);
            _viewModel.Friend.FirstName = "Changed";

            _viewModel.SaveCommand.Execute(null);
            _dataProviderMock.Verify(dp => dp.SaveFriend(_viewModel.Friend.Model), Times.Once);
        }

        [Test]
        public void ShouldAcceptChangesWhenSaveCommandIsExecuted()
        {
            _viewModel.Load(_friendId);
            _viewModel.Friend.FirstName = "Changed";
            _viewModel.SaveCommand.Execute(null);

            Assert.False(_viewModel.Friend.IsChanged);
        }

        [Test]
        public void ShouldSendFriendSavedMessageWhenSaveCommandIsExecuted()
        {
            FriendSavedMessage receivedMsg = null;

            _testMessenger.Register<FriendSavedMessage>(this, msg =>
            {
                receivedMsg = msg;
            });

            _viewModel.Load(_friendId);
            _viewModel.Friend.FirstName = "Changed";
            _viewModel.SaveCommand.Execute(null);

            Assert.NotNull(receivedMsg);
            Assert.AreEqual(_friendId, receivedMsg.Friend.Id);
        }

        [Test]
        public void ShouldCreateNewFriendWhenNullIsPassedToLoadMethod()
        {
            _viewModel.Load(null);

            Assert.NotNull(_viewModel.Friend);
            Assert.AreEqual(0, _viewModel.Friend.Id);
            Assert.Null(_viewModel.Friend.FirstName);
            Assert.Null(_viewModel.Friend.LastName);
            Assert.Null(_viewModel.Friend.Birthday);
            Assert.IsFalse(_viewModel.Friend.IsDeveloper);

            _dataProviderMock.Verify(dp => dp.GetFriendById(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void ShouldEnableDeleteCommandForExistingFriend()
        {
            _viewModel.Load(_friendId);
            Assert.True(_viewModel.DeleteCommand.CanExecute(null));
        }

        [Test]
        public void ShouldDisableDeleteCommandForNewFriend()
        {
            _viewModel.Load(null);
            Assert.False(_viewModel.DeleteCommand.CanExecute(null));
        }

        [Test]
        public void ShouldDisableDeleteCommandWithoutLoad()
        {
            Assert.False(_viewModel.DeleteCommand.CanExecute(null));
        }

        [Test]
        public void ShouldRaiseCanExecuteChangedForDeleteCommandWhenAcceptingChanges()
        {
            _viewModel.Load(_friendId);
            var fired = false;
            _viewModel.DeleteCommand.CanExecuteChanged += (s, e) => fired = true;
            _viewModel.Friend.AcceptChanges();
            Assert.True(fired);
        }

        [Test]
        public void ShouldRaiseCanExecuteChangedForDeleteCommandAfterLoad()
        {
            var fired = false;
            _viewModel.DeleteCommand.CanExecuteChanged += (s, e) => fired = true;
            _viewModel.Load(_friendId);
            Assert.True(fired);
        }

        [Test]
        [TestCase(true, 1)]
        [TestCase(false, 0)]
        public void ShouldCallDeleteFriendWhenDeleteCommandIsExecutedAndDialogConfirmed(
            bool dialogResult, int expectedInvokations)
        {
            _viewModel.Load(_friendId);

            _dialogServiceMock
                .Setup(ds => ds.ShowMessage(It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Action<bool>>()))
                .Callback<string, string, string, string, Action<bool>>((s1, s2, s3, s4, confirm) =>
                {
                    confirm(dialogResult);
                });

            _viewModel.DeleteCommand.Execute(null);

            _dataProviderMock.Verify(dp => dp.DeleteFriend(_friendId), Times.Exactly(expectedInvokations));
        }

        [Test]
        [TestCase(true, false)]
        [TestCase(false, true)]
        public void ShouldSendFriendDeletedEventWhenDeleteCommandIsExecuted(
            bool dialogResult, bool msgNull)
        {
            FriendDeletedMessage receivedMessage = null;

            _testMessenger.Register<FriendDeletedMessage>(this, msg =>
            {
                receivedMessage = msg;
            });

            _dialogServiceMock
                .Setup(ds => ds.ShowMessage(It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Action<bool>>()))
                .Callback<string, string, string, string, Action<bool>>((s1, s2, s3, s4, confirm) =>
                {
                    confirm(dialogResult);
                });

            _viewModel.Load(_friendId);
            _viewModel.DeleteCommand.Execute(null);

            if (msgNull)
            {
                Assert.Null(receivedMessage);
            }
            else
            {
                Assert.NotNull(receivedMessage);
                Assert.AreEqual(_friendId, receivedMessage.FriendId);
            }
        }

        [Test]
        public void ShouldDisplayCorrectMessageInDeleteDialog()
        {
            _viewModel.Load(_friendId);

            var f = _viewModel.Friend;
            f.FirstName = "Johnny";
            f.LastName = "Pockets";

            _viewModel.DeleteCommand.Execute(null);

            string expectedTitle = "Delete Friend";
            string expectedMessage = $"Do you really want to delete the friend {f.FirstName} {f.LastName}?";

            _dialogServiceMock.Verify(d => d.ShowMessage(expectedMessage, expectedTitle,
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Action<bool>>()), Times.Once);
        }
    }
}
