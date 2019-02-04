using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.KrakenX
{
    /// <summary>
    /// Default fan and pump curves. Copied from a decompiled version of CAM.
    /// </summary>
    public static class KrakenXCurves
    {
        /// <summary>
        /// The silent curve for fans.
        /// </summary>
        private static readonly int[] _FanSilentCurve = new int[] {
            0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x23, 0x2d, 0x37, 0x4b,
            100, 100, 100, 100, 100, 100, 100, 100, 100
        };

        /// <summary>
        /// The performance curve for fans.
        /// </summary>
        private static readonly int[] _FanPerformanceCurve = new int[] {
            50, 50, 50, 50, 50, 50, 50, 50, 60, 70, 80, 90, 100, 100, 100, 100,
            100, 100, 100, 100, 100
        };

        /// <summary>
        /// The silent curve for pumps.
        /// </summary>
        private static readonly int[] _PumpSilentCurve = new int[] {
            60, 60, 60, 60, 60, 60, 60, 60, 70, 80, 90, 100, 100, 100, 100, 100,
            100, 100, 100, 100, 100
        };

        /// <summary>
        /// The performance curve for pumps.
        /// </summary>
        private static readonly int[] _PumpPerformanceCurve = new int[] {
            70, 70, 70, 70, 70, 70, 70, 70, 80, 0x55, 90, 0x5f, 100, 100, 100, 100,
            100, 100, 100, 100, 100
        };

        /// <summary>
        /// The silent curve for fans.
        /// </summary>
        public static int[] FanSilentCurve { get => _FanSilentCurve; }

        /// <summary>
        /// The performance curve for fans.
        /// </summary>
        public static int[] FanPerformanceCurve { get => _FanPerformanceCurve; }

        /// <summary>
        /// The silent curve for pumps.
        /// </summary>
        public static int[] PumpSilentCurve { get => _PumpSilentCurve; }

        /// <summary>
        /// The performance curve for pumps.
        /// </summary>
        public static int[] PumpPerformanceCurve { get => _PumpPerformanceCurve; }
    }
}
