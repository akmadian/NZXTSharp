using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using NZXTSharp.Devices;
using NZXTSharp.Effects;

namespace NZXTSharp.Devices {
    
    /// <summary>
    /// Represents a channel on an NZXT device.
    /// </summary>
    public class Channel {
        
        private readonly int _ChannelByte;
        private IEffect _Effect = new Fixed(new Color(255, 255, 255));
        private IHueDevice _Parent;
        private bool _State = true;
        private ChannelInfo _ChannelInfo;
        #pragma warning disable IDE0044 // Add readonly modifier
        private List<ISubDevice> _SubDevices = new List<ISubDevice>();
        #pragma warning restore IDE0044 // Add readonly modifier

        #region Properties
        /// <summary>
        /// The channelbyte of the <see cref="Channel"/>.
        /// </summary>
        public int ChannelByte { get; }

        /// <summary>
        /// The <see cref="IEffect"/> currently applied to the <see cref="Channel"/>.
        /// </summary>
        public IEffect Effect { get => _Effect; }

        /// <summary>
        /// Whether or not the current <see cref="Channel"/> is active (on).
        /// </summary>
        public bool State { get => _State; }
        
        /// <summary>
        /// The <see cref="Channel"/>'s <see cref="ChannelInfo"/> object.
        /// </summary>
        public ChannelInfo ChannelInfo { get => _ChannelInfo; }

        /// <summary>
        /// The device that owns the <see cref="Channel"/>.
        /// </summary>
        public IHueDevice Parent { get => _Parent; }

        /// <summary>
        /// A list of <see cref="ISubDevice"/>s owned by the <see cref="Channel"/>.
        /// </summary>
        public List<ISubDevice> SubDevices { get => _SubDevices; }
        #endregion
        
        /// <summary>
        /// Constructs a <see cref="Channel"/> object with a given <paramref name="ChannelByte"/>.
        /// </summary>
        /// <param name="ChannelByte">The ChannelByte to construct the channel from.</param>
        public Channel(int ChannelByte) {
            this._ChannelByte = ChannelByte;
        }

        /// <summary>
        /// Constructs a <see cref="Channel"/> object with a given <paramref name="ChannelByte"/>, 
        /// owned by a given <paramref name="Parent"/> <see cref="IHueDevice"/>.
        /// </summary>
        /// <param name="ChannelByte">The ChannelByte to construct the channel from.</param>
        /// <param name="Parent">The <see cref="IHueDevice"/> that will own the <see cref="Channel"/></param>
        public Channel(int ChannelByte, IHueDevice Parent) {
            this.ChannelByte = ChannelByte;
            this._Parent = Parent;
        }

        /// <summary>
        /// Constructs a <see cref="Channel"/> object with a given <paramref name="ChannelByte"/>, 
        /// owned by a given <paramref name="Parent"/> <see cref="IHueDevice"/>, 
        /// with a given <see cref="ChannelInfo"/>.
        /// </summary>
        /// <param name="ChannelByte">The ChannelByte to construct the channel from.</param>
        /// <param name="Parent">The <see cref="IHueDevice"/> that owns the <see cref="Channel"/></param>
        /// <param name="Info">The <see cref="ChannelInfo"/> owned by the <see cref="Channel"/>.</param>
        public Channel(int ChannelByte, IHueDevice Parent, ChannelInfo Info) {
            this.ChannelByte = ChannelByte;
            this._Parent = Parent;
            this._ChannelInfo = Info;
        }

        internal void BuildSubDevices() {
            for (int i = 0; i < _ChannelInfo.NumSubDevices; i++) {
                switch (_ChannelInfo.Type) {
                    case NZXTDeviceType.Fan:
                        this._SubDevices.Add(new Fan());
                        break;
                    case NZXTDeviceType.Strip:
                        this._SubDevices.Add(new Strip());
                        break;
                }
            }
        }

        internal void UpdateEffect(IEffect newOne)
        {
            this._Effect = newOne;
        }

        /// <summary>
        /// Refreshes all <see cref="ISubDevice"/>s in the <see cref="Channel"/>'s <see cref="SubDevices"/> list.
        /// </summary>
        public void RefreshSubDevices()
        {
            BuildSubDevices();
        }

        /// <summary>
        /// Turns the <see cref="Channel"/> on.
        /// </summary>
        public void On() {
            this._State = true;
            _Parent.ApplyEffect(this, _Effect);
        }

        /// <summary>
        /// Turns the <see cref="Channel"/> off.
        /// </summary>
        public void Off() {
            this._State = false;
            _Parent.ApplyEffect(this, new Fixed(this, new Color(0, 0, 0)), false);
        }
        
        internal byte[] BuildColorBytes(Color color) {
            List<byte> outList = new List<byte>();
            foreach (ISubDevice device in _SubDevices)
            {
                if (device.IsActive) // If active, add effect color
                {
                    byte[][] exp = color.ExpandedChunks(device.NumLeds);
                    for (int LED = 0; LED < device.NumLeds; LED++) {
                        if (device.Leds[LED])
                        {
                            outList.Add(exp[LED][0]);
                            outList.Add(exp[LED][1]);
                            outList.Add(exp[LED][2]);
                        }
                        else
                        {
                            outList.Add(0x00);
                            outList.Add(0x00);
                            outList.Add(0x00);
                        }
                    }
                }
                else { // If not active, add padding bytes
                    for (int led = 0; led < device.NumLeds * 3; led++) {
                        outList.Add(0x00);
                    }
                }
            }
            for (int pad = outList.Count; pad < 120; pad++) { // Pad out remainder
                outList.Add(0x00);
            }
            return outList.ToArray();
        }

        /// <summary>
        /// Updates the <see cref="Channel"/>'s <see cref="ChannelInfo"/>.
        /// </summary>
        public void UpdateChannelInfo() {
            Parent.UpdateChannelInfo(this);
        }

        /// <summary>
        /// Sets the <see cref="Channel"/>'s <see cref="ChannelInfo"/> to the given <paramref name="info"/>.
        /// </summary>
        /// <param name="info"></param>
        public void SetChannelInfo(ChannelInfo info) {
            this._ChannelInfo = info;
        }

        /// <summary>
        /// Returns the <see cref="Channel"/>'s ChannelByte.
        /// </summary>
        /// <param name="channel"></param>
        public static explicit operator byte(Channel channel) {
            return (byte)channel.ChannelByte;
        }
    }
}
