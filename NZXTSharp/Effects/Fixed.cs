using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp.Devices;
using NZXTSharp.Params;

namespace NZXTSharp.Effects {
    public class Fixed : IEffect {
        private int _EffectByte = 0x00;
        private string _EffectName = "Fixed";
        public readonly List<string> CompatibleWith = new List<string>() { "HuePlus" };

        private Color _Color;
        private _03Param _Param1 = new _03Param();
        private _02Param _Param2 = new _02Param();
        private Channel _Channel;

        public int EffectByte { get; }
        public Channel Channel { get; set; }
        public string EffectName { get; }
        

        public Fixed(Color color) {
            this._Color = color;
        }

        public Fixed(Channel Channel, Color color) {
            this._Channel = Channel;
            this._Color = color;
        }

        public bool IsCompatibleWith(string name) {
            return CompatibleWith.Contains(name) ? true : false;
        }

        public List<byte[]> BuildBytes(Channel Channel) {
            byte[] SettingsBytes = new byte[] { 0x4b, (byte)Channel, 0x00, _Param1, _Param2 };
            byte[] final = SettingsBytes.ConcatenateByteArr(Channel.BuildColorBytes(_Color));
            return new List<byte[]>() { final };
        }
    }
}
