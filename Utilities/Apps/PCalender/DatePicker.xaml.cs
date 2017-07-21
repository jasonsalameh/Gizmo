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
    public delegate void ValueChangedHandler(object o, ValueChangedEventArgs e);
    public delegate void IncorrectValueSpecifiedHandler(object o, IncorrectValueSpecifiedEventArgs e);

    public class ValueChangedEventArgs : EventArgs
    {
        public readonly DateTime Date;

        public ValueChangedEventArgs(DateTime date)
        {
            Date = date;
        }
    }

    public class IncorrectValueSpecifiedEventArgs : EventArgs
    {
        public IncorrectValueSpecifiedEventArgs()
        {

        }
    }

    public sealed partial class DatePicker : UserControl
    {
        public PCalender.Views.AddView addViewPage = null;
        private const int YEAR_SPAN = 10;
        public static event ValueChangedHandler ValueChanged = null;
        public static event IncorrectValueSpecifiedHandler IncorrectValueSpecified = null;

        // getters and setters for publics
        public int MonthValue { get { return Month.SelectedIndex + 1; }  }
        public int DayValue { get { return Day.SelectedIndex + 1; }  }
        public int YearValue { get { return int.Parse(PCal_MainPage.GetDataFromComboBoxItem(Year.SelectedItem)); }  }

        private int MinYearValue = -1;

        public DatePicker()
        {
            this.InitializeComponent();

            SetMonth();
            SetDay();
            SetYear();

            // setup event handlers
            Day.SelectionChanged += ComboBox_SelectionChanged;
            Month.SelectionChanged += ComboBox_SelectionChanged;
            Year.SelectionChanged += ComboBox_SelectionChanged;
        }

        public void SetValueChangedEventHandler()
        {
            if (addViewPage == null)
                return;

            ValueChanged += new ValueChangedHandler(addViewPage.ValueChanged);
            IncorrectValueSpecified += new IncorrectValueSpecifiedHandler(addViewPage.IncorrectValueSpecified);
        }

        public void Set(int year, int month, int day)
        {
            // fail
            if (month > 12 || month < 1 || day > 31 || day < 1 || year > MinYearValue + YEAR_SPAN || year < MinYearValue)
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
            Month.SelectedIndex = DateTime.Today.Month - 1;
        }

        private void SetDay()
        {
            Day.SelectedIndex = DateTime.Today.Day - 1;
        }

        private void SetYear()
        {
            for(int i = DateTime.Today.Year - YEAR_SPAN; i < DateTime.Today.Year + YEAR_SPAN; i++)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = i;

                Year.Items.Add(item);

                // capture the first starting year
                if (MinYearValue < 0)
                    MinYearValue = i;
            }

            Year.SelectedIndex = YEAR_SPAN;
        }

        #endregion

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
                IncorrectValueSpecified(new object(), new IncorrectValueSpecifiedEventArgs());
            }

            // all other errors
            catch { }
        }
    }
}
