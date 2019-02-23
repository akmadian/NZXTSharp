using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp;
using NZXTSharp.Exceptions;

namespace NZXTSharp.KrakenX
{
    /// <summary>
    /// Represents a Water Cooler RGB effect.
    /// </summary>
    public class WaterCooler : IEffect
    {
        private readonly List<NZXTDeviceType> CompatibleWith = new List<NZXTDeviceType> { NZXTDeviceType.KrakenX };
        private int Speed;

        /// <summary>
        /// The <see cref="WaterCooler"/> effect's ChannelByte.
        /// </summary>
        public int EffectByte => throw new NotImplementedException();

        /// <summary>
        /// The <see cref="WaterCooler"/> effect's name.
        /// </summary>
        public string EffectName => throw new NotImplementedException();

        /// <summary>
        /// The <see cref="IChannel"/> that the <see cref="WaterCooler"/> effect is applied to.
        /// </summary>
        public IChannel Channel { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// Constructs a <see cref="WaterCooler"/> instance.
        /// </summary>
        /// <param name="Speed">The speed for the effect to move at.</param>
        public WaterCooler(int Speed = 2)
        {
            if (Speed < 0 || Speed > 4)
            {
                throw new InvalidParamException("Speed values must be between 0-4 (inclusive).");
            }

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
