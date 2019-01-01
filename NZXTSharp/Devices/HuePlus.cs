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

        private Channel _Both;
        private Channel _Channel1;
        private Channel _Channel2;

        public enum Mode {
            Fixed = 0x00,
            Fading = 0x01,
            Wave = 0x02,
            Marquee = 0x03,
            CoveringMarquee = 0x04,
            Alternating = 0x05,
            Pulse = 0x06,
            Breathing = 0x07,
            CandleLight = 0x09,
            Wings = 0x0c
        }

        public string Name { get; }
        public static SerialPort SerialPort { get; }
        public Channel Both { get; }
        public Channel Channel1 { get; }
        public Channel Channel2 { get; }

        public HuePlus() {
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
            }
            catch (Exception e) {
                // Logger.Severe(e);
            }

            //Start connection
            if (SerialPort.IsOpen) {
                //Initial handshaking
                while (true) {
                    if (serialmessage == 1) {
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
            this._Both = new Channel(0x00, this);
            this._Channel1 = new Channel(0x01, this);
            this._Channel2 = new Channel(0x02, this);
        }

        private void WriteSerial(byte[] buffer, int offset, int count) {
            if (SerialPort.IsOpen) {
                SerialPort.Write(buffer, offset, count);
            }
        }

        public bool IsInitialized() {
            return isInitialised;
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e) {
            int indata = SerialPort.ReadByte();
            serialmessage = indata;
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
            channel.Effect = effect;
            effect.Channel = channel;
            List<byte[]> commandBytes = effect.BuildBytes();
            foreach (byte[] command in commandBytes)
                WriteSerial(command, 0, command.Length);
        }

        public void ApplyCustom(byte[] Bytes) {
            WriteSerial(Bytes, 0, Bytes.Length);
        }
    }
}

