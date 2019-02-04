using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using HidLibrary;

using NZXTSharp;
using NZXTSharp.COM;

namespace NZXTSharp.KrakenX
{
    public class KrakenX
    {

        private KrakenXChannel _Both;
        private KrakenXChannel _Logo;
        private KrakenXChannel _Ring;

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

        public void ApplyEffect(KrakenXChannel Channel, IEffect Effect)
        {
            List<byte[]> CommandQueue = Effect.BuildBytes(Type, Channel);
            foreach (byte[] Command in CommandQueue)
                _COMController.Write(Command);
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
