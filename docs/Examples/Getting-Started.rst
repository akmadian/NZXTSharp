#####
Getting Started with NZXTSharp
#####

This page contains getting started examples and code snippets.

NZXTSharp's syntax is designed to be simple and easy to use, almost like python. 
The structure is built around devices, effects, and params.

Effects are created with params, and effects are applied to devices. Devices 

*****
Hue+
*****

Boilerplate
-----

To get started, you'll need the following

.. code-block:: csharp

  using NZXTSharp;
  using NZXTSharp.Devices;
  using NZXTSharp.Effects;
  using NZXTSharp.Params;
  
  HuePlus hue = new HuePlus();
  hue.OnLogMessage += HandlerMethod; // Subscribe to OnLogMessage event.
  
  Fixed effect = new Fixed(new HexColor(255, 255, 255)); // Create effect object
  hue.ApplyEffect(hue.Both, effect); // Apply the effect
