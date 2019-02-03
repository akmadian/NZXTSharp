using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp;

namespace NZXTSharp.KrakenX
{
    public class Loading : IEffect
    {
        public int EffectByte => throw new NotImplementedException();

        public string EffectName => throw new NotImplementedException();

        public IChannel Channel { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public List<byte[]> BuildBytes(IChannel Channel)
        {
            throw new NotImplementedException();
        }

        public bool IsCompatibleWith(NZXTDeviceType Type)
        {
            throw new NotImplementedException();
        }
    }
}
