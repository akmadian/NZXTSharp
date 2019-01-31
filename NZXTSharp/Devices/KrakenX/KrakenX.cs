using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using HidLibrary;

using NZXTSharp.COM;
using NZXTSharp.Effects.Kraken;

namespace NZXTSharp.Devices.KrakenX
{
    public class KrakenX : IUSBDevice
    {

        private RGBChannel _Both;
        private RGBChannel _Logo;
        private RGBChannel _Ring;

        private USBController _COMController;

        public SerialDeviceID DeviceID { get => SerialDeviceID.KrakenX; }
        public NZXTDeviceType Type { get => NZXTDeviceType.KrakenX; }



        public KrakenX()
        {
            Initialize();
        }

        private void Initialize()
        {
            IEnumerable<HidDevice> KrakenXDevices = DeviceEnumerator.EnumKrakenXDevices();

            _COMController = new USBController(KrakenXDevices.First());
        }

        private void InitializeRGBChannels()
        {

        }

        private void InitializeDeviceInfo()
        {

        }

        internal void USBWrite()
        {

        }

        public void WriteCustom(byte[] Buffer)
        {
            _COMController.Write(Buffer);
        }

        public void ApplyEffect(RGBChannel Channel, IKrakenEffect Effect)
        {
            _COMController.Write(Effect.BuildBytes(Channel));
        }

        public int GetPumpSpeed()
        {
            return 0;
        }

        public void SetPumpSpeed(int Speed)
        {
            byte[] Buffer = new byte[] { 0x2, 0x4d };

            int channel = 0xc0;
            int minDuty = 50;
            int maxDuty = 100;



            //_COMController.Write();
        }

        public int GetFanSpeed()
        {
            return 0;
        }

        public void SetFanSpeed()
        {

        }

        public int GetLiquidTemp()
        {
            return 0;
        }

        // TODO : Set return signature
        public void GetFirmwareVersion()
        {

        }
    }
}
