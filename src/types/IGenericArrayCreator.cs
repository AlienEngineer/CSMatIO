using System;

namespace csmatio.types
{
	/// <summary>
	/// Interface used to create a generic array GenericArrayCreator.
	/// </summary>
    /// <typeparam name="T">Generic Type for the array.</typeparam>
	public interface IGenericArrayCreator<T>
	{
		/// <summary>
		/// Creates a generic array.
		/// </summary>
		/// <param name="m">The number of columns in the array</param>
		/// <param name="n">The number of rows in the array</param>
		/// <returns>A generic array.</returns>
		T[] CreateArray( int m, int n );
	}
}
