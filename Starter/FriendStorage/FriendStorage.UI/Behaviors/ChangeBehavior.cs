using System.Windows;

namespace FriendStorage.UI.Behaviors
{
    //Note generate code snippets in VS as `propa`, with Resharper as `attachedProperty`
    public static class ChangeBehavior
    {
        public static readonly DependencyProperty IsChangedProperty = DependencyProperty.RegisterAttached(
            "IsChanged", typeof(bool), typeof(ChangeBehavior), new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty OriginalValueProperty = DependencyProperty.RegisterAttached(
            "OriginalValue", typeof(object), typeof(ChangeBehavior), new PropertyMetadata(default(object)));

        public static void SetIsChanged(DependencyObject element, bool value)
        {
            element.SetValue(IsChangedProperty, value);
        }

        public static bool GetIsChanged(DependencyObject element)
        {
            return (bool) element.GetValue(IsChangedProperty);
        }

        public static void SetOriginalValue(DependencyObject element, object value)
        {
            element.SetValue(OriginalValueProperty, value);
        }

        public static object GetOriginalValue(DependencyObject element)
        {
            return element.GetValue(OriginalValueProperty);
        }
    }
}