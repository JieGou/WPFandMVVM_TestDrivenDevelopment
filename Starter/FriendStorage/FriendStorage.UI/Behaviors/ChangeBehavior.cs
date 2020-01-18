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
        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.RegisterAttached(
            "IsActive", typeof(bool), typeof(ChangeBehavior), new PropertyMetadata(default(bool), OnIsActivePropertyChanged));

        public static readonly DependencyProperty IsChangedProperty = DependencyProperty.RegisterAttached(
            "IsChanged", typeof(bool), typeof(ChangeBehavior), new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty OriginalValueProperty = DependencyProperty.RegisterAttached(
            "OriginalValue", typeof(object), typeof(ChangeBehavior), new PropertyMetadata(default(object)));

        public static readonly DependencyProperty OriginalValueConverterProperty = DependencyProperty.RegisterAttached(
            "OriginalValueConverter", typeof(IValueConverter), typeof(ChangeBehavior), new PropertyMetadata(default(IValueConverter), OnOriginalValuePropertyConverterChanged));

        private static readonly Dictionary<Type,DependencyProperty> _defaultProperties = new Dictionary<Type, DependencyProperty>()
        {
            [typeof(TextBox)] = TextBox.TextProperty,
            [typeof(CheckBox)] = ToggleButton.IsCheckedProperty,
            [typeof(DatePicker)] = DatePicker.SelectedDateProperty,
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

        public static void SetOriginalValueConverter(DependencyObject element, IValueConverter value)
        {
            element.SetValue(OriginalValueConverterProperty, value);
        }

        public static IValueConverter GetOriginalValueConverter(DependencyObject element)
        {
            return (IValueConverter) element.GetValue(OriginalValueConverterProperty);
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
                        CreateOriginalValueBinding(d, bindingPath + "OriginalValue");
                    }
                }
                else
                {
                    BindingOperations.ClearBinding(d,IsChangedProperty);
                    BindingOperations.ClearBinding(d,OriginalValueProperty);
                }
            }
        }

        private static void OnOriginalValuePropertyConverterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (BindingOperations.GetBinding(d, OriginalValueProperty) is { } originalValueBinding)
            {
                CreateOriginalValueBinding(d, originalValueBinding.Path.Path);
            }
        }

        private static void CreateOriginalValueBinding(DependencyObject d, string originalValueBindingPath)
        {
            var newBinding = new Binding(originalValueBindingPath)
            {
                Converter = GetOriginalValueConverter(d)
            };
            BindingOperations.SetBinding(d, OriginalValueProperty, newBinding);
        }
    }
}