using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UsbLibrary;


namespace SadLibrary.Launcher
{
    public class LauncherCommand 
    { 
        public LauncherCommand(byte[] commandData, int durationMilliseconds)
        {
            cmdData = commandData;
            durationMs = durationMilliseconds;
        }
        public int durationMs { get; set; }    // How many seconds to delay when running this command.
        public byte[] cmdData { get; set; }               // The command send over USB.  ie: self.FIRE;
    }

    public class missileLauncher : ILauncher
    {
        protected double myPhi, myTheta;
        protected double degreeDelay = 20;
        public uint missileCount;
        public uint MAX_MISSILE_COUNT = 4;
        public string name = "";
        int HALF_CIRCLE = 180, FULL_CIRCLE = 360, MAX_UP = 30, MAX_DOWN = 8, MAX_LEFT = -135, MAX_RIGHT = 135;
        public bool m_Busy { get; private set; }
        private Queue<LauncherCommand> commandQueue;
        BackgroundWorker commandThread;

        private void AddCommandToQueue(LauncherCommand newCommand)
        {
            AddCommandsToQueue(new LauncherCommand[] { newCommand });
        }

        private void AddCommandsToQueue(LauncherCommand[] newCommands)
        {
            if (m_Busy) return;

            foreach(LauncherCommand cmd in newCommands) 
            {
                commandQueue.Enqueue(cmd);
            }
            processCommandQueue();
        }

        private void ClearCommandQueue()
        {
            commandQueue.Clear();
            m_Busy = false;
        }

        public void reload()
        {
            missileCount = MAX_MISSILE_COUNT;
        }
        public void moveUp()
        {
            command_Up((int)degreeDelay);
        }

        public void moveDown()
        {
            command_Down((int)degreeDelay);
        }

        public void moveLeft()
        {
            command_Left((int)degreeDelay);
        }

        public void moveRight()
        {
            command_Right((int)degreeDelay);
        }
        public void moveTheta(double theta)
        {
            if (theta < HALF_CIRCLE && theta > 0)
            {
                command_Right((int)((theta) * degreeDelay));
                myTheta += theta;
                if (myTheta > MAX_RIGHT)
                    myTheta = MAX_RIGHT;
            }
            else
            {
                if (theta <= 0)
                {
                    theta = theta * (-1);
                    command_Left((int)((theta) * degreeDelay));
                    myTheta -= theta;
                    if (myTheta < MAX_LEFT)
                        myTheta = MAX_LEFT;
                }
                else
                {
                    command_Left((int)((FULL_CIRCLE - theta) * degreeDelay));
                    myTheta -= FULL_CIRCLE - theta;
                    if (myTheta < MAX_LEFT)
                        myTheta = MAX_LEFT;
                }

            }
        }
        public void movePhi(double phi)
        {
            if (phi < HALF_CIRCLE && phi > 0)
            {
                command_Up((int)(phi * degreeDelay));
                myPhi += phi;
                if (myPhi > MAX_UP)
                    myPhi = MAX_UP;
            }
            else
            {
                if (phi <= 0)
                {
                    phi = phi * (-1);
                    command_Down((int)((phi) * degreeDelay));
                    myPhi -= phi;
                    if (myPhi < -MAX_DOWN)
                        myPhi = -MAX_DOWN;
                }
                else
                {
                    command_Down((int)((FULL_CIRCLE - phi) * degreeDelay));
                    myPhi -= FULL_CIRCLE - phi;
                    if (myPhi < -MAX_DOWN)
                        myPhi = -MAX_DOWN;
                }

            }
        }
        public void moveBy(double theta, double phi)
        {

            moveTheta(theta);
            movePhi(phi);
 

        }
        public void moveCoords(double x, double y, double z)
        {
            moveTo(toTheta(x, y), toPhi(x, y, z));
        }

        public void moveTo(double theta, double phi)
        {
            if (myTheta == 0 && myPhi == 0)
            {
                moveTheta(theta);
                movePhi(phi);
                
            }
            else
            {
                if (myTheta > HALF_CIRCLE)
                    theta = myTheta + theta;
                else
                    theta = theta - myTheta; 
                if(theta>FULL_CIRCLE)
                {
                    theta -= FULL_CIRCLE;
                }
                if (myPhi < HALF_CIRCLE)
                phi = phi - myPhi;
            else
                phi = phi + myPhi;
                if (phi > FULL_CIRCLE)
                {
                    phi -= FULL_CIRCLE;
                }
                moveTheta(theta);

                movePhi(phi);
            }

           
               
           
        }
        public void fire()
        {
            if (missileCount > 0)
            {
                command_Fire();
                --missileCount;
            }
            else
            {
                Console.WriteLine("I just can’t do it cap’tin, we just don’t have tha power");
            }
        }

        public void fireAt(double x, double y, double z)
        {
 //           m_Busy = true;
            moveCoords(x, y, z);
            fire();
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
            m_Busy = false;
            name = "Oblitzerater";

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

            commandQueue = new Queue<LauncherCommand>();
            commandThread = new BackgroundWorker();

            calibrate();
        }


        private void command_Stop()
        {
            this.SendUSBData(this.STOP);
        }


        private void command_Right(int duration)
        {

            AddCommandToQueue(new LauncherCommand(this.RIGHT, duration));
        }


        public void command_Left(int duration)
        {
            AddCommandToQueue(new LauncherCommand(this.LEFT, duration));
        }


        private void command_Up(int duration)
        {
            AddCommandToQueue(new LauncherCommand(this.UP, duration));
        }


        private void command_Down(int duration)
        {
            AddCommandToQueue(new LauncherCommand(this.DOWN, duration));
        }


        private void command_Fire()
        {
            AddCommandToQueue(new LauncherCommand(this.DOWN, 5000));
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
                AddCommandsToQueue(new LauncherCommand[] {
                    new LauncherCommand(this.LEFT, 5500),
                    new LauncherCommand(this.RIGHT, 2750), 
                    new LauncherCommand(this.UP, 2000),
                    new LauncherCommand(this.DOWN, 600) 
                });
                reload();
            }
        }


 //       private void moveMissileLauncher(byte[] Data, int interval)
        private void processCommandQueue()
        {
            if (DevicePresent)
            {

                // what to do in the background thread
                commandThread.DoWork += new DoWorkEventHandler(
                delegate(object o, DoWorkEventArgs args)
                {
                    BackgroundWorker b = o as BackgroundWorker;
                    while(commandQueue.Count() > 0)
                    {
                        m_Busy = true;
                        LauncherCommand cmd = commandQueue.Dequeue();
                        this.command_switchLED(true);
                        this.SendUSBData(cmd.cmdData);
                        Thread.Sleep(cmd.durationMs);
                    }                      
                });

                // what to do when worker completes its task (notify the user)
                commandThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                delegate(object o, RunWorkerCompletedEventArgs args)
                {
                    this.SendUSBData(this.STOP);
                    this.command_switchLED(false);
                    m_Busy = false;
                });

                m_Busy = true;
                commandThread.RunWorkerAsync();
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
            m_Busy = false;
            if (this.USB.ProductId == 0x1010)
            {
                this.command_switchLED(true);
            }
        }


        private void USB_OnSpecifiedDeviceRemoved(object sender, EventArgs e)
        {
            this.DevicePresent = false;
            m_Busy = false;
        }


        //Function to convert x, y to a theta for spherical coordinates.
        public double toTheta(double x, double y)
        {
            if (x >= 0)
                return (Math.Atan2(x, y) * (HALF_CIRCLE / Math.PI));
            else
                return (HALF_CIRCLE - (Math.Atan2(x, y) * (HALF_CIRCLE / Math.PI)));
        }

        //function to convert the x, y, z to phi for spherical coordinates.
        public double toPhi(double x, double y, double z)
        {
            double squaredRoot = Math.Sqrt((x * x) + (y * y));
            if (z >= 0)
                return (90 - (Math.Atan2(squaredRoot,z ) * (HALF_CIRCLE / Math.PI)));
            else
                return (90 - (HALF_CIRCLE -( Math.Atan2( squaredRoot,z) * (HALF_CIRCLE / Math.PI))));
        }



        public uint getMissleCount()
        {
            return missileCount;
        }
        public void setMissileCount(uint value)
        {
            missileCount = value;
        }

        public string getName()
        {
            return name;
        }

        public uint getMaxCount()
        {
            return MAX_MISSILE_COUNT;
        }
    }
}
