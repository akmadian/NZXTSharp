using System;
using System.Collections.Generic;
using System.Text;

namespace RGB.NET.Devices.NZXT {
    public class _NZXTLedColor {

        public int R;

        public int G;

        public int B;


        public _NZXTLedColor(int R, int G, int B) {
            this.R = R;
            this.G = G;
            this.B = B;
        }

        public byte[] AllOff() {
            List<int> outBytes = new List<int>();
            for (int i = 0; i < 40; i++) {
                outBytes.Add(0);
                outBytes.Add(0);
                outBytes.Add(0);
            }

            List<byte> outB = new List<byte>();

            foreach (int val in outBytes) {
                outB.Add(Convert.ToByte(val));
            }

            return outB.ToArray();
        }

        public byte[] Expanded() {
            List<int> outBytes = new List<int>();
            for (int i = 0; i < 40; i++) {
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
