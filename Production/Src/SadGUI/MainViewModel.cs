﻿using SadLibrary.Launcher;
using SadLibrary.Targets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SadGUI;
using SadGUI.mizaWindows;

namespace SadGUI
{
    class MainViewModel: ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //private ILauncher m_launcher;
        private string m_name;
        private double m_xPosition;
        
        public MainViewModel(ILauncher launcher)
        {
            //TargetsViewModel = new TargetViewModel();
            GameStart = new GameStartViewModel();
            LauncherViewModel = LauncherViewModel.Instance;
            VideoCheckBox = new VideoCheckBox();
            LCVM = new LauncherConnectViewModel();
            DefaultControlCheckBox = new DefaultControlCheckBox();
            TwitterFeedCheckBox = TwitterFeedCheckBox.instance;
            ServerVM = new ServerViewModel();
            TWITTER = new TwitterViewModel();
            ServerCB = ServerCheckBox.instance;
            GSVM = new GameSelectionViewModel();
            //MEDIONE = Mediator.Instance;
            TWITTERIZER = Twitterizer.Instance;
            Twitterizer.Init(@"Resources/Twitterconfig.fig");
            TWITTEREXPERIMENTS = new TwitterExperiments();
            TVM = new TargetsViewModel();
//            imageProcessor = new ImageProcessor(); 
            TheStrategy = new Strategy();
            
        }
        public TargetsViewModel TVM { get; set; }
        public GameStartViewModel GameStart { get; set; }
        public GameSelectionViewModel GSVM { get; set; }
        public TargetViewModel TargetsViewModel { get; set; }
        public ServerCheckBox ServerCB { get; set; }
        public TwitterViewModel TWITTER { get; set; }
        public LauncherViewModel LauncherViewModel { get; set; }
        public VideoCheckBox VideoCheckBox { get; set; }
        public LauncherConnectViewModel LCVM { get; set; }
        public DefaultControlCheckBox DefaultControlCheckBox { get; set; }
        public TwitterFeedCheckBox TwitterFeedCheckBox { get; set; }
        public ServerViewModel ServerVM { get; set; }
        //public Mediator MEDIONE { get; set; }
        public Twitterizer TWITTERIZER { get; set;}
        public Strategy TheStrategy { get; set; }
//        public ImageProcessor imageProcessor { get; set; }

        public TwitterExperiments TWITTEREXPERIMENTS { get; set; }
    }
}
