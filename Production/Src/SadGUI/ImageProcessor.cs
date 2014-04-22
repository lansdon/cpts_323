using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadGUI
{
    public class ImageProcessor
    {
        private HaarCascade haarCascade = new HaarCascade(@"haarcascade_frontalface_alt_tree.xml");

        public ImageProcessor() { }

        /*
         * This will take the image and overlay a HUD on top of it.
         * We also will possibly attempt to derive positional information
         *
         */
        public void ProcessImage(ref Image<Bgr, Byte> image)
        {
            DoTargetDetection(ref image);
        }

        /*
         * Image recognition
         */
        private void DoTargetDetection(ref Image<Bgr, Byte> image)
        {
            if (image != null)
            {
                Image<Gray, Byte> grayFrame = image.Convert<Gray, Byte>();

                var detectedFaces = grayFrame.DetectHaarCascade(haarCascade)[0];

                foreach (var face in detectedFaces)
                    image.Draw(face.rect, new Bgr(0, double.MaxValue, 0), 3);
             }
        }


    }
}
