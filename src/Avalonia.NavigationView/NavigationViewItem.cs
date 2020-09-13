// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;

using static Avalonia.Controls.NavigationViewItemHelper;

namespace Avalonia.Controls
{
    public partial class NavigationViewItem : NavigationViewItemBase
    {
        const string c_navigationViewItemPresenterName = "NavigationViewItemPresenter";
        const string c_repeater = "NavigationViewItemMenuItemsHost";
        const string c_rootGrid = "NVIRootGrid";
        const string c_childrenFlyout = "ChildrenFlyout";
        const string c_flyoutContentGrid = "FlyoutContentGrid";

        public NavigationViewItem()
        {
            SetValue(MenuItemsPropertyKey, new ObservableCollection<object>());
        }

        internal void UpdateVisualStateNoTransition()
        {
            UpdateVisualState(false /*useTransition*/);
        }

        private protected override void OnNavigationViewItemBaseDepthChanged()
        {
            UpdateItemIndentation();
            PropagateDepthToChildren(Depth + 1);
        }

        private protected override void OnNavigationViewItemBaseIsSelectedChanged()
        {
            UpdateVisualStateForPointer();
        }

        private protected override void OnNavigationViewItemBasePositionChanged()
        {
            UpdateVisualStateNoTransition();
            ReparentRepeater();
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            // Stop UpdateVisualState before template is applied. Otherwise the visuals may be unexpected
            m_appliedTemplate = false;

            UnhookEventsAndClearFields();

            base.OnApplyTemplate(e);

            // Find selection indicator
            // Retrieve pointers to stable controls 
            m_helper.Init(this);

            if (e.NameScope.Find<Grid>(c_rootGrid) is { } rootGrid)
            {
                m_rootGrid = rootGrid;

                if (FlyoutBase.GetAttachedFlyout(rootGrid) is { } flyoutBase)
                {
                    m_flyoutClosingRevoker = new FlyoutBaseClosingRevoker(flyoutBase, OnFlyoutClosing);
                }
            }

            HookInputEvents(e.NameScope);

            m_toolTip = e.NameScope.Find<ToolTip>("ToolTip");

            if (GetSplitView() is { } splitView)
            {
                splitView.PropertyChanged += OnSplitViewPropertyChanged;

                UpdateCompactPaneLength();
                UpdateIsClosedCompact();
            }

            // Retrieve reference to NavigationView
            if (GetNavigationView() is { } nvImpl)
            {
                if (e.NameScope.Find<ItemsRepeater>(c_repeater) is { } repeater)
                {
                    m_repeater = repeater;

                    // Primary element setup happens in NavigationView
                    m_repeaterElementPreparedRevoker = new ItemsRepeaterElementPreparedRevoker(repeater, nvImpl.OnRepeaterElementPrepared);
                    m_repeaterElementClearingRevoker = new ItemsRepeaterElementClearingRevoker(repeater, nvImpl.OnRepeaterElementClearing);

                    repeater.ItemTemplate = nvImpl.GetNavigationViewItemsFactory();
                }

                UpdateRepeaterItemsSource();
            }

            if (e.NameScope.Find<FlyoutBase>(c_childrenFlyout) is { } childrenFlyout)
            {
                childrenFlyout.Offset = 0;
            }

            m_flyoutContentGrid = e.NameScope.Find<Grid>(c_flyoutContentGrid);

            m_appliedTemplate = true;

            UpdateItemIndentation();
            UpdateVisualStateNoTransition();
            ReparentRepeater();
            // We dont want to update the repeater visibilty during OnApplyTemplate if NavigationView is in a mode when items are shown in a flyout
            if (!ShouldRepeaterShowInFlyout())
            {
                ShowHideChildren();
            }

            /*
            var visual = ElementCompositionPreview.GetElementVisual(this);
            NavigationView.CreateAndAttachHeaderAnimation(visual);
            */
        }

        void UpdateRepeaterItemsSource()
        {
            if (m_repeater is { } repeater)
            {
                IEnumerable itemsSource;
                {
                    IEnumerable init()
                    {
                        if (MenuItemsSource is { } menuItemsSource)
                        {
                            return menuItemsSource;
                        }
                        return MenuItems;
                    }
                    itemsSource = init();
                }
                m_itemsSourceViewCollectionChangedRevoker?.Revoke();
                repeater.Items = itemsSource;
                m_itemsSourceViewCollectionChangedRevoker = new ItemsSourceView.CollectionChangedRevoker(repeater.ItemsSourceView, OnItemsSourceViewChanged);
            }
        }

        private void OnItemsSourceViewChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            UpdateVisualStateForChevron();
        }

        internal IControl GetSelectionIndicator()
        {
            var selectIndicator = m_helper.GetSelectionIndicator();
            if (GetPresenter() is { } presenter)
            {
                selectIndicator = presenter.GetSelectionIndicator();
            }
            return selectIndicator;
        }

        private void OnSplitViewPropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property == SplitView.CompactPaneLengthProperty)
            {
                UpdateCompactPaneLength();
            }
            else if (e.Property == SplitView.IsPaneOpenProperty ||
                e.Property == SplitView.DisplayModeProperty)
            {
                UpdateIsClosedCompact();
                ReparentRepeater();
            }
        }

        void UpdateCompactPaneLength()
        {
            if (GetSplitView() is { } splitView)
            {
                SetValue(CompactPaneLengthPropertyKey, splitView.CompactPaneLength);

                // Only update when on left
                if (GetPresenter() is { } presenter)
                {
                    presenter.UpdateCompactPaneLength(splitView.CompactPaneLength, IsOnLeftNav());
                }
            }
        }

        internal void UpdateIsClosedCompact()
        {
            if (GetSplitView() is { } splitView)
            {
                // Check if the pane is closed and if the splitview is in either compact mode.
                var closed = !splitView.IsPaneOpen;
                var compact = splitView.DisplayMode == SplitViewDisplayMode.CompactOverlay || splitView.DisplayMode == SplitViewDisplayMode.CompactInline;
                m_isClosedCompact = closed && compact;

                UpdateVisualState(true /*useTransitions*/);

                if (GetPresenter() is { } presenter)
                {
                    presenter.UpdateClosedCompactVisualState(IsTopLevelItem, closed, compact);
                }
            }
        }

        void UpdateNavigationViewItemToolTip()
        {
            var toolTipContent = ToolTip.GetTip(this);

            // no custom tooltip, then use suggested tooltip
            if (toolTipContent == null || toolTipContent == m_suggestedToolTipContent)
            {
                if (ShouldEnableToolTip())
                {
                    ToolTip.SetTip(this, m_suggestedToolTipContent);
                }
                else
                {
                    ToolTip.SetTip(this, null);
                }
            }
        }

        void SuggestedToolTipChanged(object newContent)
        {
            var potentialString = newContent;
            bool stringableToolTip = (potentialString != null && potentialString is string);

            object newToolTipContent = null;
            if (stringableToolTip)
            {
                newToolTipContent = newContent;
            }

            // Both customer and NavigationViewItem can update ToolTipContent by ToolTipService.SetToolTip or XAML
            // If the ToolTipContent is not the same as m_suggestedToolTipContent, then it's set by customer.
            // Customer's ToolTip take high priority, and we never override Customer's ToolTip.
            var toolTipContent = ToolTip.GetTip(this);
            if (m_suggestedToolTipContent is { } oldToolTipContent)
            {
                if (oldToolTipContent == toolTipContent)
                {
                    ToolTip.SetTip(this, null);
                }
            }

            m_suggestedToolTipContent = newToolTipContent;
        }

        void OnIsExpandedPropertyChanged(AvaloniaPropertyChangedEventArgs args)
        {
            // TODO
            //if (FrameworkElementAutomationPeer.FromElement(this) is AutomationPeer peer)
            //{
            //    var navViewItemPeer = (NavigationViewItemAutomationPeer)peer;
            //    navViewItemPeer.RaiseExpandCollapseAutomationEvent(
            //        IsExpanded ?
            //            ExpandCollapseState.Expanded :
            //            ExpandCollapseState.Collapsed
            //    );
            //}
        }

        void OnIconPropertyChanged(AvaloniaPropertyChangedEventArgs args)
        {
            UpdateVisualStateNoTransition();
        }

        void OnMenuItemsPropertyChanged(AvaloniaPropertyChangedEventArgs args)
        {
            UpdateRepeaterItemsSource();
            UpdateVisualStateForChevron();
        }

        void OnMenuItemsSourcePropertyChanged(AvaloniaPropertyChangedEventArgs args)
        {
            UpdateRepeaterItemsSource();
            UpdateVisualStateForChevron();
        }

        void OnHasUnrealizedChildrenPropertyChanged(AvaloniaPropertyChangedEventArgs args)
        {
            UpdateVisualStateForChevron();
        }

        void ShowSelectionIndicator(bool visible)
        {
            if (GetSelectionIndicator() is { } selectionIndicator)
            {
                selectionIndicator.Opacity = visible ? 1.0 : 0.0;
            }
        }

        void UpdateVisualStateForIconAndContent(bool showIcon, bool showContent)
        {
            if (m_navigationViewItemPresenter is { } presenter)
            {
                //var stateName = showIcon ? (showContent ? "IconOnLeft" : "IconOnly") : "ContentOnly";
                //VisualStateManager.GoToState(presenter, stateName, false /*useTransitions*/);
                PseudoClasses.Set(":hide-icon", !showIcon);
                PseudoClasses.Set(":hide-content", !showContent);
            }
        }

        void UpdateVisualStateForNavigationViewPositionChange()
        {
            var position = Position;
            var stateName = c_OnLeftNavigation;

            bool handled = false;

            switch (position)
            {
                case NavigationViewRepeaterPosition.LeftNav:
                case NavigationViewRepeaterPosition.LeftFooter:
                    if (SharedHelpers.IsRS4OrHigher() && false /*Application.Current.FocusVisualKind == FocusVisualKind.Reveal*/)
                    {
                        // OnLeftNavigationReveal is introduced in RS6. 
                        // Will fallback to stateName for the customer who re-template rs5 NavigationViewItem
                        if (VisualStateManager.GoToState(this, c_OnLeftNavigationReveal, false /*useTransitions*/))
                        {
                            handled = true;
                        }
                    }
                    break;
                case NavigationViewRepeaterPosition.TopPrimary:
                case NavigationViewRepeaterPosition.TopFooter:
                    if (SharedHelpers.IsRS4OrHigher() && false /*Application.Current.FocusVisualKind == FocusVisualKind.Reveal*/)
                    {
                        stateName = c_OnTopNavigationPrimaryReveal;
                    }
                    else
                    {
                        stateName = c_OnTopNavigationPrimary;
                    }
                    break;
                case NavigationViewRepeaterPosition.TopOverflow:
                    stateName = c_OnTopNavigationOverflow;
                    break;
            }

            if (!handled)
            {
                VisualStateManager.GoToState(this, stateName, false /*useTransitions*/);
            }
        }

        void UpdateVisualStateForKeyboardFocusedState()
        {
            var focusState = "KeyboardNormal";
            if (m_hasKeyboardFocus)
            {
                focusState = "KeyboardFocused";
            }

            VisualStateManager.GoToState(this, focusState, false /*useTransitions*/);
        }

        void UpdateVisualStateForToolTip()
        {
            // Since RS5, ToolTip apply to NavigationViewItem directly to make Keyboard focus has tooltip too.
            // If ToolTip TemplatePart is detected, fallback to old logic and apply ToolTip on TemplatePart.
            if (m_toolTip is { } toolTip)
            {
                var shouldEnableToolTip = ShouldEnableToolTip();
                var toolTipContent = m_suggestedToolTipContent;
                if (shouldEnableToolTip && toolTipContent != null)
                {
                    toolTip.Content = toolTipContent;
                    toolTip.IsEnabled = true;
                }
                else
                {
                    toolTip.Content = null;
                    toolTip.IsEnabled = false;
                }
            }
            else
            {
                UpdateNavigationViewItemToolTip();
            }
        }

        void UpdateVisualStateForPointer()
        {
            // DisabledStates and CommonStates
            var enabledStateValue = c_enabled;
            bool isSelected = IsSelected;
            var selectedStateValue = c_normal;
            if (IsEnabled)
            {
                if (isSelected)
                {
                    if (m_isPressed)
                    {
                        selectedStateValue = c_pressedSelected;
                    }
                    else if (m_isPointerOver)
                    {
                        selectedStateValue = c_pointerOverSelected;
                    }
                    else
                    {
                        selectedStateValue = c_selected;
                    }
                }
                else if (m_isPointerOver)
                {
                    if (m_isPressed)
                    {
                        selectedStateValue = c_pressed;
                    }
                    else
                    {
                        selectedStateValue = c_pointerOver;
                    }
                }
                else if (m_isPressed)
                {
                    selectedStateValue = c_pressed;
                }
            }
            else
            {
                enabledStateValue = c_disabled;
                if (isSelected)
                {
                    selectedStateValue = c_selected;
                }
            }

            // There are scenarios where the presenter may not exist.
            // For example, the top nav settings item. In that case,
            // update the states for the item itself.
            if (m_navigationViewItemPresenter is { } presenter)
            {
                VisualStateManager.GoToState(presenter, enabledStateValue, true);
                VisualStateManager.GoToState(presenter, selectedStateValue, true);
            }
            else
            {
                VisualStateManager.GoToState(this, enabledStateValue, true);
                VisualStateManager.GoToState(this, selectedStateValue, true);
            }
        }

        void UpdateVisualState(bool useTransitions)
        {
            if (!m_appliedTemplate)
                return;

            UpdateVisualStateForPointer();

            UpdateVisualStateForNavigationViewPositionChange();

            bool shouldShowIcon = ShouldShowIcon();
            bool shouldShowContent = ShouldShowContent();

            if (IsOnLeftNav())
            {
                if (m_navigationViewItemPresenter is { } presenter)
                {
                    // Backward Compatibility with RS4-, new implementation prefer IconOnLeft/IconOnly/ContentOnly
                    // VisualStateManager.GoToState(presenter, shouldShowIcon ? "IconVisible" : "IconCollapsed", useTransitions);
                    PseudoClasses.Set(":hide-icon", shouldShowIcon);
                }
            }

            UpdateVisualStateForToolTip();

            UpdateVisualStateForIconAndContent(shouldShowIcon, shouldShowContent);

            // visual state for focus state. top navigation use it to provide different visual for selected and selected+focused
            UpdateVisualStateForKeyboardFocusedState();

            UpdateVisualStateForChevron();
        }

        void UpdateVisualStateForChevron()
        {
            if (m_navigationViewItemPresenter is { } presenter)
            {
                //var chevronState = HasChildren() && !(m_isClosedCompact && ShouldRepeaterShowInFlyout())
                //    ? (IsExpanded
                //        ? c_chevronVisibleOpen
                //        : c_chevronVisibleClosed)
                //    : c_chevronHidden;
                //VisualStateManager.GoToState(presenter, chevronState, true);
                PseudoClasses.Set(":chevron-visible", HasChildren() && !(m_isClosedCompact && ShouldRepeaterShowInFlyout()));
                PseudoClasses.Set(":chevron-expanded", IsExpanded);
            }
        }

        internal bool HasChildren()
        {
            return MenuItems.Count > 0 || (MenuItemsSource != null && m_repeater.ItemsSourceView.Count > 0) || HasUnrealizedChildren;
        }

        bool ShouldShowIcon()
        {
            return Icon != null;
        }

        bool ShouldEnableToolTip()
        {
            // We may enable Tooltip for IconOnly in the future, but not now
            return IsOnLeftNav() && m_isClosedCompact;
        }

        bool ShouldShowContent()
        {
            return Content != null;
        }

        bool IsOnLeftNav()
        {
            var position = Position;
            return position == NavigationViewRepeaterPosition.LeftNav || position == NavigationViewRepeaterPosition.LeftFooter;
        }

        bool IsOnTopPrimary()
        {
            return Position == NavigationViewRepeaterPosition.TopPrimary;
        }

        IControl GetPresenterOrItem()
        {
            if (m_navigationViewItemPresenter is { } presenter)
            {
                return presenter;
            }
            else
            {
                return this;
            }
        }

        NavigationViewItemPresenter GetPresenter()
        {
            NavigationViewItemPresenter presenter = null;
            if (m_navigationViewItemPresenter != null)
            {
                presenter = m_navigationViewItemPresenter;
            }
            return presenter;
        }

        internal ItemsRepeater GetRepeater() { return m_repeater; }

        internal void ShowHideChildren()
        {
            if (m_repeater is { } repeater)
            {
                bool shouldShowChildren = IsExpanded;
                var visibility = shouldShowChildren;
                repeater.IsVisible = visibility;

                if (ShouldRepeaterShowInFlyout())
                {
                    if (shouldShowChildren)
                    {
                        // Verify that repeater is parented correctly
                        if (!m_isRepeaterParentedToFlyout)
                        {
                            ReparentRepeater();
                        }

                        // There seems to be a race condition happening which sometimes
                        // prevents the opening of the flyout. Queue callback as a workaround.
                        SharedHelpers.QueueCallbackForCompositionRendering(
                            () =>
                            {
                                FlyoutBase.ShowAttachedFlyout(m_rootGrid);
                            });
                    }
                    else
                    {
                        if (FlyoutBase.GetAttachedFlyout(m_rootGrid) is { } flyout)
                        {
                            flyout.Hide();
                        }
                    }
                }
            }
        }

        void ReparentRepeater()
        {
            if (HasChildren())
            {
                if (m_repeater is { } repeater)
                {
                    if (ShouldRepeaterShowInFlyout() && !m_isRepeaterParentedToFlyout)
                    {
                        // Reparent repeater to flyout
                        // TODO: Replace removeatend with something more specific
                        m_rootGrid.Children.Remove(repeater);
                        m_flyoutContentGrid.Children.Add(repeater);
                        m_isRepeaterParentedToFlyout = true;

                        PropagateDepthToChildren(0);
                    }
                    else if (!ShouldRepeaterShowInFlyout() && m_isRepeaterParentedToFlyout)
                    {
                        m_flyoutContentGrid.Children.Remove(repeater);
                        m_rootGrid.Children.Add(repeater);
                        m_isRepeaterParentedToFlyout = false;

                        PropagateDepthToChildren(1);
                    }
                }
            }
        }

        internal bool ShouldRepeaterShowInFlyout()
        {
            return (m_isClosedCompact && IsTopLevelItem) || IsOnTopPrimary();
        }

        internal bool IsRepeaterVisible()
        {
            if (m_repeater is { } repeater)
            {
                return repeater.IsVisible;
            }
            return false;
        }

        void UpdateItemIndentation()
        {
            // Update item indentation based on its depth
            if (m_navigationViewItemPresenter is { } presenter)
            {
                var newLeftMargin = Depth * c_itemIndentation;
                presenter.UpdateContentLeftIndentation(newLeftMargin);
            }
        }

        internal void PropagateDepthToChildren(int depth)
        {
            if (m_repeater is { } repeater)
            {
                var itemsCount = repeater.ItemsSourceView.Count;
                for (int index = 0; index < itemsCount; index++)
                {
                    if (repeater.TryGetElement(index) is { } element)
                    {
                        if (element is NavigationViewItemBase nvib)
                        {
                            nvib.Depth = depth;
                        }
                    }
                }
            }
        }

        internal void OnExpandCollapseChevronTapped(object sender, RoutedEventArgs args)
        {
            IsExpanded = !IsExpanded;
            args.Handled = true;
        }

        void OnFlyoutClosing(object sender, FlyoutBaseClosingEventArgs args)
        {
            IsExpanded = false;
        }

        // IContentControlOverrides / IContentControlOverridesHelper
        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
            SuggestedToolTipChanged(newContent);
            UpdateVisualStateNoTransition();

            if (!IsOnLeftNav())
            {
                // Content has changed for the item, so we want to trigger a re-measure
                if (GetNavigationView() is { } navView)
                {
                    navView.TopNavigationViewItemContentChanged();
                }
            }
        }

        void OnPresenterPointerPressed(object sender, PointerEventArgs args)
        {
            Debug.Assert(!m_isPressed);
            Debug.Assert(!m_isMouseCaptured);

            // TODO: Update to look at presenter instead
            var point = args.GetCurrentPoint(this);
            m_isPressed = point.Properties.IsLeftButtonPressed || point.Properties.IsRightButtonPressed;

            var presenter = GetPresenterOrItem();

            Debug.Assert(presenter != null);

            if (presenter.CaptureMouse())
            {
                m_isMouseCaptured = true;
            }

            UpdateVisualState(true);
        }

        void OnPresenterPointerReleased(object sender, PointerEventArgs args)
        {
            if (m_isPressed)
            {
                m_isPressed = false;

                if (m_isMouseCaptured)
                {
                    var presenter = GetPresenterOrItem();

                    Debug.Assert(presenter != null);

                    presenter.ReleaseMouseCapture();
                }
            }

            UpdateVisualState(true);
        }

        void OnPresenterPointerEntered(object sender, PointerEventArgs args)
        {
            ProcessPointerOver(args);
        }

        void OnPresenterPointerMoved(object sender, PointerEventArgs args)
        {
            ProcessPointerOver(args);
        }

        void OnPresenterPointerExited(object sender, PointerEventArgs args)
        {
            m_isPointerOver = false;
            UpdateVisualState(true);
        }

        void OnPresenterPointerCanceled(object sender, PointerEventArgs args)
        {
            ProcessPointerCanceled(args);
        }

        void OnPresenterPointerCaptureLost(object sender, PointerEventArgs args)
        {
            ProcessPointerCanceled(args);
        }

        protected override void OnPropertyChanged<T>(AvaloniaPropertyChangedEventArgs<T> change)
        {
            if (change.Property == IsEnabledProperty)
            {
                OnIsEnabledChanged();
            }
        }

        void OnIsEnabledChanged()
        {
            if (!IsEnabled)
            {
                m_isPressed = false;
                m_isPointerOver = false;

                if (m_isMouseCaptured)
                {
                    var presenter = GetPresenterOrItem();

                    Debug.Assert(presenter != null);

                    presenter.ReleaseMouseCapture();
                    m_isMouseCaptured = false;
                }
            }

            UpdateVisualState(true);
        }

        internal void RotateExpandCollapseChevron(bool isExpanded)
        {
            if (GetPresenter() is { } presenter)
            {
                presenter.RotateExpandCollapseChevron(isExpanded);
            }
        }

        void ProcessPointerCanceled(PointerEventArgs args)
        {
            m_isPressed = false;
            m_isPointerOver = false;
            m_isMouseCaptured = false;
            UpdateVisualState(true);
        }

        void ProcessPointerOver(PointerEventArgs args)
        {
            if (!m_isPointerOver)
            {
                m_isPointerOver = true;
                UpdateVisualState(true);
            }
        }

        void HookInputEvents(INameScope namescope)
        {
            IControl presenter;
            {
                presenter = init();
                IControl init()
                {
                    if (namescope.Find<NavigationViewItemPresenter>(c_navigationViewItemPresenterName) is { } presenter)
                    {
                        m_navigationViewItemPresenter = presenter;
                        return presenter;
                    }
                    // We don't have a presenter, so we are our own presenter.
                    return this;
                }
            }

            Debug.Assert(presenter != null);

            // Handlers that set flags are skipped when args.Handled is already True.
            presenter.PointerPressed += OnPresenterPointerPressed;
            presenter.PointerEnter += OnPresenterPointerEntered;
            presenter.PointerMoved += OnPresenterPointerMoved;

            // Handlers that reset flags are not skipped when args.Handled is already True to avoid broken states.
            presenter.AddHandler(PointerPressedEvent, new EventHandler<PointerEventArgs>(OnPresenterPointerReleased), RoutingStrategies.Direct, true /*handledEventsToo*/);
            presenter.AddHandler(PointerReleasedEvent, new EventHandler<PointerEventArgs>(OnPresenterPointerExited), RoutingStrategies.Direct, true /*handledEventsToo*/);
            presenter.AddHandler(PointerMovedEvent, new EventHandler<PointerEventArgs>(OnPresenterPointerCaptureLost), RoutingStrategies.Direct, true /*handledEventsToo*/);
        }

        void UnhookInputEvents()
        {
            var presenter = m_navigationViewItemPresenter as IControl ?? this;
            presenter.PointerPressed -= OnPresenterPointerPressed;
            presenter.PointerReleased -= OnPresenterPointerEntered;
            presenter.PointerMoved -= OnPresenterPointerMoved;
            presenter.RemoveHandler(PointerPressedEvent, new EventHandler<PointerEventArgs>(OnPresenterPointerReleased));
            presenter.RemoveHandler(PointerReleasedEvent, new EventHandler<PointerEventArgs>(OnPresenterPointerExited));
            presenter.RemoveHandler(PointerMovedEvent, new EventHandler<PointerEventArgs>(OnPresenterPointerCaptureLost));
        }

        void UnhookEventsAndClearFields()
        {
            UnhookInputEvents();

            m_flyoutClosingRevoker?.Revoke();
            m_splitViewIsPaneOpenChangedRevoker?.Revoke();
            m_splitViewDisplayModeChangedRevoker?.Revoke();
            m_splitViewCompactPaneLengthChangedRevoker?.Revoke();
            m_repeaterElementPreparedRevoker?.Revoke();
            m_repeaterElementClearingRevoker?.Revoke();
            IsEnabledChanged -= OnIsEnabledChanged;
            m_itemsSourceViewCollectionChangedRevoker?.Revoke();

            m_rootGrid = null;
            m_navigationViewItemPresenter = null;
            m_toolTip = null;
            m_repeater = null;
            m_flyoutContentGrid = null;
        }

        SplitViewIsPaneOpenChangedRevoker m_splitViewIsPaneOpenChangedRevoker;
        SplitViewDisplayModeChangedRevoker m_splitViewDisplayModeChangedRevoker;
        SplitViewCompactPaneLengthChangedRevoker m_splitViewCompactPaneLengthChangedRevoker;

        ItemsRepeaterElementPreparedRevoker m_repeaterElementPreparedRevoker;
        ItemsRepeaterElementClearingRevoker m_repeaterElementClearingRevoker;
        ItemsSourceView.CollectionChangedRevoker m_itemsSourceViewCollectionChangedRevoker;

        FlyoutBaseClosingRevoker m_flyoutClosingRevoker;

        ToolTip m_toolTip;
        NavigationViewItemHelper<NavigationViewItem> m_helper = new NavigationViewItemHelper<NavigationViewItem>();
        NavigationViewItemPresenter m_navigationViewItemPresenter;
        object m_suggestedToolTipContent;
        ItemsRepeater m_repeater;
        Grid m_flyoutContentGrid;
        Grid m_rootGrid;

        bool m_isClosedCompact = false;

        bool m_appliedTemplate = false;

        // Visual state tracking
        bool m_isMouseCaptured = false;
        bool m_isPressed = false;
        bool m_isPointerOver = false;

        bool m_isRepeaterParentedToFlyout = false;
    }
}
