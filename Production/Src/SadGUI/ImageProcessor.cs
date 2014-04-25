using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;


namespace SadGUI
{


    public class ImageProcessor
    {
        private HaarCascade haarCascade = new HaarCascade(@"haarcascade_frontalface_alt_tree.xml");
        private bool busy = false;

        // AUTOMATIC VALUES - These are gathered internally
        private int imageHeight = 0;    // Used for frequent calculations
        private int imageWidth = 0;     // Used for frequent calculations

        // INPUT VALUES - From GUI (used to tune)
        private int horizon = 300;    // Where the back edge of the gameboard is on screen
        private int floor = 0;      // Where the front edge of the board is on screen.
        private int gridRows = 10;      // TO DO WE NEED TO KNOW THESE VALUES!!
        private int gridColumns = 10;   // TO DO WE NEED TO KNOW THESE VALUES!!
        private int gridLayers = 1;      // TO DO WE NEED TO KNOW THESE VALUES!!
        private int firstVisibleGridRow = 1;    // Which row in the grid is visible at floor.

        // CALCULATED VALUES - These are used after input is taken and used
        private int rowHeight = 0;      // This is calculated once and used to find position.
        private int colWidth = 0;      // This is calculated once and used to find position.
        private bool setupComplete = false;
        int visibleGridRows = 0;        // The number of rows we will be referring to.

        public ImageProcessor() { }

        /*
         * This will take the image and overlay a HUD on top of it.
         * We also will possibly attempt to derive positional information
         *
         */

        private void init(ref Image<Bgr, Byte> image)
        {
            imageHeight = image.Height;
            imageWidth = image.Width;

            // Create the grid
            createGrid();

            setupComplete = true;
       }

        /*
         * We're going to calculate the grid that pixels are mapped on to.
         * Do this once during initialization to save cycles
         */
        private void createGrid() 
        {
            floor = imageHeight;
            visibleGridRows = (gridRows >= firstVisibleGridRow ?  gridRows - firstVisibleGridRow + 1 : 1); // > 0 !!
            int boardHeight = (horizon <= floor ?  floor - horizon : 0);
            rowHeight = boardHeight / visibleGridRows;

            colWidth = imageWidth / gridColumns;
        }

        /*
         * MAIN ENTRYPOINT
         *  This is the primary function used to process an image.
         *  This will perform a series of processes on the image
         *  and prepare it to be displayed. This includes extracting 
         *  target positioning data from the image.
         */
        public void ProcessImage(ref Image<Bgr, Byte> image)
        {
            // This will throttle the incoming images
            if (busy) return;
            busy = true;

            // One time setting of height/width. 
            // This assumes the size will not change!!
            //if(!setupComplete) 
                init(ref image);

            // Sequential Processing here
            DoTargetDetection(ref image);
            DrawGrid(ref image);

            busy = false;
        }

        /*
         * Overlays the grid onto the image.
         */
        private void DrawGrid(ref Image<Bgr, Byte> image)
        {
            int remainingHeight = floor - horizon;
            for (int i = 0; i <= visibleGridRows; ++i )
            {
                int rowHeight = (int)((remainingHeight / (visibleGridRows - i + 1)) * ((double)i / visibleGridRows));
                int y = floor - remainingHeight + rowHeight;
                image.Draw(new LineSegment2D(new Point(0, y), new Point(imageWidth, y)), new Bgr(50, 255, 50), 1);
                remainingHeight -= rowHeight;
            }
            for (int i = 0; i <= gridColumns; ++i)
            {
                int x = (i * colWidth);
                image.Draw(new LineSegment2D(new Point((2 * x) - (imageWidth / 2), floor), new Point(x, horizon)), new Bgr(50, 255, 50), 1);
            }

        }

        /*
         * Image detection
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


        /*
         * This function will try to estimate the grid location of a target based on it's frame size
         * and position inside the image frame
         * 
         * - Distance
         *   Based on the area below the horizon, we will use the height divided into
         *   a grid based on the number of cells. 
         * 
         */
        private Point3D PositionFromFrame(ref Rectangle targetFrame, ref Rectangle imageFrame)
        {
            Point3D position = new Point3D();

            // TODO - Calculate position
            double center = imageWidth / 2.0;



            return position;
        }


        private double getXCoord(ref Rectangle targetFrame)
        {
            double center = imageWidth / 2.0;


            return 0.0;
        }
        private double getYCoord(ref Rectangle targetFrame, ref Rectangle imageFrame)
        {
            return 0.0;
        }
        private double getZCoord(ref Rectangle targetFrame, ref Rectangle imageFrame)
        {
            return 0.0;
        }

    }
}
