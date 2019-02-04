using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using HidLibrary;

using NZXTSharp.Exceptions;

namespace NZXTSharp.COM {
    internal class USBController
    { 

        private NZXTDeviceType _Type;
        private SerialDeviceID _ID;
        private int CurrProductID;
        private SerialDeviceID _VendorID = SerialDeviceID.ManufacturerID;
        private HidReport _LastReport;
        private bool _IsAttached = false;
        private HidDevice _Device;

        public NZXTDeviceType Type { get => _Type; }
        public int CurrentProductID { get => CurrProductID; }
        public SerialDeviceID DeviceID { get => _ID; }
        public HidReport LastReport { get => _LastReport; }
        public bool IsAttached { get => IsAttached; }

        public USBController(NZXTDeviceType Type) {
            this._Type = Type;
            ResolveDeviceID();
            Console.WriteLine("DeviceID Resolved, Initializing...");
            Initialize();
            Console.WriteLine("Initialization Complete");
        }

        public USBController(HidDevice Device)
        {
            Initialize();
        }
        
        public void Initialize()
        {
            HidDevice _device = HidDevices.Enumerate((int)_VendorID, (int)_ID).FirstOrDefault();
            _Device = _device;
            _Device.OpenDevice();

            _Device.Inserted += DeviceAttachedHandler;
            _Device.Removed += DeviceRemovedHandler;

            _Device.ReadReport(OnReport);
        }

        internal void OnReport(HidReport Report)
        {
            this._LastReport = Report;
            _Device.ReadReport(OnReport);
        }

        public void Dispose()
        {
            _Device.CloseDevice();
        }

        public void Write(byte[] Buffer)
        {
            _Device.Write(Buffer);
        }

        internal void DeviceAttachedHandler()
        {
            this._IsAttached = true;
        }

        internal void DeviceRemovedHandler()
        {
            this._IsAttached = false;
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
