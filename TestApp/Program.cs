using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

using NZXTSharp;
using NZXTSharp.Devices;
using NZXTSharp.Effects;

namespace TestApp {
    class Program {
        static void Main(string[] args) {

            HuePlus hue = new HuePlus();

            Fixed effect = new Fixed(new HexColor(255, 255, 255));
            hue.ApplyEffect(hue.Both, effect);
            

            Console.ReadLine();
        }
    }
}
