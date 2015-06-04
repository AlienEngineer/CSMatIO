using System;
using csmatio.common;

namespace csmatio.types
{
	/// <summary>
	/// This class represents an UInt32 (uint) array (matrix)
	/// </summary>
	/// <author>David Zier (david.zier@gmail.com)</author>
	public class MLUInt32 : MLNumericArray<uint>
	{
		#region Constructors

		/// <summary>
		/// Normally this constructor is used only by <c>MatFileReader</c> and <c>MatFileWriter</c>
		/// </summary>
		/// <param name="Name">Array name</param>
		/// <param name="Dims">Array dimensions</param>
		/// <param name="Type">Array type: here <c>mxUINT32_CLASS</c></param>
		/// <param name="Attributes">Array flags</param>
		public MLUInt32(string Name, int[] Dims, int Type, int Attributes)
			: base(Name, Dims, Type, Attributes) { }

		/// <summary>
		/// Create a <c>MLUInt32</c> array with given name and dimensions.
		/// </summary>
		/// <param name="Name">Array name</param>
		/// <param name="Dims">Array dimensions</param>
		public MLUInt32(string Name, int[] Dims)
			: base(Name, Dims, MLArray.mxUINT32_CLASS, 0) { }

		/// <summary>
		/// <a href="http://math.nist.gov/javanumerics/jama/">Jama</a> [math.nist.gov] style:
		/// construct a 2D real matrix from a one-dimensional packed array.
		/// </summary>
		/// <param name="Name">Array name</param>
		/// <param name="vals">One-dimensional array of <c>uint</c>, packed by columns</param>
		/// <param name="m">Number of rows</param>
		public MLUInt32(string Name, uint[] vals, int m)
			: base(Name, MLArray.mxUINT32_CLASS, vals, m) { }

		/// <summary>
		/// <a href="http://math.nist.gov/javanumerics/jama/">Jama</a> [math.nist.gov] style:
		/// construct a 2D real matrix from <c>uint[][]</c>.
		/// </summary>
		/// <param name="Name">Array name</param>
		/// <param name="vals">Two-dimensional array of values</param>
		public MLUInt32(string Name, uint[][] vals)
			: this(Name, Helpers.Array2DTo1D<uint>(vals), vals.Length) { }

		/// <summary>
		/// <a href="http://math.nist.gov/javanumerics/jama/">Jama</a> [math.nist.gov] style:
		/// construct a 2D imaginary matrix from a one-dimensional packed array.
		/// </summary>
		/// <param name="Name">Array name</param>
		/// <param name="Real">One-dimensional array of <c>ushort</c> for <i>real</i> values, packed by columns</param>
		/// <param name="Imag">One-dimensional array of <c>ushort</c> for <i>imaginary</i> values, packed by columns</param>
		/// <param name="M">Number of rows</param>
		public MLUInt32(string Name, uint[] Real, uint[] Imag, int M)
			: base(Name, MLArray.mxUINT32_CLASS, Real, Imag, M) { }


		/// <summary>
		/// <a href="http://math.nist.gov/javanumerics/jama/">Jama</a> [math.nist.gov] style:
		/// construct a 2D imaginary matrix from a one-dimensional packed array.
		/// </summary>
		/// <param name="Name">Array name</param>
		/// <param name="Real">One-dimensional array of <c>ushort</c> for <i>real</i> values, packed by columns</param>
		/// <param name="Imag">One-dimensional array of <c>ushort</c> for <i>imaginary</i> values, packed by columns</param>
		public MLUInt32(string Name, uint[][] Real, uint[][] Imag)
			: this(Name, Helpers.Array2DTo1D<uint>(Real), Helpers.Array2DTo1D<uint>(Imag), Real.Length) { }

		#endregion

		/// <summary>
		/// Builds a numeric object from a byte array.
		/// </summary>
		/// <param name="bytes">A byte array containing the data.</param>
		/// <returns>A numeric object</returns>
		protected override object BuildFromBytes2(byte[] bytes)
		{
			return BitConverter.ToUInt32(bytes, 0);
		}

		/// <summary>
		/// Gets a byte array from a numeric object.
		/// </summary>
		/// <param name="val">The numeric object to convert into a byte array.</param>
		public override byte[] GetByteArray(object val)
		{
			return BitConverter.GetBytes((uint)val);
		}
	}
}
