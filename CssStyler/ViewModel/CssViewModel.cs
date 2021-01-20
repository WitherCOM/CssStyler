using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using CssStyler.Model;
using Microsoft.Win32;

namespace CssStyler.ViewModel
{
    class CssViewModel : ViewModelBase
    {
        public CssModel _model;

        private ObservableCollection<HtmlItem> _htmltreeviewitems;
        public ObservableCollection<HtmlItem> HtmlTreeViewItems { get { return _htmltreeviewitems; } set { _htmltreeviewitems = value; OnPropertyChanged(); } }

        public DelegateCommand LoadHtml { get; private set; }

        public CssViewModel(CssModel model)
        {
            _model = model;
            _model.TreeViewCreated += _model_TreeViewCreated;
            LoadHtml = new DelegateCommand((param) =>
            {
                OpenFileDialog diag = new OpenFileDialog();
                diag.DefaultExt = ".html";
                if(diag.ShowDialog().Value)
                {
                    _model.CreateTree(diag.FileName);
                }
            });
            _model.HtmlDecodeError += _model_HtmlDecodeError;
        }

        private void _model_HtmlDecodeError(object sender, Persistance.StringEventArg e)
        {
            MessageBox.Show(e.Str);
        }

        private void _model_TreeViewCreated(object sender, TreeViewEventArgs e)
        {
            ObservableCollection<HtmlItem> obslist = new ObservableCollection<HtmlItem>();
            ObservableRecursion(e.List, obslist);
            HtmlTreeViewItems = obslist;
        }

        private void ObservableRecursion(List<HtmlTag> list, ObservableCollection<HtmlItem> treelist)
        {
            if (list == null || list.Count == 0) return;
            foreach (HtmlTag tag in list)
            {
                if (tag.Class != "")
                    treelist.Add(new HtmlItem(_model) { Title = tag.Tag, HtmlClassMenuText = "Class: " + tag.Class, HtmlElementMenuText = "Tag: " + tag.Tag, HtmlClassMenuTextVisibility = System.Windows.Visibility.Visible,, Index = tag.Index });
                else
                    treelist.Add(new HtmlItem(_model) { Title = tag.Tag, HtmlClassMenuText = "Class: " + tag.Class, HtmlElementMenuText = "Tag: " + tag.Tag, HtmlClassMenuTextVisibility = System.Windows.Visibility.Hidden, Index = tag.Index });
                ObservableRecursion(tag.Inner, treelist[treelist.Count - 1].Items);
            }
        }
    }
}
