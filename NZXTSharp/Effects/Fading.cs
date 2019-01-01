using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp.Params;

namespace NZXTSharp.Effects {
    class Fading : IEffect {

        public HexColor[] Colors;
        public readonly List<string> CompatibleWith = new List<string>() { "HuePlus" };
        private _03Param Param1 = new _03Param();
        private CISS Param2;
        private Channel _Channel;
        private byte[] _EffectBytes = null;

        public int EffectByte { get; }
        public Channel Channel { get; set; }
        public byte[] EffectBytes { get; }

        public Fading(Channel channel, HexColor[] Colors, CISS Param2) {
            this.Channel = channel;
            this.Colors = Colors;
            this.Param2 = Param2;
        }

        public List<byte[]> BuildBytes() {
            List<byte[]> outList = new List<byte[]>();
            foreach (HexColor Color in Colors) {
                byte[] SettingsBytes = new byte[] { 0x4b, (byte)Channel, 0x01, Param1, Param2 };
                byte[] final = SettingsBytes.ConcatenateByteArr(Color.Expanded());
                outList.Add(final);
            }
            return outList;
        }
    }
}
