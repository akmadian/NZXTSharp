using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp.Devices;
using NZXTSharp.Params;

namespace NZXTSharp.Effects {
    public class Fixed : IEffect {

        private int _EffectByte = 0x01;
        private HexColor _Color;
        private Channel _Channel;
        private byte[] _EffectBytes;
        private _03Param _Param1 = new _03Param();
        private _02Param _Param2 = new _02Param();

        public int EffectByte { get; }
        public HexColor Color { get; set; }
        public Channel Channel { get; set; }
        public byte[] EffectBytes { get; }
        public _03Param Param1 { get; }
        public _02Param Param2 { get; }

        public readonly List<string> CompatibleWith = new List<string>() {"HuePlus"};

        public bool IsCompatibleWith(string name) {
            return CompatibleWith.Contains(name) ? true : false;
        }

        public Fixed(HexColor color) {
            this.Color = color;
        }

        public Fixed(Channel channel, HexColor color) {
            this.Channel = channel;
            this.Color = color;
        }

        public byte[] BuildBytes() {
            byte[] SettingsBytes = new byte[] { 0x4b, (byte)Channel, 0x00, Param1, Param2 };
            byte[] final = SettingsBytes.ConcatenateByteArr(Color.Expanded());
            return final;
        }
    }
}
