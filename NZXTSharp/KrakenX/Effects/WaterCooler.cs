using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp;
using NZXTSharp.Exceptions;

namespace NZXTSharp.KrakenX
{
    public class WaterCooler : IEffect
    {
        private readonly List<NZXTDeviceType> CompatibleWith = new List<NZXTDeviceType> { NZXTDeviceType.KrakenX };
        private int Speed;

        public int EffectByte => throw new NotImplementedException();

        public string EffectName => throw new NotImplementedException();

        public IChannel Channel { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public WaterCooler(int Speed = 2)
        {
            if (Speed < 0 || Speed > 4)
            {
                throw new InvalidParamException("Speed values must be between 0-4 (inclusive).");
            }

            this.Speed = Speed;
        }

        public bool IsCompatibleWith(NZXTDeviceType Type)
        {
            return CompatibleWith.Contains(Type) ? true : false;
        }

        public List<byte[]> BuildBytes(NZXTDeviceType Type, IChannel Channel)
        {
            if (Channel.ChannelByte == 0x00 || Channel.ChannelByte == 0x01)
            {
                throw new IncompatibleParamException("TaiChi channel can only be Ring (ChanneByte: 0x02)");
            }

            List<byte[]> KrakenOutList = new List<byte[]>();
            byte[] KrakenLogoBytes = new byte[] { 0x02, 0x4c, 0x01, 0x06, (byte)Speed };
            byte[] KrakenLogoFinal = KrakenLogoBytes.ConcatenateByteArr(Channel.BuildColorBytes(new Color(255, 0, 0)));
            KrakenOutList.Add(KrakenLogoFinal);

            byte[] KrakenRingBytes = new byte[] { 0x02, 0x4c, 0x02, 0x09, (byte)Speed };
            byte[] KrakenRingFinal = KrakenRingBytes.ConcatenateByteArr(Channel.BuildColorBytes(new Color(0, 0, 255)));
            KrakenOutList.Add(KrakenRingFinal);

            return KrakenOutList;
        }
    }
}
