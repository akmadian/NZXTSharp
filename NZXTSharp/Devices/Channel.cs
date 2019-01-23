using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using NZXTSharp.Devices;
using NZXTSharp.Effects;

namespace NZXTSharp.Devices {
    public class Channel {
        
        private int _ChannelByte;
        private IEffect _Effect;
        private IHueDevice _Parent;
        private bool _State = true;
        private ChannelInfo _ChannelInfo;
        
        #region Properties
        public int ChannelByte { get; }
        public IEffect Effect { get; set; }
        public bool State { get; set; }
        public ChannelInfo ChannelInfo { get => _ChannelInfo; }
        public IHueDevice Parent { get; }
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

        public void On() {
            this._State = false;
            byte[] SettingsBytes = new byte[] { 0x4b, (byte)this, (byte)this.Effect.EffectByte, 0x03, 0x02 };
            // TODO : TOFIX
            //byte[] final = SettingsBytes.ConcatenateByteArr(this.Effect.Color.Expanded());
            //_Parent.ApplyCustom(final);   
        }

        public void Off() {
            this._State = false;
            _Parent.ApplyEffect(this, new Effects.Fixed(this, new HexColor(0, 0, 0)));
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
