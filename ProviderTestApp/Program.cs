using System;

using RGB.NET.Core;

using RGB.NET.Devices.NZXT;

namespace ProviderTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            RGBSurface surface = RGBSurface.Instance;
            NZXTDeviceProvider provider = new NZXTDeviceProvider();
            System.Threading.Thread.Sleep(15000);
            surface.LoadDevices(provider);

            int count = 0;
            
            foreach (IRGBDevice device in surface.Devices)
            {
                count++;
                Console.WriteLine(device);
            }
            Console.WriteLine(count);
            Console.ReadLine();
        }
    }
}
