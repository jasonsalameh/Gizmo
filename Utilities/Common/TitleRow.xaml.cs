using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Utilities
{
    public sealed partial class TitleRow : UserControl
    {
        private List<Breadcrumb> crumbs;

        public TitleRow()
        {
            this.InitializeComponent();

            crumbs = null;         
        }

        public void SetupBreadcrumbs(List<Breadcrumb> breadcrumbs)
        {
            SetupBreadcrumbs(breadcrumbs, true);
        }

        //Need to handle snapping
        private void SetupBreadcrumbs(List<Breadcrumb> breadcrumbs, bool copyLocal)
        {
            if (copyLocal)
                this.crumbs = breadcrumbs;

            // if in snapped mode
            if (App.CurrentView == App.ViewModelStates.Snapped)
            {
                this.stackCrumbs.Children.Clear();
                Breadcrumb bc = crumbs.Last();

                //Create a textblock
                TextBlock tb = new TextBlock();
                tb.Style = this.Resources["BreadcrumbStyle"] as Style;
                tb.Margin = new Thickness(0, 15, 0, 0);
                tb.Text = bc.TitleText;
                tb.Foreground = Helpers.GetBrush(Helpers.Colors.Black);

                this.stackCrumbs.Children.Add(tb);
            }

            // if we're not in snapped mode
            else
            {
                this.stackCrumbs.Children.Clear();
                for (int i = 0; i < breadcrumbs.Count; i++)
                {
                    Breadcrumb bc = breadcrumbs[i];
                    Utilities.Common.BreadCrumbItem bci = new Common.BreadCrumbItem(bc.TargetPage, App.NavigationStates.Normal, bc.TitleText, i, bc.IsLast);

                    // tilt effects
                    Callisto.Effects.Tilt.SetIsTiltEnabled(bci, true);

                    this.stackCrumbs.Children.Add(bci);
                }
            }
        }

        public double ButtonOpacity
        {
            get
            {
                return this.btnBack.Opacity;
            }
            set
            {
                this.btnBack.Opacity = value;
                if (btnBack.Opacity == 0)
                {
                    this.btnBack.IsEnabled = false;
                    this.btnBack.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                }
                else
                {
                    this.btnBack.IsEnabled = true;
                    this.btnBack.Visibility = Windows.UI.Xaml.Visibility.Visible;
                }
            }
        }

        private void btnBack_Click_1(object sender, RoutedEventArgs e)
        {
            App.NavigateToPage(this.crumbs[crumbs.Count - 2].TargetPage, ((App)App.Current).lastNavState);
        }

        public void ChangeViewState()
        {
            if(App.isLandscape() || App.isPortrait())
            {
                this.TimeControl.ShowTime();
                this.btnBack.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

                //this.btnBack.Margin = new Thickness(36, 12, 36, 0);

                if (crumbs != null)
                    this.SetupBreadcrumbs(crumbs, false);
            }
            else if (App.isSnapped())
            {
                this.TimeControl.HideTime();
                this.btnBack.Visibility = Windows.UI.Xaml.Visibility.Visible;

                this.btnBack.Margin = new Thickness(10, 12, 15, 0);

                if (crumbs != null)
                {
                    List<Breadcrumb> newList = new List<Breadcrumb>();
                    newList.Add(crumbs[crumbs.Count - 1]);
                    this.SetupBreadcrumbs(newList, false);
                }
            }
        }
    }
}
