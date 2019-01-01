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
            byte[] commandBytes = effect.BuildBytes();
            WriteSerial(commandBytes, 0, commandBytes.Length);
        }

        public void ApplyCustom(byte[] Bytes) {
            WriteSerial(Bytes, 0, Bytes.Length);
        }

        public static class Params {
            public class CISS {

                private int colorIndex;
                private int speed;

                private int evaluatedIndex;

                public CISS(int colorIndex, int speed) {
                    this.colorIndex = colorIndex;
                    this.speed = speed;

                    this.evaluatedIndex = colorIndex * 2;
                }

                private void ValidateInput() {
                    if (speed > 4 || speed < 0)
                        throw new InvalidParamException("Invalid Param; Speed Must Be Between 0 and 4 (inclusive).");

                    if (colorIndex > 15 || colorIndex < 0)
                        throw new InvalidParamException("Invalid Param; ColorIndex Value Must Be Between 15 and 0 (inclusive).");
                }

                public int GetValue() {
                    return Convert.ToInt32(evaluatedIndex.DecimalToByte().ToString() + speed.ToString());
                }
            }

            class Direction {

                private bool withMovement;
                private bool isForward;

                public Direction(bool isForward, bool withMovement) {
                    this.withMovement = withMovement;
                    this.isForward = isForward;
                }

                public int GetValue() {
                    if (isForward)
                        if (withMovement)
                            return 0x0b; // Forward W/ movement
                        else
                            return 0x03; // Forward W/O movement
                    else
                        if (withMovement)
                        return 0x1b; // Backward W/ movement
                    else
                        return 0x13; // Backward W/O movement
                }

            }

            class LSS {

                private List<List<int>> LSSTable = new List<List<int>>() {
                    new List<int>() {0x00, 0x08, 0x10, 0x18},
                    new List<int>() {0x01, 0x09, 0x11, 0x19},
                    new List<int>() {0x02, 0x0a, 0x12, 0x1a},
                    new List<int>() {0x03, 0x0b, 0x13, 0x1b},
                    new List<int>() {0x04, 0x0c, 0x14, 0x1c}
                };

                private int speed;
                private int LEDSize;

                public LSS(int speed, int LEDSize) {
                    this.speed = speed;
                    this.LEDSize = LEDSize;
                }

                private void ValidateParams() {
                    if (speed > 4 || speed < 0)
                        throw new InvalidParamException("Invalid Param; Speed Must Be Between 0 and 4 (inclusive).");

                    if (LEDSize > 6 || LEDSize < 3)
                        throw new InvalidParamException("Invalid Param; LEDSize Must Be Between 3 and 6 (inclusive).");
                }

                public int GetValue() {
                    return LSSTable[speed][this.LEDSize - 3];
                }

            }
        }
    }
}

