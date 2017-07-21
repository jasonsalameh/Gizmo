using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ApplicationSettings;
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
    public sealed partial class SettingsFlyout : UserControl
    {
        public enum FlyoutTheme { White, Black, Gray }
        private FlyoutTheme internalTheme;
        private string titleText;
        private bool isSettingsCharm;

        public SettingsFlyout()
        {
            this.InitializeComponent();

            titleText = string.Empty;
            isSettingsCharm = false;
        }

        private void MySettingsBackClicked(object sender, RoutedEventArgs e)
        {
            if (this.Parent.GetType() == typeof(Popup))
                ((Popup)this.Parent).IsOpen = false;

            // if this control is for the settings pane
            //TBD:if(isSettingsCharm)
            //TBD:SettingsPane.Show();
        }

        public void CloseFlyout()
        {
            if (this.Parent.GetType() == typeof(Popup))
                ((Popup)this.Parent).IsOpen = false;
        }

        public string TitleText
        {
            get
            {
                return this.titleText;
            }
            set
            {
                this.titleText = value;
                txtTitle.Text = titleText;
            }

        }

        public FlyoutTheme Theme
        {
            get
            {
                return this.internalTheme;
            }
            set
            {
                this.internalTheme = value;
                if (this.internalTheme == FlyoutTheme.Black)
                {
                    this.btnBack.Style = (Style)this.Resources["BackButtonBlackStyle"];
                    //Text should be white for the black theme
                    this.txtTitle.Foreground = Utilities.Helpers.GetBrush(Utilities.Helpers.Colors.Black);
                    this.MainGrid.Background = Utilities.Helpers.GetBrush(Utilities.Helpers.Colors.White);
                }
                else if (this.internalTheme == FlyoutTheme.White)
                {
                    this.btnBack.Style = (Style)((Application.Current) as App).Resources["BackButtonStyle"];
                    //Text is black for the white theme
                    this.txtTitle.Foreground = Utilities.Helpers.GetBrush(Utilities.Helpers.Colors.White);
                    this.MainGrid.Background = Utilities.Helpers.GetBrush(Utilities.Helpers.Colors.Black);
                }
                else if (this.internalTheme == FlyoutTheme.Gray)
                {
                    this.btnBack.Style = (Style)this.Resources["BackButtonGrayStyle"];
                    //Text is black for the white theme
                    this.txtTitle.Foreground = Utilities.Helpers.GetBrush(Utilities.Helpers.Colors.White);
                    this.MainGrid.Background = Utilities.Helpers.GetBrush(Utilities.Helpers.Colors.Gray);
                }
            }
        }

        public Grid InnerGrid
        {
            get { return this.InnerGridData; }
            set { this.InnerGridData = value; }
        }

        public bool IsSettingsCharm
        {
            get { return this.isSettingsCharm; }
            set { this.isSettingsCharm = value; }
        }
    }
}
