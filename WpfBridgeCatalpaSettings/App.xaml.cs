using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Windows;

namespace BridgeCatalpa
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
        }

        private void Application_Shutdown(object sender, ExitEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
