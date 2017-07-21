using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notepad
{
    public class NotepadItem : System.ComponentModel.INotifyPropertyChanged
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
        private string _Token = string.Empty;
        public string Token
        {
            get
            {
                return this._Token;
            }

            set
            {
                if (this._Token != value)
                {
                    this._Token = value;
                    this.OnPropertyChanged("Token");
                }
            }
        }

        private string _FileName = string.Empty;
        public string FileName
        {
            get
            {
                return this._FileName;
            }

            set
            {
                if (this._FileName != value)
                {
                    this._FileName = value;
                    this.OnPropertyChanged("FileName");
                }
            }
        }
    }
}
