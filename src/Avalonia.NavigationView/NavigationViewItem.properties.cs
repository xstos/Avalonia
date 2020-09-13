// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections;
using System.Windows;
using Avalonia.Controls.Primitives;

namespace Avalonia.Controls
{
    partial class NavigationViewItem
    {
        #region Icon

        public static readonly AvaloniaProperty IconProperty =
            AvaloniaProperty.Register(
                nameof(Icon),
                typeof(IconElement),
                typeof(NavigationViewItem),
                new PropertyMetadata(OnIconPropertyChanged));

        public IconElement Icon
        {
            get => (IconElement)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        private static void OnIconPropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationViewItem)sender;
            owner.OnIconPropertyChanged(args);
        }

        #endregion

        #region CompactPaneLength

        private static readonly AvaloniaPropertyKey CompactPaneLengthPropertyKey =
            AvaloniaProperty.RegisterReadOnly(
                nameof(CompactPaneLength),
                typeof(double),
                typeof(NavigationViewItem),
                new PropertyMetadata(48.0));

        public static readonly AvaloniaProperty CompactPaneLengthProperty =
            CompactPaneLengthPropertyKey.AvaloniaProperty;

        public double CompactPaneLength
        {
            get => (double)GetValue(CompactPaneLengthProperty);
            private set => SetValue(CompactPaneLengthPropertyKey, value);
        }

        #endregion

        #region SelectsOnInvoked

        public static readonly AvaloniaProperty SelectsOnInvokedProperty =
            AvaloniaProperty.Register(
                nameof(SelectsOnInvoked),
                typeof(bool),
                typeof(NavigationViewItem),
                new PropertyMetadata(true));

        public bool SelectsOnInvoked
        {
            get => (bool)GetValue(SelectsOnInvokedProperty);
            set => SetValue(SelectsOnInvokedProperty, value);
        }

        #endregion

        #region IsExpanded

        public static readonly AvaloniaProperty IsExpandedProperty =
            AvaloniaProperty.Register(
                nameof(IsExpanded),
                typeof(bool),
                typeof(NavigationViewItem),
                new PropertyMetadata(false, OnIsExpandedPropertyChanged));

        public bool IsExpanded
        {
            get => (bool)GetValue(IsExpandedProperty);
            set => SetValue(IsExpandedProperty, value);
        }

        internal event AvaloniaPropertyChangedCallback IsExpandedChanged;

        private static void OnIsExpandedPropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationViewItem)sender;
            owner.OnIsExpandedPropertyChanged(args);
            owner.IsExpandedChanged?.Invoke(owner, args.Property);
        }

        #endregion

        #region HasUnrealizedChildren

        public static readonly AvaloniaProperty HasUnrealizedChildrenProperty =
            AvaloniaProperty.Register(
                nameof(HasUnrealizedChildren),
                typeof(bool),
                typeof(NavigationViewItem),
                new PropertyMetadata(false, OnHasUnrealizedChildrenPropertyChanged));

        public bool HasUnrealizedChildren
        {
            get => (bool)GetValue(HasUnrealizedChildrenProperty);
            set => SetValue(HasUnrealizedChildrenProperty, value);
        }

        private static void OnHasUnrealizedChildrenPropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationViewItem)sender;
            owner.OnHasUnrealizedChildrenPropertyChanged(args);
        }

        #endregion

        #region IsChildSelected

        public static readonly AvaloniaProperty IsChildSelectedProperty =
            AvaloniaProperty.Register(
                nameof(IsChildSelected),
                typeof(bool),
                typeof(NavigationViewItem),
                new PropertyMetadata(false));

        public bool IsChildSelected
        {
            get => (bool)GetValue(IsChildSelectedProperty);
            set => SetValue(IsChildSelectedProperty, value);
        }

        #endregion

        #region MenuItems

        private static readonly AvaloniaPropertyKey MenuItemsPropertyKey =
            AvaloniaProperty.RegisterReadOnly(
                nameof(MenuItems),
                typeof(IList),
                typeof(NavigationViewItem),
                new PropertyMetadata(OnMenuItemsPropertyChanged));

        public static readonly AvaloniaProperty MenuItemsProperty =
            MenuItemsPropertyKey.AvaloniaProperty;

        public IList MenuItems
        {
            get => (IList)GetValue(MenuItemsProperty);
        }

        private static void OnMenuItemsPropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationViewItem)sender;
            owner.OnMenuItemsPropertyChanged(args);
        }

        #endregion

        #region MenuItemsSource

        public static readonly AvaloniaProperty MenuItemsSourceProperty =
            AvaloniaProperty.Register(
                nameof(MenuItemsSource),
                typeof(object),
                typeof(NavigationViewItem),
                new PropertyMetadata(OnMenuItemsSourcePropertyChanged));

        public IEnumerable MenuItemsSource
        {
            get => GetValue(MenuItemsSourceProperty);
            set => SetValue(MenuItemsSourceProperty, value);
        }

        private static void OnMenuItemsSourcePropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationViewItem)sender;
            owner.OnMenuItemsSourcePropertyChanged(args);
        }

        #endregion

        #region CornerRadius

        public static readonly AvaloniaProperty CornerRadiusProperty =
            ControlHelper.CornerRadiusProperty.AddOwner(typeof(NavigationViewItem));

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        #endregion
    }
}
