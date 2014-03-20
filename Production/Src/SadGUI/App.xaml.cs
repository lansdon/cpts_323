using SadLibrary.Launcher;
using SadLibrary.Targets;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SadGUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void ApplicationStartup(object sender, StartupEventArgs e)
        {
            MainWindow window = new MainWindow();


            // Make targets!!
            var targets = new List<Target>();
            for (int i = 0; i < 4; i++)
            {
                var t = new Target();
                t.Name = "target " + i;
                t.X = i;

                targets.Add(t);
            }

            ILauncher launcher = new missileLauncher();

            MainViewModel viewModel = new MainViewModel(launcher, targets);
            window.DataContext = viewModel;
            window.ShowDialog();
        }
    }
}
