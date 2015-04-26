using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;
using FusionWare.SPOT.Hardware;
using MicroLiquidCrystal;
using System.Diagnostics;

namespace LcdHelloWorld
{
    public class Program
    {
        public static void Main()
        {
            // Option 1: Use I2C provider. 
            // Default configuration coresponds to Adafruit's LCD backpack

            // initialize i2c bus (only one instance is allowed)
            //var bus = new I2CBus();

            // initialize provider (multiple devices can be attached to same bus)
            //var setup = new BaseShifterLcdTransferProvider.ShifterSetup
            //{
            //    RS = ShifterPin.GP1,
            //    RW = ShifterPin.None,
            //    Enable = ShifterPin.GP2,
            //    D4 = ShifterPin.GP3,
            //    D5 = ShifterPin.GP4,
            //    D6 = ShifterPin.GP5,
            //    D7 = ShifterPin.GP6,
            //    BL = ShifterPin.GP7
            //};
            //var lcdProvider = new MCP23008LcdTransferProvider(bus, 0x0, MCP23008LcdTransferProvider.DefaultSetup);

            /*
                         Option 2: Adafruit's LCD backup can also work in SIP mode.
                         this setup enabled this pinout.*/
            var lcdProvider = new Shifter74Hc595LcdTransferProvider(SPI_Devices.SPI1, Pins.GPIO_PIN_D10,
                Shifter74Hc595LcdTransferProvider.BitOrder.LSBFirst,
                new Shifter74Hc595LcdTransferProvider.ShifterSetup
                {
                    RS = ShifterPin.GP1,
                    RW = ShifterPin.None,
                    Enable = ShifterPin.GP2,
                    D4 = ShifterPin.GP3,
                    D5 = ShifterPin.GP4,
                    D6 = ShifterPin.GP5,
                    D7 = ShifterPin.GP6,
                    BL = ShifterPin.GP7
                });

            // create the LCD interface
            var lcd = new Lcd(lcdProvider);

            // set up the LCD's number of columns and rows: 
            lcd.Begin(20, 4);

            lcd.Backlight = !lcd.Backlight;

            Thread.Sleep(1000);

            lcd.Backlight = !lcd.Backlight;

            Thread.Sleep(1000);

            lcd.Home();
            lcd.Clear();

            // Print a message to the LCD.
            lcd.Write("test");

            //Stopwatch sw = Stopwatch.StartNew();

            while (true)
            {
                //sw.Start();

                // set the cursor to column 0, line 1
                //lcd.SetCursorPosition(0, 1);
                lcd.Home();
                lcd.Clear();

                // print the number of seconds since reset:
                lcd.Write((Utility.GetMachineTime().Ticks / 10000).ToString());

                //Debug.Print(sw.ElapsedMilliseconds.ToString());
                //sw.Reset();

                Thread.Sleep(500);

                //lcd.Backlight = !lcd.Backlight;

                //lcd.Visible = false;

                //Thread.Sleep(100);

                //lcd.Visible = true;

                // Thread.Sleep(100);
            }
        }

    }
}
