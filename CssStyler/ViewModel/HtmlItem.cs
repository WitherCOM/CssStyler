using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CssStyler.ViewModel
{
    public class HtmlItem
    {
        private string _title;

        private string _htmlelementtitle;

        private ObservableCollection<HtmlItem> _items;

        public string Title { get { return _title; } set { _title = value; } }

        public ObservableCollection<HtmlItem> Items { get { return _items; } set { _items = value; } }

        public string HtmlElementMenuText { get { return _htmlelementtitle; } set { _htmlelementtitle = value; } }
        public HtmlItem()
        {
            this._items = new ObservableCollection<HtmlItem>();
        }
    }
}
