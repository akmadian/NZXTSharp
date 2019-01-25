using System;
using System.Collections.Generic;
using System.Text;


using NZXTSharp.Devices;
using NZXTSharp.Exceptions;

namespace NZXTSharp.COM {
    internal class USBController : ICOMController {

        private NZXTDeviceType _Type;
        private SerialDeviceID _ID;

        public NZXTDeviceType Type { get => _Type; }
        public SerialDeviceID ID { get => _ID; }

        public USBController(NZXTDeviceType Type) {
            this._Type = Type;
            ResolveDeviceID();
        }

        public void Initialize()
        {

        }

        public void Write()
        {

        }

        private void ResolveDeviceID()
        {
            switch (_Type) // TODO: Find better way
            {
                // Kraken Devices
                case NZXTDeviceType.KrakenM:
                    this._ID = SerialDeviceID.KrakenM;
                    break;
                case NZXTDeviceType.KrakenM22:
                    this._ID = SerialDeviceID.KrakenM;
                    break;
                case NZXTDeviceType.KrakenX:
                    this._ID = SerialDeviceID.KrakenX;
                    break;
                case NZXTDeviceType.KrakenX42:
                    this._ID = SerialDeviceID.KrakenX;
                    break;
                case NZXTDeviceType.KrakenX52:
                    this._ID = SerialDeviceID.KrakenX;
                    break;
                case NZXTDeviceType.KrakenX62:
                    this._ID = SerialDeviceID.KrakenX;
                    break;
                case NZXTDeviceType.KrakenX72:
                    this._ID = SerialDeviceID.KrakenX;
                    break;

                // Hue Devices
                case NZXTDeviceType.Hue2:
                    this._ID = SerialDeviceID.Hue2;
                    break;
                case NZXTDeviceType.HueAmbient:
                    this._ID = SerialDeviceID.HueAmbient;
                    break;

                // Grid Devices
                case NZXTDeviceType.GridV3:
                    this._ID = SerialDeviceID.GridV3;
                    break;

                // Motherboards
                case NZXTDeviceType.N7:
                    this._ID = SerialDeviceID.N7;
                    break;
                case NZXTDeviceType.N7_Z390:
                    this._ID = SerialDeviceID.N7_Z390;
                    break;

                // Misc
                case NZXTDeviceType.H7Lumi:
                    this._ID = SerialDeviceID.H7Lumi;
                    break;
                case NZXTDeviceType.SmartDevice:
                    this._ID = SerialDeviceID.SmartDevice;
                    break;

                default:
                    throw new IncompatibleDeviceTypeException();
            }
        }

        public string[] ScanForDevices()
        {
            // TODO
            return new[] { "" };
        }
    }
}
