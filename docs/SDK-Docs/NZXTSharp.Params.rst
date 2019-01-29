#####
NZXTSharp.Params
#####

All classes in the :code:`NZXTSharp.Params` namespace inherets from the :code:`IParam` interface.

For more information on what params are for, and when to use which ones, see the :code:`Protocols` section, or the `Effect Documentation <https://nzxtsharp.readthedocs.io/en/latest/SDK-Docs/Effects.html>`_.

Direction, speed, and LED size values match what is available in CAM.

.. contents:: Table Of Contents

*****
Interfaces
*****

IParam
=====
Properties
-----
**int Value { get => GetValue(); }** Gets the param's value.

**List<string> CompatibleWith { get; }** A list of all devices that the param is compatible with.

Methods
-----
**GetValue()** Returns the param's byte value as an int.

Operators
-----
**static implicit operator byte(IParam param)** Converts the result of the :code:`GetValue()` method, and returns it.

*****
Classes
*****
All param classes inherit from the IParam.cs interface.

CISS.cs
=====
CISS stands for Color In Set/ Speed. CISS params are not meant to be used by the user, instead being used internally.

Constructors
-----

**CISS(int speed)** Constructs a CISS param from the given speed.

**CISS(int colorIndex, int speed)** Constructs a CISS param from a given speed and index.

Speed values should be between 0 and 4 (inclusive). Defaults to 2.

colorIndex values should be between 0 and 7 (inclusive, zero-indexed).

LSS.cs
=====
LSS stands for LED Size/ Speed. This param is used to define speed, and the LED size of LED groups in some effects.

Constructors
-----
**CISS(int speed = 2, int LEDSize = 4)** Constructs an LSS param with a given speed and LEDSize.

Speed values should be between 0 and 4 (inclusive). Defaults to 2.

LEDSize values should be between 3 and 6 (inclusive). Deafults to 4

Direction.cs
=====
The direction param is used to specify the direction some effects move in, sometimes defining whether the effect moves smoothly or not.

Constructors
-----
**Direction(bool isForward, bool withMovement)** Constructs a Direction param with the given bool values.
    - param bool isForward    - Whether or not the effect will move forward or backward.
    - param bool withMovement - Whether or not the effect will move smoothly.


