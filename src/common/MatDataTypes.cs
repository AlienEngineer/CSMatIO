using System;

namespace csmatio.common
{
	/// <summary>
	/// MAT-file data types
	/// </summary>
	/// <author>David Zier (david.zier@gmail.com)</author>
	public class MatDataTypes
	{
		/// <summary>Unknown data type</summary>
		public const int miUNKNOWN		= 0;
		/// <summary>8-bit, signed</summary>
		public const int miINT8			= 1;
		/// <summary>8-bit, unsigned</summary>
		public const int miUINT8		= 2;
		/// <summary>16-bit, signed</summary>
		public const int miINT16		= 3;
		/// <summary>16-bit, unsigned</summary>
		public const int miUINT16		= 4;
		/// <summary>32-bit, signed</summary>
		public const int miINT32		= 5;
		/// <summary>32-bit, unsigned</summary>
		public const int miUINT32		= 6;
		/// <summary>IEEE 754 single format</summary>
		public const int miSINGLE		= 7;
		/// <summary>IEEE 754 double format</summary>
		public const int miDOUBLE		= 9;
		/// <summary>64-bit, signed</summary>
		public const int miINT64		= 12;
		/// <summary>64-bit, unsigned</summary>
		public const int miUINT64		= 13;
		/// <summary>MATLAB array</summary>
		public const int miMATRIX		= 14;
		/// <summary>Compressed data</summary>
		public const int miCOMPRESSED	= 15;
		/// <summary>Unicode UTF-8 Encoded Character Data</summary>
		public const int miUTF8			= 16;
		/// <summary>Unicode UTF-16 Encoded Character Data</summary>
		public const int miUTF16		= 17;
		/// <summary>Unicode UTF-12 Encoded Character Data</summary>
		public const int miUTF32		= 18;

		/// <summary>Size of 64-bit, signed int in bytes</summary>
		public const int miSIZE_INT64 = 8;
		/// <summary>Size of 32-bit, signed int in bytes</summary>
		public const int miSIZE_INT32 = 4;
		/// <summary>Size of 16-bit, signed int in bytes</summary>
		public const int miSIZE_INT16	= 2;
		/// <summary>Size of 8-bit, signed int in bytes</summary>
		public const int miSIZE_INT8	= 1;
		/// <summary>Size of 64-bit, unsigned int in bytes</summary>
		public const int miSIZE_UINT64 = 8;
		/// <summary>Size of 32-bit, unsigned int in bytes</summary>
		public const int miSIZE_UINT32 = 4;
		/// <summary>Size of 16-bit, unsigned int in bytes</summary>
		public const int miSIZE_UINT16	= 2;
		/// <summary>Size of 8-bit, unsigned int in bytes</summary>
		public const int miSIZE_UINT8	= 1;
		/// <summary>Size of IEEE 754 double in bytes</summary>
		public const int miSIZE_DOUBLE = 8;
		/// <summary>Size of IEEE 754 single in bytes</summary>
		public const int miSIZE_SINGLE = 4;
		/// <summary>Size of an 8-bit char in bytes</summary>
		public const int miSIZE_CHAR	= 1;

		/// <summary>
		/// Return the number of bytes for a given type.
		/// </summary>
		/// <param name="type">MatDataTypes</param>
		/// <returns>Size of the type.</returns>
		public static int SizeOf( int type )
		{
			switch( type )
			{
				case MatDataTypes.miINT8:
					return miSIZE_INT8;
				case MatDataTypes.miUINT8:
					return miSIZE_UINT8;
				case MatDataTypes.miINT16:
					return miSIZE_INT16;
				case MatDataTypes.miUINT16:
					return miSIZE_UINT16;
				case MatDataTypes.miINT32:
					return miSIZE_INT32;
				case MatDataTypes.miUINT32:
					return miSIZE_UINT32;
				case MatDataTypes.miINT64:
					return miSIZE_INT64;
				case MatDataTypes.miUINT64:
					return miSIZE_UINT64;
				case MatDataTypes.miDOUBLE:
					return miSIZE_DOUBLE;
				case MatDataTypes.miSINGLE:
					return miSIZE_SINGLE;
				default:
					return 1;
			}
		}

		/// <summary>
		/// Get a string representation of a data type
		/// </summary>
		/// <param name="type">A MatDataTypes data type</param>
		/// <returns>String representation.</returns>
		public static string TypeToString( int type )
		{
			switch( type )
			{
				case MatDataTypes.miUNKNOWN:
					return "unknown";
				case MatDataTypes.miINT8:
					return "int8";
				case MatDataTypes.miINT16:
					return "int16";
				case MatDataTypes.miINT32:
					return "int32";
				case MatDataTypes.miUINT8:
					return "uint8";
				case MatDataTypes.miUINT16:
					return "uint16";
				case MatDataTypes.miUINT32:
					return "uint32";
				case MatDataTypes.miSINGLE:
					return "single";
				case MatDataTypes.miDOUBLE:
					return "double";
				case MatDataTypes.miINT64:
					return "int64";
				case MatDataTypes.miUINT64:
					return "uint64";
				case MatDataTypes.miMATRIX:
					return "matrix";
				case MatDataTypes.miCOMPRESSED:
					return "compressed";
				case MatDataTypes.miUTF8:
					return "utf8";
				case MatDataTypes.miUTF16:
					return "utf16";
				case MatDataTypes.miUTF32:
					return "utf32";
				default:
					return "unknown";
			}
		}
	}
}
