#########
NZXTSharp
#########

Welcome to NZXTSharp's readthedocs page!

NZXTSharp is a .NET Standard package that facilitates interaction with NZXT devices. You can find NZXTSharp on `Nuget.org <https://www.nuget.org/packages/NZXTSharp>`_, and the source on `GitHub <https://github.com/akmadian/NZXTSharp>`_.

Docs here are organized by namespace.

NZXTSharp's syntax is designed to be simple and easy to use, almost like python. 
The structure is built around devices, effects, and params.

Effects are created with params, and effects are applied to devices. Devices are the main point that the user of the SDK will interact with. 

A basic getting started example with the Hue+: 

.. code-block:: csharp

  using NZXTSharp;
  using NZXTSharp.Devices;
  using NZXTSharp.Effects;
  using NZXTSharp.Params;
  
  HuePlus hue = new HuePlus();
  hue.OnLogMessage += HandlerMethod; // Subscribe to OnLogMessage event.
  
  Fixed effect = new Fixed(new HexColor(255, 255, 255)); // Create effect object
  hue.ApplyEffect(hue.Both, effect); // Apply the effect


.. toctree:: 
   :maxdepth: 1

   Examples/Getting-Started
   Supported
   
.. toctree:: 
   :maxdepth: 2
   :caption: Protocols

   Protocols/Hue+
   
.. toctree::
   :maxdepth: 2
   :caption: SDK-Docs

   .. toctree::
     :maxdepth: 1
     :caption: NZXTSharp
     SDK-Docs/NZXTSharp/Color
     SDK-Docs/NZXTSharp/Extensions

   .. toctree::
     :maxdepth: 1
     :caption: NZXTSharp.COM
     SDK-Docs/NZXTSharp.COM/ICOMController
     SDK-Docs/NZXTSharp.COM/SerialCOMData
     SDK-Docs/NZXTSharp.COM/SerialController
     SDK-Docs/NZXTSharp.COM/USBController

   .. toctree::
     :maxdepth: 1
     :caption: NZXTSharp.Devices
     SDK-Docs/NZXTSharp.Devices/ISerialDevice
     SDK-Docs/NZXTSharp.Devices/IUSBDevice
     SDK-Docs/NZXTSharp.Devices/ISubDevice
     SDK-Docs/NZXTSharp.Devices/IHueDevice
     SDK-Docs/NZXTSharp.Devices/Channel
     SDK-Docs/NZXTSharp.Devices/ChannelInfo
     SDK-Docs/NZXTSharp.Devices/Fan
     SDK-Docs/NZXTSharp.Devices/Strip
     SDK-Docs/NZXTSharp.Devices/HuePlus
     SDK-Docs/NZXTSharp.Devices/NZXTDeviceType
     SDK-Docs/NZXTSharp.Devices/SerialDeviceID


   .. toctree::
     :maxdepth: 1
     :caption: NZXTSharp.Effects
     SDK-Docs/NZXTSharp.Effects/IEffect
     SDK-Docs/NZXTSharp.Effects/Fixed
     SDK-Docs/NZXTSharp.Effects/Fading
     SDK-Docs/NZXTSharp.Effects/SpectrumWave
     SDK-Docs/NZXTSharp.Effects/Marquee
     SDK-Docs/NZXTSharp.Effects/CoveringMarquee
     SDK-Docs/NZXTSharp.Effects/Alternating
     SDK-Docs/NZXTSharp.Effects/Pulse
     SDK-Docs/NZXTSharp.Effects/Breathing
     SDK-Docs/NZXTSharp.Effects/CandleLight
     SDK-Docs/NZXTSharp.Effects/Wings

   .. toctree::
     :maxdepth: 1
     :caption: NZXTSharp.Params
     SDK-Docs/NZXTSharp.Params/02Param
     SDK-Docs/NZXTSharp.Params/03Param
     SDK-Docs/NZXTSharp.Params/CISS
     SDK-Docs/NZXTSharp.Params/Direction
     SDK-Docs/NZXTSharp.Params/LSS

   .. toctree::
     :maxdepth: 1
     :caption: NZXTSharp.Exceptions
     SDK-Docs/NZXTSharp.Exceptions/IncompatibleDeviceTypeException
     SDK-Docs/NZXTSharp.Exceptions/IncompatibleEffectException
     SDK-Docs/NZXTSharp.Exceptions/IncompatibleParamException
     SDK-Docs/NZXTSharp.Exceptions/InvalidEffectSpeedException
     SDK-Docs/NZXTSharp.Exceptions/InvalidParamException
     SDK-Docs/NZXTSharp.Exceptions/MaxHandshakeRetryExceededException
     SDK-Docs/NZXTSharp.Exceptions/SubDeviceDoesNotExistException
     SDK-Docs/NZXTSharp.Exceptions/SubDeviceLEDDoesNotExistException
     SDK-Docs/NZXTSharp.Exceptions/TooManyColorsProvidedException

   
