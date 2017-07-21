using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.ApplicationModel.DataTransfer;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Utilities.Apps.UnitConverter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UnitConverter_MainPage : Page
    {

        #region constants yet to be added
        /*

        #region acceleration
        //primary unit: kilometers/square second
        string[] acceleration = 
        {
            "centigal",
            "centimeter/square second",
            "decigal",
            "decimeter/square second",
            "dekameter/square second",
            "foot/square second",
            "g-unit (G)",
            "gal",
            "galileo",
            "gn",
            "grav",
            "hectometer/square second",
            "inch/square second",
            "kilometer/hour second",
            "kilometer/square second",
            "meter/square second",
            "mile/hour minute",
            "mile/hour second",
            "mile/square second",
            "milligal",
            "millimeter/square second"
        };

        double[] accelerationConversions = 
        {
            10000, //centigal 0
            100, //centimeter/square second 1
            1000, //decigal 2
            10, //decimeter/square second 3
            0.1, //dekameter/square second 4
            3.280839895, //foot/square second 5
            0.1019716213, //g-unit (G) 6
            100, //gal 7
            100, //galileo 8
            0.1019716213, //gn 9
            0.1019716213, //grav 10
            0.01, //hectometer/square second 11
            39.37007874, //inch/square second 12
            3.6, //kilometer/hour second 13
            0.001, //kilometer/square second 14
            1, //meter/square second 15
            134.21617752, //mile/hour minute 16
            2.2369362921, //mile/hour second 17
            0.00062137119224, //mile/square second 18
            100000, //milligal 19
            1000 //millimeter/square second 20
        };

        #endregion

        #region angles
        //primary value: radians
        string[] angles = 
        {
            "radian",
            "mil",
            "grad",
            "degree",
            "minute",
            "second",
            "point",
            "1/16 circle",
            "1/10 circle",
            "1/8 circle",
            "1/6 circle",
            "1/4 circle",
            "1/2 circle",
            "full circle "
        };
        
        double[] anglesConversions = 
        {
            1, //radian 0
            1018.5916358, //mil 1
            63.661977237, //grad 2
            57.295779513, //degree 3
            3437.7467707, //minute 4
            206264.80625, //second 5
            5.0929581789, //point 6
            2.54647908951, //1/16 circle 7
            1.59154943091, //1/10 circle 8
            1.27323954471, //1/8 circle 9
            0.954929658551, //1/6 circle 10
            0.636619772371, //1/4 circle 11
            0.318309886182, //1/2 circle 12
            0.15915494309 //full circle  13
        };

        #endregion

        #region electricCurrent
        //primary unit is amp
        string[] electricCurrent = 
        {
            "abampere",
            "ampere",
            "amp",
            "biot",
            "centiampere",
            "coulomb/second",
            "deciampere",
            "dekaampere",
            "electromagnetic unit of current",
            "electrostatic unit of current",
            "franklin/second",
            "gaussian electric current",
            "gigaampere",
            "gilbert",
            "hectoampere",
            "kiloampere",
            "megaampere",
            "microampere",
            "milliampere",
            "milliamp",
            "nanoampere",
            "picoampere",
            "siemens volt",
            "statampere",
            "teraampere",
            "volt/ohm",
            "watt/volt",
            "weber/henry"
        };
        double[] electricCurrentConversions = 
        {
            0.1, //abampere 0
            1, //ampere 1
            1, //amp 2
            0.1, //biot 3
            100, //centiampere 4
            1, //coulomb/second 5
            10, //deciampere 6
            0.1, //dekaampere 7
            0.1, //electromagnetic unit of current 8
            2997924536.8, //electrostatic unit of current 9
            2997924536.8, //franklin/second 10
            2997924536.8, //gaussian electric current 11
            1.0 * Math.Pow(10, -9), //gigaampere 12
            1.2566370543, //gilbert 13
            0.01, //hectoampere 14
            0.001, //kiloampere 15
            0.000001, //megaampere 16
            1000000, //microampere 17
            1000, //milliampere 18
            1000, //milliamp 19
            1000000000, //nanoampere 20
            1000000000000, //picoampere 21
            1, //siemens volt 22
            2997924536.8, //statampere 23
            1.0 * Math.Pow(10, -12), //teraampere 24
            1, //volt/ohm 25
            1, //watt/volt 26
            1 //weber/henry 27
        };

        #endregion

         */
        #endregion

        public enum ConversionTypes
        {
            length = 0, mass, volume, speed, area
        };

        string[] baseFilters =
        {
            "All", "Common"
        };

        private List<ComboUnit> unitIndices, filterIndices;
        bool readyForDisplay;
        private UnitConverterData data;

        //This is the current ConversionSet, values are derived from this
        ConversionSet currentConversion;

        //TODO: make a conversion set class which holds all of the data for a particular conversion. Then just use this instead of doing a bunch of switches.
        public UnitConverter_MainPage()
        {
            readyForDisplay = false;
            this.InitializeComponent();
            
            unitIndices = null;
            filterIndices = null;

            Window.Current.SizeChanged += Current_SizeChanged;

            data = new UnitConverterData();
            ChangeUnits(ConversionTypes.length);
            readyForDisplay = true; 
        }

        //It is impossible to switch the scroll mode to vertical for a GridView. Need to see if this is fixed in RC and if not then bug file.
        private void Current_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            ChangeViewState(Windows.UI.ViewManagement.ApplicationView.Value);
        }

        private void ChangeViewState(ApplicationViewState args)
        {
            this.title.ChangeViewState(args);
            switch (args)
            {
                case Windows.UI.ViewManagement.ApplicationViewState.Filled:
                    VisualStateManager.GoToState(this, "Fill", false);

                    if (this.ItemGridView.Visibility != Windows.UI.Xaml.Visibility.Visible)
                    {
                        //Only refresh the GridView if something has changed
                        this.ItemGridView.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        this.ItemListView.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        RefreshGridView();
                        Convert();
                    }                    
                    break;
                case Windows.UI.ViewManagement.ApplicationViewState.FullScreenLandscape:
                case Windows.UI.ViewManagement.ApplicationViewState.FullScreenPortrait:
                    VisualStateManager.GoToState(this, "Full", false);

                    if (this.ItemGridView.Visibility != Windows.UI.Xaml.Visibility.Visible)
                    {
                        //Only refresh the GridView if something has changed
                        this.ItemGridView.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        this.ItemListView.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        RefreshGridView();
                        Convert();
                    }   
                    
                    break;
                case Windows.UI.ViewManagement.ApplicationViewState.Snapped:
                    VisualStateManager.GoToState(this, "Snapped", false);
                    //this.ItemGridView.ItemsPanel = (ItemsPanelTemplate)Resources["GridItemsPanelTemplateSnapped"];

                    if (this.ItemListView.Visibility != Windows.UI.Xaml.Visibility.Visible)
                    {
                        //Only refresh the GridView if something has changed
                        this.ItemListView.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        this.ItemGridView.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        RefreshGridView();
                        Convert();
                    }   
                    break;
                default:
                    break;
            }
        }

        private void ChangeUnits(ConversionTypes type)
        {
            if (currentConversion != null && currentConversion.Type == type)
                return;
            data.Collection.Clear();
            this.currentConversion = ConversionSet.GetSetByType(type);
            RetrieveUnits();
            RetrieveFilters();
            RefreshGridView();
            
            this.cmbAppBar.SelectedIndex = currentConversion.Index;
        }

        private void RetrieveUnits()
        {
            List<ComboBoxItem> itemList = new List<ComboBoxItem>();

            for (int i = 0; i < currentConversion.Units.Length; i++)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = currentConversion.Units[i];
                //possible memory leak here, I never release these event handlers
                item.KeyUp += cmbUnitKey;
                itemList.Add(item);
            }

            cmbUnits.ItemsSource = itemList;
            cmbUnits.SelectedIndex = 0;

            this.unitIndices = GenerateComboIndices(cmbUnits);
        }

        private void RetrieveFilters()
        {
            List<ComboBoxItem> itemList = new List<ComboBoxItem>();

            for (int i = 0; i < baseFilters.Length; i++)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = baseFilters[i];
                item.KeyUp += cmbFilterKey;
                itemList.Add(item);
            }

            //Add filters specific to the current type
            for (int i = 0; i < currentConversion.Filters.Length; i++)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = currentConversion.Filters[i];
                //Potential memory leak
                item.KeyUp += cmbFilterKey;
                itemList.Add(item);
            }

            //walk through the current set of units and add them to the filter list
            //make sure to call RetrieveUnits before RetrieveFilters in all cases so that this units list is accurate
            for (int i = 0; i < currentConversion.Units.Length; i++)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = currentConversion.Units[i];
                //Potential memory leak
                item.KeyUp += cmbFilterKey;
                itemList.Add(item);
            }

            cmbFilter.ItemsSource = itemList;
            cmbFilter.SelectedIndex = 1;

            this.filterIndices = GenerateComboIndices(cmbFilter);
        }

        private void RefreshGridView()
        {
            //Clear both views
            ItemGridView.ItemsSource = null;
            ItemListView.ItemsSource = null;

            if (ItemGridView.Visibility != Windows.UI.Xaml.Visibility.Visible)
            {
                //Get the proper view ready for population
                ItemListView.ItemsSource = data.Collection;
                ItemListView.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }

            else if (ItemListView.Visibility != Windows.UI.Xaml.Visibility.Visible)
            {
                this.ItemGridView.ItemsSource = data.Collection;
                this.ItemGridView.Visibility = Windows.UI.Xaml.Visibility.Visible;  
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ChangeViewState(ApplicationView.Value);
        }

        private void Convert()
        {
            if (!readyForDisplay)
                return;
            //This function will take the current value and convert it into all known types with zippering.
            //It will then create an array of UnitConverterItem objects, and pass it to the InitializeGridView method
            var units = currentConversion.Units;
            var conversions = currentConversion.Conversions;
            var masks = currentConversion.Masks;

            if (units == null || conversions == null || masks == null || units.Length != conversions.Length)
            {
                //If this is hit, then it is a programming error
                UpdateStatus("Unknown Error");
                return;
            }
            else if (this.txtUnitAmount.Text.Length <= 0 || this.cmbUnits.SelectedIndex < 0 || this.cmbFilter.SelectedIndex < 0)
            {
                UpdateStatus("Insert a value, a unit and click \"Convert\".");
                return;
            }

            double value = 0;

            if (!double.TryParse(this.txtUnitAmount.Text, out value))
            {
                UpdateStatus("There was an error trying to perform the conversion, please check your input and try again.");
                return;
            }
            //No errors, continue
            UpdateStatus("");

            //The selected index on the combo box matches the index of the conversion
            //Divide by the conversion to get the base value in the base unit
            double baseValue = value / conversions[this.cmbUnits.SelectedIndex];

            //Iterate through each of the conversions and create an array of UnitConverterItems
            List<UnitConverterItem> items = new List<UnitConverterItem>();

            for (int i = 0; i < conversions.Length; i++)
            {
                double result = baseValue * conversions[i];
                UnitConverterItem item = new UnitConverterItem(units[i], result.ToString(), PickColor(masks, i));
                items.Add(item);
            }

            //Now that we have all of the conversions, filter them appropriately
            List<UnitConverterItem> filteredItems = new List<UnitConverterItem>();

            int filterIndex = this.cmbFilter.SelectedIndex;
            if (filterIndex == -1 || masks.Count == 0)
            {
                RefreshGridView();
                return;
            }
            else if (filterIndex == 0)
            {
                //this is "All", show everything
                filteredItems = items;
            }
            else if (filterIndex > 0 && filterIndex <= masks.Count)
            {
                //This is a specific mask
                int[] mask = masks[filterIndex - 1];
                //A mask contains integers which map to the indices in the items array that we want to display
                for (int i = 0; i < mask.Length; i++)                
                    filteredItems.Add(items[mask[i]]);                
            }
            else
            {
                //filter on the specific item here
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].Title == (this.cmbFilter.Items[filterIndex] as ComboBoxItem).Content.ToString())
                    {
                        filteredItems.Add(items[i]);
                        break;
                    }
                }
            }
            
            data.InitializeGridView(filteredItems);
            RefreshGridView();
        }

        private void btnConvert_Click_1(object sender, RoutedEventArgs e)
        {
            Convert();
        }

        private void UpdateStatus(string text)
        {
            this.txtStatus.Text = text;
        }

        #region appbar buttons
        private void btnLength_Click_1(object sender, RoutedEventArgs e)
        {
            ChangeUnits(ConversionTypes.length);
        }

        private void btnMass_Click_1(object sender, RoutedEventArgs e)
        {
            ChangeUnits(ConversionTypes.mass);
        }

        private void btnVolume_Click_1(object sender, RoutedEventArgs e)
        {
            ChangeUnits(ConversionTypes.volume);
        }

        private void btnSpeed_Click_1(object sender, RoutedEventArgs e)
        {
            ChangeUnits(ConversionTypes.speed);
        }

        #endregion

        //Colorize by unit type
        private SolidColorBrush PickColor(List<int[]> filters, int index)
        {
            SolidColorBrush retVal = new SolidColorBrush(ColorHelper.FromArgb(255, 255, 255, 255));
            if (filters.Count > 4)
                return retVal;

            SolidColorBrush metroGreen = new SolidColorBrush(ColorHelper.FromArgb(255, 45, 197, 18));
            SolidColorBrush metroPurple = new SolidColorBrush(ColorHelper.FromArgb(255, 218, 45, 232));
            SolidColorBrush metroBlue = new SolidColorBrush(ColorHelper.FromArgb(255, 61, 216, 216));
            SolidColorBrush metroRed = new SolidColorBrush(ColorHelper.FromArgb(255, 255, 26, 49));            

            List<SolidColorBrush> brushes = new List<SolidColorBrush>();
            brushes.Add(metroGreen);
            brushes.Add(metroPurple);
            brushes.Add(metroRed);            
            brushes.Add(metroBlue);

            //Prefer the higher order filter
            bool found = false;

            for (int i = 0; i < filters.Count; i++)
            {
                if (filters[i].Contains(index))
                {
                    retVal = brushes[i];
                    found = true;
                    break;
                }
            }

            if (found)
                return retVal;
            else            
                return metroBlue;
        }

        #region ComboBox Highlight Code
        private void cmbUnitKey(object sender, KeyRoutedEventArgs e)
        {
            VirtualKey[] keys =
            {
                VirtualKey.A, VirtualKey.B, VirtualKey.C, VirtualKey.D, VirtualKey.E, VirtualKey.F, VirtualKey.G, VirtualKey.H, VirtualKey.I, VirtualKey.J, VirtualKey.K, VirtualKey.L,
                 VirtualKey.M, VirtualKey.N, VirtualKey.O, VirtualKey.P, VirtualKey.Q, VirtualKey.R, VirtualKey.S, VirtualKey.T, VirtualKey.U, VirtualKey.V, VirtualKey.W, VirtualKey.X,
                  VirtualKey.Y, VirtualKey.Z
            };
            if (keys.Contains(e.Key))
                HighlightProperIndex(cmbUnits, e, this.unitIndices);
        }

        private void cmbFilterKey(object sender, KeyRoutedEventArgs e)
        {
            VirtualKey[] keys =
            {
                VirtualKey.A, VirtualKey.B, VirtualKey.C, VirtualKey.D, VirtualKey.E, VirtualKey.F, VirtualKey.G, VirtualKey.H, VirtualKey.I, VirtualKey.J, VirtualKey.K, VirtualKey.L,
                 VirtualKey.M, VirtualKey.N, VirtualKey.O, VirtualKey.P, VirtualKey.Q, VirtualKey.R, VirtualKey.S, VirtualKey.T, VirtualKey.U, VirtualKey.V, VirtualKey.W, VirtualKey.X,
                  VirtualKey.Y, VirtualKey.Z
            };
            if (keys.Contains(e.Key))
                HighlightProperIndex(cmbFilter, e, this.filterIndices);
        }

        private void HighlightProperIndex(ComboBox cmbNavigate, KeyRoutedEventArgs e, List<ComboUnit> indices)
        {
            //Now we have ComboUnit array. Each one has a character and an index
            //Map the key pressed to one of these characters, then select that index

            for (int i = 0; i < indices.Count; i++)
            {
                if (e.Key.ToString().ToLower() == indices[i].Letter.ToString())
                {
                    cmbNavigate.SelectedIndex = indices[i].Index;
                    break;
                }
            }
        }

        private List<ComboUnit> GenerateComboIndices(ComboBox box)
        {
            string abc = "abcdefghijklmnopqrstuvwxyz";
            List<ComboUnit> idx = new List<ComboUnit>();
            char[] letters = abc.ToCharArray();
            int lastIndex = 0;

            for (int i = 0; i < letters.Length; i++)
            {
                for (int j = lastIndex; j < box.Items.Count; j++)
                {
                    string content = (box.Items[j] as ComboBoxItem).Content.ToString();
                    if (content[0] > letters[i])
                        break;
                    if (content.StartsWith(letters[i].ToString()))
                    {
                        idx.Add(new ComboUnit(j, letters[i]));
                        lastIndex = j;
                        break;
                    }
                }
            }
            return idx;
        }
        #endregion

        private void txtUnitAmount_KeyUp_1(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                Convert();
                e.Handled = true;
                //btnConvert.Focus(Windows.UI.Xaml.FocusState.Keyboard);
            }
        }

        private void cmbUnits_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Convert();
        }

        private void cmbFilter_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            //The user selected a filter, adjust the view by calling Convert again
            Convert();
        }

        private void cmbAppBar_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ChangeUnits(ConversionSet.GetTypeByIndex(cmbAppBar.SelectedIndex));
        }

        private void btnArea_Click_1(object sender, RoutedEventArgs e)
        {
            ChangeUnits(ConversionTypes.area);
        }

        private void ItemGridView_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var g = ItemGridView.Visibility == Windows.UI.Xaml.Visibility.Visible ? ItemGridView as ListViewBase : ItemListView as ListViewBase;
            if (g.SelectedItems.Count > 0 && g.SelectedIndex >= 0)
            {
                this.btnCopy.Visibility = Windows.UI.Xaml.Visibility.Visible;
                this.cmbAppBar.Width = 180;
            }
            else
            {
                this.btnCopy.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                this.cmbAppBar.Width = 290;
            }
        }

        private void btnCopy_Click_1(object sender, RoutedEventArgs e)
        {
            var g = ItemGridView.Visibility == Windows.UI.Xaml.Visibility.Visible ? ItemGridView as ListViewBase: ItemListView as ListViewBase;
            DataPackage dp = new DataPackage();
            dp.RequestedOperation = DataPackageOperation.Copy;
            dp.SetText((g.SelectedItem as UnitConverterItem).Result);
            Clipboard.SetContent(dp);
            g.SelectedIndex = -1;
        }

    }
}
