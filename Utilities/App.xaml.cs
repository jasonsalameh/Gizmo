using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace Utilities
{

    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
        public object CurrentPage;
        public string SearchArguments;
        public ShareTargetActivatedEventArgs ShareArguments;
        public NavigationStates lastNavState = NavigationStates.Normal;
        private static List<Action> workItems;

        public static string AppName = "Gizmo";
        public static int SnappedSize = 600;
        public static int SmallSize = 400;
        private static Size LastKnownSize;

        private static bool PrevCalcSearch = false;

        public Apps CurrentApp;
        public enum Apps
        {
            MainPage, Notepad, Calculator, ToDo, PCalendar, UnitConverter, Stopwatch, Timer, 
            
            // Calc
            Scientific, Programmer, Statistics, 
            
            // Unit converter
            Area, Distance, Mass, Speed, Volume, Acceleration, Angles, Cooking, Density, Current, Energy, Flow, Force, 
            Frequency, Light, Power, Pressure, Torque, Viscosity,
            
            
            NoPage, About
        };

        public enum NavigationStates
        {
            Normal, Share, Search
        };

        public static ViewModelStates CurrentView = ViewModelStates.Undef;
        public static ViewModelStates PreviousView = ViewModelStates.Undef;
        public enum ViewModelStates
        {
            Snapped, Landscape, Portrait, Undef
        }

        public static void UpdateViewModelFromSize(Size s)
        {
            LastKnownSize = s;
            App.PreviousView = App.CurrentView;
            if (isSnappedScreen())
                App.CurrentView = App.ViewModelStates.Snapped;
            else if (s.Width <= s.Height)
                App.CurrentView = App.ViewModelStates.Portrait;
            else
                App.CurrentView = App.ViewModelStates.Landscape;
        }

        public static bool isSnappedScreen()
        {
            return LastKnownSize.Width <= App.SnappedSize || LastKnownSize.Height <= App.SnappedSize;
        }
        public static bool isSmallScreen()
        {
            return LastKnownSize.Width <= App.SmallSize || LastKnownSize.Height <= App.SmallSize;
        }

        public static bool isSnapped()
        {
            return App.CurrentView == App.ViewModelStates.Snapped;
        }

        public static bool isPortrait()
        {
            return App.CurrentView == App.ViewModelStates.Portrait;
        }

        public static bool isLandscape()
        {
            return App.CurrentView == App.ViewModelStates.Landscape;
        }

        public static bool isPhoneDevice()
        {
            #if WINDOWS_PHONE_APP
                return true;
            #else
                return false;
            #endif
        }


        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>        

        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;

            
        }

        #region Activation

        /// <summary>
        /// Invoked when the application is launched via some protocol.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnActivated(IActivatedEventArgs args)
        {
            // this is a protocol activation
            if (args.Kind == ActivationKind.Protocol)
            {
                string scheme = (args as ProtocolActivatedEventArgs).Uri.Scheme;
                string data = (args as ProtocolActivatedEventArgs).Uri.LocalPath;
                switch (scheme)
                {
                    case "calc":
                        object frameContent = NavigateToPage(Apps.Scientific, NavigationStates.Normal);
                        (frameContent as Calculator.Calc_MainPage).insertToWindow(data);
                        break;
                    case "notepad":
                        // TODO
                        break;
                    case "todo":
                        // TODO
                        break;
                    case "timer":
                        //TODO
                        break;
                    default:
                        break;
                }
            }
        }
        
        /// <summary>
        /// Invoked when the application is trigered via the search contract
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnSearchActivated(SearchActivatedEventArgs args)
        {
            NavigateToPage(Apps.MainPage, NavigationStates.Search);
            this.SearchArguments = args.QueryText;
        }

        protected override void OnShareTargetActivated(ShareTargetActivatedEventArgs args)
        {
            NavigateToPage(Apps.MainPage, NavigationStates.Share);
            this.ShareArguments = args;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
            {
                //TODO: Load state from previously suspended application
                NavigateToPage(Apps.MainPage);
            }

            // content tiles
            else if (args.Kind == ActivationKind.Launch)
            {
                NavigateToPage(Helpers.GetAppObject(args.Arguments));
            }
            else
            {
                NavigateToPage(Apps.MainPage);
            }
        }

        protected override void OnFileActivated(FileActivatedEventArgs args)
        {
            if (args.Verb == "open")
            {
                Notepad.Notepad_MainPage notepadPage;
                if (CurrentPage is Notepad.Notepad_MainPage)
                    notepadPage = CurrentPage as Notepad.Notepad_MainPage;
                else
                {
                    var currentPage = NavigateToPage(Apps.Notepad);
                    notepadPage = currentPage as Notepad.Notepad_MainPage;
                }
                notepadPage.DisplayFile(args.Files[0], true, true);
            }
            else
                NavigateToPage(Apps.MainPage);
        }

        #endregion

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        void OnSuspending(object sender, SuspendingEventArgs e)
        {
            //TODO: Save application state and stop any background activity
        }

        #region Schedule On Next Render

        //Example of use:
        /*
         *      ScheduleOnNextRender(delegate
                {
                    ImageScroller.ScrollToHorizontalOffset(320 * idx);
                });
         */

        public static void ScheduleOnNextRender(Action action)
        {
            // If the list of work items is null, create a new one and register DoWorkOnRender as a 
            // handler for the CompositionTarget.Rendering event.
            if (workItems == null)
            {
                workItems = new List<Action>();
                CompositionTarget.Rendering += DoWorkOnRender;
            }

            // Add the supplied action to the list.
            workItems.Add(action);
        }

        private static void DoWorkOnRender(object sender, object e)
        {
            // Remove ourselves from the event and clear the list
            CompositionTarget.Rendering -= DoWorkOnRender;
            List<Action> work = workItems;
            workItems = null;

            // Loop through each action in the list
            foreach (Action action in work)
            {
                try
                {
                    action();
                }
                catch
                {
                }
            }
        }

        #endregion

        #region Navigation

        public static object NavigateToPage(App.Apps app, NavigationStates navState = NavigationStates.Normal)
        {
            Windows.Graphics.Display.DisplayInformation.AutoRotationPreferences =
                Windows.Graphics.Display.DisplayOrientations.Landscape |
                Windows.Graphics.Display.DisplayOrientations.LandscapeFlipped |
                Windows.Graphics.Display.DisplayOrientations.Portrait |
                Windows.Graphics.Display.DisplayOrientations.PortraitFlipped;

            App curApp = Application.Current as App;
            string searchArgs = curApp.SearchArguments;
            curApp.lastNavState = navState;

            //check the navigation type for share/search/normal
            Frame rootFrame;
            bool newFrame = false;
            try
            {
                rootFrame = (curApp.CurrentPage as Page).Frame;
                if (rootFrame == null)
                    throw new Exception(); //passes it to the catch block which initializes the frame
            }
            catch
            {
                rootFrame = new Frame();
                newFrame = true;
            }

            Calculator.Calc_MainPage calcPageMain;
            Utilities.Apps.UnitConverter.UnitConverter uc;
            switch (app)
            {
                case Apps.MainPage:
                    rootFrame.Navigate(typeof(MainPage));
                    MainPage mainPage = rootFrame.Content as MainPage;
                    mainPage.SetNavigationState(navState);
                    break;

                case App.Apps.Calculator:
                    if (navState == NavigationStates.Search)
                    {
                        rootFrame.Navigate(typeof(Calculator.Calc_MainPage));
                        calcPageMain = rootFrame.Content as Calculator.Calc_MainPage;

                        calcPageMain.Initialize(Apps.Scientific);

                        //Do any work to set up Search on Calculator here
                        calcPageMain.insertToWindow(searchArgs);

                        PrevCalcSearch = true;
                    }
                    else if (PrevCalcSearch == true)
                        PrevCalcSearch = false;
                    else
                    {
                        rootFrame.Navigate(typeof(Calculator.Calc_LandingPage));
                        Calculator.Calc_LandingPage calcPageLand = rootFrame.Content as Calculator.Calc_LandingPage;
                    }
                    
                    break;

                case App.Apps.Scientific:
                    rootFrame.Navigate(typeof(Calculator.Calc_MainPage));
                    calcPageMain = rootFrame.Content as Calculator.Calc_MainPage;

                    calcPageMain.Initialize(Apps.Scientific);
                    break;

                case App.Apps.Programmer:
                    rootFrame.Navigate(typeof(Calculator.Calc_MainPage));
                    calcPageMain = rootFrame.Content as Calculator.Calc_MainPage;

                    calcPageMain.Initialize(Apps.Programmer);
                    break;

                case App.Apps.Statistics:
                    rootFrame.Navigate(typeof(Calculator.Calc_MainPage));
                    calcPageMain = rootFrame.Content as Calculator.Calc_MainPage;

                    calcPageMain.Initialize(Apps.Statistics);
                    break;

                case App.Apps.Notepad:
                    rootFrame.Navigate(typeof(Notepad.Notepad_MainPage));
                    Notepad.Notepad_MainPage notepadPage = rootFrame.Content as Notepad.Notepad_MainPage;

                    if (navState != NavigationStates.Share)
                        notepadPage.recentFilesTimer.Start();
                    else
                        notepadPage.displaySharingText(curApp.ShareArguments);
                    break;

                //case App.Apps.ToDo:
                //    rootFrame.Navigate(typeof(Utilities.Apps.ToDo.ToDo_MainPage));
                //    break;

                case App.Apps.PCalendar:
                    rootFrame.Navigate(typeof(PCalender.PCal_MainPage));
                    //PCalender.PCal_MainPage pCalPage = rootFrame.Content as PCalender.PCal_MainPage;

                    break;
                case App.Apps.UnitConverter:
                    rootFrame.Navigate(typeof(Utilities.Apps.UnitConverter.UnitConverter_LandingPage));
                    break;

                //case App.Apps.Stopwatch:
                //    rootFrame.Navigate(typeof(Utilities.Apps.Stopwatch.Stopwatch_MainPage));
                //    break;

                //case App.Apps.Timer:
                //    rootFrame.Navigate(typeof(Utilities.Apps.Timer.Timer_MainPage));
                //    break;

                case App.Apps.Area:
                    rootFrame.Navigate(typeof(Utilities.Apps.UnitConverter.UnitConverter));
                    uc = rootFrame.Content as Utilities.Apps.UnitConverter.UnitConverter;
                    uc.Init(Utilities.Apps.UnitConverter.UnitConverter.ConversionTypes.area, "Area", Apps.Area);
                    break;

                case Apps.Distance:
                    rootFrame.Navigate(typeof(Utilities.Apps.UnitConverter.UnitConverter));
                    uc = rootFrame.Content as Utilities.Apps.UnitConverter.UnitConverter;
                    uc.Init(Utilities.Apps.UnitConverter.UnitConverter.ConversionTypes.distance, "Distance", Apps.Distance);
                    break;

                case Apps.Mass:
                    rootFrame.Navigate(typeof(Utilities.Apps.UnitConverter.UnitConverter));
                    uc = rootFrame.Content as Utilities.Apps.UnitConverter.UnitConverter;
                    uc.Init(Utilities.Apps.UnitConverter.UnitConverter.ConversionTypes.mass, "Mass", Apps.Mass);
                    break;

                case Apps.Speed:
                    rootFrame.Navigate(typeof(Utilities.Apps.UnitConverter.UnitConverter));
                    uc = rootFrame.Content as Utilities.Apps.UnitConverter.UnitConverter;
                    uc.Init(Utilities.Apps.UnitConverter.UnitConverter.ConversionTypes.speed, "Speed", Apps.Speed);
                    break;

                case Apps.Volume:
                    rootFrame.Navigate(typeof(Utilities.Apps.UnitConverter.UnitConverter));
                    uc = rootFrame.Content as Utilities.Apps.UnitConverter.UnitConverter;
                    uc.Init(Utilities.Apps.UnitConverter.UnitConverter.ConversionTypes.volume, "Volume", Apps.Volume);
                    break;

                case Apps.Acceleration:
                    rootFrame.Navigate(typeof(Utilities.Apps.UnitConverter.UnitConverter));
                    uc = rootFrame.Content as Utilities.Apps.UnitConverter.UnitConverter;
                    uc.Init(Utilities.Apps.UnitConverter.UnitConverter.ConversionTypes.acceleration, "Acceleration", Apps.Acceleration);
                    break;

                case Apps.Angles:
                    rootFrame.Navigate(typeof(Utilities.Apps.UnitConverter.UnitConverter));
                    uc = rootFrame.Content as Utilities.Apps.UnitConverter.UnitConverter;
                    uc.Init(Utilities.Apps.UnitConverter.UnitConverter.ConversionTypes.angles, "Angles", Apps.Angles);
                    break;
                case Apps.Current:
                    rootFrame.Navigate(typeof(Utilities.Apps.UnitConverter.UnitConverter));
                    uc = rootFrame.Content as Utilities.Apps.UnitConverter.UnitConverter;
                    uc.Init(Utilities.Apps.UnitConverter.UnitConverter.ConversionTypes.current, "Current", Apps.Current);
                    break;

                //case Apps.About:
                //    rootFrame.Navigate(typeof(Utilities.Apps._About.AboutPage));
                //    break;

                default:
                    break;
            }

            if (newFrame)
            {
                Window.Current.Content = rootFrame;
                Window.Current.Activate();
            }

            // remember the current app
            ((App)Application.Current).CurrentApp = app;

            // static variable that holds the current page
            ((App)Application.Current).CurrentPage = rootFrame.Content;
            return ((App)Application.Current).CurrentPage;
        }

        #endregion

        public async void Feedback()
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("mailto:threemonkeys@live.com"));
        }
    }
}