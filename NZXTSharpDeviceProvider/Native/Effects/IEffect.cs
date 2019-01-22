using System;
using System.Collections.Generic;
using System.Text;

using RGB.NET.Devices.NZXT.Native.Devices;

namespace RGB.NET.Devices.NZXT.Native.Effects {
    public interface IEffect {

        int EffectByte { get; }

        string EffectName { get; }

        Channel Channel { get; set; }

        List<byte[]> BuildBytes();

        bool IsCompatibleWith(string Device);
        
    }
}
