using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp {
    public interface IEffect {

        int EffectByte { get; }

        string EffectName { get; }

        Channel Channel { get; set; }

        List<byte[]> BuildBytes();

        bool IsCompatibleWith(string Device);
        
    }
}
