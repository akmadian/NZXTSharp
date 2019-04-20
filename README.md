# NZXTSharp

[![Nuget](https://img.shields.io/nuget/v/NZXTSharp.svg)](https://www.nuget.org/packages/NZXTSharp)
[![Discord](https://img.shields.io/badge/%20-Discord%20Server-blue.svg)](https://discord.gg/yK8m2CU)

NZXTSharp is a C# package that allows interaction with NZXT hardware.

You can find NZXTSharp on NuGet [here][0].

**Note:** Development of NZXTSharp is quickly becoming too expensive for me to continue. As a college student only working part time (~5 Hrs/ Week), I don't have much money to buy devices for testing. Due to NZXT being unwilling to sponsor this project (in a capacity where they would send me products for testing) at this time, and because most of my income is already spoken for, I have decided to temporarily suspend the addition of support for new devices. I still fully intend to continue development of this SDK when I can afford it, and will always be available for tech support.

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
<br>**A:** The Hue+ and KrakenX devices are fully supported, the Hue 2 or the Grid v3 is most likely next.

## Documentation
Docs are in a bit of a weird spot right now because I am working on transitioning docs from [NZXTSharp's readthedocs.io page][3] to [NZXTSharp's DocFX page][9]. <br>

 - The best place to go for source code documentation, and docs about the SDK's classes and their methods, supported devices and features, and support, is the [DocFX page][9]. <br>
 - If you want docs about the device protocols, and related SDKs/ Applications, check out the [readthedocs.io page][3].



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
 
NZXTSharp does not currently support any kind of rate limiting. I would reccomend rate limiting effect applications to ~90/ sec maximum. I believe that if you go over this, there is a potential for fried hardware, please use your best judgement.
 
Neither I (akmadian), or NZXTSharp are affiliated with NZXT.
 
Please keep in mind that NZXTSharp is in development, and will have breaking changes in the future.
 
A big thank you to [Pet0203][2], and [jonasmalacofilho][8] for their help in reverse engineering the protocols and building NZXTSharp.
 
###### </> With ♥ by Ari Madian

[0]: https://www.nuget.org/packages/NZXTSharp
[1]: https://github.com/akmadian/NZXTSharp/issues/new
[2]: https://github.com/Pet0203
[3]: https://nzxtsharp.readthedocs.io/en/latest/
[4]: https://github.com/akmadian/NZXTSharp/tree/docs-develop
[5]: https://github.com/antonpup/Aurora
[6]: https://github.com/DarthAffe/RGB.NET
[7]: https://discord.gg/yK8m2CU
[8]: https://github.com/jonasmalacofilho
[9]: http://nzxtsharp.jnhost.ml/

