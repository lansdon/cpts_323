using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SadGUI;
using SadGUI.mizaWindows;

namespace SadGUI
{
    public class ContentControllerManager
    {
        private static ContentControllerManager instance = null;

        private ContentControllerManager()
        {
            // filler!!
        }

        public static ContentControllerManager Instance()
        {
            if (instance == null)
                instance = new ContentControllerManager();
            return instance;
        }

        public void SetContentToController(System.Windows.Controls.ContentControl ContentController, System.Windows.Controls.Control Control)
        {
            ContentController.Content = Control;
        }
    }
}
