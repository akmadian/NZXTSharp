using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp;
using NZXTSharp.Params;
using NZXTSharp.Devices;

// TOTEST
namespace NZXTSharp.Effects {
    public class Marquee : IEffect {

        private int _EffectByte = 0x03;
        private string _EffectName = "Marquee";
        public readonly List<string> CompatibleWith = new List<string>() { "HuePlus" };

        private Color _Color;
        private Direction Param1;
        private LSS Param2;
        private Channel _Channel;
        private IHueDevice Parent;

        #region Properties
        public int EffectByte { get; }
        public Channel Channel { get; set; }
        public string EffectName { get; }
        #endregion

        public Marquee(IHueDevice Parent, Color Color, Direction Direction, LSS LSS) {
            this.Parent = Parent;
            this._Color = Color;
            this.Param1 = Direction;
            this.Param2 = LSS;
        }

        public bool IsCompatibleWith(string Device) {
            return CompatibleWith.Contains(Device) ? true : false;
        }

        public List<byte[]> BuildBytes(Channel Channel) {
            List<byte[]> outList = new List<byte[]>();
            foreach (Channel channel in Parent.Channels) {
                byte[] SettingsBytes = new byte[] { 0x4b, (byte)channel, 0x03, Param1, Param2 };
                byte[] final = SettingsBytes.ConcatenateByteArr(channel.State == false ? new Color().AllOff() : Channel.BuildColorBytes(_Color));
                outList.Add(final);
            }

            return outList;
        }
    }
}
