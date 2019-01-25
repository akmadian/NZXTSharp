using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using NZXTSharp.Devices;
using NZXTSharp.Effects;

namespace NZXTSharp.Devices {
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
        public int ChannelByte { get; }
        public IEffect Effect { get => _Effect; }
        public bool State { get => _State; }
        public ChannelInfo ChannelInfo { get => _ChannelInfo; }
        public IHueDevice Parent { get => _Parent; }
        public List<ISubDevice> SubDevices { get => _SubDevices; }
        #endregion

        public Channel() {

        }

        public Channel(int _ChannelByte) {
            this._ChannelByte = _ChannelByte;
        }

        public Channel(int _ChannelByte, IHueDevice Parent) {
            this.ChannelByte = _ChannelByte;
            this._Parent = Parent;
        }

        public Channel(int _ChannelByte, IHueDevice Parent, ChannelInfo Info) {
            this.ChannelByte = _ChannelByte;
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


        public void RefreshSubDevices()
        {
            BuildSubDevices();
        }

        public void On() {
            this._State = true;
            _Parent.ApplyEffect(this, _Effect);
        }

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

        public void UpdateChannelInfo() {
            Parent.UpdateChannelInfo(this);
        }

        public void SetChannelInfo(ChannelInfo info) {
            this._ChannelInfo = info;
        }

        public static explicit operator byte(Channel channel) {
            return (byte)channel.ChannelByte;
        }
    }
}
