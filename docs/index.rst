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
   SDK-Docs/Supported
   
.. toctree:: 
   :maxdepth: 2
   :caption: Protocols

   Protocols/Hue+
   
.. toctree::
   :maxdepth: 2
   :caption: SDK-Docs

   SDK-Docs/NZXTSharp
   SDK-Docs/Devices
   SDK-Docs/Effects
   SDK-Docs/Params
   SDK-Docs/Exceptions
   SDK-Docs/COM
