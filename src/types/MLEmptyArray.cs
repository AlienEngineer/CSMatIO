using System;

namespace csmatio.types
{
	/// <summary>
	/// An Empty array class
	/// </summary>
	/// <author>David Zier (david.zier@gmail.com)</author>
	public class MLEmptyArray : MLArray
	{
		/// <summary>
		/// Create an basic empty array
		/// </summary>
		public MLEmptyArray() :
			base( null, new int[] {0,0}, mxDOUBLE_CLASS, 0)
		{}

		/// <summary>
		/// Ceate an basic empty array
		/// </summary>
		/// <param name="name">The name of the array</param>
		public MLEmptyArray( string name ) :
			base( name, new int[] {0,0}, mxDOUBLE_CLASS, 0)
		{
		}

		/// <summary>
		/// Construct an MLEmptyArray object.
		/// </summary>
		/// <param name="name">The name of the array</param>
		/// <param name="dims">The array dimensions</param>
		/// <param name="type">The type of array</param>
		/// <param name="attributes">Any attributes for this array</param>
		public MLEmptyArray( string name, int[] dims, int type, int attributes ) :
			base( name, dims, type, attributes )
		{
		}
	}
}
