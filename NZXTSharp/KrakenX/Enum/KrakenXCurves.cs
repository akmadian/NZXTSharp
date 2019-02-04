using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.KrakenX
{
    /// <summary>
    /// Default fan and pump curves.
    /// </summary>
    public class KrakenXCurves
    {
        /// <summary>
        /// The silent curve for fans.
        /// </summary>
        public int[] FanSilentCurve = new int[] {
            0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x23, 0x2d, 0x37, 0x4b, 100, 100, 100, 100,
            100, 100, 100, 100, 100
        };

        /// <summary>
        /// The performance curve for fans.
        /// </summary>
        public int[] FanPerformanceCurve = new int[] {
            50, 50, 50, 50, 50, 50, 50, 50, 60, 70, 80, 90, 100, 100, 100, 100,
            100, 100, 100, 100, 100
        };

        /// <summary>
        /// The silent curve for pumps.
        /// </summary>
        public int[] PumpSilentCurve = new int[] {
            60, 60, 60, 60, 60, 60, 60, 60, 70, 80, 90, 100, 100, 100, 100, 100,
            100, 100, 100, 100, 100
        };

        /// <summary>
        /// The performance curve for pumps.
        /// </summary>
        public int[] PumpPerformanceCurve = new int[] {
            70, 70, 70, 70, 70, 70, 70, 70, 80, 0x55, 90, 0x5f, 100, 100, 100, 100,
            100, 100, 100, 100, 100
        };
    }
}
