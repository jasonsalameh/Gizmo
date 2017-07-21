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

namespace PCalender
{
    public sealed partial class CalendarMonth : UserControl
    {
        private const int ROWS = 6;
        private const int COLS = 7;
        private List<CalendarDay> _headerDays;
        private Dictionary<string,CalendarDay> _calendarDays;
        private List<CalendarDay> _coloredDays;
        private CalendarDay _today = null;
        private const string TODAY_STRING = "Today";

        // publics
        public int Year { get; set; }
        public int Month { get; set; }

        public CalendarMonth(int year, int month)
        {
            this.InitializeComponent();

            this._calendarDays = new Dictionary<string, CalendarDay>();
            this._coloredDays = new List<CalendarDay>();
            this._headerDays = new List<CalendarDay>();
            this.Year = year;
            this.Month = month;

            int days = DateTime.DaysInMonth(year, month);
            DateTime firstActualDayOfMonth = new DateTime(year, month, 1);

            // Since the first day of the month could be in the middle of the week, we'll still need to 
            // add in days for the preivous month which go before it
            DateTime startingDayOfCalMonth = firstActualDayOfMonth.AddDays(
                        numDayOfWeek(new DateTime(year, month, 1).DayOfWeek) * -1 // multiply by -1 to subtract
                        );

            DateTime currentDayOfCalMonth = startingDayOfCalMonth;
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    CalendarDay cd = new CalendarDay(currentDayOfCalMonth.Year, currentDayOfCalMonth.Month, currentDayOfCalMonth.Day);
                    cd.SetValue(Grid.RowProperty, i);
                    cd.SetValue(Grid.ColumnProperty, j);
                    

                    // if we're doing the days row
                    if (i == 0)
                    {
                        DayOfWeek nextday = (DayOfWeek)j;
                        cd.DayText = nextday.ToString();
                        cd.DayValueSnapped = nextday.ToString().Substring(0, 3); // add the three letter rep of the day
                        cd.IsDayHeader();

                        _headerDays.Add(cd);
                    }

                    // if we're no longer on the rows with the days "Sun --> Sat"
                    else
                    {
                        cd.DayValue = currentDayOfCalMonth.Day.ToString();

                        // if the current day is in the past month
                        if (currentDayOfCalMonth.Month != month)
                            cd.IsGrayed = true;

                        // if this is today
                        if (CalendarDay.IsToday(cd))
                            SetToday(cd);

                        // add to internal Dictionary
                        _calendarDays.Add(GetCalendarDayGUID(currentDayOfCalMonth.Year, currentDayOfCalMonth.Month, currentDayOfCalMonth.Day), cd);

                        // increment days
                        currentDayOfCalMonth = currentDayOfCalMonth.AddDays(1);
                    }

                    // add to UI
                    CalendarMonthGrid.Children.Add(cd);
                } 
            }
        }

        public void SnapView()
        {
            for(int i = 0; i < _calendarDays.Count; i++)
            {
                CalendarDay cd = _calendarDays.ElementAt(i).Value;
                cd.SnapView();
            }

            for (int i = 0; i < _headerDays.Count; i++)
            {
                CalendarDay cd = _headerDays.ElementAt(i);
                cd.SnapView();
            }
        }

        public void NormalView()
        {
            for (int i = 0; i < _calendarDays.Count; i++)
            {
                CalendarDay cd = _calendarDays.ElementAt(i).Value;
                cd.NormalView();
            }

            for (int i = 0; i < _headerDays.Count; i++)
            {
                CalendarDay cd = _headerDays.ElementAt(i);
                cd.NormalView();
            }
        }

        /// <summary>
        /// Sets the input calendar day to today
        /// </summary>
        /// <param name="cd">the input calendarday</param>
        private void SetToday(CalendarDay cd)
        {
            cd.DayText = TODAY_STRING;
            cd.DayColor = Utilities.Helpers.Colors.Gray;
            cd.SetAllText(Utilities.Helpers.Colors.White);
            _today = cd;

            _coloredDays.Add(_today);
        }


        /// <summary>
        /// Clears the internal _today object if it is not null
        /// </summary>
        private void ClearToday()
        {
            if (_today != null)
            {
                _today.DayColor = Utilities.Helpers.Colors.None;
                _today.DayText = string.Empty;
            }
        }

        /// <summary>
        /// Determines if this Month contains a specific day
        /// </summary>
        /// <param name="Year">The input year</param>
        /// <param name="Month">The input month</param>
        /// <param name="Day">The input day</param>
        /// <returns>true if this CalendarMonth object contains the day specified</returns>
        public bool ContainsDay(int Year, int Month, int Day)
        {
            return _calendarDays.ContainsKey(GetCalendarDayGUID(Year, Month, Day));
        }

        /// <summary>
        /// Sets the color of the specified day in the current CalendarMonth
        /// </summary>
        /// <param name="Year">The input year</param>
        /// <param name="Month">The input month</param>
        /// <param name="Day">The input day</param>
        /// <param name="Color">The color to set to</param>
        public void SetDayColor(int Year, int Month, int Day, Utilities.Helpers.Colors Color)
        {
            if (ContainsDay(Year, Month, Day))
            {
                CalendarDay cd = _calendarDays[GetCalendarDayGUID(Year, Month, Day)];
                cd.DayColor = Color;
                cd.SetAllText(Utilities.Helpers.Colors.White);
                _coloredDays.Add(cd);
            }
        }

        /// <summary>
        /// Sets the text of the specified day in the current CalendarMonth
        /// </summary>
        /// <param name="Year">The input year</param>
        /// <param name="Month">The input month</param>
        /// <param name="Day">The input day</param>
        /// <param name="Text">The text to set to</param>
        public void SetDayText(int Year, int Month, int Day, string Text)
        {
            if (ContainsDay(Year, Month, Day))
            {       
                if (_calendarDays[GetCalendarDayGUID(Year, Month, Day)].DayText == TODAY_STRING )
                    _calendarDays[GetCalendarDayGUID(Year, Month, Day)].DayText += " - " + Text;
                else
                    _calendarDays[GetCalendarDayGUID(Year, Month, Day)].DayText = Text;
            }
        }

        /// <summary>
        /// Clears all colors in the CalendarMonth
        /// </summary>
        public void ClearAllDayColors()
        {
            CalendarDay today = null;

            // first clear the today object if it exists. it will be reset later
            ClearToday();

            // clear each day unless it's today and reset it
            foreach (CalendarDay day in _coloredDays)
            {
                if (CalendarDay.IsToday(day))
                    today = day;
                else
                {
                    day.DayText = string.Empty;
                    day.DayColor = Utilities.Helpers.Colors.LightGray;
                    day.SetAllText(Utilities.Helpers.Colors.Gray);
                }
            }

            // set the today calendarDay
            if (today != null)
                SetToday(today);
        }

        /// <summary>
        /// returns the unique ID for the dictionary _calendar days
        /// </summary>
        /// <param name="year">the year</param>
        /// <param name="month">the month</param>
        /// <param name="day">the day</param>
        /// <returns>a string representation of the unique id</returns>
        private string GetCalendarDayGUID(int year, int month, int day)
        {
            return year.ToString() + PCal_MainPage.DELIM + month.ToString() + PCal_MainPage.DELIM + day.ToString();
        }

        /// <summary>
        /// Returns the day of the week in integer format
        /// 
        /// 0 - Sunday
        /// 1 - Monday
        /// 2 - Tuesday
        /// 3 - Wednesday
        /// 4 - Thursday
        /// 5 - Friday
        /// 6 - Saturday
        /// </summary>
        /// <param name="dayOfWeek">curent day of the week object</param>
        /// <returns>The int representation of the day</returns>
        private int numDayOfWeek(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return 0;
                case DayOfWeek.Monday:
                    return 1;
                case DayOfWeek.Tuesday:
                    return 2;
                case DayOfWeek.Wednesday:
                    return 3;
                case DayOfWeek.Thursday:
                    return 4;
                case DayOfWeek.Friday:
                    return 5;
                case DayOfWeek.Saturday:
                    return 6;
                default:
                    return -1;
            }
        }

        /// <summary>
        /// Compares this month to see if it's further in the future or the past
        /// than the input month
        /// </summary>
        /// <param name="cm"></param>
        /// <returns> -1 if input is further, 0 if they're the same, 1 if this month is further in future</returns>
        public int Compare(int Year, int Month)
        {
            if (this.Year > Year)
                return 1;
            else if (this.Year == Year)
            {
                if (this.Month > Month)
                    return 1;
                else if (this.Month == Month)
                    return 0;
                else
                    return -1;
            }
            else
                return -1;
        }
    }
}

