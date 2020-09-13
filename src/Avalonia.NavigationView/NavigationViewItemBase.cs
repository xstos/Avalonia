// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;

using Avalonia.Layout;
using Avalonia.Controls.Primitives;

namespace Avalonia.Controls
{
    public class NavigationViewItemBase : ContentControl
    {
        static NavigationViewItemBase()
        {
            HorizontalContentAlignmentProperty.OverrideDefaultValue<NavigationViewItemBase>(HorizontalAlignment.Center);
            VerticalContentAlignmentProperty.OverrideDefaultValue<NavigationViewItemBase>(VerticalAlignment.Center);
            // KeyboardNavigation.OverrideDefaultValue<NavigationViewItemBase>(KeyboardNavigationMode.None);
        }

        internal NavigationViewItemBase()
        {
        }

        #region IsSelected

        public static readonly StyledProperty<bool> IsSelectedProperty =
            AvaloniaProperty.Register<NavigationViewItemBase, bool>(nameof(IsSelected), false);

        public bool IsSelected
        {
            get => GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        protected override void OnPropertyChanged<T>(AvaloniaPropertyChangedEventArgs<T> change)
        {
            base.OnPropertyChanged(change);

            if (change.Property == IsSelectedProperty)
            {
                OnNavigationViewItemBaseIsSelectedChanged();
            }
        }

        #endregion

        #region UseSystemFocusVisuals

        //public static readonly AvaloniaProperty UseSystemFocusVisualsProperty =
        //    FocusVisualHelper.UseSystemFocusVisualsProperty.AddOwner(typeof(NavigationViewItemBase));

        //public bool UseSystemFocusVisuals
        //{
        //    get => (bool)GetValue(UseSystemFocusVisualsProperty);
        //    set => SetValue(UseSystemFocusVisualsProperty, value);
        //}

        #endregion

        internal NavigationViewRepeaterPosition Position
        {
            get => m_position;
            set
            {
                if (m_position != value)
                {
                    m_position = value;
                    OnNavigationViewItemBasePositionChanged();
                }
            }
        }

        private protected virtual void OnNavigationViewItemBasePositionChanged() { }

        internal NavigationView GetNavigationView()
        {
            if (m_navigationView != null && m_navigationView.TryGetTarget(out var target))
            {
                return target;
            }
            return null;
        }

        internal int Depth
        {
            get => m_depth;
            set
            {
                if (m_depth != value)
                {
                    m_depth = value;
                    OnNavigationViewItemBaseDepthChanged();
                }
            }
        }

        private protected virtual void OnNavigationViewItemBaseDepthChanged() { }

        private protected virtual void OnNavigationViewItemBaseIsSelectedChanged() { }

        internal SplitView GetSplitView()
        {
            SplitView splitView = null;
            var navigationView = GetNavigationView();
            if (navigationView != null)
            {
                splitView = navigationView.GetSplitView();
            }
            return splitView;
        }

        internal void SetNavigationViewParent(NavigationView navigationView)
        {
            m_navigationView = new WeakReference<NavigationView>(navigationView);
        }


        // TODO: Constant is a temporary measure. Potentially expose using TemplateSettings.
        internal const int c_itemIndentation = 25;

        internal bool IsTopLevelItem
        {
            get => m_isTopLevelItem;
            set => m_isTopLevelItem = value;
        }

        internal bool CreatedByNavigationViewItemsFactory
        {
            get => m_createdByNavigationViewItemsFactory;
            set => m_createdByNavigationViewItemsFactory = value;
        }

        private protected WeakReference<NavigationView> m_navigationView;

        NavigationViewRepeaterPosition m_position = NavigationViewRepeaterPosition.LeftNav;
        int m_depth = 0;
        bool m_isTopLevelItem = false;

        // Flag to keep track of whether this item was created by the custom internal NavigationViewItemsFactory.
        // This is required in order to achieve proper recycling
        bool m_createdByNavigationViewItemsFactory = false;
    }
}
