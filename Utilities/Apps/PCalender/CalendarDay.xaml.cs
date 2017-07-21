using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace PCalender
{
    public sealed partial class CalendarDay : UserControl
    {
        private int _year;
        private int _month;
        private int _day;

        public CalendarDay(int Year, int Month, int Day)
        {
            this.InitializeComponent();
         
            _color = Utilities.Helpers.Colors.None;
            IsGrayed = false;

            _year = Year;
            _month = Month;
            _day = Day;
        }

        public void IsDayHeader()
        {
            dayText.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
        }

        public static bool IsToday(CalendarDay day)
        {
            try
            {
                if (day._year == DateTime.Today.Year &&
                    day._month == DateTime.Today.Month &&
                    day._day == DateTime.Today.Day)
                    return true;

            }
            catch { }

            return false;
        }

        public string DayValue
        {
            get
            {
                return dayValue.Text;
            }
            set
            {
                dayValue.Text = value;
                dayValueSnapped.Text = value;
            }
        }

        public string DayValueSnapped
        {
            get
            {
                return dayValueSnapped.Text;
            }
            set
            {
                dayValueSnapped.Text = value;
            }
        }

        public string DayText
        {
            get
            {
                return dayText.Text;
            }
            set
            {
                dayText.Text = value;
            }

        }

        private double _overlayOpacity = .4;
        public bool IsGrayed
        {
            get
            {
                return this.OverLay.Opacity > 0;
            }
            set
            {
                if (value)
                {
                    this.OverLay.Opacity = _overlayOpacity;
                    this.dayText.Foreground = Utilities.Helpers.GetBrush(Utilities.Helpers.Colors.Gray);
                    this.dayValue.Foreground = Utilities.Helpers.GetBrush(Utilities.Helpers.Colors.Gray);
                    this.dayValueSnapped.Foreground = Utilities.Helpers.GetBrush(Utilities.Helpers.Colors.Gray);
                }
                else
                {
                    this.OverLay.Opacity = 0;
                    this.dayText.Foreground = Utilities.Helpers.GetBrush(Utilities.Helpers.Colors.Gray);
                    this.dayValue.Foreground = Utilities.Helpers.GetBrush(Utilities.Helpers.Colors.Gray);
                    this.dayValueSnapped.Foreground = Utilities.Helpers.GetBrush(Utilities.Helpers.Colors.Gray);
                }

            }
        }

        public void SetAllText(Utilities.Helpers.Colors color)
        {
            this.dayText.Foreground = Utilities.Helpers.GetBrush(color);
            this.dayValue.Foreground = Utilities.Helpers.GetBrush(color);
            this.dayValueSnapped.Foreground = Utilities.Helpers.GetBrush(color);
        }

        private Utilities.Helpers.Colors _color;
        public Utilities.Helpers.Colors DayColor
        {
            get
            {
                return this._color;
            }
            set
            {
                _color = value;
                this.CalendarDayGrid.Background = Utilities.Helpers.GetBrush(_color);
            }
        }

        


        internal void SnapView()
        {
            dayText.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            dayValue.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            dayValueSnapped.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        internal void NormalView()
        {
            dayText.Visibility = Windows.UI.Xaml.Visibility.Visible;
            dayValue.Visibility = Windows.UI.Xaml.Visibility.Visible;

            dayValueSnapped.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
    }
}
