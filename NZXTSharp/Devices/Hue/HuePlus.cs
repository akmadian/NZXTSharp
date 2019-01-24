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

    /// <summary>
    /// Represents an NZXT Hue+ lighting controller.
    /// </summary>
    public class HuePlus : IHueDevice
    {
        #region Fields
        private string _Name = "HuePlus";
        private string _CustomName = null;
        private int _MaxHandshakeRetry = 5;

        private SerialController _COMController;

        private Channel _Both;
        private Channel _Channel1;
        private Channel _Channel2;
        private List<Channel> _Channels;
        #endregion

        #region Properties
        public string Name { get; }
        public Channel Both { get => _Both; }
        public Channel Channel1 { get => _Channel1; }
        public Channel Channel2 { get => _Channel2; }
        public List<Channel> Channels { get; }
        public string CustomName { get; set; }
        public NZXTDeviceType Type { get => NZXTDeviceType.HuePlus; }
        #endregion

        public event LogHandler OnLogMessage;

        public event DataRecieved OnDataReceived;

        /// <summary>
        /// Constructs a <see cref="HuePlus"/> instance.
        /// </summary>
        public HuePlus()
        {
            Initialize();
        }

        public HuePlus(int MaxHandshakeRetry)
        {
            if (MaxHandshakeRetry <= 0)
                throw new InvalidParamException("Invalid MaxHandshakeRetry may not be less than or equal to 0.");

            this._MaxHandshakeRetry = MaxHandshakeRetry;
            Initialize();
        }

        /// <summary>
        /// Constructs a <see cref="HuePlus"/> instance with a custom name <paramref name="CustomName"/>.
        /// </summary>
        /// <param name="CustomName"></param>
        public HuePlus(string CustomName = null)
        {
            this._CustomName = CustomName;
            Initialize();
        }

        private bool Initialize()
        {
            SerialCOMData data = new SerialCOMData(
                Parity.None,
                StopBits.One,
                1000,
                1000,
                256000,
                8,
                "HuePlus"
            );

            _COMController = new SerialController
            (
                SerialPort.GetPortNames(),
                data
            );
            
            if (_COMController.IsOpen)
            {
                int Retries = 0;

                while (true)
                {

                    if (_COMController.Write(new byte[1] {0xc0}, 1).FirstOrDefault() == 1)
                    {
                        SendLogEvent("Handshake Response Good");
                        break;
                    } else
                    {
                        Retries++;

                        if (Retries >= _MaxHandshakeRetry)
                            throw new MaxHandshakeRetryExceededException(_MaxHandshakeRetry);
                    }

                    Thread.Sleep(50);

                }

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

        /// <summary>
        /// Disposes of and reconnects to the device's <see cref="SerialController"/>.
        /// </summary>
        /// <returns></returns>
        public bool Reconnect()
        {
            _COMController.Dispose();

            Initialize();
            InitializeChannels();
            return true;
        }

        /// <summary>
        /// Disposes of the device's <see cref="SerialController"/>.
        /// </summary>
        // ReSharper disable once MemberCanBePrivate.Global
        public void Dispose()
        {
            _COMController.Dispose();
        }

        /// <summary>
        /// Applies the given <paramref name="effect"/> to the given <paramref name="channel"/>.
        /// </summary>
        /// <param name="channel">The <see cref="Channel"/> to apply the effect to.</param>
        /// <param name="effect">The <see cref="IEffect"/> to apply.</param>
        public void ApplyEffect(Channel channel, IEffect effect)
        {
            if (!effect.IsCompatibleWith(_Name))
                throw new IncompatibleEffectException(_Name, effect.EffectName);

            SendLogEvent("Applying Effect: " + effect.EffectName);

            //channel.Effect = effect;
            effect.Channel = channel;
            List<byte[]> commandBytes = effect.BuildBytes(channel);

            foreach (byte[] command in commandBytes)
                _COMController.WriteNoReponse(command);
        }

        /// <summary>
        /// Writes a custom <paramref name="Buffer"/> to the device's <see cref="SerialController"/>.
        /// </summary>
        /// <param name="Bytes"></param>
        public void ApplyCustom(byte[] Buffer)
        {
            _COMController.WriteNoReponse(Buffer);
        }

        /// <summary>
        /// Turns the device's unit led on.
        /// </summary>
        public void UnitLedOn()
        {
            byte[] commandBytes = new byte[] { 0x46, 0x00, 0xc0, 0x00, 0x00, 0x00, 0xff };
            ApplyCustom(commandBytes);
        }

        /// <summary>
        /// Turns the device's unit led off.
        /// </summary>
        public void UnitLedOff()
        {
            byte[] commandBytes = new byte[] { 0x46, 0x00, 0xc0, 0x00, 0x00, 0xff, 0x00 };
            ApplyCustom(commandBytes);
        }

        /// <summary>
        /// Sets the device's unit led state. true: on, false: off.
        /// </summary>
        /// <param name="State"></param>
        public void SetUnitLed(bool State)
        {
            if (State)
            {
                UnitLedOn();
            } else
            {
                UnitLedOff();
            }
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

        /// <summary>
        /// Updates the given <see cref="Channel"/>'s <see cref="ChannelInfo"/>.
        /// </summary>
        /// <param name="Channel"></param>
        // TOTEST
        public void UpdateChannelInfo(Channel Channel)
        {
            UpdateChannelInfoOp(this._Channel1);
            UpdateChannelInfoOp(this._Channel2);
        }

        // TOTEST
        private void UpdateChannelInfoOp(Channel channel)
        {
            _COMController.Port.DiscardInBuffer(); //This will have to be removed later
            _COMController.Port.DiscardOutBuffer(); //This will have to be removed later
            channel.SetChannelInfo(new ChannelInfo(channel, _COMController.Write(new byte[] { 0x8d, (byte)channel }, 5)));
        }
    }
}

