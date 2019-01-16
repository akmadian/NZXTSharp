# NZXTSharp

NZXTSharp is a C# package that allows interaction with NZXT's Hue+. Support for other devices will be coming.

Find NZXTSharp on NuGet [here][0]. If you are adding through Visual Studio, please be sure to check `Include Prereleases`.

**Please keep in mind that NZXTSharp is in heavy development, and will have breaking changes in the future.**
<br>**If a device you want is not supported yet, please [submit an issue][1] with the `device-request` label.**
<br>**NZXTSharp is in very early development, and very little is supported at the moment. Please keep checking back!**

### Syntax
NZXTSharp's syntax is lightweight, only taking a few lines to get started.

```C#
using NZXTSharp;
using NZXTSharp.Devices;
using NZXTSharp.Effects;

HuePlus hue = new HuePlus(); // Create device
Fixed effect = new Fixed(new HexColor(255, 255, 255)); // Make fixed effect
hue.ApplyEffect(hue.Both, effect); // Apply effect
```

### FAQ

**Q:** Are there any plans to make an SDK in XX language?
<br>**A:** As of yet, no. I am focusing on support for the .NET framework. Python is most likely next.

**Q:** Is XX device supported? 
<br>**A:** Right now, I am working on support for the Hue+. Once that is complete, I plan to move into the Krakens and the Hue 2.


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
