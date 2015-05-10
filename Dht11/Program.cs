using System.Threading;
using CW.NETMF;
using CW.NETMF.Sensors;
using Microsoft.SPOT;
using SecretLabs.NETMF.Hardware.Netduino;

namespace Dht11
{
    public class Program
    {
        public static void Main()
        {
            // TODO: Change pins and pull-up resistor type according to the actual wiring
            var dhtSensor = new Dht11Sensor(Pins.GPIO_PIN_D0, Pins.GPIO_PIN_D1, PullUpResistor.Internal);

            while (true)
            {
                Thread.Sleep(2000);

                if (dhtSensor.Read())
                {
                    Debug.Print("DHT sensor Read() ok, RH = " + dhtSensor.Humidity.ToString("F1") + "%, Temp = " + dhtSensor.Temperature.ToString("F1") + "°C");
                }
                else
                {
                    Debug.Print("DHT sensor Read() failed");
                }
            }
        }

    }
}
