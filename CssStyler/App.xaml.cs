using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CssStyler.ViewModel;
using CssStyler.Model;
using CssStyler.Persistance;

namespace CssStyler
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        MainWindow _mainwindow;
        DataLoad _dataload;
        CssViewModel _cssviewmodel;
        CssModel _model;


        public App()
        {
            Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            _mainwindow = new MainWindow();
            _dataload = new DataLoad();
            _model = new CssModel(_dataload);
            _cssviewmodel = new CssViewModel(_model);

            _mainwindow.DataContext = _cssviewmodel;

            _mainwindow.Show();
        }
    }
}
