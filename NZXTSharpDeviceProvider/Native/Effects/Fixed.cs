using System;
using System.Collections.Generic;
using System.Text;

using RGB.NET.Devices.NZXT.Native.Core;
using RGB.NET.Devices.NZXT.Native.Devices;
using RGB.NET.Devices.NZXT.Native.Params;

namespace RGB.NET.Devices.NZXT.Native.Effects {
    public class Fixed : IEffect {
        private int _EffectByte = 0x00;
        private string _EffectName = "Fixed";
        public readonly List<string> CompatibleWith = new List<string>() { "HuePlus" };

        private HexColor _Color;
        private Channel _Channel;
        private _03Param _Param1 = new _03Param();
        private _02Param _Param2 = new _02Param();

        public int EffectByte { get; }
        public Channel Channel { get; set; }
        public string EffectName { get; }

        public Fixed(HexColor color) {
            this._Color = color;
        }

        public Fixed(Channel channel, HexColor color) {
            this.Channel = channel;
            this._Color = color;
        }

        public bool IsCompatibleWith(string name) {
            return CompatibleWith.Contains(name) ? true : false;
        }

        public List<byte[]> BuildBytes() {
            byte[] SettingsBytes = new byte[] { 0x4b, (byte)Channel, 0x00, _Param1, _Param2 };
            byte[] final = SettingsBytes.ConcatenateByteArr(_Color.Expanded());
            return new List<byte[]>() { final };
        }
    }
}
