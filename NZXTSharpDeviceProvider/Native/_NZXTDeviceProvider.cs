using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using System.Threading;
using System.IO;
using System.IO.Ports;
using System.Linq;

namespace RGB.NET.Devices.NZXT {
    public class _NZXTDeviceProvider { // TODO: Check performance, optimize if needed

        #region Fields and Properties

        private int _SDKVersion;

        private bool _ThrowExceptions;

        private SerialPort _serialPort;

        private List<_NZXTDeviceInfo> _Devices;

        private bool _Initialized = false;


        public int SDKVersion { get; }

        public bool ThrowExceptions { get; }

        public _NZXTDeviceInfo[] Devices { get => _Devices.ToArray(); }

        public bool Initialized { get; }
        #endregion

        #region Constructors

        public _NZXTDeviceProvider() {
            Initialize();
        }

        public _NZXTDeviceProvider(bool ThrowExceptions) {
            _ThrowExceptions = ThrowExceptions;
            Initialize();
        }

        #endregion

        #region Methods


        public void Initialize() {
            ScanForHuePlus();
            _Initialized = true;
        }

        private void ScanForHuePlus() 
        {
            // Create a new SerialPort object with default settings.
            _serialPort = new SerialPort("COM3", 256000, Parity.None, 8, StopBits.One) {
                WriteTimeout = 1000,
                ReadTimeout = 1000
            };

            // Open port
            try {
                _serialPort.Open();
            }
            catch (UnauthorizedAccessException e) {

                if (ThrowExceptions)
                    throw new UnauthorizedAccessException(
                        "UnauthorizedAccessException When Opening Serial Port. Ensure that CAM is not running and try again.");
            }
            catch (IOException e) {
                if (ThrowExceptions)
                    throw new IOException("SerialPort COM3 does not exist. Please be sure that your Hue+ is connected.");
            }
            catch (Exception e) {
                if (ThrowExceptions)
                    throw;
            }

            //Start connection
            if (_serialPort.IsOpen) {
                //Initial handshaking

                while (true) {
                    if (SerialWrite(new byte[1] { 0xc0 }, 1).FirstOrDefault() == 1) {
                        break;
                    }
                    Thread.Sleep(50); // TOTEST
                }

                // TODO
                // Do channel handshakes
                //     from received data, process into desired class

                byte[] handshake1 = SerialWrite(new byte[] { 0x8d, 0x01 }, 5);
                ProcessHuePlusChannelInfo(0x01, handshake1);

                byte[] handshake2 = SerialWrite(new byte[] { 0x8d, 0x02 }, 5);
                ProcessHuePlusChannelInfo(0x02, handshake2);
            } else { }
        }

        public bool Reload() {
            Dispose();
            Thread.Sleep(100);

            Initialize();
            return true;
        }


        public void Dispose() {
            _serialPort.Close();
        }

        private byte[] SerialWrite(byte[] buffer, int responselength) {
            /*_serialPort.DiscardInBuffer();
             THIS CREATES ERRORS FOR NOW
            _serialPort.DiscardOutBuffer();*/
            _serialPort.Write(buffer, 0, buffer.Length); //Second handshake
            Thread.Sleep(50);
            List<byte> reply = new List<byte>();
            for (int bytes = 0; bytes < responselength; bytes++)
                reply.Add(Convert.ToByte(_serialPort.ReadByte()));
            return reply.ToArray();
        }

        public void UpdateChannel(_NZXTLedColor color, _NZXTDeviceInfo info) {
            
        }

        private void ProcessHuePlusChannelInfo(int ChannelByte, byte[] bytes) {
            int NumSubDevices = bytes[4];
            NZXTDeviceType Type;

            switch (bytes[3]) {
                case 0x00:
                    Type = NZXTDeviceType.Strip;
                    break;
                case 0x01:
                    Type = NZXTDeviceType.Fan;
                    break;
                default:
                    Type = NZXTDeviceType.Unknown;
                    break;
            }

            for (int i = 0; i <= NumSubDevices; i++) {
                _Devices.Add(new _NZXTDeviceInfo(ChannelByte, Type));
            }
        }

        #endregion


    }

    public class Fixed  {
        private int _EffectByte = 0x00;
        private string _EffectName = "Fixed";
        public readonly List<string> CompatibleWith = new List<string>() { "HuePlus" };

        private _NZXTDeviceInfo info;
        private _NZXTLedColor color;

        public Fixed(_NZXTLedColor color, _NZXTDeviceInfo info) {
            this.color = color;
            this.info = info;
        }

        public bool IsCompatibleWith(string name) {
            return CompatibleWith.Contains(name) ? true : false;
        }

        public List<byte[]> BuildBytes() {
            byte[] SettingsBytes = new byte[] { 0x4b, (byte)info.ChannelByte, 0x00, 0x02, 0x03 };
            //byte[] final = SettingsBytes.ConcatenateByteArr(_Color.Expanded());
            return new List<byte[]>() { final };
        }
    }
}
