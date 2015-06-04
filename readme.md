# CSMatIO #

CSMatIO is a .NET Library to read, write, and manipulate Matlab binary MAT-files
written by David Zier. He published his code on
[Matlab Central](http://www.mathworks.com/matlabcentral/fileexchange/16319)

I found his work really useful. It had some bugs however, so I try to provide some fixes and enhancements here.

CSMatIO uses ZLIB.NET (a managed version of the ZLIB library) to compress/decompress MAT-file content. ZLIB.NET is open source provided by ComponentAce (<http://www.componentace.com/>).

For license text see below.

------------------------------------------------------------------------

# Original Readme from David Zier #

CSMatIO is a .NET Library to read, write, and manipulate Matlab binary
MAT-files.

CSMatIO is a managed version of the Matlab MAT-File I/O API written entirly in C#.  As is with
most of my projects, CSMatIO was originally intended as a small support application for my Ph.D.
research, but quickly grew out of control into a full API.  CSMatIO was orinally based off of 
converting the Java source code to C# from the JMatIO library written by Wojciech Gradkowski, 
but eventually grew morphed into its current version.  Mr. Gradkowski's CSMatIO is, I am sure,
an excellent utility, but unfortionately is imcompatable with Microsoft's .NET Architecture.

This library was written for people who develope .NET applications and would like to import or
export there data into Matlab for further processing or to use Matlabs superior graphing 
capabilities.  It was written with Visual Studio .NET 2005 and requires the .NET 2.0 
Architecture set to be installed.

If you would like to send comments, improvement requests, or to criticize the project please 
email me: david.zier@gmail.com 

Enjoy!

David A. Zier

------------------------------------------------------------------------

# CHANGE LOG #

## r13, 2014-10-15 ##

Switch from VS2005 to VS2013

- Default Target Framework Version is now v4.0
- v4.0 builds now eliminates the use of zlib.dll for (de)compressing,
  using System.IO.Compression instead.
  See https://sourceforge.net/p/csmatio/discussion/general/thread/3f380db4/
- v2.0 build ist still possible, see batch file "BuildAllReleaseVersions.bat"

## r11+r12, 2014-10-15 ##

FIXED sf ticket [#2]: "support all data types"

- Cleaned up the type-dependent code and comments.
- Changed the way several data types are read and written (I hope I changed it for good :-)
- Added/fixed support for some missing data types.
- Replaced some copy-and-paste-methods in the ML... classes with generic methods in MLNumericArray.cs,
  reducing the total amount of code lines.
- Added a button to the CSMatIOTest Form.
- Renamed ByteStorageSupport.cs -> IByteStorageSupport.cs, GenericArrayCreator.cs -> IGenericArrayCreator.cs

ATTENTION: csmatio now creates different binaries than before. This could result in compatibility issues in some cases.

## r10, 2014-10-15 ##

- FIX: removed "static" qualifier from MLNumericArray._bytes (see ticket [#7])
- FIX: MatFileReader c'tor: close the dataIn file stream when Exception is thrown (see ticket [#8])

## r9, 2014-02-25 ##

FIXED sf ticket No.6: "#6 zlib inflate broken?"

- new version 1.10 of zlib.net.dll seems to fix the problem
- new version 1.10 of zlib.net.dll has changed interface methods, so MatFileReader/MatFileWriter had to be adapted

Replaced various helper methods that converted 2D to 1D arrays with one generic method from the new file "Helpers.cs".

## r8, 2013-11-22 ##

FIXED sf ticket No.1: "problem with writing single prec array"
read/write support for single-prec floats was completely broken

(reported 2013-11-21 by Diego on [Matlab Central](http://www.mathworks.com/matlabcentral/fileexchange/16319))

## r7, 2013-06-20 ##

fixed MLNumericArray.Flags: should return

~~~~~~~
	(int)((uint)(base._type & MLArray.mtFLAG_TYPE) | (uint)(base._attributes & 0xFFFFFF00))
~~~~~~~

or it will make all the numberic data written as Double.

(contributed 2013-06-19 by Anton on [Matlab Central](http://www.mathworks.com/matlabcentral/fileexchange/16319))

## r6, 2012-08-29 ##

support for reading matrix types added: mxUINT16, mxINT16, mxUINT32, mxINT32
readme.txt -> readme.md

## r5, 2012-03-27 ##

support for reading "single" arrays added (tip on [Matlab Central](http://www.mathworks.com/matlabcentral/fileexchange/16319))

## r4, 2011-10-01 ##

FIX: saving empty strings was broken.

## r3, 2011-09-30 ##

FIX: name-based access to struct-array fields was partially broken due to string handling bug in MatFileReader.cs

## r2, 2007-09-10 ##

checked in Davids original code.

Currently supported data types are:

+ Double array
+ Single array
+ Char array
+ Structure
+ Cell array
+ Sparse array
+ Int8 array
+ UInt8 array
+ Int16 array
+ UInt16 array
+ Int32 array
+ UInt32 array
+ Int64 array
+ UInt64 array

------------------------------------------------------------------------

# CSMatIO License #

Copyright (c) 2007-2009, David Zier
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, 
are permitted provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice, 
  this list of conditions and the following disclaimer.
* Redistributions in binary form must reproduce the above copyright notice, 
  this list of conditions and the following disclaimer 
  in the documentation and/or other materials provided with the distribution.
      
THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS 
BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF 
SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
POSSIBILITY OF SUCH DAMAGE. 

# ZLIB.NET License #

Copyright (c) 2006-2007, ComponentAce
<http://www.componentace.com>
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, 
are permitted provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice, 
  this list of conditions and the following disclaimer. 
* Redistributions in binary form must reproduce the above copyright notice, 
  this list of conditions and the following disclaimer 
  in the documentation and/or other materials provided with the distribution. 

Neither the name of ComponentAce nor the names of its contributors 
may be used to endorse or promote products derived from this software 
without specific prior written permission. 

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS 
BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF 
SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
POSSIBILITY OF SUCH DAMAGE.
