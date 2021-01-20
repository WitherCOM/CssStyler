using System;
using System.Collections.Generic;
using System.Text;

namespace CssStyler.Model
{
    class HtmlTag
    {
        private string _tag;
        private string _class;
        private int _index;
        List<HtmlTag> _inner;

        public string Tag { get { return _tag; } set { _tag = value; } }
        public string Class { get { return _class; } set { _class = value; } }
        public int Index { get { return _index; } set { _index = value; } }
        public bool HtmlClassCheck { get; set; }
        public bool HtmlElementCheck { get; set; }
        public List<HtmlTag> Inner { get { return _inner; } set { _inner = value; } }
        public HtmlTag(string tag,string classn)
        {
            _tag = tag;
            _class = classn;
            _inner = new List<HtmlTag>();
        }
    }
}
