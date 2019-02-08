using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp;
using NZXTSharp.Exceptions;

namespace NZXTSharp.KrakenX
{
    public class Loading : IEffect
    {
        private int _EffectByte = 0x0a;
        private readonly List<NZXTDeviceType> CompatibleWith = new List<NZXTDeviceType> { NZXTDeviceType.KrakenX };
        private Color Color;
        private int Speed;

        /// <inheritdoc/>
        public int EffectByte { get; }

        /// <inheritdoc/>
        public string EffectName => throw new NotImplementedException();

        /// <inheritdoc/>
        public IChannel Channel { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Loading(Color Color, int Speed = 2)
        {
            if (Speed < 0 || Speed > 4)
            {
                throw new InvalidParamException("Speed values must be between 0-4 (inclusive).");
            }

            this.Color = Color;
            this.Speed = Speed;
        }

        /// <inheritdoc/>
        public bool IsCompatibleWith(NZXTDeviceType Type)
        {
            return CompatibleWith.Contains(Type) ? true : false;
        }

        /// <inheritdoc/>
        public List<byte[]> BuildBytes(NZXTDeviceType Type, IChannel Channel)
        {
            if (Channel.ChannelByte == 0x00 || Channel.ChannelByte == 0x01)
            {
                throw new IncompatibleParamException("TaiChi channel can only be Ring (ChanneByte: 0x02)");
            }

            List<byte[]> KrakenOutList = new List<byte[]>();
            byte[] KrakenSettingsBytes = new byte[] { 0x02, 0x4c, (byte)Channel.ChannelByte, 0x0a, (byte)Speed };
            byte[] KrakenFinal = KrakenSettingsBytes.ConcatenateByteArr(Channel.BuildColorBytes(Color));
            KrakenOutList.Add(KrakenFinal);

            return KrakenOutList;
        }
    }
}
