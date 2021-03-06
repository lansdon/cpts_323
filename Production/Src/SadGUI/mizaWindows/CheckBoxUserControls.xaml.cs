﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SadGUI.mizaWindows
{
    /// <summary>
    /// Interaction logic for CheckBoxUserControls.xaml
    /// </summary>
    public partial class CheckBoxUserControls : UserControl
    {
        public CheckBoxUserControls()
        {
            InitializeComponent();
            ContentController.AddContentControl("RightCheckBoxPanel", RightCheckBoxPanel);

            ContentController.SetContentToController(RightCheckBoxPanel, new DefaultUserControl());
        }
    }
}
