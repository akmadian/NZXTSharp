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
        private List<ISubDevice> _SubDevices = new List<ISubDevice>();
        
        #region Properties
        public int ChannelByte { get; }
        public IEffect Effect { get; set; }
        public bool State { get; set; }
        public ChannelInfo ChannelInfo { get => _ChannelInfo; }
        public IHueDevice Parent { get; }
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

        public void BuildSubDevices() {
            for (int i = 0; i < ChannelInfo.NumSubDevices; i++) {
                switch (ChannelInfo.Type) {
                    case NZXTDeviceType.Fan:
                        SubDevices.Add(new Fan());
                        break;
                    case NZXTDeviceType.Strip:
                        SubDevices.Add(new Strip());
                        break;
                }
            }
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
        
        public byte[] BuildColorBytes(HexColor color) {
            List<byte> outList = new List<byte>();
            foreach (ISubDevice device in SubDevices) 
            {
                if (device.IsActive) // If active, add effect color
                {
                    Console.WriteLine("Device Is Active");
                    byte[] exp = color.Expanded(device.NumLeds);
                    for (int LED = 0; LED < device.NumLeds; LED++) {
                        if (device.Leds[LED]) {
                            Console.WriteLine("    LED is Active");
                            outList.Add(exp[LED]);
                        }
                        else {
                            Console.WriteLine("    LED is Not Active");
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
