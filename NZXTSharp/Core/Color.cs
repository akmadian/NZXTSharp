using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NZXTSharp {
    public class Color
    {

        private int R;
        private int G;
        private int B;

        public Color()
        {

        }

        public Color(string hexColor)
        {
            if (hexColor.StartsWith("#")) // Strip leading # if it exists
                hexColor = hexColor.Substring(1);

            string[] splitHex = hexColor.SplitEveryN(2);
            this.R = Convert.ToInt32(splitHex[0]);
            this.G = Convert.ToInt32(splitHex[1]);
            this.B = Convert.ToInt32(splitHex[2]);
        }

        public Color(int R, int G, int B)
        {
            this.R = R;
            this.G = G;
            this.B = B;
        }

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

        public byte[] Expanded()
        {
            List<byte> outBytes = new List<byte>();
            for (int i = 0; i < 40; i++)
            {
                outBytes.Add(Convert.ToByte(G));
                outBytes.Add(Convert.ToByte(R));
                outBytes.Add(Convert.ToByte(B));
            }

            return outBytes.ToArray();
        }

        /// <summary>
        /// Expands the <see cref="Color"/> instance into an array of byte arrays. Each sub array contains the RGB values for each LED.
        /// </summary>
        /// <param name="NumLeds"></param>
        /// <returns></returns>
        public byte[][] ExpandedChunks(int NumLeds)
        {
            List<byte[]> outBytes = new List<byte[]>();
            for (int i = 0; i < NumLeds; i++)
            {
                List<byte> arr = new List<byte>();
                arr.Add(Convert.ToByte(G));
                arr.Add(Convert.ToByte(R));
                arr.Add(Convert.ToByte(B));
                outBytes.Add(arr.ToArray());
            }
            return outBytes.ToArray();
        }
    }
}

