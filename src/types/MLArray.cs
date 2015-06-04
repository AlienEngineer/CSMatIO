using System;

namespace csmatio.types
{
	/// <summary>
	/// A base class that represents a generic Matlab Array object
	/// </summary>
	/// <author>David Zier (david.zier@gmail.com)</author>
	public class MLArray
	{
		/* Matlab array types (Classes) */
		/// <summary>Unknown Class</summary>
		public const int mxUNKNOWN_CLASS	= 0;
		/// <summary>Cell Array Class</summary>
		public const int mxCELL_CLASS		= 1;
		/// <summary>Structure Class</summary>
		public const int mxSTRUCT_CLASS		= 2;
		/// <summary>Object Class</summary>
		public const int mxOBJECT_CLASS		= 3;
		/// <summary>Character Array Class</summary>
		public const int mxCHAR_CLASS		= 4;
		/// <summary>Sparse Array Class</summary>
		public const int mxSPARSE_CLASS		= 5;
		/// <summary>Double Precision Array Class</summary>
		public const int mxDOUBLE_CLASS		= 6;
		/// <summary>Single Precision Array Class</summary>
		public const int mxSINGLE_CLASS		= 7;
		/// <summary>8-bit, Signed Integer Class</summary>
		public const int mxINT8_CLASS		= 8;
		/// <summary>8-bit, Unsigned Integer Class</summary>
		public const int mxUINT8_CLASS		= 9;
		/// <summary>16-bit, Signed Integer Class</summary>
		public const int mxINT16_CLASS		= 10;
		/// <summary>16-bit, Unsigned Integer Class</summary>
		public const int mxUINT16_CLASS		= 11;
		/// <summary>32-bit, Signed Integer Class</summary>
		public const int mxINT32_CLASS		= 12;
		/// <summary>32-bit, Unsigned Integer Class</summary>
		public const int mxUINT32_CLASS		= 13;
		/// <summary>64-bit, Signed Integer Class</summary>
		public const int mxINT64_CLASS		= 14;
		/// <summary>64-bit, Unsigned Integer Class</summary>
		public const int mxUINT64_CLASS		= 15;
		/// <summary>Function Handle Class</summary>
		public const int mxFUNCTION_CLASS	= 16;
		/// <summary>Opaque Class</summary>
		public const int mxOPAQUE_CLASS		= 17;

		/* Matlab array flags */
		/// <summary>Complex Flag</summary>
		public const int mtFLAG_COMPLEX		= 0x0800;
		/// <summary>Global Flag</summary>
		public const int mtFLAG_GLOBAL		= 0x0400;
		/// <summary>Logical Flag</summary>
		public const int mtFLAG_LOGICAL		= 0x0200;
		/// <summary>Mask for Flag to determine Flag Type</summary>
		public const int mtFLAG_TYPE		= 0xFF;

        /// <summary>
        /// The array dimensions.
        /// </summary>
		protected int[] _dims;
        /// <summary>
        /// The name of the array, default name is '@'.
        /// </summary>
		protected string _name;
        /// <summary>
        /// Any <c>mtFLAG</c> type of attributes.
        /// </summary>
		protected int _attributes;
        /// <summary>
        /// The <c>mxCLASS</c> MATLAB Array Types (Classes).
        /// </summary>
		protected int _type;

		/// <summary>
		/// Construct an MLArray object
		/// </summary>
		/// <param name="name">The name of the MLArray object.</param>
		/// <param name="dims">A dimensions array for the object.</param>
		/// <param name="type">The Matlab Array Type</param>
		/// <param name="attributes">Attribute parameters for the array.</param>
		public MLArray( string name, int[] dims, int type, int attributes )
		{
			_dims = new int[dims.Length];
			Array.Copy( dims, 0, _dims, 0, dims.Length );

			if( name != null && !name.Equals("") )
				_name = name;
			else
				_name = "@"; // Default name

			_type = type;
			_attributes = attributes;
		}

		/// <summary>Gets the name of the array.</summary>
		public string Name{ get{ return _name; } }

		/// <summary>Gets the flags for this array.</summary>
		public virtual int Flags
        { 
            get{ return (int)((uint)(_type & mtFLAG_TYPE) | (uint)(_attributes & 0xFFFFFF00)); } 
        }

		/// <summary>
		/// Converts the name string into a byte array and returns it.
		/// </summary>
		/// <returns>A byte array.</returns>
		public byte[] GetNameToByteArray()
		{
			byte[] bs = new byte[ _name.Length ];
			for( int i = 0; i < _name.Length; i++ )
				bs[i] = (byte)_name[i];
			return bs;
		}

		/// <summary>
		/// Gets the arrays dimensions.
		/// </summary>
		public int[] Dimensions
		{
			get
			{
				int[] ai = null;
				if( _dims != null )
				{
					ai = new int[_dims.Length];
					Array.Copy( _dims, 0, ai, 0, _dims.Length );
				}
				return ai;
			}
		}

		/// <summary>Get the M dimension.</summary>
		public int M{ get{ return( (_dims != null) ? _dims[0] : 0 ); } }

		/// <summary>Get the N dimension.</summary>
		public int N
		{ 
			get
			{
				int i = 0;
				if( _dims != null )
				{
					if( _dims.Length > 2 )
					{
						i = 1;
						for( int j = 1; j < _dims.Length; j++ )
							i *= _dims[j];
					}
					else
						i = _dims[1];
				}
				return i;
			} 
		}
		
		/// <summary>
		/// Get the N dimensions or the size of the dimensions array
		/// </summary>
		public int NDimensions{ get{ return( (_dims != null) ? _dims.Length : 0 ); } }
		
		/// <summary>
		/// Get the size of the array.
		/// </summary>
		public int Size{ get{ return M * N; } }

		/// <summary>
		/// Get the Matlab Array Type for this array
		/// </summary>
		public int Type{ get{ return _type; } }

		/// <summary>
		/// Is the array empty?
		/// </summary>
		public bool IsEmpty{ get{ return N == 0; } }

		/// <summary>
		/// Converts a Matlab Array Class type into a string representation.
		/// </summary>
		/// <param name="type">A Matlab Array Class type</param>
		/// <returns>A string representation.</returns>
		public static string TypeToString( int type )
		{
			switch( type )
			{
				case mxUNKNOWN_CLASS:
					return "unknown";
				case mxCELL_CLASS:
					return "cell";
				case mxSTRUCT_CLASS:
					return "struct";
				case mxCHAR_CLASS:
					return "char";
				case mxSPARSE_CLASS:
					return "sparse";
				case mxDOUBLE_CLASS:
					return "double";
				case mxSINGLE_CLASS:
					return "single";
				case mxINT8_CLASS:
					return "int8";
				case mxUINT8_CLASS:
					return "uint8";
				case mxINT16_CLASS:
					return "int16";
				case mxUINT16_CLASS:
					return "uint16";
				case mxINT32_CLASS:
					return "int32";
				case mxUINT32_CLASS:
					return "uint32";
				case mxINT64_CLASS:
					return "int64";
				case mxUINT64_CLASS:
					return "uint64";
				case mxFUNCTION_CLASS:
					return "function_handle";
				case mxOPAQUE_CLASS:
					return "opaque";
				case mxOBJECT_CLASS:
					return "object";
				default:
					return "unknown";
			}
		}

		/// <summary>Is Array a Cell Class Type?</summary>
		public bool IsCell{ get{ return _type == mxCELL_CLASS; } }

		/// <summary>Is Array a Char Class Type?</summary>
		public bool IsChar{ get{ return _type == mxCHAR_CLASS; } }

		/// <summary>Is Array a Complex Number?</summary>
		public bool IsComplex{ get{ return (_attributes & mtFLAG_COMPLEX) != 0; } }
		
		/// <summary>Is Array a Sparse Array Class Type?</summary>
		public bool IsSparse{ get{ return _type == mxSPARSE_CLASS; } }

		/// <summary>Is Array a Struct Type?</summary>
		public bool IsStruct{ get{ return _type == mxSTRUCT_CLASS; } }

		/// <summary>Is Array a Double Precision Type?</summary>
		public bool IsDouble{ get{ return _type == mxDOUBLE_CLASS; } }

		/// <summary>Is Array a Single Precision Type?</summary>
		public bool IsSingle{ get{ return _type == mxSINGLE_CLASS; } }

		/// <summary>Is Array a 8-bit Signed Integer Type?</summary>
		public bool IsInt8{ get{ return _type == mxINT8_CLASS; } }

		/// <summary>Is Array a 16-bit Signed Integer Type?</summary>
		public bool IsInt16{ get{ return _type == mxINT16_CLASS; } }

		/// <summary>Is Array a 32-bit Signed Integer Type?</summary>
		public bool IsInt32{ get{ return _type == mxINT32_CLASS; } }

		/// <summary>Is Array a 8-bit Unsigned Integer Type?</summary>
		public bool IsUInt8{ get{ return _type == mxUINT8_CLASS; } }

		/// <summary>Is Array a 16-bit Unsigned Integer Type?</summary>
		public bool IsUInt16{ get{ return _type == mxUINT16_CLASS; } }

		/// <summary>Is Array a 32-bit Unsigned Integer Type?</summary>
		public bool IsUInt32{ get{ return _type == mxUINT32_CLASS; } }

		/// <summary>Is Array a 64-bit Signed Integer Type?</summary>
		public bool IsInt64{ get{ return _type == mxINT64_CLASS; } }

		/// <summary>Is Array a 64-bit Unsigned Integer Type?</summary>
		public bool IsUInt64{ get{ return _type == mxUINT64_CLASS; } }

		/// <summary>Is Array an Object Type?</summary>
		public bool IsObject{ get{ return _type == mxOBJECT_CLASS; } }

		/// <summary>Is Array an Opaque Type?</summary>
		public bool IsOpaque{ get{ return _type == mxOPAQUE_CLASS; } }

		/// <summary>Is Array a logical value?</summary>
		public bool IsLogical{ get{ return (_attributes & mtFLAG_LOGICAL) != 0; } }

		/// <summary>Is Array a Function Object?</summary>
		public bool IsFunctionObject{ get{ return _type == mxFUNCTION_CLASS; } }

		/// <summary>Is Array an Unknown Type?</summary>
		public bool IsUnknown{ get{ return _type == mxUNKNOWN_CLASS; } }

		/// <summary>
		/// Get the index into the byte array.
		/// </summary>
		/// <param name="m">The m index of an MxN array.</param>
		/// <param name="n">The n index of an MxN array.</param>
		/// <returns>An index into the byte array.</returns>
		protected int GetIndex( int m, int n )
		{
			return m+n*M;
		}

		/// <summary>
		/// A string representation for this MLArray object
		/// </summary>
		/// <returns>A string representation.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			if( _dims != null )
			{
				sb.Append("[");
				if( _dims.Length > 3 )
				{
					sb.Append(_dims.Length + "D");
				}
				else
				{
					sb.Append(_dims[0] + "x" + _dims[1]);
					if( _dims.Length == 3)
						sb.Append( "x" + _dims[2] );
				}
				sb.Append( "  " + TypeToString( _type ) + " array" );
				if( IsSparse )
				{
					sb.Append(" (sparse");
					if( IsComplex )
						sb.Append(" complex");
					sb.Append(")");
				}
				else if( IsComplex )
				{
					sb.Append(" (complex)");
				}
				sb.Append("]");
			}
			else
			{
				sb.Append("[invalid]");
			}
			return sb.ToString();
		}

		/// <summary>
		/// Get a string representation for the content of the array.
		/// </summary>
		/// <returns>A string representation.</returns>
		public virtual string ContentToString()
		{
			return "content cannot be displayed";
		}
	}
}
