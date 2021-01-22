using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using CssStyler.Model;
using CssStyler.ViewModel;
using CssStyler.Persistance;

namespace CssStyler.Model
{
    class CssModel
    {

        private List<HtmlTag> htmltree;
        private List<Selector> selectors;
        private DataLoad _dataload;
        private string _loadedhtml;
        private int _currentedit;

        public event EventHandler<TreeViewEventArgs> TreeViewCreated;
        public event EventHandler<StringEventArg> HtmlDecodeError;
        public event EventHandler<EventArgs> HtmlLoaded;
        public event EventHandler<EventArgs> AllChecked;
        public event EventHandler<EventArgs> NotAllChecked;
        public event EventHandler<EventArgs> NothingChecked;

        public CssModel(DataLoad dataload)
        {
            _dataload = dataload;
            dataload.FileLoaded += Dataload_FileLoaded;
            selectors = new List<Selector>();

        }

        private void Dataload_FileLoaded(object sender, StringEventArg e)
        {
            try
            {
                htmltree = new List<HtmlTag>();
                
                List<HtmlTag> current_root = htmltree;
                List<List<HtmlTag>> prev_root = new List<List<HtmlTag>>();
                List<string> tags = new List<string>();
                string temp = "";
                bool inTag = false;
                foreach (char c in e.Str)
                {
                    if (c == '<')
                    {
                        temp = "";
                        inTag = true;
                    }
                    if (inTag)
                    {
                        temp += c;

                    }
                    if (c == '>')
                    {
                        tags.Add(temp);
                        inTag = false;
                    }
                }
                int index = 0;
                foreach (string s in tags)
                {
                    if (!s.StartsWith("</") && s.StartsWith('<') && s.EndsWith('>') && !s.EndsWith("/>"))
                    {
                        int idclass = s.IndexOf("class=");
                        string c = "";
                        if (idclass != -1)
                            c = s.Substring(idclass + 7, s.IndexOf('"', idclass + 7) - idclass - 7);
                        int len = (s.IndexOf(' ') != -1) ? (s.IndexOf(' ') - 1) : (s.Length - 2);
                        string t = s.Substring(1, len);
                        current_root.Add(new HtmlTag(t, c) { Index = index });
                        prev_root.Add(current_root);
                        current_root = current_root[current_root.Count - 1].Inner;
                        index++;
                    }
                    else if (s.StartsWith("</") && s.EndsWith(">"))
                    {
                        current_root = prev_root[prev_root.Count - 1];
                        prev_root.RemoveAt(prev_root.Count - 1);
                    }
                    else if (s.StartsWith("<") && s.EndsWith("/>"))
                    {
                        int idclass = s.IndexOf("class=");
                        string c = "";
                        if (idclass != -1)
                            c = s.Substring(idclass + 7, s.IndexOf('"', idclass + 7) - idclass - 7);
                        int len = (s.IndexOf(' ') != -1) ? (s.IndexOf(' ') - 1) : (s.Length - 3);
                        current_root.Add(new HtmlTag(s.Substring(1, len), c) { Index = index });
                        index++;
                    }
                }
                if (prev_root.Count != 0) throw new Exception("Wrong HTML file");
                _loadedhtml = e.Str;
                OnTreeViewCreated(htmltree);
            }
            catch (Exception exp)
            {
                OnHtmlDecodeError(exp.Message);
            }

        }

        private void OnTreeViewCreated(List<HtmlTag> list)
        {
            TreeViewCreated?.Invoke(this, new TreeViewEventArgs(list));
        }

        private void OnHtmlDecodeError(string str)
        {
            HtmlDecodeError?.Invoke(this, new StringEventArg(str));
        }

        public void CreateTree(string path)
        {
            _dataload.SetPath(path);
            _dataload.LoadFile();
        }

        public void InvokeSelector(string id, string prev_id)
        {
            if (selectors.Count == 0) return;
            if (selectors.Count <= _currentedit + 1) return;
            string[] blocks = selectors[_currentedit].Id.Split(", ");
            string uj = "";
            foreach (string s in blocks)
            {
                if (s == prev_id)
                {
                    uj += id;
                    uj += ", ";
                }
                else
                {
                    uj += s;
                    uj += ", ";
                }
            }
            uj = uj.Remove(uj.Length - 2, 2);
            selectors[_currentedit].Id = uj;

        }

        public void UpdateCheckList(int index,bool element_check, bool class_check)
        {
            FindElementInTree(index, element_check, class_check, htmltree);
        }
        
        private void FindElementInTree(int index,bool element_check,bool class_check,List<HtmlTag> tree)
        {
            foreach(HtmlTag t in tree)
            {
                if(t.Index == index)
                {
                    t.HtmlClassCheck = class_check;
                    t.HtmlElementCheck = element_check;
                    return;
                }
                if(t.Inner?.Count !=0)
                {
                    FindElementInTree(index, element_check, class_check, t.Inner);
                }
            }
        }

        public void HowChecked()
        {
            int all=0,check=0;
            IsChechkedInTree(ref check,ref all, htmltree);
            if(check == 0)
            {
                NothingChecked?.Invoke(this, new EventArgs());
                return;
            }
            if(check == all)
            {
                AllChecked?.Invoke(this, new EventArgs());
                return;
            }
            NotAllChecked?.Invoke(this, new EventArgs());
        }

        private void IsChechkedInTree(ref int count,ref int all, List<HtmlTag> tree)
        {
            foreach (HtmlTag t in tree)
            {
                all++;
                if(t.Class != "")
                {
                    all++;
                }
                if (t.HtmlClassCheck)
                {
                    count++;
                }
                if(t.HtmlElementCheck)
                {
                    count++;
                }
                if (t.Inner?.Count != 0)
                {
                    IsChechkedInTree(ref count,ref all, t.Inner);
                }
            }
        }

        public void AddSelector()
        {

        }

        public void RemoveSelector(int index)
        {

        }
    }
}
