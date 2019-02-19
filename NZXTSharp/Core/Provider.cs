using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using NZXTSharp.COM;
using HidLibrary;

using NZXTSharp.KrakenX;
using NZXTSharp.HuePlus;

namespace NZXTSharp
{
    public static class Provider
    {
        public static INZXTDevice[] GetDevices(DeviceLoadFilter Filter = DeviceLoadFilter.All)
        {
            int[] SupportedHIDIDs = new int[] { 0x170e };
            List<INZXTDevice> devices = new List<INZXTDevice>();

            devices.AddRange(TryGetHIDDevices(Filter));

            return devices.ToArray();
        }

        private static INZXTDevice[] TryGetHIDDevices(DeviceLoadFilter Filter)
        {
            List<HidDevice> found = DeviceEnumerator.EnumNZXTDevices().ToList();
            Console.WriteLine("foundLen - " + found.Count);

            INZXTDevice[] devices = InstantiateHIDDevices(found, Filter);
            Console.WriteLine("outLen - " + devices.Length);
            return devices;
        }

        private static INZXTDevice[] TryGetSerialDevices()
        {
            return new INZXTDevice[0];
        }

        private static INZXTDevice[] InstantiateHIDDevices(List<HidDevice> Devices, DeviceLoadFilter Filter)
        {
            int[] SupportedHIDIDs = new int[] { 0x170e };
            int[] filterIDs = MapFilterToSupportedIDs.Map(Filter);
            List<INZXTDevice> outDevices = new List<INZXTDevice>();
            foreach (HidDevice device in Devices)
            {
                Console.WriteLine("Checking Device");
                int ID = device.Attributes.ProductId;
                if (SupportedHIDIDs.Contains(ID))
                {
                    Console.WriteLine("    In Supported IDs");
                    if (filterIDs.Contains(ID))
                    {
                        Console.WriteLine("    Not Filtered");
                        Console.WriteLine("    Adding");
                        outDevices.Add(MapIDtoInstance.Map(ID));
                    }
                }
            }
            return outDevices.ToArray();
        }
    }

    internal class MapFilterToSupportedIDs
    {
        internal static int[] Map(DeviceLoadFilter Filter)
        {
            switch (Filter)
            {
                case DeviceLoadFilter.All:
                    return new int[]
                    {
                        0x0715, 0x170e, 0x1712, 0x1711, 0x2002,
                        0x2001, 0x2005, 0x1714, 0x1713
                    };
                case DeviceLoadFilter.Coolers:
                    return new int[]
                    {
                        0x1715, 0x170e, 0x1712
                    };
                case DeviceLoadFilter.FanControllers:
                    return new int[]
                    {
                        0x1711, 0x1714, 0x1713, 0x2005
                    };
                case DeviceLoadFilter.LightingControllers:
                    return new int[]
                    {
                        0x1715, 0x170e, 0x1712, 0x2002, 0x2001,
                        0x2005, 0x1714, 0x1713
                    };
                case DeviceLoadFilter.Grid:
                    return new int[]
                    {
                        0x1711
                    };
                case DeviceLoadFilter.Gridv3:
                    return new int[]
                    {
                        0x1711
                    };
                case DeviceLoadFilter.Hue:
                    return new int[]
                    {
                        3, 5,
                        0x2002,
                        0x2001
                    };
                case DeviceLoadFilter.Hue2: return new int[] { 0x2001 };
                case DeviceLoadFilter.HueAmbient: return new int[] { 0x2002 };
                case DeviceLoadFilter.HuePlus: return new int[] { 3, 5 };
                case DeviceLoadFilter.Kraken:
                    return new int[]
                    {
                        0x1715,
                        0x170e
                    };
                case DeviceLoadFilter.KrakenM: return new int[] { 0x1715 };
                case DeviceLoadFilter.KrakenX: return new int[] { 0x170e };
                default:
                    return new int[] { };
            }
        }
    }

    internal class MapIDtoInstance
    {
        internal static INZXTDevice Map(int ID)
        {
            switch (ID)
            {
                case 0x170e:
                    return new KrakenX.KrakenX();
                default:
                    throw new Exception(); // TODO
            }
        }
    }
}
