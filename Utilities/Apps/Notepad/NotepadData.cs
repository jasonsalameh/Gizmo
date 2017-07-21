using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Streams;

namespace Notepad
{
    class NotepadData
    {
        Notepad_MainPage page;
        public int ItemCount;
        public NotepadData(Notepad_MainPage page)
        {
            this.page = page;
            ItemCount = 0;            
        }

        async public Task init(int numToLoad)
        {
            page.LoadingTxt.Visibility = Windows.UI.Xaml.Visibility.Visible;
            //Create the collection
            NotepadItem item;

            //Iterate through MRU list
            AccessListEntryView entries = StorageApplicationPermissions.MostRecentlyUsedList.Entries;
            ItemCount = 0;
            foreach (AccessListEntry entry in entries)
            {
                try
                {
                    
                    item = new NotepadItem();

                    StorageFile storageFile = await StorageApplicationPermissions.MostRecentlyUsedList.GetFileAsync(entry.Token);
                    item.Title = storageFile.DisplayName;
                    item.Token = entry.Token;
                    
                    if (ItemCount < numToLoad && item.Title != "")
                    {
                        Collection.Add(item);
                        ItemCount++;
                    }

                    //item.FileName = storageFile.Path;
                    IRandomAccessStream txtStream = await storageFile.OpenAsync(FileAccessMode.Read);

                    IInputStream readStream = txtStream.GetInputStreamAt(0);

                    DataReader reader = new DataReader(readStream);
                    uint numBytesLoaded = await reader.LoadAsync((uint)txtStream.Size);
                    if (numBytesLoaded > 25)
                        numBytesLoaded = 25;
                    item.FileName = reader.ReadString(numBytesLoaded) + "...";                    
                }
                catch 
                {
                    //int i = 0;
                }
            }
            page.LoadingTxt.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            page.RefreshGridView();

            //iterate through each of them again to reset the list
            ItemCount = 0;
            AccessListEntryView entries2 = StorageApplicationPermissions.MostRecentlyUsedList.Entries;
            foreach (AccessListEntry entry in entries2)
            {
                try
                {
                    if (ItemCount < numToLoad)
                        ItemCount++; //need to do this to keep the count the same                    

                    StorageFile storageFile = await StorageApplicationPermissions.MostRecentlyUsedList.GetFileAsync(entry.Token);
                }
                catch { }
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

    //public class ItemCollection : IEnumerable<Object>
    //{
    //    private System.Collections.ObjectModel.ObservableCollection<NotepadItem> itemCollection = new System.Collections.ObjectModel.ObservableCollection<NotepadItem>();

    //    public IEnumerator<Object> GetEnumerator()
    //    {
    //        return itemCollection.GetEnumerator();
    //    }

    //    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    //    {
    //        return GetEnumerator();
    //    }

    //    public void Add(NotepadItem item)
    //    {
    //        itemCollection.Add(item);
    //    }
    //}
    
}
