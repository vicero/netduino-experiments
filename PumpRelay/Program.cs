using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

using RelayDriver;

namespace PumpRelay
{
    public class Program
    {
        public static void Main()
        {
            const int runAtCount = 12000;

            var pump0 = new Relay(Pins.GPIO_PIN_D6, 10000);
            var pump1 = new Relay(Pins.GPIO_PIN_D7, 8000);
            var pump2 = new Relay(Pins.GPIO_PIN_D8, 5000);

            pump0.Run();
            pump1.Run();
            pump2.Run();

            Debug.Print("Pump 0 is " + (pump0.State ? "OFF" : "ON"));
            Debug.Print("Pump 1 is " + (pump1.State ? "OFF" : "ON"));
            Debug.Print("Pump 2 is " + (pump2.State ? "OFF" : "ON"));

            var stopWatch = Stopwatch.StartNew();

            while (true)
            {
                if (stopWatch.ElapsedMilliseconds > runAtCount)
                {
                    Debug.Print("Running Pumps");
                    stopWatch.Stop();
                    stopWatch.Reset();

                    pump0.Run();
                    pump1.Run();
                    pump2.Run();

                    stopWatch.Start();
                }

                Thread.Sleep(1000);
                Debug.Print("-------- " + stopWatch.ElapsedMilliseconds + "-----------");
                Debug.Print("Pump 0 is " + (pump0.State ? "OFF" : "ON"));
                Debug.Print("Pump 1 is " + (pump1.State ? "OFF" : "ON"));
                Debug.Print("Pump 2 is " + (pump2.State ? "OFF" : "ON"));
            }
        }

    }
}
