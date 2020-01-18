using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace FriendStorage.UI.Behaviors
{
    //Note generate code snippets in VS as `propa`, with Resharper as `attachedProperty`
    public static class ChangeBehavior
    {
        public static readonly DependencyProperty IsChangedProperty = DependencyProperty.RegisterAttached(
            "IsChanged", typeof(bool), typeof(ChangeBehavior), new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty OriginalValueProperty = DependencyProperty.RegisterAttached(
            "OriginalValue", typeof(object), typeof(ChangeBehavior), new PropertyMetadata(default(object)));

        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.RegisterAttached(
            "IsActive", typeof(bool), typeof(ChangeBehavior), new PropertyMetadata(default(bool), OnIsActivePropertyChanged));

        private static readonly Dictionary<Type,DependencyProperty> _defaultProperties = new Dictionary<Type, DependencyProperty>()
        {
            [typeof(TextBox)] = TextBox.TextProperty,
            [typeof(CheckBox)] = ToggleButton.IsCheckedProperty,
        };  

        public static void SetIsActive(DependencyObject element, bool value)
        {
            element.SetValue(IsActiveProperty, value);
        }

        public static bool GetIsActive(DependencyObject element)
        {
            return (bool) element.GetValue(IsActiveProperty);
        }

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

        private static void OnIsActivePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (_defaultProperties.ContainsKey(d.GetType()))
            {
                var defaultProperty = _defaultProperties[d.GetType()];
                if ((bool) e.NewValue)
                {
                    var binding = BindingOperations.GetBinding(d, defaultProperty);
                    if (binding != null)
                    {
                        var bindingPath = binding.Path.Path;
                        BindingOperations.SetBinding(d, IsChangedProperty, new Binding(bindingPath + "IsChanged"));
                        BindingOperations.SetBinding(d, OriginalValueProperty, new Binding(bindingPath + "OriginalValue"));
                    }
                }
                else
                {
                    BindingOperations.ClearBinding(d,IsChangedProperty);
                    BindingOperations.ClearBinding(d,OriginalValueProperty);
                }
            }
        }
    }
}