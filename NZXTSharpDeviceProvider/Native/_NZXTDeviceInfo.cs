using System;

namespace RGB.NET.Devices.NZXT {
    public class _NZXTDeviceInfo {
        /// <summary>
        /// CUE-SDK: enum describing device type
        /// </summary>
        internal NZXTDeviceType type;

        internal int ChannelByte;

        internal string DeviceName;

        /// <summary>
        /// CUE-SDK: number of controllable LEDs on the device
        /// </summary>
        internal int ledsCount;

        internal string Model;

        public _NZXTDeviceInfo(int ChannelByte, NZXTDeviceType Type, string submodel = null) {
            this.ChannelByte = ChannelByte;
            this.type = Type;

            Console.WriteLine("\tFields Set");

            switch(this.type) {
                case NZXTDeviceType.Strip:
                    Console.WriteLine("\tStrip Set");
                    this.DeviceName = "NZXT Fan";
                    this.Model = "Strip";
                    this.ledsCount = 10;
                    break;
                case NZXTDeviceType.Fan:
                    Console.WriteLine("\tFan Set");
                    this.DeviceName = "NZXT Fan";
                    this.Model = "Fan";
                    this.ledsCount = 8;
                    break;
                case NZXTDeviceType.Cooler:
                    this.DeviceName = "NZXT Kraken";
                    this.Model = submodel;
                    this.ledsCount = 7;
                    break;
                case NZXTDeviceType.KrakenXLogo:
                    this.DeviceName = "NZXT KrakenX Logo";
                    this.ledsCount = 1;
                    break;
                case NZXTDeviceType.KrakenXRing:
                    this.DeviceName = "NZXT KrakenX Ring";
                    this.ledsCount = 7;
                    break;
            }
            Console.WriteLine("    Finished Initializing");
        }
    }
}
