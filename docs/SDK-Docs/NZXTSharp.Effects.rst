#####
NZXTSharp.Effects
#####

Classes in the :code:`NZXTSharp.Effects` are implementations of RGB effects in CAM. All effect classes inherit from the IEffect interface.

Only methods, properties, etc. **not** implemented by the IEffect interface will be documented. In addition, effect specific values like EffectBytes and EffectNames will be documented for each effect.

**Speed Params:** All speed values must be 0-4 (inclusive). 0 being slowest, 2 being normal, and 4 being fastest. Defaults to 2.

**Color arrays:** All Color arrays passed to effect constructors MUST be of length 8 or less unless otherwise noted. If too many colors are provided, a TooManyColorsProvidedException will be thrown.

.. contents:: Table Of Contents

----------

*****
Interfaces
*****

IEffect.cs
=====
Properties
-----
**int EffectByte { get; }** The effect's EffectByte

**string EffectName { get; }** The name of the effect.

**Channel Channel { get; set; }** The Channel the effect is set to. This is generally not used by the user of NZXTSharp.

Methods
-----
**List<byte[]> BuildBytes(Channel Channel)** Builds and returns the buffer queue needed to set the effect.

**bool IsCompatibleWith(NZXTDeviceType Type)** Checks to see if the IEffect is compatible with a given NZXTDeviceType.

*****
Classes
*****

Alternating.cs
=====
The alternating effect is the only effect that uses the :code:`withMovement` param in a Direction param constructor.

EffectByte = 0x05

EffectName = "Alternating" 

Constructors
-----
ALL Color[] passed to Alternating constructurs MUST be of length two, Otherwise, a TooManyColorsProvidedException will be thrown.

**Alternating(Color[] Colors)** Constructs an Alternating effect with the given Color[].

**Alternating(Color[] Colors, Direction Direction, int speed = 2)** Constructs an Alternating effect.
    - param Color[] Colors      - The Colors to display in the effect.
    - param Direction Direction - The direction for the effect to move in.
    - param int speed           - See **Speed Params** section at top of file.
    
**Alternating(Color Color1, Color Color2, Direction Direction, int speed = 2)** Constructs an Alternating effect.
    - param Color Color1        - The first Color to display.
    - param Color Color2        - The second Color to display.
    - param Direction Direction - The direction for the effect to move in.
    - param int speed           - See **Speed Params** section at top of file.

Breathing.cs
=====
EffectByte = 0x07

EffectName = "Breathing" 

Constructors
-----
**Breathing(Color[] Colors, int Speed = 2)** Constructs a Breathing effect.
    - param Color[] Colors - The Colors to display in the effect.
    - param int Speed      - See **Speed Params** section at top of file.

CandleLight.cs
=====
EffectByte = 0x09

EffectName = "CandleLight" 

Constructors
-----
**CandleLight(Color Color)** Constructs a CandleLight effect.
    - param Color Color - The Color to display.


CoveringMarquee,cs
=====
EffectByte = 0x04

EffectName = "CoveringMarquee" 

Constructors
-----
**CoveringMarquee(Color[] Colors, Direction Direction, int speed = 2)** Constructs a CoveringMarquee effect.
    - param Color[] Colors      - The Colors to display in the effect.
    - param Direction Direction - The direction for the effect to move in.
    - param int speed           - See **Speed Params** section at top of file.
    
**CoveringMarquee(Color Color1, Color Color2, Direction Direction, int speed = 2)** Constructs a CoveringMarquee effect.
    - param Color Color1        - The first Color to display.
    - param Color Color2        - The second Color to display.
    - param Direction Direction - The direction for the effect to move in.
    - param int speed           - See **Speed Params** section at top of file.

Fading.cs
=====
EffectByte = 0x01

EffectName = "Fading" 

Constructors
----
**Fading(Color[] Colors, int speed = 2)** Constructs a Fading effect.
    - param Color[] Colors - The Colors to display in the effect.
    - param int speed      - See **Speed Params** section at top of file.

Fixed.cs
=====
EffectByte = 0x00

EffectName = "Fixed" 

Constructors
-----
**Fixed(Color Color)** Constructs a Fixed effect.
    - param Color Color - The Color to display.

Marquee.cs
=====
EffectByte = 0x03

EffectName = "Marquee" 

Constructors
----
**Marquee(Color Color, Direction Direction, LSS LSS)** Constructs a Marquee effect.
    - param Color Color         - The Color to display.
    - param Direction Direction - The direction for the effect to move in.
    - param LSS LSS             - An LSS param denoting LED group size and speed.

Pulse.cs
=====
EffectByte = 0x06

EffectName = "Pulse" 

Constructors
-----
**Pulse(Color[] Colors, int Speed = 2)** Constructs a Pulse effect.
    - param Color[] Colors - The Colors to display in the effect.
    - param int Speed      - See **Speed Params** section at top of file.

SpectrumWave.cs
=====
EffectByte = 0x0c

EffectName = "Wings" 

Constructors
-----
**SpectrumWave(Direction Direction, int Speed = 2)** Constructs a Spectrum Wave effect.
    - param Direction Direction - The direction for the effect to move in.
    - param int Speed      - See **Speed Params** section at top of file.

Wings.cs
=====
EffectByte = 0x0c

EffectName = "Wings" 

Constructors
-----
**Wings(Color[] Colors, int Speed = 2)** Constructs a Wings effect.
    - param Color[] Colors - The Colors to display in the effect.
    - param int Speed      - See **Speed Params** section at top of file.
