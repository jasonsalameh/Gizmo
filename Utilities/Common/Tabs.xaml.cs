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
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Utilities
{
    public class Tab
    {
        private static int nextIndex = 0;
        public int Index;
        public string Name;
        public RoutedEventHandler Handler;

        public Tab(string name, RoutedEventHandler handler)
        {
            Index = nextIndex++;
            Name = name;
            Handler = handler;
        }
    }

    public sealed partial class Tabs : UserControl
    {
        List<Tab> tabs;

        public Tabs()
        {
            this.InitializeComponent();
            tabs = new List<Tab>();
        }

        public void Add(Tab t)
        {
            tabs.Add(t);
            Refresh(t.Index);
        }

        public void Remove(string name)
        {
            foreach (Tab t in tabs)
            {
                if (t.Name == name)
                    tabs.Remove(t);
            }
            Refresh();
        }

        public void Clear()
        {
            tabs = new List<Tab>();
            Refresh();
        }

        private void Refresh(int selected = -1)
        {
            bool indexSet = selected >= 0 ? true : false;

            stkTabs.Children.Clear();

            foreach (Tab t in tabs.OrderBy(tab => tab.Index))
            {
                Button tab = new Button();
                tab.Content = t.Name;
                tab.Click += t.Handler;
                if (t.Index == selected)
                {
                    tab.Style = this.Resources["SelectedTabButtonStyle"] as Style;
                    indexSet = true;
                }
                else
                    tab.Style = this.Resources["TabButtonStyle"] as Style;
                stkTabs.Children.Add(tab);
            }

            if(!indexSet && stkTabs.Children.Count > 0)
                ((Button)stkTabs.Children.ElementAt(0)).Style = this.Resources["SelectedTabButtonStyle"] as Style;
        }

        public void Select(Button selectedButton)
        {
            IEnumerable<UIElement> tabButtons = stkTabs.Children.AsEnumerable();
            foreach(Button btn in tabButtons)
            {
                if(btn == selectedButton)
                    btn.Style = this.Resources["SelectedTabButtonStyle"] as Style;
                else
                    btn.Style = this.Resources["TabButtonStyle"] as Style;
            }                    
        }
    }
}
