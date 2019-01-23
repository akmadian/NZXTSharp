using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NZXTSharp {
    public class HexColor
    {

        private int R;
        private int G;
        private int B;

        public HexColor()
        {

        }

        public HexColor(string hexColor)
        {
            if (hexColor.StartsWith("#")) // Strip leading # if it exists
                hexColor = hexColor.Substring(1);

            string[] splitHex = hexColor.SplitEveryN(2);
            this.R = Convert.ToInt32(splitHex[0]);
            this.G = Convert.ToInt32(splitHex[1]);
            this.B = Convert.ToInt32(splitHex[2]);
        }

        public HexColor(int R, int G, int B)
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
            List<int> outBytes = new List<int>();
            for (int i = 0; i < 40; i++)
            {
                outBytes.Add(G);
                outBytes.Add(R);
                outBytes.Add(B);
            }

            List<byte> outB = new List<byte>();

            foreach (int val in outBytes)
            {
                outB.Add(Convert.ToByte(val));
            }

            return outB.ToArray();
        }

        public byte[] Expanded(int NumLeds) {
            List<int> outBytes = new List<int>();
            for (int i = 0; i < NumLeds; i++) {
                outBytes.Add(G);
                outBytes.Add(R);
                outBytes.Add(B);
            }

            List<byte> outB = new List<byte>();

            foreach (int val in outBytes) {
                outB.Add(Convert.ToByte(val));
            }

            return outB.ToArray();
        }
    }
}

