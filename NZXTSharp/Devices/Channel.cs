using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NZXTSharp.Devices.Hue;

namespace NZXTSharp {
    public class Channel {

        #region Fields
        private int _ChannelByte;
        private IEffect _Effect;
        private IHueDevice _Parent;

        #endregion
        #region Properties
        public int ChannelByte { get; }
        public IEffect Effect { get; set; }

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

        public void On() {
            byte[] SettingsBytes = new byte[] { 0x4b, (byte)this, (byte)this.Effect.EffectByte, 0x03, 0x02 };
            byte[] final = SettingsBytes.ConcatenateByteArr(this.Effect.Color.Expanded());
            _Parent.ApplyCustom(final);   
        }

        public void Off() {
            _Parent.ApplyEffect(this, new Effects.Fixed(this, new HexColor(0, 0, 0)));
        }

        public static explicit operator byte(Channel channel) {
            return (byte)channel.ChannelByte;
        }
    }
}
