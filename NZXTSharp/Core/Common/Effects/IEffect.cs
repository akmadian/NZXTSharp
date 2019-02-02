using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp.Devices;

namespace NZXTSharp.Core {

    /// <summary>
    /// Represents a generic RGB effect.
    /// </summary>
    public interface IEffect {

        /// <summary>
        /// The <see cref="IEffect"/>'s EffectByte.
        /// </summary>
        int EffectByte { get; }

        /// <summary>
        /// The name of the <see cref="IEffect"/>.
        /// </summary>
        string EffectName { get; }

        /// <summary>
        /// The <see cref="Channel"/> to set the <see cref="IEffect"/> on.
        /// </summary>
        Channel Channel { get; set; }

        /// <summary>
        /// Builds and returns the buffer queue needed to set the <see cref="IEffect"/>.
        /// </summary>
        /// <param name="Channel"></param>
        /// <returns></returns>
        List<byte[]> BuildBytes(Channel Channel);

        /// <summary>
        /// Checks to see if the <see cref="IEffect"/> is compatible with a given <see cref="NZXTDeviceType"/> <paramref name="Type"/>.
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        bool IsCompatibleWith(NZXTDeviceType Type);
        
    }
}
