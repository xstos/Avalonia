// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Windows;

namespace Avalonia.Controls
{
    enum NavigationViewVisualStateDisplayMode
    {
        Compact,
        Expanded,
        Minimal,
        MinimalWithBackButton
    }

    enum NavigationViewRepeaterPosition
    {
        LeftNav,
        TopPrimary,
        TopOverflow,
        LeftFooter,
        TopFooter
    }

    enum NavigationViewPropagateTarget
    {
        LeftListView,
        TopListView,
        OverflowListView,
        All
    }

    class NavigationViewItemHelper
    {
        internal const string c_OnLeftNavigationReveal = "OnLeftNavigationReveal";
        internal const string c_OnLeftNavigation = "OnLeftNavigation";
        internal const string c_OnTopNavigationPrimary = "OnTopNavigationPrimary";
        internal const string c_OnTopNavigationPrimaryReveal = "OnTopNavigationPrimaryReveal";
        internal const string c_OnTopNavigationOverflow = "OnTopNavigationOverflow";
    }

    // Since RS5, a lot of functions in NavigationViewItem is moved to NavigationViewItemPresenter. So they both share some common codes.
    // This class helps to initialize and maintain the status of SelectionIndicator and ToolTip
    class NavigationViewItemHelper<T> : NavigationViewItemHelper
    {
        public NavigationViewItemHelper()
        {
        }

        public IControl GetSelectionIndicator() { return m_selectionIndicator; }

        public void Init(IControl controlProtected)
        {
            m_selectionIndicator = controlProtected.FindNameScope().Find<IControl>(c_selectionIndicatorName);
        }

        IControl m_selectionIndicator;

        const string c_selectionIndicatorName = "SelectionIndicator";
    }
}
