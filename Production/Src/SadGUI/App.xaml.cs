using SadLibrary.Launcher;
using SadLibrary.Targets;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SadLibrary.FileLoader;

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
            LoadTargetsFromFile("targets.ini");

            ILauncher launcher = LauncherFactory.NewLauncher((LauncherType)1);

            MainViewModel viewModel = new MainViewModel(launcher, Target_Manager.Instance.Target_List);
            window.DataContext = viewModel;
            window.ShowDialog();
        }

        void LoadTargetsFromFile(string filename)
        {
            FileTargetLoader fLoader = FileLoaderFactory.GetFileLoader(SadLibrary.FileLoader.FileLoaderTypes.FILE_INI, filename);
            Target_Manager.Instance.Target_List = fLoader.Parse();
        }

    }
}
