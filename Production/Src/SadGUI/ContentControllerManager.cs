using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SadGUI;
using SadGUI.mizaWindows;
using System.Windows.Controls;

namespace SadGUI
{
    public class ContentController
    {
        private static ContentController instance = null;
        private static Dictionary<string, ContentControl> contentDictionary = new Dictionary<string, ContentControl>();

        private ContentController()
        {
            // Filler!!
        }

        public static ContentController Instance()
        {
            if (instance == null)
                instance = new ContentController();
            return instance;
        }

        public static void SetContentToController(ContentControl _ContentController, Control _Control)
        {
            if (_ContentController != null && _Control != null)
                _ContentController.Content = _Control;
        }

        public static void SetContentToController(string Name, Control _Control)
        {
            ContentControl ContentController = getContentControl(Name);
            if (ContentController != null && _Control != null)
                ContentController.Content = _Control;
        }

        public static void AddContentControl(string Name, ContentControl _ContentController)
        {
            if (_ContentController != null)
                contentDictionary.Add(Name, _ContentController);
        }

        public static ContentControl getContentControl(string Name)
        {
            ContentControl Temp = null;
            contentDictionary.TryGetValue(Name, out Temp);
            return Temp;
        }
    }
}
