using System;
using System.Collections.ObjectModel;
using System.Linq;
using FriendStorage.Model;

namespace FriendStorage.UI.Wrapper
{
    public class FriendWrapper : ModelWrapper<Friend>
    {
        public FriendWrapper(Friend model) : base(model)
        {
            InitializeComplexProperties(model);
            InitializeCollectionProperties(model);
        }

        public int Id
        {
            get => GetValue<int>();
            set => SetValue(value);
        }

        public int FriendGroupId
        {
            get => GetValue<int>();
            set => SetValue(value);
        }

        public string FirstName
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string LastName
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public DateTime? Birthday
        {
            get => GetValue<DateTime?>();
            set => SetValue(value);
        }

        public bool IsDeveloper
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }

        public AddressWrapper Address { get; private set; }

        public ObservableCollection<FriendEmailWrapper> Emails { get; private set; }

        private void InitializeCollectionProperties(Friend model)
        {
            if (model.Emails == null) throw new ArgumentException("Emails cannot be null");
            Emails = new ObservableCollection<FriendEmailWrapper>(
                model.Emails.Select(e => new FriendEmailWrapper(e)));
            RegisterCollection(Emails, model.Emails);
        }

        private void InitializeComplexProperties(Friend model)
        {
            if (model.Address == null) throw new ArgumentException("Address cannot be null");
            Address = new AddressWrapper(model.Address);
        }
    }
}