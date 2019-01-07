/*
    HuePlus.cs facilitates interaction with NZXT's Hue+ RGB Controller
    Copyright (C) 2018  Ari Madian

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.IO.Ports;

using NZXTSharp.Devices.Hue;
using NZXTSharp.Exceptions;
// ReSharper disable InconsistentNaming
// ReSharper disable UnassignedGetOnlyAutoProperty

namespace NZXTSharp.Devices 
{

    public delegate void LogHandler(string message);

    public delegate void DataRecieved(string message);

    public class HuePlus : IHueDevice 
    {

        private string _Name = "HuePlus";
        private bool isInitialised = false;
        private SerialPort _serialPort;
        private static int serialmessage;
        private string _CustomName = null;

        private bool WasLastSentHelloShake = false;
        private bool WasLastSentChannelShake = false;
        private Channel ChannelShakeChannel;

        private Channel _Both;
        private Channel _Channel1;
        private Channel _Channel2;
        private List<Channel> _Channels;

        public string Name { get; }
        public static SerialPort SerialPort { get; set; }
        public Channel Both { get; }
        public Channel Channel1 { get; }
        public Channel Channel2 { get; }
        public List<Channel> Channels { get; }
        public string CustomName { get; set; }
        

        public event LogHandler OnLogMessage;

        public event DataRecieved OnDataReceived;

        public HuePlus(bool manualStart = false) 
        {
            if (manualStart) return;
            
            SendLogEvent("Initializing HuePlus");
            Initialize();
            InitializeChannels();
            InitializeChannelInfo();
        }

        public HuePlus(string CustomName) 
        {
            this.CustomName = CustomName;
            SendLogEvent("Initializing HuePlus");
            Initialize();
            InitializeChannels();
            InitializeChannelInfo();
        }

        private bool Initialize() 
        {
            // Create a new SerialPort object with default settings.
            _serialPort = new SerialPort("COM3", 256000, Parity.None, 8, StopBits.One)
            {

                //Set the read/write timeouts
                WriteTimeout = 1000,
                ReadTimeout = 1000
            };

            SerialPort = _serialPort;

            //Set event handler
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            // Open port
            try
            {
                _serialPort.Open();
                SendLogEvent("Opened Serial Port");
            }
            catch (UnauthorizedAccessException e)
            {
                SendLogEvent(
                    "UnauthorizedAccessException When Opening Serial Port. Ensure that CAM is not running and try again.");
                throw new UnauthorizedAccessException(
                    "UnauthorizedAccessException When Opening Serial Port. Ensure that CAM is not running and try again.");
            } 
            catch (IOException e) 
            {
                SendLogEvent("SerialPort COM3 does not exist. Please be sure that your Hue+ is connected.");
                throw new IOException("SerialPort COM3 does not exist. Please be sure that your Hue+ is connected.");
            } 
            catch (Exception e) 
            {
                SendLogEvent("Exception Occurred When Opening Serial Port");
                throw;
            }

            //Start connection
            if (_serialPort.IsOpen) 
            {
                SendLogEvent("Initiating Handshake");
                //Initial handshaking

                while (true)
                {
                    if (serialmessage == 1)
                    {
                        SendLogEvent("Handshake Response Good");
                        WasLastSentHelloShake = false;
                        break;
                    }

                    _serialPort.Write(new byte[] {0xc0}, 0, 1); //Check if hue+ responds
                    Thread.Sleep(500);
                    WasLastSentHelloShake = true;

                }

                return true;
            }
            else { /*Logger.Error("Could not connect to serial port");*/ return false; }
        }

        private void InitializeChannels() 
        {
            SendLogEvent("Initializing Channels");
            this._Both = new Channel(0x00, this);
            this._Channel1 = new Channel(0x01, this);
            this._Channel2 = new Channel(0x02, this);
            this._Channels = new List<Channel>() { _Both, _Channel1, _Channel2};
        }

        private void InitializeChannelInfo()
        {
            UpdateChannelInfo(this._Both);
        }

        private static void WriteSerial(byte[] buffer, int offset, int count) 
        {
            if (SerialPort.IsOpen)
                SerialPort.Write(buffer, offset, count);
        }

        public bool IsInitialized() 
        {
            return isInitialised;
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            int readbyte;
            readbyte = _serialPort.ReadByte();
            if (WasLastSentHelloShake)
                serialmessage = readbyte;

            SendLogEvent("Response: " + readbyte);
        }

        public bool Reconnect(bool fromCold = false) 
        {
            if (!fromCold)
            {
                Dispose();
                Thread.Sleep(100);
            }

            Initialize();
            InitializeChannels();
            return true;
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public void Dispose() 
        {
            _serialPort.Close();
        }

        public void ApplyEffect(Channel channel, IEffect effect) 
        {
            if (!effect.IsCompatibleWith(_Name))
                throw new IncompatibleEffectException(_Name, effect.EffectName);

            SendLogEvent("Applying Effect: " + effect.EffectName);

            channel.Effect = effect;
            effect.Channel = channel;
            List<byte[]> commandBytes = effect.BuildBytes();
            foreach (byte[] command in commandBytes)
                WriteSerial(command, 0, command.Length);
        }

        public void ApplyCustom(byte[] Bytes) 
        {
            WriteSerial(Bytes, 0, Bytes.Length);
        }

        public void UnitLedOn() 
        {
            byte[] commandBytes = new byte[] { 0x46, 0x00, 0xc0, 0x00, 0x00, 0x00, 0xff};
            ApplyCustom(commandBytes);
        }

        public void UnitLedOff() 
        {
            byte[] commandBytes = new byte[] { 0x46, 0x00, 0xc0, 0x00, 0x00, 0xff, 0x00};
        }
        
        private void SendLogEvent(string Message) 
        {
            var baseString = "NZXTSharp HuePlus " + (this.CustomName ?? "") + " - ";
            OnLogMessage?.Invoke(baseString + Message);
        }

        private void SendDataRecvd(string Message)
        {
            var baseString = "NZXTSharp HuePlus " + (this.CustomName ?? "") + " - ";
            OnDataReceived?.Invoke(baseString + Message);
        }

        // TOTEST
        public void UpdateChannelInfo(Channel Channel) 
        {
            if (Channel == _Both) 
            {
                UpdateChannelInfoOp(_Channel1);
                UpdateChannelInfoOp(_Channel2);
            }
            else
            {
                UpdateChannelInfoOp(Channel);
            }
        }

        // TOTEST
        private void UpdateChannelInfoOp(Channel channel) 
        {
            _serialPort.DataReceived -= new SerialDataReceivedEventHandler(DataReceivedHandler);
            ClearBuffers();
            
            _serialPort.Write(new byte[] { 0x8d, (byte)channel }, 0, 2); //Second handshake
            Thread.Sleep(50);

            List<byte> reply = new List<byte>();
            for (int bytes = 0; bytes < 5; bytes++)
                reply.Add(Convert.ToByte(_serialPort.ReadByte()));

            channel.ChannelInfo = new ChannelInfo(channel, reply.ToArray());
        }

        public void ClearBuffers()
        {
            _serialPort.DiscardInBuffer();
            _serialPort.DiscardOutBuffer();
        }
    }
}

