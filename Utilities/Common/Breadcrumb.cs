using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Utilities
{
    public class Breadcrumb
    {
        public string TitleText;
        public App.Apps TargetPage;
        public bool IsLast;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TitleText">The string to show on the breadcrumb bar</param>
        /// <param name="TargetPage">The App.Apps page to navigate to if this item is clicked. Should be App.Apps.NoPage if this is the last page in the breadcrumb series.</param>
        public Breadcrumb(App.Apps TargetPage, bool IsLast = false, string Title = "")
        {
            if (Title == string.Empty)
                this.TitleText = Helpers.GetAppName(TargetPage);
            else
                this.TitleText = Title;

            this.TargetPage = TargetPage;
            this.IsLast = IsLast;
        }
    }
}
