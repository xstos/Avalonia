// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Avalonia.Controls
{
    public sealed class NavigationViewSelectionChangedEventArgs
    {
        internal NavigationViewSelectionChangedEventArgs()
        {
        }

        public object SelectedItem { get; internal set; }
        public bool IsSettingsSelected { get; internal set; }

        public NavigationViewItemBase SelectedItemContainer { get; internal set; }
        // TODO
        // public NavigationTransitionInfo RecommendedNavigationTransitionInfo { get; internal set; }
    }
}
