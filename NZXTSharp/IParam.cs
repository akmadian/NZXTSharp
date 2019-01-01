using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp {
    public interface IParam {

        int Value { get; }

        List<string> CompatibleWith { get; }

        int GetValue();


    }
}
