#####
NZXTSharp.Effects
#####

.. contents:: Table Of Contents

*****
Interfaces
*****

IEffect.cs
^^^^^

**Properties**

:code:`int EffectByte { get; }` The effect's EffectByte. See the `Protocols` Section for more information on EffectBytes.

:code:`string EffectName { get; }` The name of the effect, usually the name of the effect's class.

:code:`Channel Channel { get; set; }` The Channel the effect is being applied to.


**Methods**

:code:`List<byte[]> BuildBytes()` Builds the byte array(s) to be sent to the device.

:code:`bool IsCompatibleWith(string device)` Checks if the deviceName passed is in the effect's compatibility list. 

  Returns: true if the deviceName is in the list, false if the name is not in the list.
    

*****
Classes
*****

All effect contructors will require the channel they're being applied to as a parameter.

Alternating.cs
^^^^^

Breathing.cs
^^^^^

CandleLight.cs
^^^^^

CoveringMarquee.cs
^^^^^

Fading.cs
^^^^^

Marquee.cs
^^^^^

Pulse.cs
^^^^^

SpectrumWave.cs
^^^^^

Wings.cs
^^^^^
