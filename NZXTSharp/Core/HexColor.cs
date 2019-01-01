using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NZXTSharp {
    public class HexColor {
        
        private int R;
        private int G;
        private int B;

        public HexColor(int R, int G, int B) {
            this.R = R;
            this.G = G;
            this.B = B;
        }

        public byte[] Expanded() {
            List<int> outBytes = new List<int>();
            for (int i = 0; i < 40; i++) {
                outBytes.Add(G);
                outBytes.Add(R);
                outBytes.Add(B);
            }
            List<byte> outB = new List<byte>();
            foreach(int val in outBytes) {
                outB.Add(Convert.ToByte(val));
            }
            return outB.ToArray();
        }
    }
}
