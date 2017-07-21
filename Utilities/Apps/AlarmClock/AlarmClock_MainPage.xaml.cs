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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace Utilities.Apps.AlarmClock
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AlarmClock_MainPage : Page
    {
        public AlarmClock_MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            for (int i = 0; i < 10; i++)
                AddAlarm(i);
        }

        private void AddAlarm(int i)
        {
            //TextBlock foo = new TextBlock();
            //foo.Text = "FOO" + i;
            Image foo = new Image();
            foo.Source = new BitmapImage(new Uri(new Uri("ms-appx:///"), "Apps/AlarmClock/Assets/clock.png"));
            foo.Style = (Style)this.Resources["AlarmStateStyle"];
            TextBlock bar = new TextBlock();
            bar.Text = DateTime.Now.ToString();
            bar.Style = (Style)this.Resources["AlarmTimeStyle"];
            AlarmState.Children.Add(foo);
            AlarmTime.Children.Add(bar);
        }
    }
}
