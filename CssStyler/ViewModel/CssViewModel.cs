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

        private ObservableCollection<HtmlItemViewModel> _htmltreeviewitems;
        public ObservableCollection<HtmlItemViewModel> HtmlTreeViewItems { get { return _htmltreeviewitems; } set { _htmltreeviewitems = value; OnPropertyChanged(); } }

        private bool? _allchecked;

        public bool? AllChecked { get { return _allchecked; } set { _allchecked = value; OnPropertyChanged(); } }

        private string _htmlpath;
        public string HtmlPath { get { return _htmlpath; } set { _htmlpath = value;OnPropertyChanged(); } }

        public DelegateCommand LoadHtml { get; private set; }

        public DelegateCommand AllCheckedChange { get; private set; }

        public CssViewModel(CssModel model)
        {
            _model = model;
            _model.TreeViewCreated += _model_TreeViewCreated;
            _model.AllChecked += _model_AllChecked;
            _model.NotAllChecked += _model_NotAllChecked;
            _model.NothingChecked += _model_NothingChecked;
            AllChecked = false;
            LoadHtml = new DelegateCommand((param) =>
            {
                OpenFileDialog diag = new OpenFileDialog();
                diag.DefaultExt = ".html";
                if (diag.ShowDialog().Value)
                {
                    _model.CreateTree(diag.FileName);
                    _htmlpath = diag.FileName;
                }
            });
            AllCheckedChange = new DelegateCommand((param) =>
            {
                if(AllChecked.HasValue)
                ObservationCheckAll(AllChecked.Value, _htmltreeviewitems);
            });
            _model.HtmlDecodeError += _model_HtmlDecodeError;

        }

        private void _model_NothingChecked(object sender, EventArgs e)
        {
            AllChecked = false;
        }

        private void _model_NotAllChecked(object sender, EventArgs e)
        {
            AllChecked = null;
        }

        private void _model_AllChecked(object sender, EventArgs e)
        {
            AllChecked = true;
        }

        private void _model_HtmlDecodeError(object sender, Persistance.StringEventArg e)
        {
            MessageBox.Show(e.Str);
        }

        private void _model_TreeViewCreated(object sender, TreeViewEventArgs e)
        {
            ObservableCollection<HtmlItemViewModel> obslist = new ObservableCollection<HtmlItemViewModel>();
            ObservableRecursion(e.List, obslist);
            HtmlTreeViewItems = obslist;
        }

        private void ObservableRecursion(List<HtmlTag> list, ObservableCollection<HtmlItemViewModel> treelist)
        {
            if (list == null || list.Count == 0) return;
            foreach (HtmlTag tag in list)
            {
                if (tag.Class != "")
                    treelist.Add(new HtmlItemViewModel(_model) { Title = tag.Tag, HtmlClassMenuText = "Class: " + tag.Class, HtmlElementMenuText = "Tag: " + tag.Tag, HtmlClassMenuTextVisibility = System.Windows.Visibility.Visible, Index = tag.Index });
                else
                    treelist.Add(new HtmlItemViewModel(_model) { Title = tag.Tag, HtmlClassMenuText = "Class: " + tag.Class, HtmlElementMenuText = "Tag: " + tag.Tag, HtmlClassMenuTextVisibility = System.Windows.Visibility.Hidden, Index = tag.Index });
                ObservableRecursion(tag.Inner, treelist[treelist.Count - 1].Items);
            }
        }

        private void ObservationCheckAll(bool state,ObservableCollection<HtmlItemViewModel> treelist)
        {
            if (treelist == null || treelist.Count == 0) return;
            foreach(HtmlItemViewModel i in treelist)
            {
                i.HtmlClassCheck = state;
                i.HtmlElementCheck = state;
                ObservationCheckAll(state, i.Items);
            }
        }
    }
}
