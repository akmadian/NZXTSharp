#####
NZXTSharp
#####

.. contents:: Table of Contents

---------

*****
Classes
*****

Color.cs
=====
The Color class is used by all NZXTSharp effects, and represents a Color

Constructors
-----
Color()
^^^^^

Color(int R, int G, int B)
^^^^^
Creates a Color instance from the provided R, G, B values.

Values must be 0 - 255 (inclusive).

Color(string hexColor)
^^^^^
Creates a Color instance from the provided hex color code. 

Supports color codes with a leading #: :code:`#ffffff`, or without: :code:`ffffff`.
