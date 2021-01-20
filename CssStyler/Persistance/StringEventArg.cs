using System;
using System.Collections.Generic;
using System.Text;

namespace CssStyler.Persistance
{
    class StringEventArg : EventArgs
    {
        private string _str;
        public string Str { get { return _str; } set { _str = value; } }
        public StringEventArg(string str)
        {
            _str = str;
        }
    }
}
