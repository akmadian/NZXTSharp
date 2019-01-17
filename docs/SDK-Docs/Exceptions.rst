#####
NZXTSharp.Exceptions
#####

Exceptions are public, but are not meant for use by the user of the NZXTSharp SDK.

.. contents:: Table Of Contents

*****
InvalidParamException
*****
A generic exception; thrown when an invalid value is passed as a method or constructor param. The exception message should contain more information regarding the context of why the exception was thrown.


*****
IncompatibleEffectException
*****
Thrown when an incompatible effect is passed to a device's :code:`ApplyEffect` method.

*****
IncompatibleParamException
*****
Thrown when an incompatible param is passed to an effect or command constructor.

This exception differs from an InvalidParamException because an InvalidParamException is thrown when an invalid value is passed, and an IncompatibleParamException is thrown when the value is valid, but not compatible with the particular device it's been passed to.

*****
InvalidEffectSpeedException
*****
Thrown when an invalid effect speed param is passed to a param or effect constructor.

All speed params passed must be between 0 and 4 (inclusive). 0 is slowest, 2 is normal, and 4 is fastest.


*****
TooManyColorsProvidedException
*****
Thrown when a :code:`HexColor[]` passed to an effect constructor has too many colors in it.

The maximum number of colors that can be passed to an effect is 8, meaning that the :code:`HexColor[].Length` property should be 8 maximum.
