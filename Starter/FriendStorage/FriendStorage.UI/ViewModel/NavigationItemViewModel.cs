﻿using FriendStorage.UI.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Windows.Input;
using FriendStorage.UI.Messages;

namespace FriendStorage.UI.ViewModel
{
    public class NavigationItemViewModel
    {
        #region Private Fields

        private string _displayMember;
        private int _id;
        private IMessenger _messenger;

        #endregion

        #region Public Fields

        public ICommand OpenFriendEditViewCommand { get; private set; }
        
        #endregion

        #region Properties

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string DisplayMember
        {
            get { return _displayMember; }
            set { _displayMember = value; }
        }

        #endregion

        #region Constructor

        public NavigationItemViewModel(int id, string displayMember, IMessenger messenger)
        {
            Id = id;
            DisplayMember = displayMember;
            OpenFriendEditViewCommand = new DelegateCommand(OnFriendEditViewExecute);
            _messenger = messenger;
        }

        #endregion

        #region Private Methods

        private void OnFriendEditViewExecute(object obj)
        {
            _messenger.Send(new OpenFriendEditViewMessage(Id));
        }

        #endregion
    }
}
