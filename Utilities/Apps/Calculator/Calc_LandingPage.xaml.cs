using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Utilities;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ApplicationSettings;
using Windows.UI.StartScreen;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Calculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Calc_LandingPage : Page
    {
        public Calc_LandingPage()
        {
            this.InitializeComponent();

            // set viewstate handler
            //Window.Current.SizeChanged += Current_SizeChanged;
            this.SizeChanged += Current_SizeChangedUAP;

            App.ScheduleOnNextRender(delegate
            {
                SetupGrid();
            });

            //Initialize TitleRow
            App.ScheduleOnNextRender(delegate
            {
                List<Breadcrumb> breadcrumbs = new List<Breadcrumb>();
                breadcrumbs.Add(new Breadcrumb(App.Apps.MainPage));
                breadcrumbs.Add(new Breadcrumb(App.Apps.Calculator, true));
                title.SetupBreadcrumbs(breadcrumbs);
            });

            Current_SizeChanged();

            // content tiles
            rootPage.RightTapped += rootPage_RightTapped;

        }

        private void Current_SizeChangedUAP(object sender, SizeChangedEventArgs e)
        {
            App.UpdateViewModelFromSize(e.NewSize);
            if (App.CurrentView == App.PreviousView)
                return;

            Current_SizeChanged();
        }

        private void Current_SizeChanged()
        {
            MainScape.ChangeViewState();
            title.ChangeViewState();

            if (App.isLandscape() || App.isPortrait())
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

        // content tiles
        private void rootPage_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            e.Handled = true;
        }

        private void SetupGrid()
        {
            MainScape.Clear();

            MainScape.Configure(AppsGridView_ItemClick, this.Resources["AppTileStyle"] as Style, this.Resources["AppTileTemplate"] as DataTemplate,
                AppsGridView_ItemClick, this.Resources["AppTileStyleSnapped"] as Style, this.Resources["AppTileTemplateSnapped"] as DataTemplate,
                LandscapeRow.ActualHeight, true);



            //All apps should be added here to appear on the main page.
            MainPageItem i;

            if (App.isSmallScreen())
            {
                i = new MainPageItem(App.Apps.Scientific);
                MainScape.AddItem(i);
            }
            else
            {
                i = new MainPageItem(App.Apps.Scientific);
                MainScape.AddItem(i);
                i = new MainPageItem(App.Apps.Programmer);
                MainScape.AddItem(i);
                i = new MainPageItem(App.Apps.Statistics);
                MainScape.AddItem(i);
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            //TBD: SettingsPane.GetForCurrentView().CommandsRequested += Settings_CommandsRequested;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            //TBD:SettingsPane.GetForCurrentView().CommandsRequested -= Settings_CommandsRequested;
        }

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

        private void AppsGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Utilities.MainPageItem item = e.ClickedItem as Utilities.MainPageItem;

            //Navigate to the page, keeping the navigation context
            Utilities.App.NavigateToPage(item.App);

        }

        #region Content Tiles

        private Button pinToAppBar;
        private App.Apps selectedAppObject;
        private void SetupContentTileAppBarPinning()
        {

        }

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
                Uri logo = Helpers.GetSnappedURI(Helpers.selectImage(selectedAppObject, true));
                string tileActivationArguments = selectedApp;

                SecondaryTile secondaryTile = new SecondaryTile(selectedApp,
                                                                App.Apps.Calculator.ToString() + " - " + selectedApp,
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
