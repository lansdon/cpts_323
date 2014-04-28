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
        private HaarCascade foeHaarCascade = new HaarCascade(@"foe_haarcascade.xml");
        private HaarCascade friendHaarCascade = new HaarCascade(@"friend_haarcascade.xml");

        private bool busy = false;

        // AUTOMATIC VALUES - These are gathered internally
        private int imageHeight = 0;    // Used for frequent calculations
        private int imageWidth = 0;     // Used for frequent calculations

        // INPUT VALUES - From GUI (used to tune)
        private int horizon = 300;              // Where the back edge of the gameboard is on screen
        private int floor = 0;                  // Where the front edge of the board is on screen.
        private int gridRows = 10;              // TO DO WE NEED TO KNOW THESE VALUES!!
        private int gridColumns = 10;           // TO DO WE NEED TO KNOW THESE VALUES!!
        private int gridLayers = 1;             // TO DO WE NEED TO KNOW THESE VALUES!!
        private int firstVisibleGridRow = 1;    // Which row in the grid is visible at floor.

        // CALCULATED VALUES - These are used after input is taken and used
        private int rowHeight = 0;      // This is calculated once and used to find position.
        private int colWidth = 0;      // This is calculated once and used to find position.
        private bool setupComplete = false;
        int visibleGridRows = 0;        // The number of rows we will be referring to.

        // HARDCODED SETTINGS
        private double BOARD_Y_MAX = 48.0;             // Playing field Y length in inches.
        private double BOARD_X_MAX = 24.0;             // Playing field Y length in inches.
        private double Y_RADIUS_MAX_DISTANCE = 50.0;   // RADIUS of target when it's furthest away from camera
        private double Y_RADIUS_MIN_DISTANCE = 250.0;  // RADIUS of target when it's closest to camera
        private double Y_PIXEL_DELTA_TO_INCHES_MULT;   // Multiply target pixel delta by this num to get Y in inches.
        private double X_MAX = 20;                     // This is the max value X can have when y=BOARD_Y_MAX as seen by camera

        // Text Overlay
        Font coordFont = new Font("Arial", 24);

        
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

            configureY();

            createGrid();

            setupComplete = true;
       }

        /*
         * This will determine the scale for targets size as it relates to distance from camera
         */
        private void configureY()
        {
            // Need to know the difference in min/max target sizes (in pixels)
            double targetPixelDelta = Y_RADIUS_MIN_DISTANCE - Y_RADIUS_MAX_DISTANCE;
            // Find the ratio for inches to pixelDelta
            Y_PIXEL_DELTA_TO_INCHES_MULT = BOARD_Y_MAX / targetPixelDelta;
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

        private void DrawPositionText(ref Image<Bgr, Byte> image, Rectangle targetFrame, Point3D pos)
        {
            Graphics drawing = Graphics.FromImage(image.Bitmap);

            if (pos.Y < 1) pos.Y = 1.0;
    
            var vector2 = pos;
            var vector1 = new Point(0, 1);  // 12 o'clock == 0°, assuming that y goes from bottom to to
            double angleInRadians = -(Math.Atan2(vector2.Y, vector2.X) - Math.Atan2(vector1.Y, vector1.X));
            double angleInDegrees = angleInRadians * 180.0 / Math.PI;

            String posText = String.Format("({0},{1},{2}) {3}deg", (int)pos.X, (int)pos.Y, (int)pos.Z, (int)(angleInDegrees));

            //create a brush for the text
            Brush textBrush = new SolidBrush(Color.Yellow);

            drawing.DrawString(posText, coordFont, textBrush, targetFrame.X + 20, targetFrame.Y-20);

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

        }

        /*
         * Image detection
         */
        private void DoTargetDetection(ref Image<Bgr, Byte> image)
        {
            if (image != null)
            {
                Image<Gray, Byte> grayFrame = image.Convert<Gray, Byte>();
                Point3D pos;

                // TEMPORARY - FACES
                //var detectedFaces = grayFrame.DetectHaarCascade(haarCascade)[0];
                //foreach (var face in detectedFaces)
                //{
                //    pos = PositionFromFrame(face.rect);
                //    image.Draw(face.rect, new Bgr(double.MaxValue, double.MaxValue, 0), 3);
                //    DrawPositionText(ref image, face.rect, pos);
                //}

                // FOES
                //var detectedFoes = grayFrame.DetectHaarCascade(foeHaarCascade)[0];
                //foreach (var foe in detectedFoes)
                //{
                //    pos = PositionFromFrame(foe.rect);
                //    image.Draw(foe.rect, new Bgr(0, 0, double.MaxValue), 3);
                //    DrawPositionText(ref image, foe.rect, pos);
                //}

                //// FRIENDS
                //var detectedFriends = grayFrame.DetectHaarCascade(friendHaarCascade)[0];
                //foreach (var friend in detectedFriends)
                //{
                //    pos = PositionFromFrame(friend.rect);
                //    image.Draw(friend.rect, new Bgr(0, double.MaxValue, 0), 3);
                //    DrawPositionText(ref image, friend.rect, pos);
                //}

                // FOES
                Image<Bgr, byte> template = new Image<Bgr, byte>("foe1.png"); // 
                Image<Bgr, byte> imageToShow = image.Copy();

                int sourceWidth = image.Size.Width;
                int templateWidth = template.Size.Width;

                using (Image<Gray, float> result = image.MatchTemplate(template, Emgu.CV.CvEnum.TM_TYPE.CV_TM_CCOEFF_NORMED))
                {

                    // after your call to MatchTemplate
                    float threshold = 0.08f;
                    MCvMat thresholdedImage;
                    result.thresthreshold(result, thresholdedImage, threshold, 255, CV_THRESH_BINARY);
                    // the above will set pixels to 0 in thresholdedImage if their value in result is lower than the threshold, to 255 if it is larger.
                    // in C++ it could also be written cv::Mat thresholdedImage = result < threshold;
                    // Now loop over pixels of thresholdedImage, and draw your matches
                    for (int r = 0; r < thresholdedImage.rows; ++r) {
                      for (int c = 0; c < thresholdedImage.cols; ++c) {
                        if (!thresholdedImage.at<unsigned char>(r, c)) // = thresholdedImage(r,c) == 0
                          cv::circle(sourceColor, cv::Point(c, r), template.cols/2, CV_RGB(0,255,0), 1);
                      }
                    }


                    double[] minValues, maxValues;
                    Point[] minLocations, maxLocations;
                    result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);
           
                    // You can try different values of the threshold. I guess somewhere between 0.75 and 0.95 would be good.
                    for (int i = 0; i < maxValues.Count(); ++i)
                    {
                        if (maxValues[i] > 0.50)
                        {
                            // This is a match
                            Rectangle match = new Rectangle(maxLocations[i], template.Size);
                            pos = PositionFromFrame(match);
                            image.Draw(match, new Bgr(Color.Red), 3);
                            DrawPositionText(ref image, match, pos);
                        }
                    }
                }
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
        private Point3D PositionFromFrame(Rectangle targetFrame)
        {
            Point3D position = new Point3D();
            position.Y = getYCoord(targetFrame);
            position.X = getXCoord(targetFrame, position.Y);
            position.Z = getZCoord(position.Y);

            return position;
        }


        private double getXCoord(Rectangle targetFrame, double y)
        {
            double center = imageWidth / 2.0;
            double x = targetFrame.X -center + targetFrame.Width / 2.0;
 //           double xMax = Math.Sqrt(y*y + )

            return x;
        }

        /*
         * Find the target radius delta (difference between max size and actual size)
         * Use that delta with pre-calculated multiplier to determine the y value in inches.
         */
        private double getYCoord(Rectangle targetFrame)
        {
            double targetRadiusDelta = Y_RADIUS_MIN_DISTANCE - targetFrame.Size.Width / 2.0;
            if(targetRadiusDelta < 0) targetRadiusDelta = Y_RADIUS_MIN_DISTANCE;
            if(targetRadiusDelta > Y_RADIUS_MIN_DISTANCE-Y_RADIUS_MAX_DISTANCE) targetRadiusDelta =  Y_RADIUS_MIN_DISTANCE-Y_RADIUS_MAX_DISTANCE;
            double y = targetRadiusDelta * Y_PIXEL_DELTA_TO_INCHES_MULT;
            Console.WriteLine("Calculated Y = {0}", y);
            return y;
        }
        private double getZCoord(double y)
        {
            // if the target is > 75% of max distance, angle up slightly.
            if (y > BOARD_Y_MAX * 0.75)
                return 1.0;
            return 0.0;
        }

    }
}
