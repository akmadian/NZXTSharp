using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NZXTSharp {

    /// <summary>
    /// Represents a color.
    /// </summary>
    public class Color
    {
        #region Properties and Fields
        private readonly int _R;
        private readonly int _G;
        private readonly int _B;

        /// <summary>
        /// The R value of the color.
        /// </summary>
        public int R { get => _R; }

        /// <summary>
        /// The G value of the color.
        /// </summary>
        public int G { get => _G; }

        /// <summary>
        /// The B value of the color.
        /// </summary>
        public int B { get => _B; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs an empty <see cref="Color"/>.
        /// </summary>
        public Color()
        {

        }

        /// <summary>
        /// Creates a Color instance from a hex color code.
        /// </summary>
        /// <param name="hexColor">The color code. Supports codes with a leading #, and without.</param>
        public Color(string hexColor)
        {
            if (hexColor.StartsWith("#")) // Strip leading # if it exists
                hexColor = hexColor.Substring(1);

            string[] splitHex = hexColor.SplitEveryN(2);
            this._R = Convert.ToInt32(splitHex[0]);
            this._G = Convert.ToInt32(splitHex[1]);
            this._B = Convert.ToInt32(splitHex[2]);
        }

        /// <summary>
        /// Creates a Color instance from R, G, B values.
        /// </summary>
        /// <param name="R">The color's R value. Must be 0-255 (inclusive).</param>
        /// <param name="G">The color's G value. Must be 0-255 (inclusive).</param>
        /// <param name="B">The color's B value. Must be 0-255 (inclusive).</param>
        public Color(int R, int G, int B)
        {
            this._R = R;
            this._G = G;
            this._B = B;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns a list of 40 "#ffffff" color codes in G, R, B format.
        /// </summary>
        /// <returns></returns>
        public byte[] AllOff()
        {
            List<int> outBytes = new List<int>();
            for (int i = 0; i < 40; i++)
            {
                outBytes.Add(0);
                outBytes.Add(0);
                outBytes.Add(0);
            }

            List<byte> outB = new List<byte>();

            foreach (int val in outBytes)
            {
                outB.Add(Convert.ToByte(val));
            }

            return outB.ToArray();
        }

        /// <summary>
        /// Expands the <see cref="Color"/> instance into a byte array.
        /// </summary>
        /// <returns></returns>
        internal byte[] Expanded()
        {
            List<byte> outBytes = new List<byte>();
            for (int i = 0; i < 40; i++)
            {
                outBytes.Add(Convert.ToByte(_G));
                outBytes.Add(Convert.ToByte(_R));
                outBytes.Add(Convert.ToByte(_B));
            }

            return outBytes.ToArray();
        }

        /// <summary>
        /// Expands the <see cref="Color"/> instance into an array of byte arrays. Each sub array contains the RGB values for each LED.
        /// </summary>
        /// <param name="NumLeds">The number of LED triplets to create in the array.</param>
        /// <returns></returns>
        internal byte[][] ExpandedChunks(int NumLeds)
        {
            List<byte[]> outBytes = new List<byte[]>();
            for (int i = 0; i < NumLeds; i++)
            {
                byte[] arr = new byte[3]
                {
                    Convert.ToByte(_G),
                    Convert.ToByte(_R),
                    Convert.ToByte(_B)
                };
                outBytes.Add(arr);
            }
            return outBytes.ToArray();
        }
        #endregion
    }
}

