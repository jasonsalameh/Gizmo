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
using Windows.Globalization;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.ApplicationSettings;
using Utilities;
using Windows.UI.Core;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

// Via http://www.kotex.com/na/period-planners/quick-period-calculator.aspx

namespace PCalender
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PCal_MainPage : Page
    {
        // constants
        private const int DURATION_INDEX = -1;
        private const int DURATION_DEFAULT = 4;
        private const int CYCLE_LENGTH_INDEX = -15;
        private const int CYCLE_LENGTH_DEFAULT = 28;


        // globals
        private ObservableCollection<CalendarMonth> _allMonths;
        private Dictionary<DateTime, Period> _allPeriods;
        public Period mostRecentPeriod;
        private bool _changingSelection;
        private bool _editingPeriod;
        private Period _editedPeriod;
        public Utilities.SettingsFlyout editPopup;
        private CalendarMonth _thisMonth;

        public const string DELIM = "#";

        public PCal_MainPage()
        {
            this.InitializeComponent();

            App.ScheduleOnNextRender(delegate
            {
                List<Breadcrumb> breadcrumbs = new List<Breadcrumb>();
                breadcrumbs.Add(new Breadcrumb(App.Apps.MainPage));
                breadcrumbs.Add(new Breadcrumb(App.Apps.PCalendar, true));
                TitleRow.SetupBreadcrumbs(breadcrumbs);
            });
        }

        #region View

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _allMonths = new ObservableCollection<CalendarMonth>();
            _allPeriods = new Dictionary<DateTime, Period>();


            // add this month
            _thisMonth = AddMonthToCal(DateTime.Today.Year, DateTime.Today.Month);
            CalendarFlip.ItemsSource = _allMonths.ToArray();
            CalendarFlip.SelectedIndex = 1;

            // Load sate
            LoadState();

            // Populate the snapped list
            Add_FuturePeriodsToList(PeriodListSnapped);

            // set viewstate handler
            //Window.Current.SizeChanged += Current_SizeChanged;
            this.SizeChanged += Current_SizeChangedUAP;

            // init calc view
            MainPage_ViewStateChanged();

            // set colors for the snapped view text
            PeriodSnappedText.Foreground = Utilities.Helpers.GetBrush(Utilities.Helpers.Colors.Red);
            OvulationSnappedText.Foreground = Utilities.Helpers.GetBrush(Utilities.Helpers.Colors.Blue);
            FertileSnappedText.Foreground = Utilities.Helpers.GetBrush(Utilities.Helpers.Colors.Green);

            //TBD:SettingsPane.GetForCurrentView().CommandsRequested += Settings_CommandsRequested;

            // show help
            Utilities.Common.ShowHelp.ShowHelpMessage(App.Apps.PCalendar);

            // show up comming period information 
            ShowNextPeriod();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            //TBD:SettingsPane.GetForCurrentView().CommandsRequested -= Settings_CommandsRequested;
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
        private void Current_SizeChangedUAP(object sender, SizeChangedEventArgs e)
        {
            App.UpdateViewModelFromSize(e.NewSize);
            if (App.CurrentView == App.PreviousView)
                return;

            Current_SizeChanged();
        }

        private void Current_SizeChanged()
        {
            MainPage_ViewStateChanged();
        }

        private void MainPage_ViewStateChanged()
        {
            TitleRow.ChangeViewState();
            if (App.isSnapped())
            {
                SnappedViewPane.Visibility = Windows.UI.Xaml.Visibility.Visible;
                SnappedViewPaneHeight.Height = new GridLength(1, GridUnitType.Star);
                CalendarFlipHeight.Height = new GridLength(250, GridUnitType.Pixel);
                CalendarFlip.Margin = new Thickness(10);

                for (int i = 0; i < _allMonths.Count; i++)
                    _allMonths[i].SnapView();
            }

            else if (App.isPortrait())
            {
                SnappedViewPane.Visibility = Windows.UI.Xaml.Visibility.Visible;
                SnappedViewPaneHeight.Height = new GridLength(1, GridUnitType.Star);
                CalendarFlipHeight.Height = new GridLength(1, GridUnitType.Star);
                CalendarFlip.Margin = new Thickness(0);

                for (int i = 0; i < _allMonths.Count; i++)
                    _allMonths[i].NormalView();
            }

            else if (App.isLandscape())
            {
                SnappedViewPane.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                SnappedViewPaneHeight.Height = new GridLength(1, GridUnitType.Auto);
                CalendarFlipHeight.Height = new GridLength(1, GridUnitType.Star);
                CalendarFlip.Margin = new Thickness(0);

                for (int i = 0; i < _allMonths.Count; i++)
                    _allMonths[i].NormalView();
            }
        }



        #endregion

        #region Show Next period

        private void ShowNextPeriod()
        {
            try
            {
                // figure out the next period
                int nextPeriodDays = int.MaxValue;
                bool nowOnPeriod = false;
                foreach (DateTime pDate in mostRecentPeriod.AllPeriodDates.Keys)
                {
                    // if pdate is before today
                    if (DateTime.Compare(pDate, DateTime.Today) < 0)
                    {
                        continue;
                    }

                    // else pdate dt is after today
                    else if (DateTime.Compare(pDate, DateTime.Today) > 0)
                    {
                        // make sure this is the first period in the set
                        if (mostRecentPeriod.AllPeriodDates[pDate])
                        {
                            int days = Math.Abs((pDate - DateTime.Today).Days);
                            if (nextPeriodDays > days)
                                nextPeriodDays = days;
                        }
                    }

                    // else pdate is today
                    else
                    {
                        nowOnPeriod = true;
                        if (mostRecentPeriod.AllPeriodDates[pDate])
                        {
                            nextPeriodDays = 0;
                            break;
                        }
                    }
                }

                // add the text to show when the next period starts
                if (nextPeriodDays == 0)
                    NextPeriod.Text = "Next period starts today!";
                else if (nextPeriodDays == 1)
                    NextPeriod.Text = "Next period starts tomorrow!";
                else if (nextPeriodDays == 7)
                    NextPeriod.Text = "Next period starts next week!";
                else
                    NextPeriod.Text = "Next period in " + nextPeriodDays.ToString() + " days";

                // if there is an active period add this information
                if (nowOnPeriod && nextPeriodDays != 0)
                    NextPeriod.Text = "You're on a period now. " + NextPeriod.Text;
            }
            catch
            {
                // just show nothing
                NextPeriod.Text = string.Empty;
            }
        }

        #endregion

        #region Add Month

        /// <summary>
        /// Add a single month to the UI and underlying calendar 
        /// </summary>
        /// <param name="Year">The current Year as int</param>
        /// <param name="Month">The current Month as int</param>
        /// <returns>True if the add was successfull, false if the month already existed in the list</returns>
        private CalendarMonth AddMonthToCal(int Year, int Month)
        {
            bool toFront = false;
            CalendarMonth returnMonth = null;

            // determine if this month should go in the front or back
            // we always assume the caller has given us either the next
            // or previous month... we do no validation
            if (_allMonths == null || _allMonths.Count == 0)
                toFront = false;
            else
            {
                CalendarMonth first = _allMonths[0];

                if (first.Compare(Year, Month) > 0)
                    toFront = true;
                else
                    toFront = false;
            }

            if (toFront)
            {
                // figure out how much time from today to this value "in the past"
                TimeSpan ts = DateTime.Today - new DateTime(Year, Month, 1);

                // if it's too far back don't add it
                if (Math.Abs(ts.Days) > Period.DAYS_IN_YEAR * Period.YEARS_BACK)
                    return returnMonth;
            }
            else
            {
                // figure out how much time from today to this value "in the past"
                TimeSpan ts = DateTime.Today - new DateTime(Year, Month, 1);

                // if it's too far back don't add it
                if (Math.Abs(ts.Days) > Period.DAYS_IN_YEAR * Period.YEARS_FORWARD)
                    return returnMonth;
            }


            // if this month isn't already loaded, add it
            if (!isMonthLoaded(Year, Month))
            {
                CalendarMonth cm = new CalendarMonth(Year, Month);

                // ensure the proper colors are added to it with the 
                // most recent period 
                if (mostRecentPeriod != null)
                    Add_Period_To_Calendar(mostRecentPeriod, cm);

                // ensure the calendar is appropriate to the current
                // view mode (snapped vs normal)
                if (App.isSnapped())
                    cm.SnapView();

                if (toFront)
                    _allMonths.Insert(0, cm);
                else
                    _allMonths.Add(cm);

                _changingSelection = true;
                CalendarFlip.ItemsSource = _allMonths;
                _changingSelection = false;

                returnMonth = cm;
            }

            return returnMonth;
        }

        #endregion

        #region Flipview Control

        /// <summary>
        /// When the user flips the month on the calendar to move to the next or previous months
        /// we'll need to add in the views for the previous and next months of the new month navigated
        /// to
        /// </summary>
        /// <param name="sender">The Selector in the flipview</param>
        /// <param name="e">Args passed in</param>
        private void CalendarFlip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // to stop from recursing 
            if (_changingSelection)
                return;

            CalendarMonth selectedMonth = (sender as Selector).SelectedItem as CalendarMonth;

            // if we just set the itemsSource there was nothing selected
            if (selectedMonth == null)
                return;

            // get the current datetime object
            DateTime dt = new DateTime(selectedMonth.Year, selectedMonth.Month, 1);

            // add next month and last month
            AddMonthToCal(dt.AddMonths(1).Year, dt.AddMonths(1).Month);
            AddMonthToCal(dt.AddMonths(-1).Year, dt.AddMonths(-1).Month);

            CalendarFlip.SelectedItem = selectedMonth;
            CalendarFlip.UpdateLayout();

            // change the month
            MonthName.Text = dt.ToString("MMM") + " " + dt.Year.ToString();
        }

        #endregion

        #region State

        private void LoadState()
        {
            try
            {
                bool success = true;
                object retVal = Utilities.StateManager.LoadRoamingState(Utilities.App.Apps.PCalendar, Utilities.StateManager.PCAL_PERIOD_KEY, ref success);

                List<Period> list = retVal as List<Period>;

                // add each period to internal state and determine the most recent period
                foreach (Period p in list)
                {
                    _allPeriods.Add(p.StartDate, p);

                    // determine what was the most recent period
                    if ((mostRecentPeriod == null) || (p.StartDate.CompareTo(mostRecentPeriod.StartDate) > 0))
                        mostRecentPeriod = p;
                }

                // draw the period to the calendar
                Draw_Period_To_Calendar(mostRecentPeriod);
            }
            catch { }
        }

        private void SaveState()
        {
            Utilities.StateManager.SaveRoamingState(Utilities.App.Apps.PCalendar, Utilities.StateManager.PCAL_PERIOD_KEY, _allPeriods.Values.ToList());
        }

        private void ClearState()
        {
            Utilities.StateManager.ClearRoamingState(Utilities.App.Apps.PCalendar, Utilities.StateManager.PCAL_PERIOD_KEY);
        }

        #endregion

        #region Add Period

        public void Close_AddPeriod_Button_Click(object sender, RoutedEventArgs e)
        {
            _editingPeriod = false;
            _editedPeriod = null;
        }

        private void AddPeriod_Button_Click(object sender, RoutedEventArgs e)
        {
            SettingsAppBar.IsOpen = false;

            Utilities.MainPage.ShowPopup(new PCalender.Views.AddView(this), Window.Current.Bounds.Height, "Add Period");

            _editingPeriod = false;
            _editedPeriod = null;
        }

        /// <summary>
        /// When the "Add Button" is clicked in the UI for the add period flyout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool AddPeriod_Data_Button_Click(Utilities.DatePicker StartDate, ComboBox Duration, ComboBox CycleLength)
        {
            try
            {
                DateTime startDate = new DateTime(StartDate.YearValue, StartDate.MonthValue, StartDate.DayValue);
                int duration = int.Parse(GetDataFromComboBoxItem(Duration.SelectedItem));
                int cycleLength = int.Parse(GetDataFromComboBoxItem(CycleLength.SelectedItem));

                // create and add the new period information
                Period p = new Period(startDate, duration, cycleLength);

                // add to internal lists
                Add_Period_Internal(p);

                // if there is a period being edited remove it
                if (_editingPeriod && _editedPeriod != null)
                {
                    RemovePeriod(_editedPeriod);
                    _editingPeriod = false;
                    _editedPeriod = null;

                    // since we completed the edit there's no need for the edit window anymore
                    editPopup.CloseFlyout();
                }

                SaveState();
                ResetSnappedList();

                // update period information
                ShowNextPeriod();
            }
            catch { return false; }

            return true;
        }

        /// <summary>
        /// Actually adds a period to the internal lists and calendar
        /// </summary>
        /// <param name="p">Period to add</param>
        private void Add_Period_Internal(Period p)
        {
            _allPeriods.Add(p.StartDate, p);

            // if there is no recent period add the incomming or if the newly added period is more recent
            if ((mostRecentPeriod == null) || (p.StartDate.CompareTo(mostRecentPeriod.StartDate) > 0))
                Draw_Period_To_Calendar(p);
        }

        /// <summary>
        /// Draws the period colors to the calendarmonth object. function assumes input is most recent
        /// period from the entire series
        /// </summary>
        /// <param name="p">The period to draw</param>
        private void Draw_Period_To_Calendar(Period p)
        {
            // first clear the calendars
            Clear_All_Periods();

            // reset the most recent period information
            mostRecentPeriod = p;

            // only add to the calendar if this was a more recent period
            foreach (CalendarMonth cm in _allMonths)
                Add_Period_To_Calendar(p, cm);
        }

        /// <summary>
        /// Clears all period information from the current set of CalendarMonths. This 
        /// includes all color information
        /// </summary>
        private void Clear_All_Periods()
        {
            foreach (CalendarMonth cm in _allMonths)
                cm.ClearAllDayColors();
        }

        /// <summary>
        /// Add the period data to a specific calendar month
        /// </summary>
        /// <param name="p">The most recent period information present</param>
        /// <param name="cm">The calendar month to add to</param>
        private void Add_Period_To_Calendar(Period p, CalendarMonth cm)
        {
            // for fertility dates
            foreach (DateTime fDate in p.AllFertilityDates)
                if (cm.ContainsDay(fDate.Year, fDate.Month, fDate.Day))
                {
                    cm.SetDayColor(fDate.Year, fDate.Month, fDate.Day, Utilities.Helpers.Colors.Green);
                    cm.SetDayText(fDate.Year, fDate.Month, fDate.Day, "Fertility");
                }

            // for the Periods
            foreach (DateTime pDate in p.AllPeriodDates.Keys)
                if (cm.ContainsDay(pDate.Year, pDate.Month, pDate.Day))
                {
                    cm.SetDayColor(pDate.Year, pDate.Month, pDate.Day, Utilities.Helpers.Colors.Red);
                    cm.SetDayText(pDate.Year, pDate.Month, pDate.Day, "Period");
                }

            // for ovulation dates
            foreach (DateTime oDate in p.AllOvulationDates)
                if (cm.ContainsDay(oDate.Year, oDate.Month, oDate.Day))
                {
                    cm.SetDayColor(oDate.Year, oDate.Month, oDate.Day, Utilities.Helpers.Colors.Blue);
                    cm.SetDayText(oDate.Year, oDate.Month, oDate.Day, "Ovulation");
                }

        }

        #endregion

        #region Edit Period

        private void EditPeriod_Button_Click(object sender, RoutedEventArgs e)
        {
            _editingPeriod = false;
            _editedPeriod = null;

            if ((App.isSnapped()) &&
                (PeriodListSnapped.SelectedIndex >= 0))
            {
                EditPeriod_Context_Change(PeriodListSnapped);
            }
            else
            {
                // create the page
                PCalender.Views.EditView editView = new Views.EditView(this);

                // close the settings bar
                SettingsAppBar.IsOpen = false;

                // add all the periods to the internal list
                Add_PeriodsToList(editView.PeriodList);

                // pop the settings pane
                editPopup = Utilities.MainPage.ShowPopup(editView, Window.Current.Bounds.Height, "Edit Period");
            }
        }

        private void Add_PeriodsToList(ListView list)
        {
            foreach (Period p in _allPeriods.Values)
            {
                string date = p.StartDate.ToString("ddd, MMM dd yyyy");
                list.Items.Add(GetViewBox(date, p));
            }
        }

        private Viewbox GetViewBox(string date, object context)
        {
            // Create color for text
            Color c = new Color();
            c.A = 255;
            c.R = 90;
            c.G = 90;
            c.B = 90;

            // get the text block
            TextBlock tb = new TextBlock();

            // adding in extra spaces because when it gets highlighted first char gets chopped a bit
            tb.Text = "  " + date;
            tb.FontSize = 25;
            tb.Foreground = new SolidColorBrush(c);

            Viewbox vb = new Viewbox();
            vb.Height = 30;
            vb.Child = tb;

            // set the data context so it can be referenced later
            vb.DataContext = context;

            return vb;
        }

        public void EditPeriod_Context_Change(ListView list)
        {
            try
            {
                // get the selected Period
                Period selectedPeriod = (list.SelectedItem as Viewbox).DataContext as Period;

                PCalender.Views.AddView addView = new PCalender.Views.AddView(this);

                // Set all the items in the add date control then switch to it
                addView.StartDate.Set(selectedPeriod.StartDate.Year, selectedPeriod.StartDate.Month, selectedPeriod.StartDate.Day);
                addView.Duration.SelectedIndex = DURATION_INDEX + selectedPeriod.Duration;
                addView.CycleLength.SelectedIndex = CYCLE_LENGTH_INDEX + selectedPeriod.CycleLength;

                _editingPeriod = true;
                _editedPeriod = selectedPeriod;

                Utilities.MainPage.ShowPopup(addView, Window.Current.Bounds.Height, "Add Period");
            }
            catch
            {
                // show error
            }
        }

        #endregion

        #region Remove period

        public void RemovePeriod(Period period, ListView PeriodList = null)
        {
            // remove from internal list and UI
            _allPeriods.Remove(period.StartDate);

            if (PeriodList != null)
                PeriodList.Items.Remove(PeriodList.SelectedItem);

            // if this period was actually drawn to the calendar
            if (mostRecentPeriod == period)
            {
                mostRecentPeriod = null;

                // find the next most recent period
                foreach (Period p in _allPeriods.Values)
                    if ((mostRecentPeriod == null) || (p.StartDate.CompareTo(mostRecentPeriod.StartDate) > 0))
                        mostRecentPeriod = p;

                // redraw
                if (mostRecentPeriod != null)
                    Draw_Period_To_Calendar(mostRecentPeriod);
                else
                    Clear_All_Periods();
            }

            SaveState();
            ResetSnappedList();

            // show recent period

            ShowNextPeriod();
        }

        #endregion

        #region Snap Specific

        private void ResetSnappedList()
        {
            PeriodListSnapped.Items.Clear();
            Add_FuturePeriodsToList(PeriodListSnapped);
        }

        /// <summary>
        /// Using the mostRecentPeriod object, adds all future periods
        /// to the input list
        /// </summary>
        /// <param name="list"></param>
        private void Add_FuturePeriodsToList(ListView list)
        {
            if (mostRecentPeriod == null)
                return;

            foreach (DateTime dt in mostRecentPeriod.AllPeriodDates.Keys)
            {
                // since there are many dates in a period (1st day of a p, 2nd of a p...)
                // we shouldn't add them all, rather we should add the first day of a series
                if (mostRecentPeriod.AllPeriodDates[dt])
                {
                    // determine if the period has already occured... if it has don't show
                    if (DateTime.Today.CompareTo(dt) < 0)
                    {
                        string date = dt.ToString("ddd, MMM dd yyyy");
                        list.Items.Add(GetViewBox(date, new DateTime(dt.Year, dt.Month, 1)));
                    }
                }
            }
        }


        private void PeriodListSnapped_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                DateTime nav = ((DateTime)((e.ClickedItem as Viewbox).DataContext));
                CalculateDateDifference dateDiff;

                // if the month has NOT already been loaded, load into memory
                if (!isMonthLoaded(nav.Year, nav.Month))
                {
                    // since this is specific to months in the future we only need to see
                    // where it is relative to our last month
                    DateTime last = new DateTime(_allMonths.Last().Year, _allMonths.Last().Month, 1);

                    // get the difference
                    dateDiff = new CalculateDateDifference(last, nav);

                    // now add all the months to the flipview
                    for (int i = 0; i < dateDiff.Months; i++)
                    {
                        last = last.AddMonths(1);
                        AddMonthToCal(last.Year, last.Month);
                    }
                }

                // now that it's loaded, navigate to it

                // first determine which month we're at
                CalendarMonth curMonth = (CalendarFlip.SelectedItem as CalendarMonth);
                DateTime curDate = new DateTime(curMonth.Year, curMonth.Month, 1);

                // then determine how many months we need to move (not specific to forward or back)
                dateDiff = new CalculateDateDifference(curDate, nav);

                // then determine if the current month is ahead or behind where we need to go
                int compare = curMonth.Compare(nav.Year, nav.Month);

                // if we're trying to navigate to the current month we're at... gtfo
                if (compare == 0)
                    return;
                else if (compare > 0) // the current month is further than where we want to nav... go back
                {
                    CalendarFlip.SelectedIndex -= dateDiff.Months;
                }
                else // the navigation point is further than where we're at... go forward
                {
                    CalendarFlip.SelectedIndex += dateDiff.Months;
                }
            }
            catch { }
        }

        /// <summary>
        /// determines if the inputted month has been loaded into memory
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        private bool isMonthLoaded(int Year, int Month)
        {
            foreach (CalendarMonth cm in _allMonths)
                if (cm.Compare(Year, Month) == 0)
                    return true;

            return false;
        }

        #endregion

        #region Helpers


        public static string GetDataFromComboBoxItem(object selectedItem)
        {
            try
            {
                return (selectedItem as ComboBoxItem).Content.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion

        private void Today_Button_Click(object sender, RoutedEventArgs e)
        {
            CalendarFlip.SelectedItem = _thisMonth;
        }
    }
}
