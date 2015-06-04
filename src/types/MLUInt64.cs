using System;
using csmatio.common;

namespace csmatio.types
{
	/// <summary>
	/// This class represents an UInt64 (ulong) array (matrix)
	/// </summary>
	/// <remarks>
	/// <para>
	/// Davids original comment: 
	/// For some reason, Matlab sees the <c>mxUINT64</c> class as an array of
	/// doubles, so in order to get this to work, <c>MLUInt64</c> actually converts
	/// all of the ulong data to doubles.
	/// </para>
	/// <para>
	/// Tobias: I changed this behaviour into a more "standard" behaviour because I couldn't reproduce Davids findings.
	/// </para>
	/// </remarks>
	/// <author>David Zier (david.zier@gmail.com)</author>
	public class MLUInt64 : MLNumericArray<ulong>
	{
		#region Constructors

		/// <summary>
		/// Normally this constructor is used only by <c>MatFileReader</c> and <c>MatFileWriter</c>
		/// </summary>
		/// <param name="Name">Array name</param>
		/// <param name="Dims">Array dimensions</param>
		/// <param name="Type">Array type: here <c>mxUINT64_CLASS</c></param>
		/// <param name="Attributes">Array flags</param>
		public MLUInt64(string Name, int[] Dims, int Type, int Attributes)
			: base(Name, Dims, Type, Attributes) { }

		/// <summary>
		/// Create a <c>MLUInt64</c> array with given name and dimensions.
		/// </summary>
		/// <param name="Name">Array name</param>
		/// <param name="Dims">Array dimensions</param>
		public MLUInt64(string Name, int[] Dims)
			: base(Name, Dims, MLArray.mxUINT64_CLASS, 0) { }

		/// <summary>
		/// <a href="http://math.nist.gov/javanumerics/jama/">Jama</a> [math.nist.gov] style:
		/// construct a 2D real matrix from a one-dimensional packed array.
		/// </summary>
		/// <param name="Name">Array name</param>
		/// <param name="vals">One-dimensional array of <c>ulong</c>, packed by columns</param>
		/// <param name="m">Number of rows</param>
		public MLUInt64(string Name, ulong[] vals, int m)
			: base(Name, MLArray.mxUINT64_CLASS, vals, m) { }

		/// <summary>
		/// <a href="http://math.nist.gov/javanumerics/jama/">Jama</a> [math.nist.gov] style:
		/// construct a 2D real matrix from <c>ulong[][]</c>.
		/// </summary>
		/// <param name="Name">Array name</param>
		/// <param name="vals">Two-dimensional array of values</param>
		public MLUInt64(string Name, ulong[][] vals)
			: this(Name, Helpers.Array2DTo1D<ulong>(vals), vals.Length) { }

		/// <summary>
		/// <a href="http://math.nist.gov/javanumerics/jama/">Jama</a> [math.nist.gov] style:
		/// construct a 2D imaginary matrix from a one-dimensional packed array.
		/// </summary>
		/// <param name="Name">Array name</param>
		/// <param name="Real">One-dimensional array of <c>ulong</c> for <i>real</i> values, packed by columns</param>
		/// <param name="Imag">One-dimensional array of <c>ulong</c> for <i>imaginary</i> values, packed by columns</param>
		/// <param name="M">Number of rows</param>
		public MLUInt64(string Name, ulong[] Real, ulong[] Imag, int M)
			: base(Name, MLArray.mxUINT64_CLASS, Real, Imag, M) { }


		/// <summary>
		/// <a href="http://math.nist.gov/javanumerics/jama/">Jama</a> [math.nist.gov] style:
		/// construct a 2D imaginary matrix from a one-dimensional packed array.
		/// </summary>
		/// <param name="Name">Array name</param>
		/// <param name="Real">One-dimensional array of <c>ulong</c> for <i>real</i> values, packed by columns</param>
		/// <param name="Imag">One-dimensional array of <c>ulong</c> for <i>imaginary</i> values, packed by columns</param>
		public MLUInt64(string Name, ulong[][] Real, ulong[][] Imag)
			: this(Name, Helpers.Array2DTo1D<ulong>(Real), Helpers.Array2DTo1D<ulong>(Imag), Real.Length) { }

		#endregion

		/// <summary>
		/// Builds a numeric object from a byte array.
		/// </summary>
		/// <param name="bytes">A byte array containing the data.</param>
		/// <returns>A numeric object</returns>
		protected override object BuildFromBytes2(byte[] bytes)
		{
			return BitConverter.ToUInt64(bytes, 0);
		}

		/// <summary>
		/// Gets a byte array from a numeric object.
		/// </summary>
		/// <param name="val">The numeric object to convert into a byte array.</param>
		public override byte[] GetByteArray(object val)
		{
			return BitConverter.GetBytes((ulong)val);
		}
	}
}