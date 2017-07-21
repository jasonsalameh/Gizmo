using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Utilities
{
    public class MainPageItem : System.ComponentModel.INotifyPropertyChanged
    {
        #region Boiler plate GridView Code
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        public App.Apps App;


        public MainPageItem(App.Apps app)
        {
            this.Title = Helpers.GetAppName(app);
            this.TitleColor = Helpers.GetBrush(Helpers.Colors.White);

            string imageFileName = Helpers.selectImage(app);

            this.ImageFile = Helpers.GetMainImage(imageFileName);
            this.SnappedImageFile = Helpers.GetSnappedImage(imageFileName);
            this.BackgroundColor = Helpers.GetSnappedBrush(app);
            this.App = app;

            if (this.App == Utilities.App.Apps.NoPage)
                this._CommingSoon = true;
        }

        private bool _CommingSoon = false;
        public bool CommingSoon
        {
            get
            {
                return this._CommingSoon;
            }
            set
            {
                this._CommingSoon = value;
            }
        }

        private string _Title = string.Empty;
        public string Title
        {
            get
            {
                return this._Title;
            }

            set
            {
                if (this._Title != value)
                {
                    this._Title = value;
                    this.OnPropertyChanged("Title");
                }
            }
        }

        private SolidColorBrush _BackgroundColor = new SolidColorBrush(ColorHelper.FromArgb(255, 0, 0, 0));
        public SolidColorBrush BackgroundColor
        {
            get
            {
                return this._BackgroundColor;
            }

            set
            {
                if (this._BackgroundColor != value)
                {
                    this._BackgroundColor = value;
                    this.OnPropertyChanged("BackgroundColor");
                }
            }
        }

        private SolidColorBrush _TitleColor = new SolidColorBrush(ColorHelper.FromArgb(255, 0, 0, 0));
        public SolidColorBrush TitleColor
        {
            get
            {
                return this._TitleColor;
            }

            set
            {
                if (this._TitleColor != value)
                {
                    this._TitleColor = value;
                    this.OnPropertyChanged("TitleColor");
                }
            }
        }

        private ImageSource _ImageFile = null;
        public ImageSource ImageFile
        {
            get
            {
                return this._ImageFile;
            }

            set
            {
                this._ImageFile = value;
                this.OnPropertyChanged("ImageFile");
            }
        }

        private ImageSource _SnappedImageFile = null;
        public ImageSource SnappedImageFile
        {
            get
            {
                return this._SnappedImageFile;
            }

            set
            {
                this._SnappedImageFile = value;
                this.OnPropertyChanged("SnappedImageFile");
            }
        }
    }
}
