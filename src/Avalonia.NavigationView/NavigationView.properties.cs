// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace Avalonia.Controls
{
    partial class NavigationView
    {
        #region IsPaneOpen

        public static readonly AvaloniaProperty IsPaneOpenProperty =
            AvaloniaProperty.Register(
                nameof(IsPaneOpen),
                typeof(bool),
                typeof(NavigationView),
                new PropertyMetadata(true, OnIsPaneOpenPropertyChanged));

        public bool IsPaneOpen
        {
            get => (bool)GetValue(IsPaneOpenProperty);
            set => SetValue(IsPaneOpenProperty, value);
        }

        private static void OnIsPaneOpenPropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationView)sender;
            owner.PropertyChanged(args);
        }

        #endregion

        #region CompactModeThresholdWidth

        public static readonly AvaloniaProperty CompactModeThresholdWidthProperty =
            AvaloniaProperty.Register(
                nameof(CompactModeThresholdWidth),
                typeof(double),
                typeof(NavigationView),
                new PropertyMetadata(641.0, OnCompactModeThresholdWidthPropertyChanged, CoerceToGreaterThanZero));

        public double CompactModeThresholdWidth
        {
            get => (double)GetValue(CompactModeThresholdWidthProperty);
            set => SetValue(CompactModeThresholdWidthProperty, value);
        }

        private static void OnCompactModeThresholdWidthPropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationView)sender;
            owner.PropertyChanged(args);
        }

        #endregion

        #region ExpandedModeThresholdWidth

        public static readonly AvaloniaProperty ExpandedModeThresholdWidthProperty =
            AvaloniaProperty.Register(
                nameof(ExpandedModeThresholdWidth),
                typeof(double),
                typeof(NavigationView),
                new PropertyMetadata(1008.0, OnExpandedModeThresholdWidthPropertyChanged, CoerceToGreaterThanZero));

        public double ExpandedModeThresholdWidth
        {
            get => (double)GetValue(ExpandedModeThresholdWidthProperty);
            set => SetValue(ExpandedModeThresholdWidthProperty, value);
        }

        private static void OnExpandedModeThresholdWidthPropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationView)sender;
            owner.PropertyChanged(args);
        }

        #endregion

        #region FooterMenuItems

        private static readonly AvaloniaPropertyKey FooterMenuItemsPropertyKey =
            AvaloniaProperty.RegisterReadOnly(
                nameof(FooterMenuItems),
                typeof(IList),
                typeof(NavigationView),
                new PropertyMetadata(OnFooterMenuItemsPropertyChanged));

        private static readonly AvaloniaProperty FooterMenuItemsProperty =
            FooterMenuItemsPropertyKey.AvaloniaProperty;

        public IList FooterMenuItems
        {
            get => (IList)GetValue(FooterMenuItemsProperty);
        }

        private static void OnFooterMenuItemsPropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationView)sender;
            owner.PropertyChanged(args);
        }

        #endregion

        #region FooterMenuItemsSource

        public static readonly AvaloniaProperty FooterMenuItemsSourceProperty =
            AvaloniaProperty.Register(
                nameof(FooterMenuItemsSource),
                typeof(object),
                typeof(NavigationView),
                new PropertyMetadata(OnFooterMenuItemsSourcePropertyChanged));

        public object FooterMenuItemsSource
        {
            get => GetValue(FooterMenuItemsSourceProperty);
            set => SetValue(FooterMenuItemsSourceProperty, value);
        }

        private static void OnFooterMenuItemsSourcePropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationView)sender;
            owner.PropertyChanged(args);
        }

        #endregion

        #region PaneFooter

        public static readonly AvaloniaProperty PaneFooterProperty =
            AvaloniaProperty.Register(
                nameof(PaneFooter),
                typeof(IControl),
                typeof(NavigationView),
                new PropertyMetadata(OnPaneFooterPropertyChanged));

        public IControl PaneFooter
        {
            get => (IControl)GetValue(PaneFooterProperty);
            set => SetValue(PaneFooterProperty, value);
        }

        private static void OnPaneFooterPropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationView)sender;
            owner.PropertyChanged(args);
        }

        #endregion

        #region Header

        public static readonly AvaloniaProperty HeaderProperty =
            AvaloniaProperty.Register(
                nameof(Header),
                typeof(object),
                typeof(NavigationView),
                new PropertyMetadata(OnHeaderPropertyChanged));

        public object Header
        {
            get => GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        private static void OnHeaderPropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationView)sender;
            owner.PropertyChanged(args);
        }

        #endregion

        #region HeaderTemplate

        public static readonly AvaloniaProperty HeaderTemplateProperty =
            AvaloniaProperty.Register(
                nameof(HeaderTemplate),
                typeof(DataTemplate),
                typeof(NavigationView),
                new PropertyMetadata(OnHeaderTemplatePropertyChanged));

        public DataTemplate HeaderTemplate
        {
            get => (DataTemplate)GetValue(HeaderTemplateProperty);
            set => SetValue(HeaderTemplateProperty, value);
        }

        private static void OnHeaderTemplatePropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationView)sender;
            owner.PropertyChanged(args);
        }

        #endregion

        #region DisplayMode

        private static readonly AvaloniaPropertyKey DisplayModePropertyKey =
            AvaloniaProperty.RegisterReadOnly(
                nameof(DisplayMode),
                typeof(NavigationViewDisplayMode),
                typeof(NavigationView),
                new PropertyMetadata(NavigationViewDisplayMode.Minimal, OnDisplayModePropertyChanged));

        public static readonly AvaloniaProperty DisplayModeProperty = DisplayModePropertyKey.AvaloniaProperty;

        public NavigationViewDisplayMode DisplayMode
        {
            get => (NavigationViewDisplayMode)GetValue(DisplayModeProperty);
        }

        private static void OnDisplayModePropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationView)sender;
            owner.PropertyChanged(args);
        }

        #endregion

        #region IsSettingsVisible

        public static readonly AvaloniaProperty IsSettingsVisibleProperty =
            AvaloniaProperty.Register(
                nameof(IsSettingsVisible),
                typeof(bool),
                typeof(NavigationView),
                new PropertyMetadata(true, OnIsSettingsVisiblePropertyChanged));

        public bool IsSettingsVisible
        {
            get => (bool)GetValue(IsSettingsVisibleProperty);
            set => SetValue(IsSettingsVisibleProperty, value);
        }

        private static void OnIsSettingsVisiblePropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationView)sender;
            owner.PropertyChanged(args);
        }

        #endregion

        #region IsPaneToggleButtonVisible

        public static readonly AvaloniaProperty IsPaneToggleButtonVisibleProperty =
            AvaloniaProperty.Register(
                nameof(IsPaneToggleButtonVisible),
                typeof(bool),
                typeof(NavigationView),
                new PropertyMetadata(true, OnIsPaneToggleButtonVisiblePropertyChanged));

        public bool IsPaneToggleButtonVisible
        {
            get => (bool)GetValue(IsPaneToggleButtonVisibleProperty);
            set => SetValue(IsPaneToggleButtonVisibleProperty, value);
        }

        private static void OnIsPaneToggleButtonVisiblePropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationView)sender;
            owner.PropertyChanged(args);
        }

        #endregion

        #region AlwaysShowHeader

        public static readonly AvaloniaProperty AlwaysShowHeaderProperty =
            AvaloniaProperty.Register(
                nameof(AlwaysShowHeader),
                typeof(bool),
                typeof(NavigationView),
                new PropertyMetadata(true, OnAlwaysShowHeaderPropertyChanged));

        public bool AlwaysShowHeader
        {
            get => (bool)GetValue(AlwaysShowHeaderProperty);
            set => SetValue(AlwaysShowHeaderProperty, value);
        }

        private static void OnAlwaysShowHeaderPropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationView)sender;
            owner.PropertyChanged(args);
        }

        #endregion

        #region CompactPaneLength

        public static readonly AvaloniaProperty CompactPaneLengthProperty =
            AvaloniaProperty.Register(
                nameof(CompactPaneLength),
                typeof(double),
                typeof(NavigationView),
                new PropertyMetadata(48.0, OnCompactPaneLengthPropertyChanged, CoerceToGreaterThanZero));

        public double CompactPaneLength
        {
            get => (double)GetValue(CompactPaneLengthProperty);
            set => SetValue(CompactPaneLengthProperty, value);
        }

        private static void OnCompactPaneLengthPropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationView)sender;
            owner.PropertyChanged(args);
        }

        #endregion

        #region OpenPaneLength

        public static readonly AvaloniaProperty OpenPaneLengthProperty =
            AvaloniaProperty.Register(
                nameof(OpenPaneLength),
                typeof(double),
                typeof(NavigationView),
                new PropertyMetadata(320.0, OnOpenPaneLengthPropertyChanged, CoerceToGreaterThanZero));

        public double OpenPaneLength
        {
            get => (double)GetValue(OpenPaneLengthProperty);
            set => SetValue(OpenPaneLengthProperty, value);
        }

        private static void OnOpenPaneLengthPropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationView)sender;
            owner.PropertyChanged(args);
        }

        #endregion

        #region PaneToggleButtonStyle

        public static readonly AvaloniaProperty PaneToggleButtonStyleProperty =
            AvaloniaProperty.Register(
                nameof(PaneToggleButtonStyle),
                typeof(Style),
                typeof(NavigationView),
                new PropertyMetadata(OnPaneToggleButtonStylePropertyChanged));

        public Style PaneToggleButtonStyle
        {
            get => (Style)GetValue(PaneToggleButtonStyleProperty);
            set => SetValue(PaneToggleButtonStyleProperty, value);
        }

        private static void OnPaneToggleButtonStylePropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationView)sender;
            owner.PropertyChanged(args);
        }

        #endregion

        #region SelectedItem

        public static readonly AvaloniaProperty SelectedItemProperty =
            AvaloniaProperty.Register(
                nameof(SelectedItem),
                typeof(object),
                typeof(NavigationView),
                new PropertyMetadata(OnSelectedItemPropertyChanged));

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        private static void OnSelectedItemPropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationView)sender;
            owner.PropertyChanged(args);
        }

        #endregion

        #region MenuItems

        private static readonly AvaloniaPropertyKey MenuItemsPropertyKey =
            AvaloniaProperty.RegisterReadOnly(
                nameof(MenuItems),
                typeof(IList),
                typeof(NavigationView),
                new PropertyMetadata(OnMenuItemsPropertyChanged));

        private static readonly AvaloniaProperty MenuItemsProperty =
            MenuItemsPropertyKey.AvaloniaProperty;

        public IList MenuItems
        {
            get => (IList)GetValue(MenuItemsProperty);
        }

        private static void OnMenuItemsPropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationView)sender;
            owner.PropertyChanged(args);
        }

        #endregion

        #region MenuItemsSource

        public static readonly AvaloniaProperty MenuItemsSourceProperty =
            AvaloniaProperty.Register(
                nameof(MenuItemsSource),
                typeof(object),
                typeof(NavigationView),
                new PropertyMetadata(OnMenuItemsSourcePropertyChanged));

        public object MenuItemsSource
        {
            get => GetValue(MenuItemsSourceProperty);
            set => SetValue(MenuItemsSourceProperty, value);
        }

        private static void OnMenuItemsSourcePropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationView)sender;
            owner.PropertyChanged(args);
        }

        #endregion

        #region SettingsItem

        private static readonly AvaloniaPropertyKey SettingsItemPropertyKey =
            AvaloniaProperty.RegisterReadOnly(
                nameof(SettingsItem),
                typeof(object),
                typeof(NavigationView),
                new PropertyMetadata(OnSettingsItemPropertyChanged));

        public static readonly AvaloniaProperty SettingsItemProperty = SettingsItemPropertyKey.AvaloniaProperty;

        public object SettingsItem
        {
            get => GetValue(SettingsItemProperty);
        }

        private static void OnSettingsItemPropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationView)sender;
            owner.PropertyChanged(args);
        }

        #endregion

        #region AutoSuggestBox

        public static readonly AvaloniaProperty AutoSuggestBoxProperty =
            AvaloniaProperty.Register(
                nameof(AutoSuggestBox),
                typeof(AutoSuggestBox),
                typeof(NavigationView),
                new PropertyMetadata(OnAutoSuggestBoxPropertyChanged));

        public AutoSuggestBox AutoSuggestBox
        {
            get => (AutoSuggestBox)GetValue(AutoSuggestBoxProperty);
            set => SetValue(AutoSuggestBoxProperty, value);
        }

        private static void OnAutoSuggestBoxPropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationView)sender;
            owner.PropertyChanged(args);
        }

        #endregion

        #region MenuItemTemplate

        public static readonly AvaloniaProperty MenuItemTemplateProperty =
            AvaloniaProperty.Register(
                nameof(MenuItemTemplate),
                typeof(DataTemplate),
                typeof(NavigationView),
                new PropertyMetadata(OnMenuItemTemplatePropertyChanged));

        public DataTemplate MenuItemTemplate
        {
            get => (DataTemplate)GetValue(MenuItemTemplateProperty);
            set => SetValue(MenuItemTemplateProperty, value);
        }

        private static void OnMenuItemTemplatePropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationView)sender;
            owner.PropertyChanged(args);
        }

        #endregion

        #region MenuItemTemplateSelector

        public static readonly AvaloniaProperty MenuItemTemplateSelectorProperty =
            AvaloniaProperty.Register(
                nameof(MenuItemTemplateSelector),
                typeof(DataTemplateSelector),
                typeof(NavigationView),
                new PropertyMetadata(OnMenuItemTemplateSelectorPropertyChanged));

        public DataTemplateSelector MenuItemTemplateSelector
        {
            get => (DataTemplateSelector)GetValue(MenuItemTemplateSelectorProperty);
            set => SetValue(MenuItemTemplateSelectorProperty, value);
        }

        private static void OnMenuItemTemplateSelectorPropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationView)sender;
            owner.PropertyChanged(args);
        }

        #endregion

        #region MenuItemContainerStyle

        public static readonly AvaloniaProperty MenuItemContainerStyleProperty =
            AvaloniaProperty.Register(
                nameof(MenuItemContainerStyle),
                typeof(Style),
                typeof(NavigationView),
                new PropertyMetadata(OnMenuItemContainerStylePropertyChanged));

        public Style MenuItemContainerStyle
        {
            get => (Style)GetValue(MenuItemContainerStyleProperty);
            set => SetValue(MenuItemContainerStyleProperty, value);
        }

        private static void OnMenuItemContainerStylePropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationView)sender;
            owner.PropertyChanged(args);
        }

        #endregion

        #region MenuItemContainerStyleSelector

        public static readonly AvaloniaProperty MenuItemContainerStyleSelectorProperty =
            AvaloniaProperty.Register(
                nameof(MenuItemContainerStyleSelector),
                typeof(StyleSelector),
                typeof(NavigationView),
                new PropertyMetadata(OnMenuItemContainerStyleSelectorPropertyChanged));

        public StyleSelector MenuItemContainerStyleSelector
        {
            get => (StyleSelector)GetValue(MenuItemContainerStyleSelectorProperty);
            set => SetValue(MenuItemContainerStyleSelectorProperty, value);
        }

        private static void OnMenuItemContainerStyleSelectorPropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            ((NavigationView)sender).OnMenuItemContainerStyleSelectorPropertyChanged(args);
        }

        private void OnMenuItemContainerStyleSelectorPropertyChanged(AvaloniaPropertyChangedEventArgs args)
        {

        }

        #endregion

        #region IsBackButtonVisible

        public static readonly AvaloniaProperty IsBackButtonVisibleProperty =
            AvaloniaProperty.Register(
                nameof(IsBackButtonVisible),
                typeof(NavigationViewBackButtonVisible),
                typeof(NavigationView),
                new PropertyMetadata(NavigationViewBackButtonVisible.Auto, OnIsBackButtonVisiblePropertyChanged));

        public NavigationViewBackButtonVisible IsBackButtonVisible
        {
            get => (NavigationViewBackButtonVisible)GetValue(IsBackButtonVisibleProperty);
            set => SetValue(IsBackButtonVisibleProperty, value);
        }

        private static void OnIsBackButtonVisiblePropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationView)sender;
            owner.PropertyChanged(args);
        }

        #endregion

        #region IsBackEnabled

        public static readonly AvaloniaProperty IsBackEnabledProperty =
            AvaloniaProperty.Register(
                nameof(IsBackEnabled),
                typeof(bool),
                typeof(NavigationView),
                new PropertyMetadata(OnIsBackEnabledPropertyChanged));

        public bool IsBackEnabled
        {
            get => (bool)GetValue(IsBackEnabledProperty);
            set => SetValue(IsBackEnabledProperty, value);
        }

        private static void OnIsBackEnabledPropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationView)sender;
            owner.PropertyChanged(args);
        }

        #endregion

        #region PaneTitle

        public static readonly AvaloniaProperty PaneTitleProperty =
            AvaloniaProperty.Register(
                nameof(PaneTitle),
                typeof(string),
                typeof(NavigationView),
                new PropertyMetadata(string.Empty, OnPaneTitlePropertyChanged));

        public string PaneTitle
        {
            get => (string)GetValue(PaneTitleProperty);
            set => SetValue(PaneTitleProperty, value);
        }

        private static void OnPaneTitlePropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationView)sender;
            owner.PropertyChanged(args);
        }

        #endregion

        #region PaneDisplayMode

        public static readonly AvaloniaProperty PaneDisplayModeProperty =
            AvaloniaProperty.Register(
                nameof(PaneDisplayMode),
                typeof(NavigationViewPaneDisplayMode),
                typeof(NavigationView),
                new PropertyMetadata(NavigationViewPaneDisplayMode.Auto, OnPaneDisplayModePropertyChanged));

        public NavigationViewPaneDisplayMode PaneDisplayMode
        {
            get => (NavigationViewPaneDisplayMode)GetValue(PaneDisplayModeProperty);
            set => SetValue(PaneDisplayModeProperty, value);
        }

        private static void OnPaneDisplayModePropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationView)sender;
            owner.PropertyChanged(args);
        }

        #endregion

        #region PaneHeader

        public static readonly AvaloniaProperty PaneHeaderProperty =
            AvaloniaProperty.Register(
                nameof(PaneHeader),
                typeof(IControl),
                typeof(NavigationView),
                null);

        public IControl PaneHeader
        {
            get => (IControl)GetValue(PaneHeaderProperty);
            set => SetValue(PaneHeaderProperty, value);
        }

        #endregion

        #region PaneCustomContent

        public static readonly AvaloniaProperty PaneCustomContentProperty =
            AvaloniaProperty.Register(
                nameof(PaneCustomContent),
                typeof(IControl),
                typeof(NavigationView),
                null);

        public IControl PaneCustomContent
        {
            get => (IControl)GetValue(PaneCustomContentProperty);
            set => SetValue(PaneCustomContentProperty, value);
        }

        #endregion

        #region ContentOverlay

        public static readonly AvaloniaProperty ContentOverlayProperty =
            AvaloniaProperty.Register(
                nameof(ContentOverlay),
                typeof(IControl),
                typeof(NavigationView),
                null);

        public IControl ContentOverlay
        {
            get => (IControl)GetValue(ContentOverlayProperty);
            set => SetValue(ContentOverlayProperty, value);
        }

        #endregion

        #region IsPaneVisible

        public static readonly AvaloniaProperty IsPaneVisibleProperty =
            AvaloniaProperty.Register(
                nameof(IsPaneVisible),
                typeof(bool),
                typeof(NavigationView),
                new PropertyMetadata(true, OnIsPaneVisiblePropertyChanged));

        public bool IsPaneVisible
        {
            get => (bool)GetValue(IsPaneVisibleProperty);
            set => SetValue(IsPaneVisibleProperty, value);
        }

        private static void OnIsPaneVisiblePropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationView)sender;
            owner.PropertyChanged(args);
        }

        #endregion

        #region SelectionFollowsFocus

        public static readonly AvaloniaProperty SelectionFollowsFocusProperty =
            AvaloniaProperty.Register(
                nameof(SelectionFollowsFocus),
                typeof(NavigationViewSelectionFollowsFocus),
                typeof(NavigationView),
                new PropertyMetadata(NavigationViewSelectionFollowsFocus.Disabled, OnSelectionFollowsFocusPropertyChanged));

        public NavigationViewSelectionFollowsFocus SelectionFollowsFocus
        {
            get => (NavigationViewSelectionFollowsFocus)GetValue(SelectionFollowsFocusProperty);
            set => SetValue(SelectionFollowsFocusProperty, value);
        }

        private static void OnSelectionFollowsFocusPropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationView)sender;
            owner.PropertyChanged(args);
        }

        #endregion

        #region TemplateSettings

        private static readonly AvaloniaPropertyKey TemplateSettingsPropertyKey =
            AvaloniaProperty.RegisterReadOnly(
                nameof(TemplateSettings),
                typeof(NavigationViewTemplateSettings),
                typeof(NavigationView),
                null);

        public static readonly AvaloniaProperty TemplateSettingsProperty =
            TemplateSettingsPropertyKey.AvaloniaProperty;

        public NavigationViewTemplateSettings TemplateSettings
        {
            get => (NavigationViewTemplateSettings)GetValue(TemplateSettingsProperty);
            private set => SetValue(TemplateSettingsPropertyKey, value);
        }

        #endregion

        #region ShoulderNavigationEnabled

        public static readonly AvaloniaProperty ShoulderNavigationEnabledProperty =
            AvaloniaProperty.Register(
                nameof(ShoulderNavigationEnabled),
                typeof(NavigationViewShoulderNavigationEnabled),
                typeof(NavigationView),
                new PropertyMetadata(NavigationViewShoulderNavigationEnabled.Never, OnShoulderNavigationEnabledPropertyChanged));

        public NavigationViewShoulderNavigationEnabled ShoulderNavigationEnabled
        {
            get => (NavigationViewShoulderNavigationEnabled)GetValue(ShoulderNavigationEnabledProperty);
            set => SetValue(ShoulderNavigationEnabledProperty, value);
        }

        private static void OnShoulderNavigationEnabledPropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationView)sender;
            owner.PropertyChanged(args);
        }

        #endregion

        #region OverflowLabelMode

        public static readonly AvaloniaProperty OverflowLabelModeProperty =
            AvaloniaProperty.Register(
                nameof(OverflowLabelMode),
                typeof(NavigationViewOverflowLabelMode),
                typeof(NavigationView),
                new PropertyMetadata(NavigationViewOverflowLabelMode.MoreLabel, OnOverflowLabelModePropertyChanged));

        public NavigationViewOverflowLabelMode OverflowLabelMode
        {
            get => (NavigationViewOverflowLabelMode)GetValue(OverflowLabelModeProperty);
            set => SetValue(OverflowLabelModeProperty, value);
        }

        private static void OnOverflowLabelModePropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationView)sender;
            owner.PropertyChanged(args);
        }

        #endregion

        #region IsTitleBarAutoPaddingEnabled

        public static readonly AvaloniaProperty IsTitleBarAutoPaddingEnabledProperty =
            AvaloniaProperty.Register(
                nameof(IsTitleBarAutoPaddingEnabled),
                typeof(bool),
                typeof(NavigationView),
                new PropertyMetadata(true, OnIsTitleBarAutoPaddingEnabledPropertyChanged));

        public bool IsTitleBarAutoPaddingEnabled
        {
            get => (bool)GetValue(IsTitleBarAutoPaddingEnabledProperty);
            set => SetValue(IsTitleBarAutoPaddingEnabledProperty, value);
        }

        private static void OnIsTitleBarAutoPaddingEnabledPropertyChanged(AvaloniaObject sender, AvaloniaPropertyChangedEventArgs args)
        {
            var owner = (NavigationView)sender;
            owner.PropertyChanged(args);
        }

        #endregion

        public event TypedEventHandler<NavigationView, NavigationViewSelectionChangedEventArgs> SelectionChanged;
        public event TypedEventHandler<NavigationView, NavigationViewItemInvokedEventArgs> ItemInvoked;
        public event TypedEventHandler<NavigationView, NavigationViewDisplayModeChangedEventArgs> DisplayModeChanged;
        public event TypedEventHandler<NavigationView, NavigationViewBackRequestedEventArgs> BackRequested;
        public event TypedEventHandler<NavigationView, object> PaneClosed;
        public event TypedEventHandler<NavigationView, NavigationViewPaneClosingEventArgs> PaneClosing;
        public event TypedEventHandler<NavigationView, object> PaneOpened;
        public event TypedEventHandler<NavigationView, object> PaneOpening;
        public event TypedEventHandler<NavigationView, NavigationViewItemExpandingEventArgs> Expanding;
        public event TypedEventHandler<NavigationView, NavigationViewItemCollapsedEventArgs> Collapsed;

        private static object CoerceToGreaterThanZero(AvaloniaObject d, object baseValue)
        {
            if (baseValue is double value)
            {
                ((NavigationView)d).CoerceToGreaterThanZero(ref value);
                return value;
            }
            return baseValue;
        }
    }
}
