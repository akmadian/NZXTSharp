using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp
{
    public interface INZXTDevice
    {
        NZXTDeviceType Type { get; }

        void ApplyEffect(IEffect Effect);
    }
}
