using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Utilities.Apps.UnitConverter
{
    public class UnitConverterItem : System.ComponentModel.INotifyPropertyChanged
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

        

        public UnitConverterItem(string title, string result, SolidColorBrush background)
        {
            Title = title;
            Result = result;
            BackgroundColor = background;
            index = 0;
        }

        private int index = 0;
        public int Index
        {
            get
            {
                return this.index;
            }
            set
            {
                if (this.index != value)
                {
                    this.index = value;
                    this.OnPropertyChanged("Index");
                }
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

        private string _Result = string.Empty;
        public string Result
        {
            get
            {
                return this._Result;
            }

            set
            {
                if (this._Result != value)
                {
                    this._Result = value;
                    this.OnPropertyChanged("Result");
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
    }
}
