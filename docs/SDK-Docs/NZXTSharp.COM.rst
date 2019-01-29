#####
NZXTSharp.COM
#####

The :code:`NZXTSharp.COM` namespace contains classes used for facilitating communication between INZXTDevices and their respective hardware.

Nothing in the COM namespace is meant to be accessed by the user of NZXTSharp. All classes serve infrastructural purposes.

.. contents:: Table Of Contents

---------

*****
Interfaces
*****

ICOMController.cs
=====
Currently empty.


*****
Classes
*****

SerialController.cs
=====
**Inherets** ICOMController.cs

Properties
-----
**SerialPort Port { get; }** The serial port operated by the SerialController.

**SerialCOMData StartData { get; }** The SerialCOMData used to start the SerialController.

**bool IsOpen { get => Port.IsOpen; }** Whether or not the SerialPort operated by the SerialController is open.

Constructors
-----
**SerialController(string[] PossiblePorts, SerialCOMData Data)**
    - param string[] PossiblePorts - A string array containing the ports to attempt opening. Format: "COM3".
    - param SerialCOMData Data     - A SerialCOMData object containing the information needed to open the port.

Methods
-----
**byte[] Write(byte[] buffer, int responselength)** Writes the bytes in the given buffer, and returns the device's response.
    - param byte[] buffer     - The bytes to write to the device.
    - param int reponselength - The expected length of the response: dictates the size of the returned buffer. Improper sizing will result in lost data.
        
**void WriteNoResponse(byte[] buffer)** Writes the bytes in the given buffer to the device. Does not return a response.
    - param byte[] buffer - The bytes to write to the device.
        
**void Reconnect()** Disposes of and reinitializes the SerialController instance.

**void Dispose()** Disposes of the SerialController instance.

SerialCOMData.cs
=====

Properties
-----
**System.IO.Ports.Parity Parity { get; }** The parity type to use.

**System.IO.Ports.StopBits StopBits { get; }** The stopbits to use.

**int WriteTimeout { get; }** The write timeout in ms.

**int ReadTimeout { get; }** The read timeout in ms.

**int Baud { get; }** The baud rate to open the port with.

**int DataBits { get; }** The number of databits to use.

Constructors
-----
The SerialCOMData class only has one constructor, and takes arguments corresponding to each of the available properties. The name parameter is optional, and defaults to an empty string.
