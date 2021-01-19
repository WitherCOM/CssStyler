using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CssStyler.ViewModel;

namespace CssStyler
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        MainWindow _mainwindow;
        CssViewModel _cssviewmodel;


        public App()
        {
            Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            _mainwindow = new MainWindow();
            _cssviewmodel = new CssViewModel();

            _mainwindow.DataContext = _cssviewmodel;

            _mainwindow.Show();
        }
    }
}
