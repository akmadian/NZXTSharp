#######
NZXTSharp
#######

This page is documentation for the NZXTSharp namepsace. 

The using statement for these contents is :code:`using NZXTSharp;`

.. contents:: Table Of Contents

*****
Classes
*****

HexColor.cs
=====

Constructors
-----
:code:`HexColor() {}`

:code:`HexColor(string hexColor) {}` Creates a HexColor instance from a hex color code.
  Params:
    - :code:`hexColor` - The color code to create a HexColor instance from. Can take colors with and without a leading #.
    
:code:`HexColor(int R, int G, int B) {}` Creates a HexColor instance from RGB values.
  Params:
    - :code:`R, G, B` - The red, green, and blue values to create the instance from. Values should be 0 - 255 (inclusive).

Methods
-----
:code:`byte[] AllOff() {}` Returns a byte[] of 40 `#000000` color codes in BRG format.

:code:`byte[] Expanded() {}` Returns a byte[] of 40 color codes in BRG format. The colors are the RGB fields of the HexColor instance.


*****
Extensions
*****

ByteArrExtensions
=====

Methods
-----
:code:`static byte[] ConcatenateByteArr(this byte[] thisone, byte[] other) {}` Concatenates two byte arrays.

StringExtensions
=====

Methods
-----
:code:`static string[] SplitEveryN(this string toSplit, int n) {}` Splits a string every n characters.
  Params:
    - :code:`n` - When to split the string.
  **Returns:** A string[] containing the split sections of the string.
 
:code:`static string StripSpaces(this string str) {}` Removes all spaces from a given string.

:code:`static string MultString(this string str, int n)` Multiplies a string. Ex: :code:`"test".MultString(2);` -> :code:`"testtest".
  Params:
    - :code:`n` - How many times the string should be multiplied
  **Returns:** The multiplied string.


