using System;
using System.Windows.Input;
using FriendStorage.UI.Command;

namespace FriendStorage.UI.ViewModel
{
    public class NavigationItemViewModel
    {
        #region Private Fields

        private string _displayMember;
        private int _id;

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

        public NavigationItemViewModel(int id, string displayMember)
        {
            Id = id;
            DisplayMember = displayMember;
            OpenFriendEditViewCommand = new DelegateCommand(OnFriendEditViewExecute);
        }

        #endregion

        #region Private Methods

        private void OnFriendEditViewExecute(object obj)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
