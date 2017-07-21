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
using PCalender;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PCalender.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditView : Page
    {
        private PCal_MainPage mainPage;

        public EditView(PCal_MainPage MainPage)
        {
            this.InitializeComponent();
            this.mainPage = MainPage;
        }

        public ListView PeriodList
        {
            get { return this.periodList; }
            set { this.PeriodList = value; }
        }


        private void EditPeriod_Data_Button_Click(object sender, RoutedEventArgs e)
        {
            mainPage.EditPeriod_Context_Change(PeriodList);
        }

        #region Remove Period

        private void RemovePeriod_Data_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // get the selected Period
                Period selectedPeriod = (PeriodList.SelectedItem as Viewbox).DataContext as Period;

                // remove the period and allow the user to 
                mainPage.RemovePeriod(selectedPeriod, this.periodList);

                PeriodList.UpdateLayout();
            }
            catch
            {
                // show error
            }
        }

        #endregion

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private TextBlock _previousSelection;
        private void PeriodList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_previousSelection != null)
                _previousSelection.Foreground = Utilities.Helpers.GetBrush(Utilities.Helpers.Colors.Gray);

            if (PeriodList.SelectedIndex >= 0)
            {
                EditButton.Visibility = Windows.UI.Xaml.Visibility.Visible;
                RemoveButton.Visibility = Windows.UI.Xaml.Visibility.Visible;

                ((PeriodList.SelectedItem as Viewbox).Child as TextBlock).Foreground = Utilities.Helpers.GetBrush(Utilities.Helpers.Colors.White);
                _previousSelection = ((PeriodList.SelectedItem as Viewbox).Child as TextBlock);
            }
            else
            {
                EditButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                RemoveButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }
    }
}
