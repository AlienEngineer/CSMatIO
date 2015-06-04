using System;
using csmatio.common;

namespace csmatio.types
{
	/// <summary>
	/// This class represents an Int8 array (matrix)
	/// </summary>
	/// <author>David Zier (david.zier@gmail.com)</author>
	public class MLInt8 : MLNumericArray<sbyte>
	{
		#region Constructors

		/// <summary>
		/// Normally this constructor is used only by <c>MatFileReader</c> and <c>MatFileWriter</c>
		/// </summary>
		/// <param name="Name">Array name</param>
		/// <param name="Dims">Array dimensions</param>
		/// <param name="Type">Array type: here <c>mxINT8_CLASS</c></param>
		/// <param name="Attributes">Array flags</param>
		public MLInt8(string Name, int[] Dims, int Type, int Attributes)
			: base(Name, Dims, Type, Attributes) { }

		/// <summary>
		/// Create a <c>MLInt8</c> array with given name and dimensions.
		/// </summary>
		/// <param name="Name">Array name</param>
		/// <param name="Dims">Array dimensions</param>
		public MLInt8(string Name, int[] Dims)
			: base(Name, Dims, MLArray.mxINT8_CLASS, 0) { }

		/// <summary>
		/// <a href="http://math.nist.gov/javanumerics/jama/">Jama</a> [math.nist.gov] style:
		/// construct a 2D real matrix from a one-dimensional packed array.
		/// </summary>
		/// <param name="Name">Array name</param>
		/// <param name="vals">One-dimensional array of doubles, packed by columns</param>
		/// <param name="m">Number of rows</param>
		public MLInt8(string Name, sbyte[] vals, int m)
			: base(Name, MLArray.mxINT8_CLASS, vals, m) { }

		/// <summary>
		/// <a href="http://math.nist.gov/javanumerics/jama/">Jama</a> [math.nist.gov] style:
		/// construct a 2D real matrix from <c>byte[][]</c>.
		/// </summary>
		/// <remarks>Note: Array is converted to <c>byte[]</c></remarks>
		/// <param name="Name">Array name</param>
		/// <param name="vals">Two-dimensional array of values</param>
		public MLInt8(string Name, sbyte[][] vals)
			: this(Name, Helpers.Array2DTo1D<sbyte>(vals), vals.Length) { }

		/// <summary>
		/// <a href="http://math.nist.gov/javanumerics/jama/">Jama</a> [math.nist.gov] style:
		/// construct a 2D imaginary matrix from a one-dimensional packed array.
		/// </summary>
		/// <param name="Name">Array name</param>
		/// <param name="Real">One-dimensional array of <c>byte</c> for <i>real</i> values, packed by columns</param>
		/// <param name="Imag">One-dimensional array of <c>byte</c> for <i>imaginary</i> values, packed by columns</param>
		/// <param name="M">Number of rows</param>
		public MLInt8(string Name, sbyte[] Real, sbyte[] Imag, int M)
			: base(Name, MLArray.mxINT8_CLASS, Real, Imag, M) { }


		/// <summary>
		/// <a href="http://math.nist.gov/javanumerics/jama/">Jama</a> [math.nist.gov] style:
		/// construct a 2D imaginary matrix from a one-dimensional packed array.
		/// </summary>
		/// <param name="Name">Array name</param>
		/// <param name="Real">One-dimensional array of <c>byte</c> for <i>real</i> values, packed by columns</param>
		/// <param name="Imag">One-dimensional array of <c>byte</c> for <i>imaginary</i> values, packed by columns</param>
		public MLInt8(string Name, sbyte[][] Real, sbyte[][] Imag)
			: this(Name, Helpers.Array2DTo1D<sbyte>(Real), Helpers.Array2DTo1D<sbyte>(Imag), Real.Length) { }

		#endregion

		/// <summary>
		/// Builds a numeric object from a byte array.
		/// </summary>
		/// <param name="bytes">A byte array containing the data.</param>
		/// <returns>A numeric object</returns>
		protected override object BuildFromBytes2(byte[] bytes)
		{
			////return BitConverter.ToInt8(bytes, 0);
			return bytes[0]; // faster than Bitconverter?
		}

		/// <summary>
		/// Gets a byte array from a numeric object.
		/// </summary>
		/// <param name="val">The numeric object to convert into a byte array.</param>
		public override byte[] GetByteArray(object val)
		{
			////return BitConverter.GetBytes((sbyte)val); BitConverter returns 2 bytes -> doesnt work this way
			return new byte[] { (byte)((sbyte)val) }; 
		}

		/// <summary>
		/// Gets a single objects data from a <c>ByteBuffer</c>.
		/// </summary>
		/// <remarks>Override to accelerate the performance</remarks>
		/// <param name="Buffer">The <c>ByteBuffer</c> object.</param>
		/// <param name="Index">A column-packed index.</param>
		/// <returns>The object data.</returns>
		protected override sbyte _Get(ByteBuffer Buffer, int Index)
		{
			return (sbyte)Buffer.Get(Index);
		}
	}
}
