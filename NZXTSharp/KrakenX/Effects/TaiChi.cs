using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp;

namespace NZXTSharp.KrakenX
{
    public class TaiChi : IEffect
    {
        private readonly List<NZXTDeviceType> CompatibleWith = new List<NZXTDeviceType> { NZXTDeviceType.KrakenX };

        private int _EffectByte = 0x08;

        private IChannel _Channel;
        
        /// <inheritdoc/>
        public int EffectByte { get; }

        /// <inheritdoc/>
        public string EffectName => "TaiChi";

        /// <inheritdoc/>
        public IChannel Channel { get; set; }

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
