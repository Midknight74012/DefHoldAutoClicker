using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Navigation;
using WindowsInput;

namespace WpfApp1
{
    internal class ScrapTimer
    {
        public static int customerNum;
        public static string customerStr;
        //private static string today = DateTime.Today.ToString("MM/dd/yy").Replace("/", "");
        
        

        public static void ScrapProcess()
        {
            string today = DateTime.Now.ToString("MM/dd/yy").Replace("/", ""); //<------ Added as of 11/29/2022 @ 9:45 PM Central US Time
            Click c = new Click();
            InputSimulator isim = new InputSimulator();
            System.Drawing.Point p = new System.Drawing.Point();
            ///These XY coordinates is where the Defective Hold Screen is located on
            ///the Tellabs Gen. In the future, I'd like to make it where these can
            ///be changed in a settings menu
            p.X = 1805;
            p.Y = 224;
            c.leftClick(p);
            Thread.Sleep(300);

            ///These will scrap the two LPIDs. The Triplexer pallet will be 
            ///customerStr and today concatenated together. The non-triplexer
            ///pallet will be customerStr and "123456" concatenated together
            isim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.F4);
            Thread.Sleep(2000);
            for (int i = 0; i < customerNum; i++)
            {
                isim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.DOWN);
                Thread.Sleep(50);
            }
            isim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
            Thread.Sleep(2000);
            SendKeys.SendWait(customerStr + today);
            Thread.Sleep(2000);
            isim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
            Thread.Sleep(2000);
            SendKeys.SendWait("Y");
            Thread.Sleep(2000);
            isim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
            Thread.Sleep(2000);
            SendKeys.SendWait(customerStr + "123456");
            Thread.Sleep(2000);
            isim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
            Thread.Sleep(2000);
            SendKeys.SendWait("Y");
            Thread.Sleep(2000);
            isim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
            Thread.Sleep(2000);

            ///After scraping the two pallets, it will return the screen
            ///back to def-hold
            isim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.F4);
            Thread.Sleep(2000);
            isim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.DOWN);
            Thread.Sleep(50);
            isim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
            Thread.Sleep(2000);
            SendKeys.SendWait("1");
            Thread.Sleep(2000);
            isim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
            Thread.Sleep(2000);
            SendKeys.SendWait("none");
            Thread.Sleep(2000);
            isim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
            Thread.Sleep(300);

            ///This section will clear the triplexer pallet LPID to force
            ///the user to enter a new LPID for the day. They have to remember
            ///it is either "vzt" for verizon or "fnt" for frontier
            ///followed by today's date MMddYY with no "/" in it
            ///
            System.Drawing.Point t = new System.Drawing.Point();
            ///These are the coordinates I use to load units with an LPID
            ///that has the process of OOW-DH-TP
            t.X = 642;
            t.Y = 26;
            c.leftClick(t);
            Thread.Sleep(300);
            isim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.F3);
            Thread.Sleep(50);

        }

        public static System.Timers.Timer timer = new System.Timers.Timer(2000);
        public static String timeToCheck24 = "20:30";
        public static String timeToCheck12 = "08:30 PM";
        public static ManualResetEvent Wait = new ManualResetEvent(false);


        private static void timer1_elapsed(object  sender, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("elapsed");

            var timeNow = DateTime.Now.ToString("HH:mm");
            if (timeNow == timeToCheck24 || timeNow == timeToCheck12)
            {
                timer.Stop();
                ScrapProcess();
                timer = new System.Timers.Timer(60000);       // 1 minute
                timer.Elapsed += timer1_reset;
                timer.AutoReset = false;
                timer.Start();
            }
        }

        private static void timer1_reset(object  sender, System.Timers.ElapsedEventArgs e)
        {
            timer.Stop();
            TimerSetup();
        }

        public static void timer1_stop(object sender, System.Timers.ElapsedEventArgs e)
        {
            timer.Stop();
        }

        public static void TimerSetup()
        {
            timer = new System.Timers.Timer(2000);
            timer.Elapsed += timer1_elapsed;
            timer.AutoReset = true;
            timer.Start();
        }
    }
}
