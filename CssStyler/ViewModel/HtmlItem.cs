using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows;

namespace CssStyler.ViewModel
{
    class HtmlItem
    {
        private string _title;

        private string id;

        private string _htmlclasstitle;

        private string _htmlelementtitle;

        private bool _htmlClassCheck;

        private bool _htmlElementCheck;

        private CssStyler.Model.CssModel _model;

        private Visibility _htmlClassMenuTextVisibility;

        private ObservableCollection<HtmlItem> _items;

        public string Title { get { return _title; } set { _title = value; } }

        public ObservableCollection<HtmlItem> Items { get { return _items; } set { _items = value; } }

        public int Index { get; set; }

        public string HtmlClassMenuText { get { return _htmlclasstitle; } set { _htmlclasstitle = value; } }

        public string HtmlElementMenuText { get { return _htmlelementtitle; } set { _htmlelementtitle = value; } }
        public bool HtmlClassCheck { get { return _htmlClassCheck; } set { _htmlClassCheck = value; _model.UpdateCheckList(Index, HtmlElementCheck, HtmlClassCheck); } }
        public bool HtmlElementCheck { get { return _htmlElementCheck; } set { _htmlElementCheck = value; _model.UpdateCheckList(Index, HtmlElementCheck, HtmlClassCheck); } }
        public DelegateCommand Clicked { get; private set; }
        public Visibility HtmlClassMenuTextVisibility { get { return _htmlClassMenuTextVisibility; } set { _htmlClassMenuTextVisibility = value; } }
        public HtmlItem(CssStyler.Model.CssModel model)
        {
            this._items = new ObservableCollection<HtmlItem>();
            _model = model;
            Clicked = new DelegateCommand((param)=>{
                string prev_id = id;
                id = "";
                if (HtmlElementCheck) id += _htmlelementtitle;
                if (HtmlClassCheck) id += "." + _htmlclasstitle;
                _model.InvokeSelector(id,prev_id);
                _model.UpdateCheckList(Index, HtmlElementCheck, HtmlClassCheck);
            });
                
        }
    }
}
