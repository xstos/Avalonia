// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Windows;

namespace Avalonia.Controls
{
    public class NavigationViewTemplateSettings : AvaloniaObject
    {
        public NavigationViewTemplateSettings()
        {
        }

        #region TopPadding

        private static readonly AvaloniaPropertyKey TopPaddingPropertyKey =
            AvaloniaProperty.RegisterReadOnly(
                nameof(TopPadding),
                typeof(double),
                typeof(NavigationViewTemplateSettings),
                new PropertyMetadata(0.0));

        public static readonly AvaloniaProperty TopPaddingProperty =
            TopPaddingPropertyKey.AvaloniaProperty;

        public double TopPadding
        {
            get => (double)GetValue(TopPaddingProperty);
            internal set => SetValue(TopPaddingPropertyKey, value);
        }

        #endregion

        #region OverflowButtonVisibility

        private static readonly AvaloniaPropertyKey OverflowButtonVisibilityPropertyKey =
            AvaloniaProperty.RegisterReadOnly(
                nameof(OverflowButtonVisibility),
                typeof(Visibility),
                typeof(NavigationViewTemplateSettings),
                new PropertyMetadata(Visibility.Collapsed));

        public static readonly AvaloniaProperty OverflowButtonVisibilityProperty =
            OverflowButtonVisibilityPropertyKey.AvaloniaProperty;

        public Visibility OverflowButtonVisibility
        {
            get => (Visibility)GetValue(OverflowButtonVisibilityProperty);
            internal set => SetValue(OverflowButtonVisibilityPropertyKey, value);
        }

        #endregion

        #region PaneToggleButtonVisibility

        private static readonly AvaloniaPropertyKey PaneToggleButtonVisibilityPropertyKey =
            AvaloniaProperty.RegisterReadOnly(
                nameof(PaneToggleButtonVisibility),
                typeof(Visibility),
                typeof(NavigationViewTemplateSettings),
                new PropertyMetadata(Visibility.Visible));

        public static readonly AvaloniaProperty PaneToggleButtonVisibilityProperty =
            PaneToggleButtonVisibilityPropertyKey.AvaloniaProperty;

        public Visibility PaneToggleButtonVisibility
        {
            get => (Visibility)GetValue(PaneToggleButtonVisibilityProperty);
            internal set => SetValue(PaneToggleButtonVisibilityPropertyKey, value);
        }

        #endregion

        #region BackButtonVisibility

        private static readonly AvaloniaPropertyKey BackButtonVisibilityPropertyKey =
            AvaloniaProperty.RegisterReadOnly(
                nameof(BackButtonVisibility),
                typeof(Visibility),
                typeof(NavigationViewTemplateSettings),
                new PropertyMetadata(Visibility.Collapsed));

        public static readonly AvaloniaProperty BackButtonVisibilityProperty =
            BackButtonVisibilityPropertyKey.AvaloniaProperty;

        public Visibility BackButtonVisibility
        {
            get => (Visibility)GetValue(BackButtonVisibilityProperty);
            internal set => SetValue(BackButtonVisibilityPropertyKey, value);
        }

        #endregion

        #region TopPaneVisibility

        private static readonly AvaloniaPropertyKey TopPaneVisibilityPropertyKey =
            AvaloniaProperty.RegisterReadOnly(
                nameof(TopPaneVisibility),
                typeof(Visibility),
                typeof(NavigationViewTemplateSettings),
                new PropertyMetadata(Visibility.Collapsed));

        public static readonly AvaloniaProperty TopPaneVisibilityProperty =
            TopPaneVisibilityPropertyKey.AvaloniaProperty;

        public Visibility TopPaneVisibility
        {
            get => (Visibility)GetValue(TopPaneVisibilityProperty);
            internal set => SetValue(TopPaneVisibilityPropertyKey, value);
        }

        #endregion

        #region LeftPaneVisibility

        private static readonly AvaloniaPropertyKey LeftPaneVisibilityPropertyKey =
            AvaloniaProperty.RegisterReadOnly(
                nameof(LeftPaneVisibility),
                typeof(Visibility),
                typeof(NavigationViewTemplateSettings),
                new PropertyMetadata(Visibility.Visible));

        public static readonly AvaloniaProperty LeftPaneVisibilityProperty =
            LeftPaneVisibilityPropertyKey.AvaloniaProperty;

        public Visibility LeftPaneVisibility
        {
            get => (Visibility)GetValue(LeftPaneVisibilityProperty);
            internal set => SetValue(LeftPaneVisibilityPropertyKey, value);
        }

        #endregion

        #region SingleSelectionFollowsFocus

        private static readonly AvaloniaPropertyKey SingleSelectionFollowsFocusPropertyKey =
            AvaloniaProperty.RegisterReadOnly(
                nameof(SingleSelectionFollowsFocus),
                typeof(bool),
                typeof(NavigationViewTemplateSettings),
                null);

        public static readonly AvaloniaProperty SingleSelectionFollowsFocusProperty =
            SingleSelectionFollowsFocusPropertyKey.AvaloniaProperty;

        public bool SingleSelectionFollowsFocus
        {
            get => (bool)GetValue(SingleSelectionFollowsFocusProperty);
            internal set => SetValue(SingleSelectionFollowsFocusPropertyKey, value);
        }

        #endregion
    }
}
