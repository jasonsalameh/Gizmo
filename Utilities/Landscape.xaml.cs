using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Utilities
{
    public delegate void RightTappedHandler(object o, RightTappedEventArgs e);

    public class RightTappedEventArgs : EventArgs
    {
        public readonly App.Apps appObject;

        public RightTappedEventArgs(App.Apps AppObject)
        {
            appObject = AppObject;
        }
    }

    public sealed partial class Landscape : UserControl
    {
        private bool configured;
        private const double MAX_ITEM_HEIGHT = 400, HEIGHT_SCALE = 0.8;
        private double adjustedHeight;

        // event handler for righttapped
        public event RightTappedHandler RightTapped = null;

        public Landscape()
        {
            this.InitializeComponent();
            configured = false;
            adjustedHeight = 0;
        }

        private ItemCollection _Collection = new ItemCollection();

        public ItemCollection Collection
        {
            get
            {
                return this._Collection;
            }
        }

        /// <summary>
        /// Configures the Landscape with a click event delegate, styles, and sizes. Must be called before it can be used.
        /// </summary>
        /// <param name="ClickHandler">An ItemClickEventHandler delegate which is called when any item in the GridView is clicked.</param>
        /// <param name="TileStyle">The styling for each tile in the Grid View.</param>
        /// <param name="ItemTemplate">The XAML describing what controls are in each tile in the Grid View.</param>
        /// <param name="ClickHandlerSnapped">An ItemClickEventHandler delegate which is called when any item in the ListView is clicked while snapped. This is normally the same as the ClickHandler parameter.</param>
        /// <param name="TileStyleSnapped">The styling for each tile in the List View when snapped. Pass null if ListView isn't supported.</param>
        /// <param name="ItemTemplateSnapped">The XAML describing what controls are in each tile in the List View when snapped. Pass null if ListView isn't supported.</param>
        /// <param name="height">The available height in the Grid row hosting the Landscape control.</param>
        /// <param name="startingState">The starting view state (snapped or filled) for proper visual set up.</param>
        /// <param name="IsItemClickEnabled">Determines whether the Grid View items themselves can be clicked - use false if they are carrying controls.</param>
        public void Configure(ItemClickEventHandler ClickHandler, Style TileStyle, DataTemplate ItemTemplate, ItemClickEventHandler ClickHandlerSnapped,
            Style TileStyleSnapped, DataTemplate ItemTemplateSnapped, double height, bool IsItemClickEnabled)
        {
            if (ClickHandler != null)
                this.LandscapeContent.ItemClick += ClickHandler;
            this.LandscapeContent.ItemTemplate = ItemTemplate;
            
            this.LandscapeContent.IsItemClickEnabled = IsItemClickEnabled;
            this.LandscapeContentSnapped.IsItemClickEnabled = IsItemClickEnabled;

            adjustedHeight = 450; //hard coded for now
            //if (adjustedHeight > MAX_ITEM_HEIGHT)
            //    adjustedHeight = MAX_ITEM_HEIGHT;

            try //If the setter is already added, then there will be an error if you're trying to add it again
            {
                TileStyle.Setters.Add(new Setter(GridViewItem.HeightProperty, adjustedHeight));
                TileStyle.Setters.Add(new Setter(GridViewItem.WidthProperty, adjustedHeight));
            }
            catch { }

            this.LandscapeContent.ItemContainerStyle = TileStyle;

            if (ClickHandlerSnapped != null)
                this.LandscapeContentSnapped.ItemClick += ClickHandlerSnapped;

            this.LandscapeContentSnapped.ItemTemplate = ItemTemplateSnapped;
            this.LandscapeContentSnapped.ItemContainerStyle = TileStyleSnapped;

            //This also sets the built-in view state, which is used for proper scrolling
            ChangeViewState();

            configured = true;
        }

        public void ScrollToItem(int ItemID)
        {
            if (!App.isSnapped())
                this.LandscapeContent.ScrollIntoView(Collection.ItemAt(ItemID), ScrollIntoViewAlignment.Leading);
            else
                this.LandscapeContentSnapped.ScrollIntoView(Collection.ItemAt(ItemID), ScrollIntoViewAlignment.Leading);
        }

        public void ChangeViewState()
        {
            if(App.isLandscape() || App.isPortrait())
            {
                this.LandscapeContent.Visibility = Visibility.Visible;
                this.LandscapeContentSnapped.Visibility = Visibility.Collapsed;
            }
            else if (App.isSnapped())
            {
                if (this.LandscapeContentSnapped.ItemTemplate != null && this.LandscapeContentSnapped.ItemContainerStyle != null)
                {
                    this.LandscapeContent.Visibility = Visibility.Collapsed;
                    this.LandscapeContentSnapped.Visibility = Visibility.Visible;
                }
                else
                {
                    this.LandscapeContent.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    this.LandscapeContentSnapped.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                }
            }
            else
            {
                this.LandscapeContent.Visibility = Visibility.Visible;
                this.LandscapeContentSnapped.Visibility = Visibility.Collapsed;
            }
        }

        public void AddItem(object item)
        {
            if (!configured)
                throw new Exception("Must call Configure before adding Items");

            this.Collection.Add(item);
            RefreshViews();
        }

        public void RemoveItem(int ItemID)
        {
            if (ItemID > this.Collection.Count() || ItemID < 0)
                throw new Exception("ItemID is out of range");

            this.Collection.RemoveAt(ItemID);
            RefreshViews();
        }

        public ListViewBase GetView()
        {
            ListViewBase list = LandscapeContent;
            if (App.CurrentView == App.ViewModelStates.Snapped)
                list = LandscapeContentSnapped;

            return list;
        }

        public void Clear()
        {
            this.Collection.Clear();
            RefreshViews();
        }

        private void RefreshViews()
        {
            // for snapped mode remove the mainpage items that are comming soon
            ItemCollection snappedCollection = new ItemCollection();
            foreach (object item in this.Collection)
            {
                // if the item is NOT comming soon
                if (!(item as MainPageItem).CommingSoon)
                    snappedCollection.Add(item);
            }

            this.LandscapeContent.ItemsSource = null;
            this.LandscapeContent.ItemsSource = snappedCollection;

            this.LandscapeContentSnapped.ItemsSource = null;
            this.LandscapeContentSnapped.ItemsSource = snappedCollection;
        }

        private void LandscapeContent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                RightTappedEventArgs args = new RightTappedEventArgs((e.AddedItems.First() as MainPageItem).App);
                (e.AddedItems.First() as MainPageItem).BackgroundColor = Helpers.GetBrush(Helpers.Colors.LightGray);
                RightTapped(new object(), args);
            }
            catch { }
        }

        public void UnselectCurrentItem()
        {
            LandscapeContent.SelectionMode = ListViewSelectionMode.None;
            LandscapeContentSnapped.SelectionMode = ListViewSelectionMode.None;

            LandscapeContent.SelectionMode = ListViewSelectionMode.Single;
            LandscapeContentSnapped.SelectionMode = ListViewSelectionMode.Single;
        }

    }
}
