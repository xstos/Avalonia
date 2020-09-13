// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
using Avalonia.Media;
using Avalonia.Layout;

namespace Avalonia.Controls.Primitives
{
    public class NavigationViewItemPresenter : ContentControl
    {
        const string c_contentGrid = "PresenterContentRootGrid";
        const string c_expandCollapseChevron = "ExpandCollapseChevron";
        const string c_expandCollapseRotateExpandedStoryboard = "ExpandCollapseRotateExpandedStoryboard";
        const string c_expandCollapseRotateCollapsedStoryboard = "ExpandCollapseRotateCollapsedStoryboard";
        const string c_expandCollapseRotateTransform = "ExpandCollapseChevronRotateTransform";

        const string c_iconBoxColumnDefinitionName = "IconColumn";

        static NavigationViewItemPresenter()
        {
            HorizontalContentAlignmentProperty.OverrideDefaultValue<NavigationViewItemBase>(HorizontalAlignment.Center);
            VerticalContentAlignmentProperty.OverrideDefaultValue<NavigationViewItemBase>(VerticalAlignment.Center);
        }

        #region Icon

        public static readonly StyledProperty<IControl> IconProperty =
            AvaloniaProperty.Register<NavigationViewItemPresenter, IControl>(nameof(Icon));

        public /* IconElement */ IControl Icon
        {
            get => GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        #endregion

        #region UseSystemFocusVisuals

        //public static readonly AvaloniaProperty UseSystemFocusVisualsProperty =
        //    FocusVisualHelper.UseSystemFocusVisualsProperty.AddOwner(typeof(NavigationViewItemPresenter));

        //public bool UseSystemFocusVisuals
        //{
        //    get => (bool)GetValue(UseSystemFocusVisualsProperty);
        //    set => SetValue(UseSystemFocusVisualsProperty, value);
        //}

        #endregion

        #region CornerRadius

        public static readonly StyledProperty<CornerRadius> CornerRadiusProperty =
            Border.CornerRadiusProperty.AddOwner<NavigationViewItemPresenter>();

        public CornerRadius CornerRadius
        {
            get => GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        #endregion

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            // Retrieve pointers to stable controls 
            m_helper.Init(this);

            if (e.NameScope.Find<Grid>(c_contentGrid) is { } contentGrid)
            {
                m_contentGrid = contentGrid;
            }

            if (GetNavigationViewItem() is { } navigationViewItem)
            {
                if (e.NameScope.Find<Grid>(c_expandCollapseChevron) is { } expandCollapseChevron)
                {
                    m_expandCollapseChevron = expandCollapseChevron;
                    expandCollapseChevron.Tapped += navigationViewItem.OnExpandCollapseChevronTapped;
                }
                navigationViewItem.UpdateVisualStateNoTransition();
                navigationViewItem.UpdateIsClosedCompact();

                // We probably switched displaymode, so restore width now, otherwise the next time we will restore is when the CompactPaneLength changes
                if (navigationViewItem.GetNavigationView() is { } navigationView)
                {
                    if (navigationView.PaneDisplayMode != NavigationViewPaneDisplayMode.Top)
                    {
                        UpdateCompactPaneLength(m_compactPaneLengthValue, true);
                    }
                }
            }

            //m_chevronExpandedStoryboard = GetTemplateChildT<Storyboard>(c_expandCollapseRotateExpandedStoryboard, this);
            //m_chevronCollapsedStoryboard = GetTemplateChildT<Storyboard>(c_expandCollapseRotateCollapsedStoryboard, this);
            if (this.GetTemplateRoot() is IControl templateRoot)
            {
                m_chevronExpandedStoryboard = templateRoot.Resources[c_expandCollapseRotateExpandedStoryboard] as Storyboard;
                m_chevronCollapsedStoryboard = templateRoot.Resources[c_expandCollapseRotateCollapsedStoryboard] as Storyboard;
            }

            m_expandCollapseRotateTransform = e.NameScope.Find<RotateTransform>(c_expandCollapseRotateTransform);

            UpdateMargin();
        }

        internal void RotateExpandCollapseChevron(bool isExpanded)
        {
            if (isExpanded)
            {
                if (m_chevronExpandedStoryboard is { } openStoryboard)
                {
                    openStoryboard.Begin();
                }

                if (m_expandCollapseRotateTransform != null)
                {
                    m_expandCollapseRotateTransform.Angle = 180;
                }
            }
            else
            {
                if (m_chevronCollapsedStoryboard is { } closedStoryboard)
                {
                    closedStoryboard.Begin();
                }

                if (m_expandCollapseRotateTransform != null)
                {
                    m_expandCollapseRotateTransform.Angle = 0;
                }
            }
        }

        internal IControl GetSelectionIndicator()
        {
            return m_helper.GetSelectionIndicator();
        }

        // TODO: WPF - GoToElementStateCore
        /*
        bool GoToElementStateCore(string state, bool useTransitions)
        {
            // GoToElementStateCore: Update visualstate for itself.
            // VisualStateManager.GoToState: update visualstate for it's first child.

            // If NavigationViewItemPresenter is used, two sets of VisualStateGroups are supported. One set is help to switch the style and it's NavigationViewItemPresenter itself and defined in NavigationViewItem
            // Another set is defined in style for NavigationViewItemPresenter.
            // OnLeftNavigation, OnTopNavigationPrimary, OnTopNavigationOverflow only apply to itself.
            if (state == c_OnLeftNavigation || state == c_OnLeftNavigationReveal || state == c_OnTopNavigationPrimary
                || state == c_OnTopNavigationPrimaryReveal || state == c_OnTopNavigationOverflow)
            {
                return base.GoToElementStateCore(state, useTransitions);
            }
            return VisualStateManager.GoToState(this, state, useTransitions);
        }
        */

        NavigationViewItem GetNavigationViewItem()
        {
            NavigationViewItem navigationViewItem = null;

            // winrt::AvaloniaObject obj = operator winrt::AvaloniaObject();
            AvaloniaObject obj = this;

            if (SharedHelpers.GetAncestorOfType<NavigationViewItem>(VisualTreeHelper.GetParent(obj)) is { } item)
            {
                navigationViewItem = item;
            }
            return navigationViewItem;
        }

        internal void UpdateContentLeftIndentation(double leftIndentation)
        {
            m_leftIndentation = leftIndentation;
            UpdateMargin();
        }

        void UpdateMargin()
        {
            if (m_contentGrid is { } grid)
            {
                var oldGridMargin = grid.Margin;
                grid.Margin = new Thickness(m_leftIndentation, oldGridMargin.Top, oldGridMargin.Right, oldGridMargin.Bottom);
            }
        }

        internal void UpdateCompactPaneLength(double compactPaneLength, bool shouldUpdate)
        {
            m_compactPaneLengthValue = compactPaneLength;
            if (shouldUpdate)
            {
                if (this.Find<ColumnDefinition>(c_iconBoxColumnDefinitionName) is { } iconGridColumn)
                {
                    iconGridColumn.Width = new GridLength(compactPaneLength);
                }
            }
        }

        internal void UpdateClosedCompactVisualState(bool isTopLevelItem, bool closed, bool compact)
        {
            // We increased the ContentPresenter margin to align it visually with the expand/collapse chevron. This updated margin is even applied when the
            // NavigationView is in a visual state where no expand/collapse chevrons are shown, leading to more content being cut off than necessary.
            // This is the case for top-level items when the NavigationView is in a compact mode and the NavigationView pane is closed. To keep the original
            // cutoff visual experience intact, we restore  the original ContentPresenter margin for such top-level items only (children shown in a flyout
            // will use the updated margin).
            //var stateName = isClosedCompact && isTopLevelItem
            //    ? "ClosedCompactAndTopLevelItem"
            //    : "NotClosedCompactAndTopLevelItem";

            //VisualStateManager.GoToState(this, stateName, false /*useTransitions*/);

            PseudoClasses.Set(":top-level", isTopLevelItem);
            PseudoClasses.Set(":open", !closed);
            PseudoClasses.Set(":compact", compact);
        }

        AvaloniaObject IControlProtected.GetTemplateChild(string childName)
        {
            return GetTemplateChild(childName);
        }

        double m_compactPaneLengthValue = 40;

        NavigationViewItemHelper<NavigationViewItemPresenter> m_helper = new NavigationViewItemHelper<NavigationViewItemPresenter>();
        Grid m_contentGrid;
        Grid m_expandCollapseChevron;

        double m_leftIndentation = 0;

        Storyboard m_chevronExpandedStoryboard;
        Storyboard m_chevronCollapsedStoryboard;

        RotateTransform m_expandCollapseRotateTransform;
    }
}
