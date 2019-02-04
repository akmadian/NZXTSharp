using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp;

namespace NZXTSharp.KrakenX
{
    public class Loading : IEffect
    {
        private int _EffectByte = 0x0a;
        private readonly List<NZXTDeviceType> CompatibleWith = new List<NZXTDeviceType> { NZXTDeviceType.KrakenX };

        /// <inheritdoc/>
        public int EffectByte { get; }

        /// <inheritdoc/>
        public string EffectName => throw new NotImplementedException();

        /// <inheritdoc/>
        public IChannel Channel { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <inheritdoc/>
        public bool IsCompatibleWith(NZXTDeviceType Type)
        {
            return CompatibleWith.Contains(Type) ? true : false;
        }

        /// <inheritdoc/>
        public List<byte[]> BuildBytes(NZXTDeviceType Type, IChannel Channel)
        {
            throw new NotImplementedException();
        }
    }
}
