using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using CssStyler.Model;

namespace CssStyler.ViewModel
{
    class CssViewModel : ViewModelBase
    {

        private ObservableCollection<HtmlItem> _htmltreeviewitems;
        public ObservableCollection<HtmlItem> HtmlTreeViewItems { get { return _htmltreeviewitems; } set { _htmltreeviewitems = value; OnPropertyChanged(); } }

        public CssViewModel()
        {
            
        }

        private void UpdateHtmlTreeView()
        {

        }
    }
}
