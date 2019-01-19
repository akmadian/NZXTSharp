using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp.Devices;
using NZXTSharp.Params;

namespace NZXTSharp.Effects {
    public class CandleLight : IEffect {
        private int _EffectByte = 0x09;
        private string _EffectName = "CandleLight";
        public readonly List<string> CompatibleWith = new List<string>() { "HuePlus" };

        public HexColor Color;
        private Channel _Channel;
        private _03Param _Param1;
        private _02Param _Param2;

        public int EffectByte { get; }
        public Channel Channel { get; set; }
        public string EffectName { get; }

        public CandleLight(HexColor Color) {
            this.Color = Color;
        }

        public bool IsCompatibleWith(string Device) {
            return CompatibleWith.Contains(Device) ? true : false;
        }

        public List<byte[]> BuildBytes() {
            byte[] SettingsBytes = new byte[] { 0x4b, (byte)Channel, 0x09, _Param1, _Param2 };
            byte[] final = SettingsBytes.ConcatenateByteArr(Color.Expanded());
            return new List<byte[]>() { final };
        }
    }
}
