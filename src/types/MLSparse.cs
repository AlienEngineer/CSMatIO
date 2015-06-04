using System;
using System.Collections.Generic;

namespace csmatio.types
{
	/// <summary>
	/// This class represents a Matlab Sparse matrix
	/// </summary>
	/// <author>David Zier (david.zier@gmail.com)</author>
	public class MLSparse : MLNumericArray<double>
	{
		private int _nzMax;
		private List<IndexMN> _indexSet;
		private Dictionary<IndexMN, double> _real;
		private Dictionary<IndexMN, double> _imaginary;

		/// <summary>
		/// Matrix index (m,n)
		/// </summary>
		/// <author>David Zier (david.zier@gmail.com)</author>
		private class IndexMN
		{
			private int _m;
			private int _n;

			/// <summary>
			/// Construct a basic <c>IndexMN</c> object from
			/// row and column indices.
			/// </summary>
			/// <param name="m">The row index</param>
			/// <param name="n">The column index</param>
			public IndexMN(int m, int n)
			{
				_m = m;
				_n = n;
			}

			/// <summary>
			/// Public accessor to the row index.
			/// </summary>
			public int M
			{
				get { return _m; }
				set { _m = value; }
			}

			/// <summary>
			/// Public accessor to the column index.
			/// </summary>
			public int N
			{
				get { return _n; }
				set { _n = value; }
			}

			/// <summary>
			/// Serves as a hash function for an IndexMN.
			/// </summary>
			/// <returns>A hashcode for this object</returns>
			public override int GetHashCode()
			{
				long l = (long)_m;
				l ^= (long)_n * 31L;
				return (int)l ^ (int)(l >> 32);
			}

			/// <summary>
			/// Overridden equals operator, see <see cref="System.Object.Equals(System.Object)"/>
			/// </summary>
			/// <param name="obj">A <c>System.Object</c> to be compared with.</param>
			/// <returns>True if the object match.</returns>
			public override bool Equals(object obj)
			{
				if (obj.GetType() == typeof(IndexMN))
				{
					return _m == ((IndexMN)obj).M && _n == ((IndexMN)obj).N;
				}
				return base.Equals(obj);
			}

			/// <summary>
			/// Get a string representation for this <c>IndexMN</c>.
			/// </summary>
			/// <returns>A <c>System.String</c></returns>
			public override string ToString()
			{
				return "{m=" + _m + ", n=" + _n + "}";
			}
		}

		#region Constructors

		/// <summary>
		/// Construct a new <c>MLSparse</c> object
		/// </summary>
		/// <param name="Name">Array name</param>
		/// <param name="Dims">Array dimensions</param>
		/// <param name="Attributes">Array flags</param>
		/// <param name="nzMax">Maximum number of non-zero numbers</param>
		public MLSparse(string Name, int[] Dims, int Attributes, int nzMax)
			: base(Name, Dims, MLArray.mxSPARSE_CLASS, Attributes)
		{
			_nzMax = nzMax;
			_real = new Dictionary<IndexMN, double>();
			_imaginary = new Dictionary<IndexMN, double>();
			_indexSet = new List<IndexMN>();
		}

		#endregion

		/// <summary>
		/// Gets the maximum number of non-zero values.
		/// </summary>
		public int MaxNZ
		{
			get { return _nzMax; }
		}

		/// <summary>
		/// Gets row indices
		/// </summary>
		/// <remarks><c>IR</c> points to an integer array of length <c>MaxNZ</c> containing the 
		/// row indices of the correspoding elements in <c>PR</c> and <c>PI</c></remarks>
		public int[] IR
		{
			get
			{
				int[] ir = new int[_nzMax];
				int i = 0;
				foreach (IndexMN index in _indexSet)
				{
					ir[i++] = index.M;
				}
				return ir;
			}
		}

		/// <summary>
		/// Gets column indices.
		/// </summary>
		/// <remarks>
		/// <c>JC</c> points to an integer array of length <c>N+1</c> that contains column index information.
		/// For j, in the range of <c>0&lt;=j&lt;=N-1</c>, <c>JC[j]</c> is the index in the <c>IR</c> and <c>PI</c>
		/// (and <c>PI</c> if it exists) of the first nonzero entry in the jth column and <c>JC[j+1]-1</c> index
		/// of the last nonzero entry. As a result, <c>JC[N]</c> is also equal to <c>nnz</c>, the number of
		/// nonzero entries in the matrix.  If <c>nnz</c> is less than <c>MaxNZ</c>, then more nonzero entries
		/// can be inserted in the array without allocating additional storage.
		/// </remarks>
		public int[] JC
		{
			get
			{
				int[] jc = new int[N + 1];

				// Create tmp array of nnz column indices
				int[] tmp = new int[_nzMax];
				int i = 0;
				foreach (IndexMN index in _indexSet)
				{
					tmp[i++] = index.N;
				}

				// Create JC
				int c = 0;
				for (int k = 0; k < jc.Length - 1; k++)
				{
					if (k < tmp.Length)
						c = tmp[k];
					jc[k] = c;
				}

				// last one is the nzMax
				jc[jc.Length - 1] = _nzMax;

				return jc;
			}
		}

		/// <summary>
		/// Creates a generic array.
		/// </summary>
		/// <param name="m">The number of columns in the array</param>
		/// <param name="n">The number of rows in the array</param>
		/// <returns>A generic array.</returns>
		public override double[] CreateArray(int m, int n)
		{
			return null;
		}

		/// <summary>
		/// Gets a single real array element of A(m,n).
		/// </summary>
		/// <param name="M">Row index</param>
		/// <param name="N">Column index</param>
		/// <returns>Array Element</returns>
		public override double GetReal(int M, int N)
		{
			IndexMN i = new IndexMN(M, N);
			if (_real.ContainsKey(i))
				return _real[i];
			return (double)0;
		}

		/// <summary>
		/// Get a single real array element
		/// </summary>
		/// <param name="Index">Column-packed vector index.</param>
		/// <returns>Array Element.</returns>
		/// <exception cref="ArgumentException">Always becuase a sparse array cannot be
		/// access by a column-packed index.</exception>
		public override double GetReal(int Index)
		{
			throw new ArgumentException("Can't get Sparse array elements by index. " +
				"Please use GetReal( int M, int N ) instead.");
		}

		/// <summary>
		/// Sets a single real array element.
		/// </summary>
		/// <param name="Val">The element value.</param>
		/// <param name="M">The row index.</param>
		/// <param name="N">The column index.</param>
		public override void SetReal(double Val, int M, int N)
		{
			IndexMN i = new IndexMN(M, N);
			if (!_indexSet.Contains(i))
				_indexSet.Add(i);
			_real.Add(i, Val);
		}

		/// <summary>
		/// Sets a single real array element.
		/// </summary>
		/// <param name="Val">The element value.</param>
		/// <param name="Index">Column-packed vector index.</param>
		/// <exception cref="ArgumentException">Always becuase a sparse array cannot be
		/// access by a column-packed index.</exception>
		public override void SetReal(double Val, int Index)
		{
			throw new ArgumentException("Can't set Sparse array elements by index. " +
				"Please use SetReal( object Val, int M, int N ) instead.");
		}

		/// <summary>
		/// Gets a single imaginary array element of A(m,n)
		/// </summary>
		/// <param name="M">Row index</param>
		/// <param name="N">Column index</param>
		/// <returns>Array element</returns>
		public override double GetImaginary(int M, int N)
		{
			if (IsComplex)
			{
				IndexMN i = new IndexMN(M, N);
				if (_imaginary.ContainsKey(i))
					return _imaginary[i];
			}
			return (double)0;
		}

		/// <summary>
		/// Gets a single imaginary array element.
		/// </summary>
		/// <param name="Index">Column-packed vector index</param>
		/// <returns>Array Element</returns>
		/// <exception cref="ArgumentException">Always becuase a sparse array cannot be
		/// access by a column-packed index.</exception>
		public override double GetImaginary(int Index)
		{
			throw new ArgumentException("Can't get Sparse array elements by index. " +
				"Please use GetImaginary( int M, int N ) instead.");
		}

		/// <summary>
		/// Sets a single imaginary array element.
		/// </summary>
		/// <param name="Val">Element value.</param>
		/// <param name="M">Row Index.</param>
		/// <param name="N">Column Index.</param>
		public override void SetImaginary(double Val, int M, int N)
		{
			if (IsComplex)
			{
				IndexMN i = new IndexMN(M, N);
				if (!_indexSet.Contains(i))
					_indexSet.Add(i);
				_imaginary.Add(i, (double)Val);
			}
		}

		/// <summary>
		/// Sets a single imaginary array element.
		/// </summary>
		/// <param name="Val">Element Value</param>
		/// <param name="Index">Column-packed vector index.</param>
		/// /// <exception cref="ArgumentException">Always becuase a sparse array cannot be
		/// access by a column-packed index.</exception>
		public override void SetImaginary(double Val, int Index)
		{
			throw new ArgumentException("Can't get Sparse array elements by index. " +
				"Please use SetImaginary( int M, int N ) instead.");
		}

		/// <summary>
		/// Export all the real numbers in the sparse matrix to a <c>double</c> array.
		/// </summary>
		/// <returns><c>System.Double</c> array</returns>
		public double[] ExportReal()
		{
			double[] ad = new double[_nzMax];
			int i = 0;
			foreach (double d in _real.Values)
				ad[i++] = d;
			return ad;
		}

		/// <summary>
		/// Export all the imaginary numbers in the sparse matrix to a <c>double</c> array.
		/// </summary>
		/// <returns><c>System.Double</c> array</returns>
		public double[] ExportImaginary()
		{
			double[] ad = new double[_nzMax];
			int i = 0;
			foreach (double d in _imaginary.Values)
				ad[i++] = d;
			return ad;
		}

		/// <summary>Gets the flags for this array.</summary>
		public override int Flags
		{
			get
			{
				return (int)((uint)(_type & MLArray.mtFLAG_TYPE)
					| (uint)(base._attributes & 0xFFFFFF00));
			}
		}


		/// <summary>
		/// Get a string representation for the content of the array.
		/// See <see cref="csmatio.types.MLArray.ContentToString()"/>
		/// </summary>
		/// <returns>A string representation.</returns>
		public override string ContentToString()
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append(Name + " = \n");

			foreach (IndexMN i in _indexSet)
			{
				sb.Append("\t(" + i.M + "," + i.N + ")");
				sb.Append("\t" + GetReal(i.M, i.N));
				if (IsComplex)
					sb.Append("+" + GetImaginary(i.M, i.N));
				sb.Append("\n");
			}

			return sb.ToString();
		}

		/// <summary>
		/// Builds a numeric object from a byte array.
		/// </summary>
		/// <param name="bytes">A byte array containing the data.</param>
		/// <returns>A numeric object</returns>
		protected override object BuildFromBytes2(byte[] bytes)
		{
			return BitConverter.ToDouble(bytes, 0);
		}

		/// <summary>
		/// Gets a byte array from a numeric object.
		/// </summary>
		/// <param name="val">The numeric object to convert into a byte array.</param>
		public override byte[] GetByteArray(object val)
		{
			return BitConverter.GetBytes((double)val);
		}
	}
}
