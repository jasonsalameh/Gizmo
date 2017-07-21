using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Utilities.Common
{
    public sealed partial class TimeViewer : UserControl
    {
        //private bool blackTheme;
        private DispatcherTimer timer, dateTimer;
        private string basicFormatString = "h:mm tt";
        private string militaryFormatString = "HH:mm";
        private string currentFormatString;

        public TimeViewer()
        {
            this.InitializeComponent();

            //Load this from settings in the future

            if (StateManager.RoamingStateExists(App.Apps.NoPage, StateManager.TITLE_ROW_TIME))
            {
                bool loaded = false;
                currentFormatString = (string)StateManager.LoadRoamingState(App.Apps.NoPage, StateManager.TITLE_ROW_TIME, ref loaded);
            }
            else
                currentFormatString = basicFormatString;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += timer_Tick;
            timer.Start();

            dateTimer = new DispatcherTimer();
            dateTimer.Interval = TimeSpan.FromMinutes(1);
            dateTimer.Tick += dateTimer_Tick;
            dateTimer.Start();

            timer_Tick(null, null);
            dateTimer_Tick(null, null);

            this.InnerAppBar.Fill = Helpers.GetSnappedBrush(App.Apps.MainPage);
        }

        public void HideTime()
        {
            this.RootGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        public void ShowTime()
        {
            this.RootGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        void dateTimer_Tick(object sender, object e)
        {
            this.txtDate.Text = DateTime.Now.Month + "/" + DateTime.Now.Day + "/" + DateTime.Now.Year;
        }

        void timer_Tick(object sender, object e)
        {
            this.txtTime.Text = DateTime.Now.ToString(currentFormatString);
        }

        private void timerTapped(object sender, PointerRoutedEventArgs e)
        {
            if (currentFormatString == basicFormatString)
            {
                currentFormatString = militaryFormatString;
                StateManager.SaveRoamingState(App.Apps.NoPage, StateManager.TITLE_ROW_TIME, militaryFormatString);
            }
            else
            {
                currentFormatString = basicFormatString;
                StateManager.SaveRoamingState(App.Apps.NoPage, StateManager.TITLE_ROW_TIME, basicFormatString);
            }

            timer_Tick(null, null);
        }

        private void TextBlock_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            App.Apps currentApp = (Application.Current as App).CurrentApp;

            switch (currentApp)
            {
                case App.Apps.Notepad:
                    Utilities.Common.ShowHelp.ShowHelpMessage(App.Apps.Notepad, true);
                    break;

                case App.Apps.Calculator:
                case App.Apps.Programmer:
                case App.Apps.Scientific:
                case App.Apps.Statistics:
                    Utilities.Common.ShowHelp.ShowHelpMessage(App.Apps.Calculator, true);
                    break;

                case App.Apps.UnitConverter:
                case App.Apps.Area:
                case App.Apps.Distance:
                case App.Apps.Mass:
                case App.Apps.Speed:
                case App.Apps.Volume:
                case App.Apps.Stopwatch:
                case App.Apps.Timer:
                case App.Apps.Acceleration:
                case App.Apps.Angles:
                case App.Apps.Cooking:
                case App.Apps.Density:
                case App.Apps.Current:
                case App.Apps.Energy:
                case App.Apps.Flow:
                case App.Apps.Force:
                case App.Apps.Frequency:
                case App.Apps.Light:
                case App.Apps.Power:
                case App.Apps.Pressure:
                case App.Apps.Torque:
                case App.Apps.Viscosity:
                    Utilities.Common.ShowHelp.ShowHelpMessage(App.Apps.UnitConverter, true);
                    break;

                case App.Apps.PCalendar:
                    Utilities.Common.ShowHelp.ShowHelpMessage(App.Apps.PCalendar, true);
                    break;
                    
                case App.Apps.ToDo:
                    Utilities.Common.ShowHelp.ShowHelpMessage(App.Apps.ToDo, true);
                    break;

                case App.Apps.MainPage:
                    Utilities.Common.ShowHelp.ShowHelpMessage(App.Apps.MainPage, true);
                    break;

                default:
                    break;
            }

            // show help
            
        }
    }
}
