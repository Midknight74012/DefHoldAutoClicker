using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using System.Drawing;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using WindowsInput;
using Application = System.Windows.Forms.Application;
using System.Runtime.CompilerServices;
using WpfApp1;

namespace WpfApp1
{

    public class Click
    {
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [Flags]
        public enum MouseEventFlags
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
        }

        public void leftClick(System.Drawing.Point p)
        {
            System.Windows.Forms.Cursor.Position = p;
            mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
            mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
        }

    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }


        /// <summary>
        /// This function is made to press the down arrow key 'num' times in order to reach
        /// the fail code in the Def-Hold screen of NetTerm. This function is called
        /// for each of the fails that has been checked on the MainWindow.xaml window
        /// </summary>
        /// <param name="num">
        /// The 'num' is the fail on how many times you must press the down arrow key
        /// on the keyboard to reach the specific fail.
        /// </param>
        public void DownArrow(int num)
        {
            InputSimulator isim = new InputSimulator();

            for (int i = 0; i < num; i++)
            {
                isim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.DOWN);
                Thread.Sleep(30);
            }
            isim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
            Thread.Sleep(50);
            isim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.F3);
            Thread.Sleep(50);
        }


        Click c = new Click();

        private void serialNumberTextbox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            ///This was put together so either the user or the scanner can send
            ///the Enter key, making this application much easier to use.
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                executeButton_Click(this, new RoutedEventArgs());
            }
        }


        private void executeButton_Click(object sender, RoutedEventArgs e)
        {
            bool triplexer = false;
            InputSimulator isim = new InputSimulator();
            System.Drawing.Point p = new System.Drawing.Point();
            ///These XY coordinates is where the Defective Hold Screen is located on
            ///the Tellabs Gen. In the future, I'd like to make it where these can
            ///be changed in a settings menu
            p.X = 1805;
            p.Y = 224;
            c.leftClick(p);

            ///After the Defective Hold Screen is selected from the process above
            ///this will enter the serial number in the textbox after a short delay
            Thread.Sleep(300);
            System.Windows.Forms.SendKeys.SendWait(serialNumberTextbox.Text);
            Thread.Sleep(100);
            isim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
            ///This first Return Key will have the Def-Hold Screen tell the user
            ///this unit is out of warranty
            Thread.Sleep(30);
            isim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
            ///Simply is to just to click something to continue after the warranty
            ///step right above
            Thread.Sleep(30);
            System.Windows.Forms.SendKeys.SendWait(benchTextBox.Text);
            ///After the steps above, you have to enter a Test Bench ID to the
            ///Def-Hold screen. For now, it is set to TLBSGEN by default but can
            ///be changed in the main window. ARRISGEN can also be used if the Arris
            ///Gen is being ran
            Thread.Sleep(100);
            isim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
            Thread.Sleep(30);


            ///All of the following steps will have the application select each
            ///fail in the Def-Hold screen if the box is checked for that
            ///specific fail.

            //This will select Power
            if ((bool)powerCheck.IsChecked)
            {
                DownArrow(17);
            }
            //This will select Estimated Video
            if ((bool)estVidCheck.IsChecked)
            {
                triplexer = true;
                DownArrow(2);
            }

            //This will select Optical Power
            if ((bool)opticalPowerCheck.IsChecked)
            {
                triplexer = true;
                DownArrow(12);
            }


            //This will select Pots
            if ((bool)potCheck.IsChecked)
            {
                DownArrow(16);
            }


            //This will select Ranging
            if ((bool)rangingCheck.IsChecked)
            {
                triplexer = true;
                DownArrow(20);
            }

            //This will select RF Power
            if ((bool)rfPowerCheck.IsChecked)
            {
                triplexer = true;
                DownArrow(18);
            }

            //This will select Ethernet
            if ((bool)ethernetCheck.IsChecked)
            {
                DownArrow(3);
            }

            //This will select MOCA
            if ((bool)mocaCheck.IsChecked)
            {
                DownArrow(7);
            }

            //This will select Battery Alarms
            if ((bool)alarmsCheck.IsChecked)
            {
                DownArrow(0);
            }


            isim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.F2);
            ///This first F2 key will have the Def-Hold screen Review the fails
            Thread.Sleep(30);
            isim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.F2);
            ///This second F2 key will save the data

            //This will select the Test Complete Screen and press the appropriate keys
            //to save the data of the fail
            System.Drawing.Point q = new System.Drawing.Point();
            q.X = 60;
            q.Y = 620;
            c.leftClick(q);
            Thread.Sleep(300);
            SendKeys.SendWait(serialNumberTextbox.Text);
            Thread.Sleep(100);
            isim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
            Thread.Sleep(30);
            isim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.F6);
            Thread.Sleep(100);

            ///This step will assign which of the two click points
            ///which will add the unit to an LPID depending on if bool triplexer
            ///is set to false or true.

            System.Drawing.Point t = new System.Drawing.Point();
            ///These are the coordinates I use to load units with an LPID
            ///that has the process of OOW-DH-TP
            t.X = 642;
            t.Y = 26;

            System.Drawing.Point x = new System.Drawing.Point();
            ///These are the coordinates I use to load units with an LPID
            ///that has the process of OOW-DEF-HOLD
            x.X = 1191;
            x.Y = 26;

            if (triplexer == true)
            {

                c.leftClick(t);

            }
            else
            {
                c.leftClick(x);
            }
            Thread.Sleep(300);
            SendKeys.SendWait(serialNumberTextbox.Text);
            Thread.Sleep(100);
            isim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
            Thread.Sleep(30);
            //This will go back to the mainwindow of the autoclicker with the serial
            //number highlighted. This will allow the user to scan the next
            //unit and run the application instead of having to click
            //the execute button each time.
            MainWindow1.Activate();
            serialNumberTextbox.SelectAll();


        }

        private void verizonButton_Checked(object sender, RoutedEventArgs e)
        {
            ScrapTimer.customerNum = 58;
            ScrapTimer.customerStr = "vzt";
        }

        private void frontierButton_Checked(object sender, RoutedEventArgs e)
        {
            ScrapTimer.customerNum = 21;
            ScrapTimer.customerStr = "fnt";
        }

        private void scrapRadio_Checked(object sender, RoutedEventArgs e)
        {
            ScrapTimer.TimerSetup();
        }

        private void noScrap_Checked(object sender, RoutedEventArgs e)
        {
            ScrapTimer.timer.Stop();
        }

        private void scrapButton_Click(object sender, RoutedEventArgs e)
        {
            ScrapTimer.ScrapProcess();
        }
    }
}
        
