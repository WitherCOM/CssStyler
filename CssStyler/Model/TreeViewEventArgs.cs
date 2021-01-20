using CssStyler.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;


namespace CssStyler.Model
{
    class TreeViewEventArgs : EventArgs
    {
        private List<HtmlTag> _list;

        public List<HtmlTag> List { get { return _list; } set { _list = value; } }

        public TreeViewEventArgs(List<HtmlTag> list)
        {
            _list = list;
        }

    }
}
