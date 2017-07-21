using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Utilities;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PCalender.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddView : Page
    {
        private PCal_MainPage mainPage;

        private const int DURATION_INDEX = -1;
        private const int DURATION_DEFAULT = 4;
        private const int CYCLE_LENGTH_INDEX = -15;
        private const int CYCLE_LENGTH_DEFAULT = 28;

        public Utilities.DatePicker StartDate
        {
            get { return this.startDate; }
            set { this.startDate = value; }
        }

        public ComboBox Duration
        {
            get { return this.duration; }
            set { this.duration = value; }
        }

        public ComboBox CycleLength
        {
            get { return this.cycleLength; }
            set { this.cycleLength = value; }
        }

        public AddView(PCal_MainPage MainPage)
        {
            this.InitializeComponent();
            this.mainPage = MainPage;

            // set defaults on the Add Period view
            Duration.SelectedIndex = DURATION_DEFAULT + DURATION_INDEX;
            CycleLength.SelectedIndex = CYCLE_LENGTH_DEFAULT + CYCLE_LENGTH_INDEX;

            // if the most recent period is avail, copy it's details into the new one
            if (mainPage.mostRecentPeriod != null)
            {
                Duration.SelectedIndex = mainPage.mostRecentPeriod.Duration + DURATION_INDEX;
                CycleLength.SelectedIndex = mainPage.mostRecentPeriod.CycleLength + CYCLE_LENGTH_INDEX;
            }

            // initialize the date picker and setup the event handler
            StartDate.ValueChanged += ValueChanged;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        /// <summary>
        /// When the Cycle Length or Duration comboboxes change this function is invoked
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The event args</param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ValueChangedEventArgs args = new ValueChangedEventArgs(new DateTime(StartDate.YearValue, StartDate.MonthValue, StartDate.DayValue));
                ValueChanged(new object(), args);
            }

            // all other errors
            catch { }
        }

        /// <summary>
        /// When the "Add Button" is clicked in the UI for the add period flyout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddPeriod_Data_Button_Click(object sender, RoutedEventArgs e)
        {            
            if(mainPage.AddPeriod_Data_Button_Click(StartDate, Duration, CycleLength))
                PeriodAddText.Visibility = Windows.UI.Xaml.Visibility.Visible;
            else
                PeriodErrorText.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        /// <summary>
        /// Is called when the DatePicker "StartDate" is changed
        /// </summary>
        /// <param name="o">A new object, not used</param>
        /// <param name="e">The args will be used, specifically e.Date should be filled</param>
        public void ValueChanged(object o, ValueChangedEventArgs e)
        {
            ValueChanged(e.Date);
        }

        /// <summary>
        /// Calculates the upcomming period from the inputs 
        /// </summary>
        /// <param name="startDate">The start date from the last period</param>
        public void ValueChanged(DateTime startDate)
        {
            // remove the previous added text
            PeriodAddText.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            DateTime StartDate = startDate;
            int PeriodDuration = int.Parse(PCal_MainPage.GetDataFromComboBoxItem(Duration.SelectedItem));
            int PeriodCycleLength = int.Parse(PCal_MainPage.GetDataFromComboBoxItem(CycleLength.SelectedItem));

            NextPeriod.Text = StartDate.AddDays(PeriodCycleLength).ToString("D");
        }
    }
}
