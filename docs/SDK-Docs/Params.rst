#####
NZXTSharp.Params
#####

All classes in the :code:`NZXTSharp.Params` namespace inherets from the :code:`IParam` interface.

For more information on what params are for, and when to use which ones, see the :code:`Protocols` section, or the `Effect Documentation <https://nzxtsharp.readthedocs.io/en/latest/SDK-Docs/Effects.html>`_.

.. contents:: Table Of Contents

Interfaces
=====

*****
IParam
*****

**Properties**

:code:`int Value { get => GetValue(); }` Gets the param's value.

:code:`List<string> CompatibleWith { get; }` A list of all devices that the param is compatible with.

**Methods**

:code:`int GetValue()` Returns the param's byte value as an int.


Classes
=====

All param classes have the following:

**Properties**

:code:`int Value { get => GetValue(); }` Gets the param's value.

:code:`List<string> CompatibleWith { get; }` A list of all devices that the param is compatible with.

**Methods**

:code:`int GetValue()` Returns the param's byte value as an int.

**Operators**

:code:`static implicit operator byte(IParam param)` Converts the result of the :code:`GetValue()` method, and returns it.

*****
_02Param.cs
*****

The _02Param's value will always be 0x02. The intent is not for the user to use this param, all uses of this class are internal.

**Constructors**

To make an instace of an 02Param:

.. code-block:: csharp

   _02Param param = new _02Param();
   
*****
_03Param.cs
*****

The _03Param's value will always be 0x03. The intent is not for the user to use this param, all uses of this class are internal.

**Constructors**

To make an instace of an 03Param:

.. code-block:: csharp

   _03Param param = new _03Param();

*****
CISS.cs
*****

CISS stands for Color In Set/ Speed. CISS params are not meant to be used by the user, instead being used internally.

**Constructors**

:code:`CISS(int speed) {}`

:code:`CISS(int colorIndex, int speed) {}`

Speed values should be between 0 and 4 (inclusive).

colorIndex values should be between 0 and 7 (inclusive, zero-indexed)


*****
LSS.cs
*****

LSS stands for LED Size/ Speed.

**Constructors**

:code:`CISS(int speed, int LEDSize) {}`

Speed values should be between 0 and 4 (inclusive).

LEDSize values should be between 3 and 6 (inclusive)

*****
Direction.cs
*****

The direction param is used 

**Constructors**

:code:`Direction(bool isForward, bool withMovement) {}`


