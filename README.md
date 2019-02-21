# NZXTSharp

[![Documentation Status](https://readthedocs.org/projects/nzxtsharp/badge/?version=latest)](https://nzxtsharp.readthedocs.io/en/latest/?badge=latest) 
[![Nuget](https://img.shields.io/nuget/v/NZXTSharp.svg)](https://www.nuget.org/packages/NZXTSharp)
[![Discord](https://img.shields.io/badge/%20-Discord%20Server-blue.svg)](https://discord.gg/yK8m2CU)

NZXTSharp is a C# package that allows interaction with NZXT hardware.

You can find NZXTSharp on NuGet [here][0].

**I am looking for people to help NZXTSharp grow by integrating NZXTSharp with [ProjectAurora][5] and/ or [RGB.NET][6]. If you are interested, please contact me on [Discord][7], or by email.**

## Syntax
NZXTSharp's syntax is lightweight, only taking a few lines to get started.

```C#
using NZXTSharp;
using NZXTSharp.HuePlus;

HuePlus hue = new HuePlus(); // Create device
Fixed effect = new Fixed(new Color(255, 255, 255)); // Make fixed effect
hue.ApplyEffect(hue.Both, effect); // Apply effect
```

## FAQ

**Q:** Are there any plans to make an SDK in XX language?
<br>**A:** As of yet, no. I am focusing on support for the .NET Framework. Python or C++ is most likely next.

**Q:** Is XX device supported? 
<br>**A:** The Hue+ is fully supported, the KrakenX is a WIP, and the Hue 2 or the Grid v3 is most likely next.

## Documentation
Documentation can be found at [NZXTSharp's readthedocs.io page][3]. Docs are built from [the docs branch][4].
There are also XML docs in the source.



## Installation
You can find NZXTSharp on [Nuget][0]. If you are adding through VS and want to get pre-releases, be sure to check `Include Prerelease`.

You can also install with:

The package manager CLI: `PM> Install-Package NZXTSharp`
<br>The .NET CLI: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`> dotnet add package NZXTSharp`
<br>Or, the Packet CLI: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`> packet add NZXTSharp`

## Disclaimer
NZXTSharp is provided under the GNU GPLv3 license, but I want to re-iterate these stipulations:
 - This software is provided as is.
 - This software is provided with no warranty.
 - Neither I, nor any of NZXTSharp's collaborators hold any liability for any broken or non-functional hardware resulting from use of this package. Please use your best judgement.
 
Please keep in mind that NZXTSharp is in development, and will have breaking changes in the future.
 
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
