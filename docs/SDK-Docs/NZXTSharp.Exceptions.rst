#####
NZXTSharp.Exceptions
#####

.. contents:: Table of Contents

---------

*****
Classes
*****

IncompatibleDeviceTypeException
=====
**thrown** 


IncompatibleEffectException
=====
**thrown** When an effect passed to a device's :code:`ApplyEffect()` method is not compatible with that device.

IncompatibleParamException
=====
**thrown** When a param object passed to an effect constructor is not compatible with that effect.

InvalidEffectSpeedException
=====
**thrown** When an invalid speed value is passed to a param or effect constructor.

Speed values must be 0 - 4 (inclusive); 0 being slowest, 2 being normal, and 4 being fastest.

MaxHandshakeRetryExceededException
=====
**thrown** When the maximum number of handshake attempts has been exceeded during device intitialization.

Max Retry Count is :code:`5` by default.

SubDeviceDoesNotExistException
=====
**thrown** When a user attempts to reference a subdevice that does not exist.

Ex: If there are only four fans connected to a given channel (SubDevices highest index: 3), and the user attempts to reference 
:code:`Channel.SubDevices[4]`, this exception will be thrown.

SubDeviceLEDDoesNotExistException
=====
**thrown** When a user attempts to reference a subdevice LED that does not exist.

Ex: If there is only one strip connected to a given channel (SubDevices.Leds highest index: 9), and the user attempts to reference 
:code:`Channel.SubDevices[0].Leds[10]`, this exception will be thrown.

TooManyColorsProvidedException
=====
**thrown** When a Color[] of length greater than 8 is passed to an effect constructor.
