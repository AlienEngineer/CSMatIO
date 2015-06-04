namespace csmatio.common
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Text;

	/// <summary>
	/// common static helper methods
	/// </summary>
	public static class Helpers
	{
		/// <summary>
		/// generic stream copy helper function
		/// </summary>
		/// <param name="input">input stream</param>
		/// <param name="output">output stream</param>
		public static void CopyStream(Stream input, Stream output)
		{
			CopyStream(input, output, 4096);
		}

		/// <summary>
		/// generic stream copy helper function
		/// </summary>
		/// <param name="input">input stream</param>
		/// <param name="output">output stream</param>
		/// <param name="bufsize">size of the copy buffer</param>
		public static void CopyStream(Stream input, Stream output, int bufsize)
		{
			byte[] buffer = new byte[bufsize];
			int numBytesRead;
			while ((numBytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
			{
				output.Write(buffer, 0, numBytesRead);
			}

			output.Flush();
		}

		/// <summary>
		/// Converts T[][] to T[], assuming equal length of each subarray.
		/// The T[] array consists of the concatenated columns(!) of the T[][] array.
		/// </summary>
		/// <param name="array2D">Array of type T[][]</param>
		/// <returns>Array of Type T[]</returns>
		public static T[] Array2DTo1D<T>(T[][] array2D)
		{
			int numRows = array2D.Length;
			int numCols = array2D[0].Length;
			T[] array1D = new T[numRows * numCols];
			for (int i = 0; i < numCols; ++i)
			{
				for (int j = 0; j < numRows; ++j)
				{
					array1D[j + i * numRows] = array2D[j][i];
				}
			}

			return array1D;
		}
	}
}
