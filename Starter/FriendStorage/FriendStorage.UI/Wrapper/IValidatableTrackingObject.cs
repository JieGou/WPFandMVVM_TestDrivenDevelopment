using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FriendStorage.UI.Wrapper
{
    public interface IValidatableTrackingObject : IRevertibleChangeTracking, INotifyPropertyChanged
    {
        bool IsValid { get; }
    }
}
