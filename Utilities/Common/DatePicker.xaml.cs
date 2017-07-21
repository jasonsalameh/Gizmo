using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Utilities
{
    public delegate void ValueChangedHandler(object o, ValueChangedEventArgs e);

    public class ValueChangedEventArgs : EventArgs
    {
        public readonly DateTime Date;

        public ValueChangedEventArgs(DateTime date)
        {
            Date = date;
        }
    }

    public sealed partial class DatePicker : UserControl
    {
        private const int YEAR_SPAN_FORWARD = 10;
        private const int YEAR_SPAN_BACK = 2;
        private int YEAR_OFFSET = DateTime.Today.Year - YEAR_SPAN_BACK;

        public event ValueChangedHandler ValueChanged = null;

        private ObservableCollection<ComboBoxItem> daysInMonth;

        // getters and setters for publics
        public int MonthValue
        {
            get 
            { 
                return Month.SelectedIndex + 1; 
            }
            set
            {
                Month.SelectedIndex = value - 1;
            }
        }
        public int DayValue
        {
            get
            {
                return Day.SelectedIndex + 1;
            }
            set
            {
                Day.SelectedIndex = value - 1;
            }
        }

        //need to add 2002 to account for the list starting at 2002
        public int YearValue
        {
            get
            {
                return Year.SelectedIndex + YEAR_OFFSET;
            }
            set
            {
                Year.SelectedIndex = value - YEAR_OFFSET;
            }
        }

        /// <summary>
        /// Returns the currently chosen date
        /// </summary>
        /// <returns></returns>
        public DateTime GetDate()
        {
            return new DateTime(YearValue, MonthValue, DayValue);
        }

        private int MinYearValue = -1;

        public DatePicker()
        {
            this.InitializeComponent();
            daysInMonth = new ObservableCollection<ComboBoxItem>();

            SetYear();
            SetMonth();

            // setup event handlers
            Day.SelectionChanged += DayChanged;
            Month.SelectionChanged += MonthChanged;
            Year.SelectionChanged += YearChanged;

            // setup the binding for the UX
            Day.ItemsSource = daysInMonth;
            SetTotalDaysInMonth(YearValue, MonthValue);
            SetDay();

        }

        public void Set(int year, int month, int day)
        {
            // fail
            if (month > 12 || month < 1 || day > 31 || day < 1 || year > MinYearValue + YEAR_SPAN_FORWARD || year < MinYearValue)
                return;

            // subtract 1 since they are zero-based
            Month.SelectedIndex = month - 1;
            Day.SelectedIndex = day - 1;

            // subtract MinYearValue since it's indexed far along
            Year.SelectedIndex = year - MinYearValue;
        }

        #region Private Initializers

        private void SetMonth()
        {
            // indexing starts at 0
            Month.SelectedIndex = DateTime.Today.Month - 1;
        }

        private void SetDay()
        {
            // indexing starts at 0
            Day.SelectedIndex = DateTime.Today.Day - 1;
        }

        private void SetYear()
        {
            for (int i = DateTime.Today.Year - YEAR_SPAN_BACK; i < DateTime.Today.Year + YEAR_SPAN_FORWARD; i++)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = i;

                Year.Items.Add(item);

                // capture the first starting year
                if (MinYearValue < 0)
                    MinYearValue = i;
            }

            Year.SelectedIndex = YEAR_SPAN_BACK;
        }

        #endregion

        private void SetTotalDaysInMonth(int year, int month)
        {
            int totalDays = DateTime.DaysInMonth(year, month);

            daysInMonth.Clear();

            for (int i = 1; i < totalDays + 1; i++)
            {
                ComboBoxItem cbi = new ComboBoxItem();
                cbi.Content = i.ToString();
                daysInMonth.Add(cbi);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ValueChangedEventArgs args = new ValueChangedEventArgs(new DateTime(this.YearValue, this.MonthValue, this.DayValue));
                ValueChanged(new object(), args);
            }

            // if the value was invalid like they entered Feb 31 2012
            catch (System.ArgumentOutOfRangeException)
            {
               
            }

            // all other errors
            catch { }
        }

        private void YearChanged(object sender, SelectionChangedEventArgs e)
        {
            SetTotalDaysInMonth(YearValue, MonthValue);
            Day.SelectedIndex = 0;
        }

        private void MonthChanged(object sender, SelectionChangedEventArgs e)
        {
            SetTotalDaysInMonth(YearValue, MonthValue);
            Day.SelectedIndex = 0;
        }

        private void DayChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ValueChangedEventArgs args = new ValueChangedEventArgs(new DateTime(this.YearValue, this.MonthValue, this.DayValue));
                ValueChanged(new object(), args);
            }
            catch 
            { 
            
            }
        }
    }
}
