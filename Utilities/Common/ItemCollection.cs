using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class ItemCollection : IEnumerable<Object>
    {
        private System.Collections.ObjectModel.ObservableCollection<object> itemCollection = new System.Collections.ObjectModel.ObservableCollection<object>();

        public IEnumerator<Object> GetEnumerator()
        {
            return itemCollection.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(object item)
        {
            itemCollection.Add(item);
        }

        public void Clear()
        {
            itemCollection.Clear();
        }
        
        public int Count()
        {
            return this.Count();
        }

        public void RemoveAt(int item)
        {
            this.RemoveAt(item);
        }
        
        public object ItemAt(int item)
        {
            return itemCollection[item];
        }
    }
}

