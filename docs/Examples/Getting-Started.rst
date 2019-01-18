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

  using NZXTSharp.Devices;
  
  HuePlus hue = new HuePlus();
  

Applying Effects
-----
Effects are applied to a Hue+ device with the :code:`ApplyEffect()` method. The channel(s) to apply the effect to, and the effect object are passed as params to this method.

Adding on to the last example:

.. code-block:: csharp

  using NZXTSharp;
  using NZXTSharp.Devices;
  using NZXTSharp.Effects;
  
  HuePlus hue = new HuePlus();
  
  HexColor color = new HexColor(255, 255, 255); // You can make colors with RGB values
  HexColor color = new HexColor("#ffffff"); // Also works with Hex codes (with or without the leading #)
  
  Fixed effect = new Fixed(color); // Create effect
  
  hue.ApplyEffect(hue.Both, effect); // Apply effect to both channels
  hue.ApplyEffect(hue.Channel1, effect); // Or just one
  
