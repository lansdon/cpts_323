using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UsbLibrary;


namespace ConsoleLauncher
{
    class missileLauncher : ILauncher 
    {
        double myPhi, myTheta;
        double degreeDelay=19;
        public void moveUp()
        {
            command_Up(10);
        }

        public void moveDown()
        {
            command_Down(10);
        }

        public void moveLeft()
        {
            command_Left(10);
        }

        public void moveRight()
        {
            command_Right(10);
        }

        public void moveBy(double x, double y, double z)
        {
            moveTo(toTheta(x, y), toPhi(x, y, z));
        }

        public void moveTo(double theta, double phi)
        {
            if (myTheta == 0 && myPhi == 0)
            {
                if (theta < 180 && theta > 0)
                {
                    command_Right((int)((theta) * degreeDelay));
                    myTheta += theta;
                }
                else
                {
                    if (theta <= 0)
                    {
                        theta = theta * (-1);
                        command_Left((int)((theta) * degreeDelay));
                        myTheta -= theta;
                    }
                    else
                    {
                        command_Left((int)((360 - theta) * degreeDelay));
                        myTheta -= 360 - theta;
                    }
                    
                }
                if (phi < 180 && phi > 0)
                {
                    command_Up((int)(phi * degreeDelay));
                    myPhi += phi;
                    if (myPhi > 30)
                        myPhi = 30;
                }
                else if (phi > 300)
                {
                    if (phi <= 0)
                    {
                        phi = phi * (-1);
                        command_Down((int)((phi) * degreeDelay));
                        myPhi -= phi;
                        if (myPhi < -5)
                            myPhi = -5;
                    }
                    else
                    {
                        command_Down((int)((360 - phi) * degreeDelay));
                        myPhi -= 360 - phi;
                        if (myPhi < -5)
                            myPhi = -5;
                    }
                }
                
            }
            else
            {
                if (myTheta > 180)
                    theta = myTheta + theta;
                else
                    theta = theta - myTheta; 
                if(theta>360)
                {
                    theta -= 360;
                }
                if (myPhi < 180)
                phi = phi - myPhi;
            else
                phi = phi + myPhi;
                if (phi > 360)
                {
                    phi -= 360;
                }
                if (theta < 180 && theta > 0)
                {
                    command_Right((int)((theta) * degreeDelay));
                    myTheta += theta;
                }
                else
                {
                    if (theta <= 0)
                    {
                        theta = theta * (-1);
                        command_Left((int)((theta) * degreeDelay));
                        myTheta -= theta;
                    }
                    else
                    {
                        command_Left((int)((360 - theta) * degreeDelay));
                        myTheta -= 360 - theta;
                    }
                    
                }
               
                if (phi < 180 && phi > 0)
                {
                    command_Up((int)(phi * degreeDelay));
                    myPhi += phi;
                    if (myPhi > 30)
                        myPhi = 30;
                }
                else 
                {
                    if (phi <= 0)
                    {
                        phi = phi * (-1);
                        command_Down((int)((phi) * degreeDelay));
                        myPhi -= phi;
                        if (myPhi < -5)
                            myPhi = -5;
                    }
                    else
                    {
                        command_Down((int)((360 - phi) * degreeDelay));
                        myPhi -= 360 - phi;
                        if (myPhi < -5)
                            myPhi = -5;
                    }
                    
                }
            }

           
               
           
        }
        public void fire()
        {
            command_Fire();
        }

        public void fireAt(double x, double y, double z)
        {
            moveBy(x, y, z);
            command_Fire();
        }

        public void calibrate()
        {
            command_reset();
            myPhi = 0;
            myTheta = 0;
            
        }
        
        
        private bool DevicePresent;


        //Bytes used in command
        private byte[] UP;
        private byte[] RIGHT;
        private byte[] LEFT;
        private byte[] DOWN;


        private byte[] FIRE;
        private byte[] STOP;
        private byte[] LED_OFF;
        private byte[] LED_ON;


        private UsbHidPort USB;


        public missileLauncher()
        {


            this.UP = new byte[10];
            this.UP[1] = 2;
            this.UP[2] = 2;


            this.DOWN = new byte[10];
            this.DOWN[1] = 2;
            this.DOWN[2] = 1;


            this.LEFT = new byte[10];
            this.LEFT[1] = 2;
            this.LEFT[2] = 4;


            this.RIGHT = new byte[10];
            this.RIGHT[1] = 2;
            this.RIGHT[2] = 8;


            this.FIRE = new byte[10];
            this.FIRE[1] = 2;
            this.FIRE[2] = 0x10;


            this.STOP = new byte[10];
            this.STOP[1] = 2;
            this.STOP[2] = 0x20;


            this.LED_ON = new byte[9];
            this.LED_ON[1] = 3;
            this.LED_ON[2] = 1;


            this.LED_OFF = new byte[9];
            this.LED_OFF[1] = 3;


            this.USB = new UsbHidPort();
            this.USB.ProductId = 0;
            this.USB.SpecifiedDevice = null;
            this.USB.VendorId = 0;
            this.USB.OnSpecifiedDeviceRemoved += new EventHandler(this.USB_OnSpecifiedDeviceRemoved);
            this.USB.OnDataRecieved += new DataRecievedEventHandler(this.USB_OnDataRecieved);
            this.USB.OnSpecifiedDeviceArrived += new EventHandler(this.USB_OnSpecifiedDeviceArrived);


            
            this.USB.VID_List[0] = 0xa81;
            this.USB.PID_List[0] = 0x701;
            this.USB.VID_List[1] = 0x2123;
            this.USB.PID_List[1] = 0x1010;
            this.USB.ID_List_Cnt = 2;


            IntPtr handle = new IntPtr();
            this.USB.RegisterHandle(handle);
            calibrate();
        }


        private void command_Stop()
        {
            this.SendUSBData(this.STOP);
        }


        private void command_Right(int degrees)
        {
            this.moveMissileLauncher(this.RIGHT, degrees);
        }


        public void command_Left(int degrees)
        {
            this.moveMissileLauncher(this.LEFT, degrees);
        }


        private void command_Up(int degrees)
        {
            this.moveMissileLauncher(this.UP, degrees);
        }


        private void command_Down(int degrees)
        {
            this.moveMissileLauncher(this.DOWN, degrees);
        }


        private void command_Fire()
        {
            this.moveMissileLauncher(this.FIRE, 5000);
        }


        private void command_switchLED(Boolean turnOn)
        {
            if (DevicePresent)
            {
                if (turnOn)
                {
                    this.SendUSBData(this.LED_ON);
                }
                else
                {
                    this.SendUSBData(this.LED_OFF);
                }
                this.SendUSBData(this.STOP);
            }
        }




        private void command_reset()
        {
            if (DevicePresent)
            {
                
                this.moveMissileLauncher(this.LEFT, 5500);
                this.moveMissileLauncher(this.RIGHT, 2750);
                this.moveMissileLauncher(this.UP, 2000);
                this.moveMissileLauncher(this.DOWN, 600);
            }
        }


        private void moveMissileLauncher(byte[] Data, int interval)
        {
            if (DevicePresent)
            {
                this.command_switchLED(true);
                this.SendUSBData(Data);
                Thread.Sleep(interval);
                this.SendUSBData(this.STOP);
                this.command_switchLED(false);
            }
        }


        private void SendUSBData(byte[] Data)
        {
            if (this.USB.SpecifiedDevice != null)
            {
                this.USB.SpecifiedDevice.SendData(Data);
            }
       
        }

        


        private void USB_OnDataRecieved(object sender, DataRecievedEventArgs args)
        {
           
        }


        private void USB_OnSpecifiedDeviceArrived(object sender, EventArgs e)
        {
            this.DevicePresent = true;
            if (this.USB.ProductId == 0x1010)
            {
                this.command_switchLED(true);
            }
        }


        private void USB_OnSpecifiedDeviceRemoved(object sender, EventArgs e)
        {
            this.DevicePresent = false;
        }


        //Function to convert x, y to a theta for spherical coordinates.
        double toTheta(double x, double y)
        {
            if (x >= 0)
                return (Math.Atan2(x, y) * (180 / Math.PI));
            else
                return (180 - (Math.Atan2(x, y) * (180 / Math.PI)));
        }

        //function to convert the x, y, z to phi for spherical coordinates.
        double toPhi(double x, double y, double z)
        {
            double squaredRoot = Math.Sqrt((x * x) + (y * y));
            if (z >= 0)
                return (90 - (Math.Atan2(squaredRoot,z ) * (180 / Math.PI)));
            else
                return (90 - (180 -( Math.Atan2( squaredRoot,z) * (180 / Math.PI))));
        }
       
    }
}
