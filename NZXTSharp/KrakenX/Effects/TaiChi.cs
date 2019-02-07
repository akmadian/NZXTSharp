using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp;
using NZXTSharp.Exceptions;

namespace NZXTSharp.KrakenX
{
    public class TaiChi : IEffect
    {
        private readonly List<NZXTDeviceType> CompatibleWith = new List<NZXTDeviceType> { NZXTDeviceType.KrakenX };

        private int _EffectByte = 0x08;
        private Color[] Colors;
        private int Speed;

        private IChannel _Channel;
        
        /// <inheritdoc/>
        public int EffectByte { get; }

        /// <inheritdoc/>
        public string EffectName => "TaiChi";

        /// <inheritdoc/>
        public IChannel Channel { get; set; }

        public TaiChi(Color[] Colors, int Speed = 2)
        {
            if (Colors.Length < 2)
            {
                throw new InvalidParamException("TaiChi colors must be of length 2 or more.");
            }

            if (Speed < 0 || Speed > 4)
            {
                throw new InvalidParamException("Speed values must be between 0-4 (inclusive).");
            }

            this.Colors = Colors;
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
                throw new IncompatibleParamException("TaiChi channel can only be Ring or 0x02");
            }

            List<byte[]> KrakenOutList = new List<byte[]>();
            for (int ColorIndex = 0; ColorIndex < Colors.Length; ColorIndex++)
            {
                byte[] KrakenSettingsBytes = new byte[] { 0x02, 0x4c, (byte)Channel.ChannelByte, 0x08, new CISS(ColorIndex, Speed) };
                byte[] KrakenFinal = KrakenSettingsBytes.ConcatenateByteArr(Channel.BuildColorBytes(Colors[ColorIndex]));
                KrakenOutList.Add(KrakenFinal);
            }
            return KrakenOutList;
        }
    }
}
