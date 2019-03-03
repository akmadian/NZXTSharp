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
  
  HuePlus hue = new HuePlus();
  
  Fixed effect = new Fixed(new Color(255, 255, 255)); // Create effect object
  hue.ApplyEffect(hue.Both, effect); // Apply the effect

.. toctree::
  :maxdepth: 2
  :caption: SDK-Docs

  SDK-Docs/NZXTSharp
  SDK-Docs/NZXTSharp.COM  
  SDK-Docs/NZXTSharp.Devices
  SDK-Docs/NZXTSharp.Effects
  SDK-Docs/NZXTSharp.Params
  SDK-Docs/NZXTSharp.Exceptions
  Supported Devices and Features <Supported>
  
.. toctree::
  :maxdepth: 1
  :caption: Examples

  Examples/Getting-Started
  
.. toctree:: 
  :maxdepth: 2
  :caption: Protocols

  Protocols/Hue+
  Protocols/KrakenX

.. toctree:: 
  :maxdepth: 1

  Support
