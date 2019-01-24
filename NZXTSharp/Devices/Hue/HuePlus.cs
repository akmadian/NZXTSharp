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
using System.Linq;

using NZXTSharp.Exceptions;
using NZXTSharp.Effects;

using NZXTSharp.COM;

// ReSharper disable InconsistentNaming
// ReSharper disable UnassignedGetOnlyAutoProperty

namespace NZXTSharp.Devices
{

    public delegate void LogHandler(string message);

    public delegate void DataRecieved(string message);

    public class HuePlus : IHueDevice
    {

        private string _Name = "HuePlus";
        private SerialPort _serialPort;
        private static int serialmessage;
        private string _CustomName = null;

        private bool WasLastSentHelloShake = false;
        private bool WasLastSentChannelShake = false;
        private Channel ChannelShakeChannel;

        private bool _IsComInitialized = false;
        private bool _AreChannelsInitialized = false;
        private bool _AreChannelInfosInitialized = false;

        private Channel _Both;
        private Channel _Channel1;
        private Channel _Channel2;
        private List<Channel> _Channels;

        public string Name { get; }
        public static SerialPort SerialPort { get; set; }
        public Channel Both
        {
            get => _Both;
        }
        public Channel Channel1 { get => _Channel1; }
        public Channel Channel2 { get => _Channel2; }
        public List<Channel> Channels { get; }
        public string CustomName { get; set; }
        public NZXTDeviceType Type { get => NZXTDeviceType.HuePlus; }

        public bool IsComInitialized
        {
            get => _IsComInitialized;
        }

        public bool AreChannelsInitialized => _AreChannelsInitialized;

        public bool AreChannelInfosInitialized => _AreChannelInfosInitialized;


        public event LogHandler OnLogMessage;

        public event DataRecieved OnDataReceived;


        public HuePlus(bool HoldInitialize = false, string CustomName = null)
        {
            this._CustomName = CustomName;
            
            if (!HoldInitialize)
                Initialize();
        }

        private bool Initialize()
        {
            SendLogEvent("Initializing HuePlus");
            // Create a new SerialPort object with default settings.
            _serialPort = new SerialPort("COM3", 256000, Parity.None, 8, StopBits.One)
            {
                WriteTimeout = 1000,
                ReadTimeout = 1000
            };

            SerialPort = _serialPort;

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
                    if (SerialWrite(new byte[1] {0xc0}, 1).FirstOrDefault() == 1)
                    {
                        SendLogEvent("Handshake Response Good");
                        WasLastSentHelloShake = false;
                        break;
                    }

                    Thread.Sleep(500);
                    WasLastSentHelloShake = true;

                }
                _IsComInitialized = true;

                InitializeChannels();
                InitializeChannelInfo();

                Channel1.BuildSubDevices();
                Channel2.BuildSubDevices();
                
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
            this._Channels = new List<Channel>() { _Both, _Channel1, _Channel2 };
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

            //channel.Effect = effect;
            effect.Channel = channel;
            List<byte[]> commandBytes = effect.BuildBytes(channel);
            foreach (byte[] command in commandBytes)
                WriteSerial(command, 0, command.Length);
        }

        public void ApplyCustom(byte[] Bytes)
        {
            WriteSerial(Bytes, 0, Bytes.Length);
        }

        public void UnitLedOn()
        {
            byte[] commandBytes = new byte[] { 0x46, 0x00, 0xc0, 0x00, 0x00, 0x00, 0xff };
            ApplyCustom(commandBytes);
        }

        public void UnitLedOff()
        {
            byte[] commandBytes = new byte[] { 0x46, 0x00, 0xc0, 0x00, 0x00, 0xff, 0x00 };
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
            UpdateChannelInfoOp(this._Channel1);
            UpdateChannelInfoOp(this._Channel2);
        }

        // TOTEST
        private void UpdateChannelInfoOp(Channel channel)
        {
            _serialPort.DiscardInBuffer(); //This will have to be removed later
            _serialPort.DiscardOutBuffer(); //This will have to be removed later
            channel.SetChannelInfo(new ChannelInfo(channel, SerialWrite(new byte[] { 0x8d, (byte)channel }, 5)));
        }

        private byte[] SerialWrite(byte[] buffer, int responselength)
        {
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

    }
}

