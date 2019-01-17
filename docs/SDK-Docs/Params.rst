#####
NZXTSharp.Params
#####

.. contents:: Table Of Contents

Classes
=====

*****
_02Param.cs
*****

Properties
^^^^^

:code:`int Value { get => GetValue(); }` Gets the param's value.

:code:`List<string> CompatibleWith { get; }` A list of all devices that the param is compatible with.

Constructors
^^^^^

To make an instace of an 02Param:

.. code-block:: csharp

   _02Param param = new _02Param();

Methods
^^^^^

:code:`int GetValue()` 
   Gets the Param's value, for 02Param, will always be 0x02
   
*****
_03Param.cs
*****

Properties
^^^^^

:code:`int Value { get => GetValue(); }` Gets the param's value.

:code:`List<string> CompatibleWith { get; }` A list of all devices that the param is compatible with.

Constructors
^^^^^

To make an instace of an 03Param:

.. code-block:: csharp

   _03Param param = new _03Param();

Methods
^^^^^

:code:`int GetValue()` 
   Gets the Param's value, for 023Param, will always be 0x03

*****
CISS.cs
*****

CISS stands for Color In Set/ Speed. CISS params are not meant to be used by the user, instead being used internally.

Properties
^^^^^

:code:`int Value { get => GetValue(); }` Gets the param's value.

:code:`List<string> CompatibleWith { get; }` A list of all devices that the param is compatible with.

Constructors
^^^^^

:code:`CISS(int speed) {}`

:code:`CISS(int colorIndex, int speed) {}`

Methods
^^^^^

:code:`int GetValue()` 
   Gets the Param's value, for 023Param, will always be 0x03

*****
LSS.cs
*****


*****
Direction.cs
*****
