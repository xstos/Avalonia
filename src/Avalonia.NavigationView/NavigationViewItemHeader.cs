// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reactive.Linq;

using Avalonia.Controls.Primitives;

namespace Avalonia.Controls
{
    public class NavigationViewItemHeader : NavigationViewItemBase
    {
        const string c_rootGrid = "NavigationViewItemHeaderRootGrid";

        public NavigationViewItemHeader()
        {
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            if (GetSplitView() is { } splitView)
            {
                splitView.PropertyChanged += SplitView_PropertyChanged;

                UpdateIsClosedCompact();
            }

            if (e.NameScope.Find<Grid>(c_rootGrid) is Grid rootGrid)
            {
                m_rootGrid = rootGrid;
            }

            UpdateVisualState(false /*useTransitions*/);
            UpdateItemIndentation();

            // TODO: WPF - Header Animation
            /*
            var visual = ElementCompositionPreview.GetElementVisual(*this);
            NavigationView.CreateAndAttachHeaderAnimation(visual);
            */
        }

        private void SplitView_PropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property == SplitView.IsPaneOpenProperty ||
                e.Property == SplitView.DisplayModeProperty)
            {
                UpdateIsClosedCompact();
            }
        }

        void UpdateIsClosedCompact()
        {
            if (GetSplitView() is { } splitView)
            {
                // Check if the pane is closed and if the splitview is in either compact mode.
                m_isClosedCompact = !splitView.IsPaneOpen && (splitView.DisplayMode == SplitViewDisplayMode.CompactOverlay || splitView.DisplayMode == SplitViewDisplayMode.CompactInline);
                UpdateVisualState(true /*useTransitions*/);
            }
        }

        void UpdateVisualState(bool useTransitions)
        {
            PseudoClasses.Set(":header-text-visibile", !m_isClosedCompact || !IsTopLevelItem);
        }

        private protected override void OnNavigationViewItemBaseDepthChanged()
        {
            UpdateItemIndentation();
        }

        void UpdateItemIndentation()
        {
            // Update item indentation based on its depth
            if (m_rootGrid is { } rootGrid)
            {
                var oldMargin = rootGrid.Margin;
                var newLeftMargin = Depth * c_itemIndentation;
                rootGrid.Margin = new Thickness(newLeftMargin, oldMargin.Top, oldMargin.Right, oldMargin.Bottom);
            }
        }

        bool m_isClosedCompact = false;

        Grid m_rootGrid;
    }
}
