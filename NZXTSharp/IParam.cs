using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp {
    public interface IParam {

        int Value { get; }

        int GetValue();
    }
}
