# NZXTSharp

[![Documentation Status](https://readthedocs.org/projects/nzxtsharp/badge/?version=latest)](https://nzxtsharp.readthedocs.io/en/latest/?badge=latest) 
[![Nuget](https://img.shields.io/nuget/v/NZXTSharp.svg)](https://www.nuget.org/packages/NZXTSharp)
[![Discord](https://img.shields.io/badge/%20-Discord%20Server-blue.svg)](https://discord.gg/yK8m2CU)

NZXTSharp is a C# package that allows interaction with NZXT's Hue+. Support for other devices will be coming.

You can find NZXTSharp on NuGet [here][0].

**I am looking for people to help NZXTSharp grow by integrating NZXTSharp with [ProjectAurora][5] and/ or [RGB.NET][6]. If you are interested, please contact me on [Discord][7], or by email.**

Please keep in mind that NZXTSharp is in development, and will have breaking changes in the future.

Documentation can be found at [NZXTSharp's readthedocs.io page][3]. Docs are built from [the docs branch][4].

### Syntax
NZXTSharp's syntax is lightweight, only taking a few lines to get started.

```C#
using NZXTSharp;
using NZXTSharp.Devices;
using NZXTSharp.Effects;

HuePlus hue = new HuePlus(); // Create device
Fixed effect = new Fixed(new Color(255, 255, 255)); // Make fixed effect
hue.ApplyEffect(hue.Both, effect); // Apply effect
```

### FAQ

**Q:** Are there any plans to make an SDK in XX language?
<br>**A:** As of yet, no. I am focusing on support for the .NET framework. Python is most likely next.

**Q:** Is XX device supported? 
<br>**A:** Next up on the list are KrakenX devices, after that is most likely the Hue 2.


### Disclaimer
NZXTSharp is provided under the GNU GPLv3 license, but I want to re-iterate these stipulations:
 - This software is provided as is.
 - This software is provided with no warranty.
 - Neither I, nor any of NZXTSharp's collaborators hold any liability for any broken or not working hardware resulting from using this package. Please use your best judgement.
 
 A big thank you to [Pet0203][2] for his help in reverse engineering the protocols and building NZXTSharp.
 
 ###### </> With ♥ by Ari Madian

[0]: https://www.nuget.org/packages/NZXTSharp
[1]: https://github.com/akmadian/NZXTSharp/issues/new
[2]: https://github.com/Pet0203
[3]: https://nzxtsharp.readthedocs.io/en/latest/
[4]: https://github.com/akmadian/NZXTSharp/tree/docs-develop
[5]: https://github.com/antonpup/Aurora
[6]: https://github.com/DarthAffe/RGB.NET
[7]: https://discord.gg/yK8m2CU
