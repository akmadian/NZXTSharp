using System;
using System.Collections.Generic;
using System.Threading;
using System.IO.Ports;

using NZXTSharp.Devices.Hue;
using NZXTSharp.Exceptions;

namespace NZXTSharp.Devices {
    public class HuePlus : IHueDevice {

        private string _Name = "HuePlus";
        private bool isInitialised = false;
        private SerialPort _serialPort;
        private static int NumOfLeds = 40;
        private static int serialmessage;
        private string _CustomName = null;

        private Channel _Both;
        private Channel _Channel1;
        private Channel _Channel2;

        public string Name { get; }
        public static SerialPort SerialPort { get; }
        public Channel Both { get; }
        public Channel Channel1 { get; }
        public Channel Channel2 { get; }
        public string CustomName { get; set; }

        public delegate void LogHandler(string message);

        public event LogHandler OnLogMessage;

        public HuePlus() {
            SendLogEvent("Initializing HuePlus");
            Initialize();
            InitializeChannels();
        }

        public HuePlus(string CustName) {
            this.CustomName = CustomName;
            SendLogEvent("Initializing HuePlus");
            Initialize();
            InitializeChannels();
        }

        private bool Initialize() {
            // Create a new SerialPort object with default settings.
            _serialPort = new SerialPort("COM3", 115200, Parity.None, 8, StopBits.One) {

                //Set the read/write timeouts
                WriteTimeout = 1000,
                ReadTimeout = 1000
            };

            //Set event handler
            SerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            // Open port
            try {
                SerialPort.Open();
                SendLogEvent("Opened Serial Port");
            }
            catch (Exception e) {
                SendLogEvent("Exception Occurred When Opening Serial Port");
                throw e;
            }

            //Start connection
            if (SerialPort.IsOpen) {
                SendLogEvent("Initiating Handshake");
                //Initial handshaking
                while (true) {
                    if (serialmessage == 1) {
                        SendLogEvent("Handshake Response Good");
                        break;
                    }
                    Thread.Sleep(500);
                    SerialPort.Write(new byte[] { 0xc0 }, 0, 1); //Check if hue+ responds

                }
                Thread.Sleep(100);
                SerialPort.Write(new byte[] { 0x8d, 0x01 }, 0, 2); //First handshake
                Thread.Sleep(100);
                SerialPort.Write(new byte[] { 0x8d, 0x02 }, 0, 2); //Second handshake

                Thread.Sleep(100);

                return true;
            }
            else { /*Logger.Error("Could not connect to serial port");*/ return false; }
        }

        private void InitializeChannels() {
            SendLogEvent("Initializing Channels");
            this._Both = new Channel(0x00, this);
            this._Channel1 = new Channel(0x01, this);
            this._Channel2 = new Channel(0x02, this);
        }

        private void WriteSerial(byte[] buffer, int offset, int count) {
            if (SerialPort.IsOpen)
                SerialPort.Write(buffer, offset, count);
            
        }

        public bool IsInitialized() {
            return isInitialised;
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e) {
            int indata = SerialPort.ReadByte();
            serialmessage = indata;
            SendLogEvent("Response: " + indata);
            //Logger.Debug("Devices", "NZXTDevice: " + String.Format("Data Received: {0}", indata));
        }

        public bool Reconnect() {
            Shutdown();
            Thread.Sleep(100);
            Initialize();
            return true;
        }

        public void Shutdown() {
            SerialPort.Close();
        }

        public void ApplyEffect(Channel channel, IEffect effect) {

            if (!effect.IsCompatibleWith(_Name))
                throw new IncompatibleEffectException(_Name, effect.EffectName);

            SendLogEvent("Applying Effect: " + effect.EffectName);

            channel.Effect = effect;
            effect.Channel = channel;
            List<byte[]> commandBytes = effect.BuildBytes();
            foreach (byte[] command in commandBytes)
                WriteSerial(command, 0, command.Length);
        }

        public void ApplyCustom(byte[] Bytes) {
            WriteSerial(Bytes, 0, Bytes.Length);
        }

        private void SendLogEvent(string Message) {
            string baseString = "NZXTSharp HuePlus " + (this.CustomName != null ? this.CustomName : "") + " - ";
            OnLogMessage(baseString + Message);
        }
    }
}

