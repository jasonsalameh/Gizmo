using Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Search;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ApplicationSettings;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.StartScreen;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Utilities
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public App.NavigationStates CurrentNavigationState;

        // settings charm
        // taken from http://msdn.microsoft.com/en-us/library/windows/apps/xaml/hh872190.aspx
        private static Rect _windowBounds;
        private static double _settingsWidth = 320;
        private static Popup _settingsPopup;

        public MainPage()
        {
            this.InitializeComponent();

            ApplicationView.PreferredLaunchViewSize = new Size(1366, 768);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            CurrentNavigationState = App.NavigationStates.Normal;

            this.SizeChanged += Current_SizeChangedUAP;

            App.ScheduleOnNextRender(delegate
            {
                InitializeGridView(CurrentNavigationState);

                List<Breadcrumb> breadcrumbs = new List<Breadcrumb>();
                breadcrumbs.Add(new Breadcrumb(App.Apps.MainPage, true));
                this.title.SetupBreadcrumbs(breadcrumbs);
            });

            // content tiles
            rootPage.RightTapped += rootPage_RightTapped;
        }


        // content tiles
        private void rootPage_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            e.Handled = true;
        }

        private void InitializeGridView(App.NavigationStates state)
        {
            MainScape.Clear();

            MainScape.Configure(AppsGridView_ItemClick, this.Resources["AppTileStyle"] as Style, this.Resources["AppTileTemplate"] as DataTemplate,
                AppsGridView_ItemClick, this.Resources["AppTileStyleSnapped"] as Style, this.Resources["AppTileTemplateSnapped"] as DataTemplate,
                LandscapeRow.ActualHeight, true);

            switch (state)
            {
                case App.NavigationStates.Normal:


                    MainPageItem i = new MainPageItem(App.Apps.Notepad);
                    MainScape.AddItem(i);
                    i = new MainPageItem(App.Apps.Calculator);
                    MainScape.AddItem(i);
                    i = new MainPageItem(App.Apps.UnitConverter);
                    MainScape.AddItem(i);
                    i = new MainPageItem(App.Apps.PCalendar);
                    MainScape.AddItem(i);
                    i = new MainPageItem(App.Apps.NoPage);
                    MainScape.AddItem(i);

                    break;
                case App.NavigationStates.Search:
                    //Any app which implements Search should appear here                
                    MainPageItem calc = new MainPageItem(App.Apps.Calculator);
                    MainScape.AddItem(calc);
                    break;
                case App.NavigationStates.Share:
                    //Any app which implements Share should appear here
                    MainPageItem notepad = new MainPageItem(App.Apps.Notepad);
                    MainScape.AddItem(notepad);
                    break;
                default:
                    break;
            }
        }

        private void Current_SizeChangedUAP(object sender, SizeChangedEventArgs e)
        {
            App.UpdateViewModelFromSize(e.NewSize);

            if (App.CurrentView == App.PreviousView)
                return;

            MainPage_ViewStateChanged();
        }

        private void Current_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {

            MainPage_ViewStateChanged();
        }

        void MainPage_ViewStateChanged()
        {
            title.ChangeViewState();
            this.MainScape.ChangeViewState();

            if(App.isLandscape() || App.isPortrait())
            {
                VisualStateManager.GoToState(this, "Full", false);
                this.title.Margin = new Thickness(0, 0, 0, 0);
                this.MainScape.Margin = new Thickness(0, 48, 0, 0);
            }
            else if (App.isSnapped())
            {
                VisualStateManager.GoToState(this, "Snapped", false);
                this.title.Margin = new Thickness(0, 0, 0, 0);
                this.MainScape.Margin = new Thickness(8, 48, 0, 0);
            }

        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RegisterSearch();
            //Layout
            this.SizeChanged += Current_SizeChangedUAP;
            MainPage_ViewStateChanged();

            // settings charm
            _windowBounds = Window.Current.Bounds;
            Window.Current.SizeChanged += OnWindowSizeChanged;

            //Set the Landscape and TitleRow to the proper ApplicationViewState
            //title.ChangeViewState();
            //this.MainScape.ChangeViewState();
            //TBD:SettingsPane.GetForCurrentView().CommandsRequested += Settings_CommandsRequested;

            // For futureproofing
            if (!StateManager.RoamingStateExists(App.Apps.MainPage, StateManager.APP_VERSION))
                StateManager.SaveRoamingState(App.Apps.MainPage, StateManager.APP_VERSION, Windows.ApplicationModel.Package.Current.Id.Version);

            // show help
            Utilities.Common.ShowHelp.ShowHelpMessage(App.Apps.MainPage);
        }

        //TBD:
        //public void Settings_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        //{
        //    SettingsCommand about = new SettingsCommand("About", "About", (x) =>
        //    {
        //        MainPage.ShowPopup(new Utilities.Settings.About(), this.ActualHeight, "About");
        //    });

        //    SettingsCommand feedback = new SettingsCommand("Feedback", "Report a bug", (x) =>
        //    {
        //        (Application.Current as App).Feedback();
        //    });

        //    args.Request.ApplicationCommands.Add(about);
        //    args.Request.ApplicationCommands.Add(feedback);

        //}

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            UnregisterSearch();

            //TBD:SettingsPane.GetForCurrentView().CommandsRequested -= Settings_CommandsRequested;
        }

        public void SetNavigationState(App.NavigationStates state)
        {
            if (CurrentNavigationState != state)
            {
                CurrentNavigationState = state;
                ((App)Application.Current).lastNavState = state;
                InitializeGridView(state);
            }
            //add text to pick an app for search/share
            Breadcrumb bc;
            List<Breadcrumb> b = new List<Breadcrumb>();

            if (CurrentNavigationState == App.NavigationStates.Search)
            {
                bc = new Breadcrumb(App.Apps.NoPage, true, "Select an app to search...");

                //this.title.ButtonOpacity = 1;
                //This is a small hack to allow the back arrow to bring the user back to a normal main page after searching
                ((App)Application.Current).lastNavState = App.NavigationStates.Normal;
            }
            else if (CurrentNavigationState == App.NavigationStates.Share)
                bc = new Breadcrumb(App.Apps.NoPage, true, "Share to...");
            else
                bc = new Breadcrumb(App.Apps.NoPage, true, App.AppName);

            b.Add(bc);
            this.title.SetupBreadcrumbs(b);
        }

        #region Search Code

        /// <summary>
        /// Invoked to init the Search functionality for Search suggestions requested
        /// </summary>
        private void RegisterSearch()
        {
            //SearchPane searchPane = SearchPane.GetForCurrentView();
            //searchPane.QuerySubmitted += searchPane_QuerySubmitted;
            //searchPane.QueryChanged += searchPane_QueryChanged;
            //searchPane.SuggestionsRequested += MainPage.searchPane_SuggestionsRequested;
        }

        public void UnregisterSearch()
        {
            //SearchPane searchPane = SearchPane.GetForCurrentView();
            //searchPane.QuerySubmitted -= searchPane_QuerySubmitted;
            //searchPane.QueryChanged -= searchPane_QueryChanged;
            //searchPane.SuggestionsRequested -= MainPage.searchPane_SuggestionsRequested;
        }

        //TBD:
        //void searchPane_QueryChanged(SearchPane sender, SearchPaneQueryChangedEventArgs args)
        //{
        //    UpdateUIForSearch(args.QueryText);
        //}

        //void searchPane_QuerySubmitted(SearchPane sender, SearchPaneQuerySubmittedEventArgs args)
        //{
        //    UpdateUIForSearch(args.QueryText);
        //}

        private void UpdateUIForSearch(string queryText)
        {
            //set the search data
            ((App)Application.Current).SearchArguments = queryText;
            //change the grid view state
            this.SetNavigationState(App.NavigationStates.Search);
        }

        /// <summary>
        /// Invoked to when search suggestions are requested
        /// </summary>
        /// //TBD:
        //public static void searchPane_SuggestionsRequested(SearchPane sender, SearchPaneSuggestionsRequestedEventArgs args)
        //{
        //    // try to see if the user entered in something valid
        //    try
        //    {
        //        // Calculator serach
        //        string calcReturn = Calculator.Calc_MainPage.searchPane_SuggestionsRequested(args.QueryText);
        //        if (calcReturn != string.Empty)
        //            args.Request.SearchSuggestionCollection.AppendQuerySuggestion("Answer: " + calcReturn);

        //        // TODO : Notepad

        //        // TODO : TODO                
        //    }
        //    catch { }
        //}
        #endregion

        private void AppsGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            MainPageItem item = e.ClickedItem as MainPageItem;

            //Navigate to the page, keeping the navigation context
            App.NavigateToPage(item.App, this.CurrentNavigationState);

            //Reset the navigation state in case the user navigates back during a Search            
            if (CurrentNavigationState == App.NavigationStates.Search)
                this.SetNavigationState(App.NavigationStates.Normal);
        }

        #region Settings Charm

        public static void OnWindowSizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            _windowBounds = Window.Current.Bounds;
        }

        private static void OnWindowActivated(object sender, Windows.UI.Core.WindowActivatedEventArgs e)
        {
            if (e.WindowActivationState == Windows.UI.Core.CoreWindowActivationState.Deactivated)
            {
                _settingsPopup.IsOpen = false;
            }
        }

        private static void OnPopupClosed(object sender, object e)
        {
            Window.Current.Activated -= OnWindowActivated;
        }

        public static Utilities.SettingsFlyout ShowPopup(Page pageToShow, double height, string titleText)
        {
            _settingsPopup = new Popup();
            _settingsPopup.Closed += OnPopupClosed;
            Window.Current.Activated += OnWindowActivated;
            _settingsPopup.IsLightDismissEnabled = true;
            _settingsPopup.Width = _settingsWidth;
            _settingsPopup.Height = _windowBounds.Height;

            Utilities.SettingsFlyout mypane = new Utilities.SettingsFlyout();
            mypane.Width = _settingsWidth;
            mypane.Height = height;
            mypane.InnerGrid.Children.Add(pageToShow);
            mypane.TitleText = titleText;
            mypane.Theme = SettingsFlyout.FlyoutTheme.Gray;

            _settingsPopup.Child = mypane;
            _settingsPopup.SetValue(Canvas.LeftProperty, _windowBounds.Width - _settingsWidth);
            _settingsPopup.SetValue(Canvas.TopProperty, 0);
            _settingsPopup.IsOpen = true;

            return mypane;
        }

        #endregion

        #region Content Tiles

        private Button pinToAppBar;
        private App.Apps selectedAppObject;
        public static Rect GetElementRect(FrameworkElement element)
        {
            GeneralTransform buttonTransform = element.TransformToVisual(null);
            Point point = buttonTransform.TransformPoint(new Point());
            return new Rect(point, new Size(element.ActualWidth, element.ActualHeight));
        }

        async void pinToAppBar_Click(object sender, RoutedEventArgs e)
        {
            string selectedApp = selectedAppObject.ToString();

            SecondaryTileAppBar.IsSticky = true;
            if (SecondaryTile.Exists(selectedApp))
            {
                SecondaryTile secondaryTile = new SecondaryTile(selectedApp);
                bool isUnpinned = await secondaryTile.RequestDeleteForSelectionAsync(GetElementRect((FrameworkElement)sender), Windows.UI.Popups.Placement.Above);

                ToggleAppBarButton(isUnpinned);
            }
            else
            {
                // TODO: update when pictures are made
                Uri logo = Helpers.GetSnappedURI(Helpers.selectImage(selectedAppObject, true));
                string tileActivationArguments = selectedApp;

                SecondaryTile secondaryTile = new SecondaryTile(selectedApp,
                                                                App.Apps.UnitConverter.ToString() + " - " + selectedApp,
                                                                tileActivationArguments,
                                                                logo,
                                                                TileSize.Default
                                                                );

                secondaryTile.VisualElements.ForegroundText = ForegroundText.Dark;
                secondaryTile.VisualElements.Square44x44Logo = Helpers.GetSnappedURI(Helpers.selectImage(selectedAppObject, true));

                bool isPinned = await secondaryTile.RequestCreateForSelectionAsync(GetElementRect((FrameworkElement)sender), Windows.UI.Popups.Placement.Above);

                ToggleAppBarButton(!isPinned);

                // uncheck the items in mainscape
                MainScape.UnselectCurrentItem();


            }
            SecondaryTileAppBar.IsSticky = false;
            SecondaryTileAppBar.IsOpen = false;
        }

        private void MainScape_RightTapped(object o, RightTappedEventArgs e)
        {
            if (RightPanel != null)
            {
                // capture the app thats been selected into the global
                selectedAppObject = e.appObject;

                // create the button
                pinToAppBar = new Button();

                // check if the app is already pinned then change the button based on that information
                ToggleAppBarButton(!SecondaryTile.Exists(selectedAppObject.ToString()));

                // add the event handler for click
                pinToAppBar.Click += new RoutedEventHandler(pinToAppBar_Click);

                // clear all the childredn first
                RightPanel.Children.Clear();

                // add the button to the app bar
                RightPanel.Children.Add(pinToAppBar);

                SecondaryTileAppBar.IsOpen = true;
            }
        }

        private void ToggleAppBarButton(bool showPinButton)
        {
            if (pinToAppBar != null)
            {
                pinToAppBar.Style = (showPinButton) ? (this.Resources["PinAppBarButtonStyle"] as Style) : (this.Resources["UnpinAppBarButtonStyle"] as Style);
            }
        }

        #endregion
    }
}
