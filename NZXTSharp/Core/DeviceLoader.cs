using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading;

using HidLibrary;

using NZXTSharp.COM;
using NZXTSharp.KrakenX;
using NZXTSharp.HuePlus;
using NZXTSharp.Exceptions;

namespace NZXTSharp
{
    public class DeviceLoader
    {
        private static SerialCOMData HuePlusCOMData = new SerialCOMData(Parity.None, StopBits.One, 1000, 1000, 256000, 8);
        private INZXTDevice[] _Devices;

        /// <summary>
        /// Returns the first <see cref="KrakenX.KrakenX"/> instance owned 
        /// by the <see cref="DeviceLoader"/> if one exists.
        /// </summary>
        public KrakenX.KrakenX KrakenX { get => (KrakenX.KrakenX)FindDevice(NZXTDeviceType.KrakenX); }

        /// <summary>
        /// Returns the first <see cref="HuePlus.HuePlus"/> instance owned 
        /// by the <see cref="DeviceLoader"/> if one exists.
        /// </summary>
        public HuePlus.HuePlus HuePlus { get => (HuePlus.HuePlus)FindDevice(NZXTDeviceType.HuePlus); }

        /// <summary>
        /// All <see cref="INZXTDevice"/>s owned by the <see cref="DeviceLoader"/> object.
        /// </summary>
        public INZXTDevice[] Devices { get; }

        public int NumDevices { get => _Devices.Length; }

        public DeviceLoader(DeviceLoadFilter Filter = DeviceLoadFilter.All)
        {
            Initialize(Filter);
        }

        public DeviceLoader(bool InitializeDevices, DeviceLoadFilter Filter = DeviceLoadFilter.All)
        {
            if (InitializeDevices)
            {
                Initialize(Filter);
            }
        }

        public void Initialize(DeviceLoadFilter Filter = DeviceLoadFilter.All)
        {
            Console.WriteLine("Initing");
            _Devices = GetDevices(Filter);
        }

        /// <summary>
        /// Applies a given <see cref="IEffect"/> to all devices owned by the 
        /// <see cref="DeviceLoader"/> instance which have RGB capabilites.
        /// </summary>
        /// <param name="Effect">An <see cref="IEffect"/> to apply.</param>
        /// <param name="ThrowExceptions">Whether or not to throw exceptions, defaults to true.</param>
        public void ApplyEffectToDevices(IEffect Effect, bool ThrowExceptions = true)
        {
            foreach (INZXTDevice Device in this._Devices)
            {
                try
                {
                    Device.ApplyEffect(Effect);
                }
                catch (InvalidOperationException e) {}
                catch (IncompatibleEffectException e)
                {
                    if (ThrowExceptions)
                        throw new IncompatibleEffectException(
                            "DeviceLoader.ApplyEffectToDevices; Given effect incompatible with an owned device",
                            e
                        );
                }
            }
        }

        /// <summary>
        /// Adds an <see cref="INZXTDevice"/> instance to the <see cref="DeviceLoader"/>'s devices array.
        /// </summary>
        /// <param name="Device">The device to add.</param>
        public void AddDevice(INZXTDevice Device)
        {
            List<INZXTDevice> Devices;

            if (_Devices == null) Devices = new List<INZXTDevice>();
            else Devices = new List<INZXTDevice>(_Devices);

            Devices.Add(Device);

            _Devices = Devices.ToArray();
        }

        /// <summary>
        /// Removes the first occurrance of an <see cref="INZXTDevice"/> with the given <paramref name="Type"/>.
        /// </summary>
        /// <param name="Type">The <see cref="NZXTDeviceType"/> to remove.</param>
        public void RemoveDevice(NZXTDeviceType Type)
        {
            if (_Devices.Length == 0) return;
            List<INZXTDevice> Devices = new List<INZXTDevice>(_Devices);

            foreach (INZXTDevice Device in Devices)
            {
                if (Device.Type == Type)
                {
                    Devices.Remove(Device);
                    break;
                }
            }

            _Devices = Devices.ToArray();
        }

        /// <summary>
        /// Removes a given <see cref="INZXTDevice"/> from the <see cref="DeviceLoader"/> array.
        /// </summary>
        /// <param name="Device">The <see cref="INZXTDevice"/> to remove.</param>
        public void RemoveDevice(INZXTDevice Device)
        {
            if (_Devices.Length == 0) return;
            List<INZXTDevice> Devices = new List<INZXTDevice>(_Devices);

            foreach (INZXTDevice _Device in Devices)
            {
                if (_Device == Device)
                {
                    Devices.Remove(Device);
                    break;
                }
            }

            _Devices = Devices.ToArray();
        }

        /// <summary>
        /// Gets and returns all connected devices.
        /// </summary>
        /// <param name="Filter">A <see cref="DeviceLoadFilter"/>, returned devices will only include
        /// devices that fit into categories as defined by the filter.</param>
        /// <returns>An array of all NZXT devices connected to the system.</returns>
        public static INZXTDevice[] GetDevices(DeviceLoadFilter Filter = DeviceLoadFilter.All)
        {
            int[] SupportedHIDIDs = new int[] { 0x170e };
            List<INZXTDevice> devices = new List<INZXTDevice>();

            devices.AddRange(TryGetHIDDevices(Filter));
            devices.AddRange(TryGetSerialDevices(Filter));

            return devices.ToArray();
        }

        private static INZXTDevice[] TryGetHIDDevices(DeviceLoadFilter Filter)
        {
            List<HidDevice> found = DeviceEnumerator.EnumNZXTDevices().ToList();

            INZXTDevice[] devices = InstantiateHIDDevices(found, Filter);
            return devices;
        }

        private static INZXTDevice[] TryGetSerialDevices(DeviceLoadFilter Filter)
        {
            List<NZXTDeviceType> DevicesFound = new List<NZXTDeviceType>();
            
            SerialController HuePlusController = new SerialController(SerialPort.GetPortNames(), HuePlusCOMData);
            if (HuePlusController.IsOpen) // Try to open connection to Hue+
            {
                int Retries = 0;

                while (true)
                {
                    if (HuePlusController.Write(new byte[] { 0xc0}, 1).FirstOrDefault() == 1)
                    {
                        DevicesFound.Add(NZXTDeviceType.HuePlus);
                        break;
                    } else
                    {
                        Retries++;

                        Thread.Sleep(40);

                        if (Retries >= 5) break;
                    }
                }
                HuePlusController.Dispose();
            }

            return InstantiateSerialDevices(DevicesFound, Filter);
        }

        private static INZXTDevice[] InstantiateSerialDevices(List<NZXTDeviceType> Devices, DeviceLoadFilter Filter)
        {
            List<INZXTDevice> outDevices = new List<INZXTDevice>();
            int[] filterIDs = MapFilterToSupportedIDs.Map(Filter);

            foreach (NZXTDeviceType Type in Devices)
            {
                switch (Type)
                {
                    case NZXTDeviceType.HuePlus:
                        if (filterIDs.Contains(0x11111111))
                        {
                            outDevices.Add(new HuePlus.HuePlus());
                        }
                        break;
                    default:
                        break;
                }
            }

            return outDevices.ToArray();
        }

        private static INZXTDevice[] InstantiateHIDDevices(List<HidDevice> Devices, DeviceLoadFilter Filter)
        {
            int[] SupportedHIDIDs = new int[] { 0x170e };
            int[] filterIDs = MapFilterToSupportedIDs.Map(Filter);
            List<INZXTDevice> outDevices = new List<INZXTDevice>();
            foreach (HidDevice device in Devices)
            {
                int ID = device.Attributes.ProductId;
                if (SupportedHIDIDs.Contains(ID))
                {
                    if (filterIDs.Contains(ID))
                    {
                        outDevices.Add(MapIDtoInstance.Map(ID));
                    }
                }
            }
            return outDevices.ToArray();
        }

        internal INZXTDevice FindDevice(NZXTDeviceType Type)
        {
            foreach (INZXTDevice Device in _Devices)
                if (Device.Type == Type)
                    return Device;

            return null;
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
                        0x2001, 0x2005, 0x1714, 0x1713, 0x11111111
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
                        0x2005, 0x1714, 0x1713, 0x11111111
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
                        0x11111111, // HuePlus ID
                        0x2002,
                        0x2001
                    };
                case DeviceLoadFilter.Hue2: return new int[] { 0x2001 };
                case DeviceLoadFilter.HueAmbient: return new int[] { 0x2002 };
                case DeviceLoadFilter.HuePlus: return new int[] { 0x11111111 };
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
