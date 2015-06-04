using System;
using csmatio.common;

namespace csmatio.types
{
	/// <summary>
	/// This class represents an Int64 (long) array (matrix)
	/// </summary>
	/// <remarks>
	/// <para>
	/// Davids original comment: 
	/// For some reason, Matlab sees the <c>mxINT64</c> class as an array of
	/// doubles, so in order to get this to work, <c>MLInt64</c> actually converts
	/// all of the long data to doubles.
	/// </para>
	/// <para>
	/// Tobias: I changed this behaviour into a more "standard" behaviour because I couldn't reproduce Davids findings.
	/// </para>
	/// </remarks>
	/// <author>David Zier (david.zier@gmail.com)</author>
	public class MLInt64 : MLNumericArray<long>
	{
		#region Constructors

		/// <summary>
		/// Normally this constructor is used only by <c>MatFileReader</c> and <c>MatFileWriter</c>
		/// </summary>
		/// <param name="Name">Array name</param>
		/// <param name="Dims">Array dimensions</param>
		/// <param name="Type">Array type: here <c>mxINT64_CLASS</c></param>
		/// <param name="Attributes">Array flags</param>
		public MLInt64(string Name, int[] Dims, int Type, int Attributes)
			: base(Name, Dims, Type, Attributes) { }

		/// <summary>
		/// Create a <c>MLInt64</c> array with given name and dimensions.
		/// </summary>
		/// <param name="Name">Array name</param>
		/// <param name="Dims">Array dimensions</param>
		public MLInt64(string Name, int[] Dims)
			: base(Name, Dims, MLArray.mxINT64_CLASS, 0) { }

		/// <summary>
		/// <a href="http://math.nist.gov/javanumerics/jama/">Jama</a> [math.nist.gov] style:
		/// construct a 2D real matrix from a one-dimensional packed array.
		/// </summary>
		/// <param name="Name">Array name</param>
		/// <param name="vals">One-dimensional array of <c>long</c>, packed by columns</param>
		/// <param name="m">Number of rows</param>
		public MLInt64(string Name, long[] vals, int m)
			: base(Name, MLArray.mxINT64_CLASS, vals, m) { }

		/// <summary>
		/// <a href="http://math.nist.gov/javanumerics/jama/">Jama</a> [math.nist.gov] style:
		/// construct a 2D real matrix from <c>long[][]</c>.
		/// </summary>
		/// <remarks>Note: Array is converted to <c>long[]</c></remarks>
		/// <param name="Name">Array name</param>
		/// <param name="vals">Two-dimensional array of values</param>
		public MLInt64(string Name, long[][] vals)
			: this(Name, Helpers.Array2DTo1D<long>(vals), vals.Length) { }

		/// <summary>
		/// <a href="http://math.nist.gov/javanumerics/jama/">Jama</a> [math.nist.gov] style:
		/// construct a 2D imaginary matrix from a one-dimensional packed array.
		/// </summary>
		/// <param name="Name">Array name</param>
		/// <param name="Real">One-dimensional array of <c>long</c> for <i>real</i> values, packed by columns</param>
		/// <param name="Imag">One-dimensional array of <c>long</c> for <i>imaginary</i> values, packed by columns</param>
		/// <param name="M">Number of rows</param>
		public MLInt64(string Name, long[] Real, long[] Imag, int M)
			: base(Name, MLArray.mxINT64_CLASS, Real, Imag, M) { }


		/// <summary>
		/// <a href="http://math.nist.gov/javanumerics/jama/">Jama</a> [math.nist.gov] style:
		/// construct a 2D imaginary matrix from a one-dimensional packed array.
		/// </summary>
		/// <param name="Name">Array name</param>
		/// <param name="Real">One-dimensional array of <c>long</c> for <i>real</i> values, packed by columns</param>
		/// <param name="Imag">One-dimensional array of <c>long</c> for <i>imaginary</i> values, packed by columns</param>
		public MLInt64(string Name, long[][] Real, long[][] Imag)
			: this(Name, Helpers.Array2DTo1D<long>(Real), Helpers.Array2DTo1D<long>(Imag), Real.Length) { }

		#endregion

		/// <summary>
		/// Builds a numeric object from a byte array.
		/// </summary>
		/// <param name="bytes">A byte array containing the data.</param>
		/// <returns>A numeric object</returns>
		protected override object BuildFromBytes2(byte[] bytes)
		{
			return BitConverter.ToInt64(bytes, 0);
		}

		/// <summary>
		/// Gets a byte array from a numeric object.
		/// </summary>
		/// <param name="val">The numeric object to convert into a byte array.</param>
		public override byte[] GetByteArray(object val)
		{
			return BitConverter.GetBytes((long)val);
		}
	}
}
