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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Utilities.Common
{
    public sealed partial class BreadCrumbItem : UserControl
    {
        private bool _isLast = false;
        private App.Apps _targetPage;
        private App.NavigationStates _navState;

        public BreadCrumbItem(App.Apps TargetPage, App.NavigationStates NavState, string BreadCrumbName, int Position, bool IsLast = false)
        {
            this.InitializeComponent();

            this._isLast = IsLast;
            this._targetPage = TargetPage;
            this._navState = NavState;
            this.TextBlock.Text = BreadCrumbName;

            // if we need to use the logo image
            if (this._targetPage == App.Apps.MainPage || Position == 0)
            {
                RootGrid.Margin = new Thickness(-40, 0, 0, 0);
                this.BaseAppBar.Fill = Helpers.GetSnappedBrush(App.Apps.MainPage);
                this.BaseAppBar.Visibility = Windows.UI.Xaml.Visibility.Visible;
                this.InnerAppBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                this.Image.Source = Helpers.GetSnappedImage(Helpers.selectImage(App.Apps.MainPage));
            }
            else
            {
                RootGrid.Margin = new Thickness(-40, 0, 0, 0);
                this.InnerAppBar.Fill = Helpers.GetSnappedBrush(this._targetPage); 
                this.BaseAppBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                this.InnerAppBar.Visibility = Windows.UI.Xaml.Visibility.Visible;
                this.Image.Source = Helpers.GetSnappedImage(Helpers.selectImage(this._targetPage));
            }
        }

        private void RootGrid_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            if (this._isLast)
                return;

            App.NavigateToPage(_targetPage, _navState);
        }


    }
}
