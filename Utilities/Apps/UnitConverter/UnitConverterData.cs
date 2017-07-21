using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Utilities.Apps.UnitConverter
{
    class UnitConverterData
    {

        public void InitializeGridView(List<UnitConverterItem> items)
        {
            Collection.Clear();
            foreach (UnitConverterItem item in items)
            {
                Collection.Add(item);
            }
        }

        private ItemCollection _Collection = new ItemCollection();

        public ItemCollection Collection
        {
            get
            {
                return this._Collection;
            }
        }

    }    
}
