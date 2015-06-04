using System;

namespace csmatio.types
{
	/// <summary>
	/// A public interface for storing and manipulating byte arrays
	/// </summary>
	public interface IByteStorageSupport
	{
		/// <summary>
		/// Gets the number of bytes allocated for a type
		/// </summary>
		int GetBytesAllocated { get; }

		/// <summary>
		/// Builds a numeric object from a byte array.
		/// </summary>
		/// <param name="bytes">A byte array containing the data.</param>
		/// <returns>A numeric object</returns>
		object BuildFromBytes(byte[] bytes);
		
		/// <summary>
		/// Gets a byte array from a numeric object.
		/// </summary>
		/// <param name="val">The numeric object to convert into a byte array.</param>
		byte[] GetByteArray(object val);
		
		/// <summary>
		/// Gets the type of numeric object that this byte storage represents
		/// </summary>
		Type GetStorageType { get; }
	}
}
