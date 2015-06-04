using System;
using csmatio.common;

namespace csmatio.types
{
	/// <summary>
	/// This class represents an Int32 (int) array (matrix)
	/// </summary>
	/// <author>David Zier (david.zier@gmail.com)</author>
	public class MLInt32 : MLNumericArray<int>
	{
		#region Constructors

		/// <summary>
		/// Normally this constructor is used only by <c>MatFileReader</c> and <c>MatFileWriter</c>
		/// </summary>
		/// <param name="Name">Array name</param>
		/// <param name="Dims">Array dimensions</param>
		/// <param name="Type">Array type: here <c>mxINT32_CLASS</c></param>
		/// <param name="Attributes">Array flags</param>
		public MLInt32(string Name, int[] Dims, int Type, int Attributes)
			: base(Name, Dims, Type, Attributes) { }

		/// <summary>
		/// Create a <c>MLInt32</c> array with given name and dimensions.
		/// </summary>
		/// <param name="Name">Array name</param>
		/// <param name="Dims">Array dimensions</param>
		public MLInt32(string Name, int[] Dims)
			: base(Name, Dims, MLArray.mxINT32_CLASS, 0) { }

		/// <summary>
		/// <a href="http://math.nist.gov/javanumerics/jama/">Jama</a> [math.nist.gov] style:
		/// construct a 2D real matrix from a one-dimensional packed array.
		/// </summary>
		/// <param name="Name">Array name</param>
		/// <param name="vals">One-dimensional array of <c>int</c>, packed by columns</param>
		/// <param name="m">Number of rows</param>
		public MLInt32(string Name, int[] vals, int m)
			: base(Name, MLArray.mxINT32_CLASS, vals, m) { }

		/// <summary>
		/// <a href="http://math.nist.gov/javanumerics/jama/">Jama</a> [math.nist.gov] style:
		/// construct a 2D real matrix from <c>int[][]</c>.
		/// </summary>
		/// <remarks>Note: Array is converted to <c>int[]</c></remarks>
		/// <param name="Name">Array name</param>
		/// <param name="vals">Two-dimensional array of values</param>
		public MLInt32(string Name, int[][] vals)
			: this(Name, Helpers.Array2DTo1D<int>(vals), vals.Length) { }

		/// <summary>
		/// <a href="http://math.nist.gov/javanumerics/jama/">Jama</a> [math.nist.gov] style:
		/// construct a 2D imaginary matrix from a one-dimensional packed array.
		/// </summary>
		/// <param name="Name">Array name</param>
		/// <param name="Real">One-dimensional array of <c>int</c> for <i>real</i> values, packed by columns</param>
		/// <param name="Imag">One-dimensional array of <c>int</c> for <i>imaginary</i> values, packed by columns</param>
		/// <param name="M">Number of rows</param>
		public MLInt32(string Name, int[] Real, int[] Imag, int M)
			: base(Name, MLArray.mxINT32_CLASS, Real, Imag, M) { }


		/// <summary>
		/// <a href="http://math.nist.gov/javanumerics/jama/">Jama</a> [math.nist.gov] style:
		/// construct a 2D imaginary matrix from a one-dimensional packed array.
		/// </summary>
		/// <param name="Name">Array name</param>
		/// <param name="Real">One-dimensional array of <c>int</c> for <i>real</i> values, packed by columns</param>
		/// <param name="Imag">One-dimensional array of <c>int</c> for <i>imaginary</i> values, packed by columns</param>
		public MLInt32(string Name, int[][] Real, int[][] Imag)
			: this(Name, Helpers.Array2DTo1D<int>(Real), Helpers.Array2DTo1D<int>(Imag), Real.Length) { }

		#endregion

		/// <summary>
		/// Builds a numeric object from a byte array.
		/// </summary>
		/// <param name="bytes">A byte array containing the data.</param>
		/// <returns>A numeric object</returns>
		protected override object BuildFromBytes2(byte[] bytes)
		{
			return BitConverter.ToInt32(bytes, 0);
		}

		/// <summary>
		/// Gets a byte array from a numeric object.
		/// </summary>
		/// <param name="val">The numeric object to convert into a byte array.</param>
		public override byte[] GetByteArray(object val)
		{
			return BitConverter.GetBytes((int)val);
		}
	}
}
