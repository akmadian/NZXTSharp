using System;
using System.Collections.Generic;
using System.Text;

using HidLibrary;
using NZXTSharp;

namespace NZXTSharp.COM
{
    /// <summary>
    /// Copied from https://github.com/DarkMio/Octopode with modifications.
    /// </summary>
    public class DeviceEnumerator
    {
        /// <summary>
        /// Enumerates all HID devices connected to the system.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> containing all found <see cref="HidDevice"/>s.</returns>
        public static IEnumerable<HidDevice> EnumAllDevices()
        {
            return HidDevices.Enumerate();
        }

        /// <summary>
        /// Enumerates all NZXT devices connected to the system.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> containing all found NZXT <see cref="HidDevice"/>s.</returns>
        public static IEnumerable<HidDevice> EnumNZXTDevices()
        {
            return HidDevices.Enumerate(0x1E71);
        }

        /// <summary>
        /// Enumerates all <see cref="KrakenX.KrakenX"/> devices connected to the system.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> containing all found
        /// <see cref="KrakenX.KrakenX"/> <see cref="HidDevice"/>s.</returns>
        public static IEnumerable<HidDevice> EnumKrakenXDevices()
        {
            foreach (var device in EnumNZXTDevices())
            {
                if (device.Attributes.ProductId == (int)HIDDeviceID.KrakenX)
                {
                    yield return device;
                }
            }
        }
    }
}
