using System;

namespace csmatio.common
{
	/// <summary>
	/// A byte buffer class similiar to JAVA's <c>java.nio.ByteBuffer</c>.
	/// This class only really contains the neccessary methods needed for the 
	/// Matlab I/O API.
	/// </summary>
	/// <author>David Zier (david.zier@gmail.com)</author>
	public class ByteBuffer
	{
		private byte[] _bytes;
		private int _pos;

		/// <summary>
		/// Create a new <c>ByteBuffer</c> object.
		/// </summary>
		/// <param name="Size">The size of the buffer.</param>
		public ByteBuffer(int Size)
		{
			_bytes = new byte[Size];
			_pos = 0;
		}

		/// <summary>
		/// Create a new <c>ByteBuffer</c> from a byte array.
		/// </summary>
		/// <param name="bytes">A byte array.</param>
		public ByteBuffer(byte[] bytes)
		{
			_bytes = bytes;
			_pos = 0;
		}

		/// <summary>
		/// Get a copy of the <c>ByteBuffer</c>
		/// </summary>
		/// <returns>A <c>ByteBuffer</c> copy.</returns>
		public ByteBuffer Duplicate()
		{
			return new ByteBuffer(_bytes);
		}

		//public ByteBuffer CopyFrom(Array ai, int index, int length)
		//{

		//}

		/// <summary>
		/// Indicates the amount of bytes remaining in the buffer.
		/// </summary>
		/// <returns>The amount of bytes remaining</returns>
		public int Remaining
		{
			get { return _bytes.Length - _pos; }
		}

		/// <summary>
		/// Does the <c>ByteBuffer</c> have any bytes remaining?
		/// </summary>
		public bool HasRemaining
		{
			get { return Remaining > 0; }
		}

		/// <summary>
		/// Set the relative position marker to a specific index within the buffer.
		/// </summary>
		/// <param name="index">The index within the buffer</param>
		/// <exception cref="IndexOutOfRangeException">If the index is either negative or beyond the length of the buffer.</exception>
		/// <returns>This buffer.</returns>
		public ByteBuffer Position(int index)
		{
			if (index < 0 || index >= _bytes.Length)
				throw new IndexOutOfRangeException("Index is either negative or beyond the buffer length.");

			_pos = index;

			return this;
		}

		/// <summary>
		/// Gets the relative position of the <c>ByteBuffer</c>.
		/// </summary>
		/// <returns>Position of the <c>ByteBuffer</c> relative index</returns>
		public int Position()
		{
			return _pos;
		}

		/// <summary>
		/// Get the total limit to the byte buffer
		/// </summary>
		public int Limit
		{
			get { return this._bytes.Length; }
		}

		/// <summary>
		/// Resets the relative position marker to the beginning of the buffer.
		/// </summary>
		/// <returns>This array.</returns>
		public ByteBuffer Rewind()
		{
			_pos = 0;

			return this;
		}

		/// <summary>
		/// Relative <c>Get</c> method.  Reads the byte at this buffer's
		/// position, and then increments the position
		/// </summary>
		/// <returns>The byte at the buffer's current position</returns>
		public byte Get()
		{
			if (_pos >= _bytes.Length)
				throw new OverflowException("The buffer's current position is not smaller than it's limit");

			return _bytes[_pos++];
		}

		/// <summary>
		/// Relative <c>Put</c> method, writes the given byte into this buffer at
		/// current position, and then increments the position.
		/// </summary>
		/// <param name="b">The byte to be written.</param>
		/// <returns>This buffer.</returns>
		public ByteBuffer Put(byte b)
		{
			if (_pos >= _bytes.Length)
				throw new OverflowException("The buffer's current position is not smaller than it's limit");

			_bytes[_pos++] = b;

			return this;
		}

		/// <summary>
		/// Absolute <c>Get</c> method. Reads the byte at a the given index.
		/// </summary>
		/// <param name="index">The index from which the byte will be read</param>
		/// <returns>The byte at the given index</returns>
		public byte Get(int index)
		{
			if (index < 0 || index >= _bytes.Length)
				throw new IndexOutOfRangeException("The index is negative or not smaller than the buffer's limit.");

			return _bytes[index];
		}

		/// <summary>
		/// Absolute <c>Put</c> method, writes the given byte into this buffer at
		/// a given index.
		/// </summary>
		/// <param name="index">The index at which the byte will be written</param>
		/// <param name="b">The byte to be written</param>
		/// <returns>This buffer</returns>
		public ByteBuffer Put(int index, byte b)
		{
			if (index < 0 || index >= _bytes.Length)
				throw new IndexOutOfRangeException("The index is negative or not smaller than the buffer's limit.");

			_bytes[index] = b;

			return this;
		}

		/// <summary>
		/// Relative bulk <c>Get</c> method. This method transfers bytes from this buffer into the given
		/// destination array.
		/// </summary>
		/// <param name="dst">The array into which bytes are to be written</param>
		/// <param name="offset">The offset within the array to of the first byte to be written;
		/// must be non-negative and no larger than <c>dst.length</c></param>
		/// <param name="length">The maximum number of butes to be written to the given array;
		/// must be non-negative and no larger than <c>dst.length - offset</c></param>
		/// <returns>This buffer</returns>
		public ByteBuffer Get(ref byte[] dst, int offset, int length)
		{
			if (length > Remaining)
				throw new OverflowException("There are fewer than " + length + " bytes remaining in this buffer.");
			if (offset < 0 || offset > dst.Length || length < 0 || length > dst.Length - offset)
				throw new IndexOutOfRangeException("The preconditions on the offset and lenght parameters do not hold for this buffer.");

			System.Array.Copy(_bytes, _pos, dst, offset, length);
			_pos += length;

			return this;
		}

		/// <summary>
		/// Relative bulk <c>Get</c> method.  This method transfers bytes from this buffer into the 
		/// given destination array.
		/// </summary>
		/// <param name="dst">The array into which bytes are to be written</param>
		/// <returns>This buffer</returns>
		public ByteBuffer Get(ref byte[] dst)
		{
			if (dst.Length > Remaining)
				throw new OverflowException("There are fewer than " + dst.Length + " bytes remaining in this buffer.");

			return Get(ref dst, 0, dst.Length);
		}

		/// <summary>
		/// Relative bulk <c>Put</c> method.  This method transfers the butes remaining in the given source buffer 
		/// into this buffer, starting at each buffer's current position.  The positions of both buffers are
		/// then incrementend by <c>n</c>.
		/// </summary>
		/// <param name="src">the source buffer from which bytes are to be read; must not be this buffer.</param>
		/// <returns>This buffer</returns>
		public ByteBuffer Put(ByteBuffer src)
		{
			if (src == this)
				throw new ArgumentException("The source buffer cannot be the same as this buffer.");
			if (src.Remaining > Remaining)
				throw new OverflowException("There is insufficient space in this buffer for the remaining bytes in the source buffer.");

			while (src.HasRemaining)
				_bytes[_pos++] = src.Get();

			return this;
		}

		/// <summary>
		/// Relative bulk <c>Put</c> method.  This method transfers bytes into this buffer 
		/// from the given source array.
		/// </summary>
		///	<param name="src">The array from which bytes are to be read</param>
		/// <param name="offset">The offset within the array of the first byte to be read; must not be
		/// non-negative and no larger than <c>array.length</c></param>
		/// <param name="length">The number of bytes to be read from the given array;  must not be
		/// non-negative and no larger than <c>array.length - offset</c></param>
		/// <returns>This buffer</returns>
		public ByteBuffer Put(byte[] src, int offset, int length)
		{
			if (length > Remaining)
				throw new OverflowException("There are fewer than " + length + " bytes remaining in this buffer.");
			if (offset < 0 || offset >= src.Length || length < 0 || length > src.Length - offset)
				throw new IndexOutOfRangeException("The preconditions on the offset and lenght parameters do not hold for this buffer.");

			System.Array.Copy(src, offset, _bytes, _pos, length);
			_pos += length;

			return this;
		}

		/// <summary>
		/// Relative bulk <c>Put</c> method.  This method transfers bytes into this buffer 
		/// from the given source array.
		/// </summary>
		///	<param name="src">The array from which bytes are to be read</param>
		/// <returns>This buffer</returns>
		public ByteBuffer Put(byte[] src)
		{

			if (src.Length > Remaining)
				throw new OverflowException("There are fewer than " + src.Length + " bytes remaining in this buffer.");

			Put(src, 0, src.Length);

			return this;
		}

		/// <summary>
		/// Returns the byte array that backs this buffer.
		/// </summary>
		/// <returns></returns>
		public byte[] Array()
		{
			return _bytes;
		}

		/// <summary>
		/// Return a string summarizing the state of this buffer
		/// </summary>
		/// <returns>A summary string.</returns>
		public override string ToString()
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			for (int i = 0; i < _bytes.Length; i++)
			{
				sb.Append(_bytes[i].ToString("x2"));
				if (i < _bytes.Length - 1)
					sb.Append("-");
			}

			return sb.ToString();
		}

		/// <summary>
		/// Relative <c>Get</c> method for reading a char value.
		/// </summary>
		/// <returns>The char value at the buffer's current position</returns>
		public char GetChar()
		{
			if (_pos > _bytes.Length - 2)
				throw new OverflowException("There are fewer than two bytes remaining in this buffer");

			char ret = BitConverter.ToChar(_bytes, _pos);
			_pos += 2;

			return ret;
		}

		/// <summary>
		/// Relative <c>Put</c> method for writing a char value.
		/// </summary>
		/// <param name="val">The char value to be written</param>
		/// <returns>This buffer</returns>
		public ByteBuffer PutChar(char val)
		{
			if (_pos > _bytes.Length - 2)
				throw new OverflowException("There are fewer than two bytes remaining in this buffer");

			byte[] src = BitConverter.GetBytes(val);
			System.Array.Copy(src, 0, _bytes, _pos, src.Length);
			_pos += src.Length;

			return this;
		}

		/// <summary>
		/// Absolute <c>Get</c> method for reading a char value.
		/// </summary>
		/// <param name="index">The index from which the bytes will be read</param>
		/// <returns>The char value at the given index</returns>
		public char GetChar(int index)
		{
			if (index < 0 || index > _bytes.Length - 2)
				throw new IndexOutOfRangeException("The index is either negative or larger than the buffer's limit.");

			char ret = BitConverter.ToChar(_bytes, index);

			return ret;
		}

		/// <summary>
		/// Absolute <c>Put</c> method for writing a char value.
		/// </summary>
		/// <param name="index">The index from which the bytes will be written</param>
		/// <param name="val">The char value to be written</param>
		/// <returns>This buffer</returns>
		public ByteBuffer PutChar(int index, char val)
		{
			if (index < 0 || index > _bytes.Length - 2)
				throw new IndexOutOfRangeException("The index is either negative or larger than the buffer's limit.");

			byte[] src = BitConverter.GetBytes(val);
			System.Array.Copy(src, 0, _bytes, index, src.Length);

			return this;
		}

		/// <summary>
		/// Relative <c>Get</c> method for reading a short value.
		/// </summary>
		/// <returns>The short value at the buffer's current position</returns>
		public short GetShort()
		{
			if (_pos > _bytes.Length - 2)
				throw new OverflowException("There are fewer than two bytes remaining in this buffer");

			short ret = BitConverter.ToInt16(_bytes, _pos);
			_pos += 2;

			return ret;
		}

		/// <summary>
		/// Relative <c>Put</c> method for writing a short value.
		/// </summary>
		/// <param name="val">The short value to be written</param>
		/// <returns>This buffer</returns>
		public ByteBuffer PutShort(short val)
		{
			if (_pos > _bytes.Length - 2)
				throw new OverflowException("There are fewer than two bytes remaining in this buffer");

			byte[] src = BitConverter.GetBytes(val);
			System.Array.Copy(src, 0, _bytes, _pos, src.Length);
			_pos += src.Length;

			return this;
		}

		/// <summary>
		/// Absolute <c>Get</c> method for reading a short value.
		/// </summary>
		/// <param name="index">The index from which the bytes will be read</param>
		/// <returns>The short value at the given index</returns>
		public short GetShort(int index)
		{
			if (index < 0 || index > _bytes.Length - 2)
				throw new IndexOutOfRangeException("The index is either negative or larger than the buffer's limit.");

			short ret = BitConverter.ToInt16(_bytes, index);

			return ret;
		}

		/// <summary>
		/// Absolute <c>Put</c> method for writing a short value.
		/// </summary>
		/// <param name="index">The index from which the bytes will be written</param>
		/// <param name="val">The short value to be written</param>
		/// <returns>This buffer</returns>
		public ByteBuffer PutShort(int index, short val)
		{
			if (index < 0 || index > _bytes.Length - 2)
				throw new IndexOutOfRangeException("The index is either negative or larger than the buffer's limit.");

			byte[] src = BitConverter.GetBytes(val);
			System.Array.Copy(src, 0, _bytes, index, src.Length);

			return this;
		}

		/// <summary>
		/// Relative <c>Get</c> method for reading a int value.
		/// </summary>
		/// <returns>The int value at the buffer's current position</returns>
		public int GetInt()
		{
			if (_pos > _bytes.Length - 4)
				throw new OverflowException("There are fewer than four bytes remaining in this buffer");

			int ret = BitConverter.ToInt32(_bytes, _pos);
			_pos += 4;

			return ret;
		}

		/// <summary>
		/// Relative <c>Put</c> method for writing a int value.
		/// </summary>
		/// <param name="val">The int value to be written</param>
		/// <returns>This buffer</returns>
		public ByteBuffer PutInt(int val)
		{
			if (_pos > _bytes.Length - 4)
				throw new OverflowException("There are fewer than four bytes remaining in this buffer");

			byte[] src = BitConverter.GetBytes(val);
			System.Array.Copy(src, 0, _bytes, _pos, src.Length);
			_pos += src.Length;

			return this;
		}

		/// <summary>
		/// Absolute <c>Get</c> method for reading a int value.
		/// </summary>
		/// <param name="index">The index from which the bytes will be read</param>
		/// <returns>The int value at the given index</returns>
		public int GetInt(int index)
		{
			if (index < 0 || index > _bytes.Length - 4)
				throw new IndexOutOfRangeException("The index is either negative or larger than the buffer's limit.");

			int ret = BitConverter.ToInt32(_bytes, index);

			return ret;
		}

		/// <summary>
		/// Absolute <c>Put</c> method for writing a int value.
		/// </summary>
		/// <param name="index">The index from which the bytes will be written</param>
		/// <param name="val">The int value to be written</param>
		/// <returns>This buffer</returns>
		public ByteBuffer PutShort(int index, int val)
		{
			if (index < 0 || index > _bytes.Length - 4)
				throw new IndexOutOfRangeException("The index is either negative or larger than the buffer's limit.");

			byte[] src = BitConverter.GetBytes(val);
			System.Array.Copy(src, 0, _bytes, index, src.Length);

			return this;
		}

		/// <summary>
		/// Relative <c>Get</c> method for reading a long value.
		/// </summary>
		/// <returns>The long value at the buffer's current position</returns>
		public long GetLong()
		{
			if (_pos > _bytes.Length - 8)
				throw new OverflowException("There are fewer than eight bytes remaining in this buffer");

			long ret = BitConverter.ToInt64(_bytes, _pos);
			_pos += 8;

			return ret;
		}

		/// <summary>
		/// Relative <c>Put</c> method for writing a long value.
		/// </summary>
		/// <param name="val">The long value to be written</param>
		/// <returns>This buffer</returns>
		public ByteBuffer PutLong(long val)
		{
			if (_pos > _bytes.Length - 8)
				throw new OverflowException("There are fewer than eight bytes remaining in this buffer");

			byte[] src = BitConverter.GetBytes(val);
			System.Array.Copy(src, 0, _bytes, _pos, src.Length);
			_pos += src.Length;

			return this;
		}

		/// <summary>
		/// Absolute <c>Get</c> method for reading a long value.
		/// </summary>
		/// <param name="index">The index from which the bytes will be read</param>
		/// <returns>The long value at the given index</returns>
		public long GetLong(int index)
		{
			if (index < 0 || index > _bytes.Length - 8)
				throw new IndexOutOfRangeException("The index is either negative or larger than the buffer's limit.");

			long ret = BitConverter.ToInt64(_bytes, index);

			return ret;
		}

		/// <summary>
		/// Absolute <c>Put</c> method for writing a long value.
		/// </summary>
		/// <param name="index">The index from which the bytes will be written</param>
		/// <param name="val">The long value to be written</param>
		/// <returns>This buffer</returns>
		public ByteBuffer PutLong(int index, long val)
		{
			if (index < 0 || index > _bytes.Length - 8)
				throw new IndexOutOfRangeException("The index is either negative or larger than the buffer's limit.");

			byte[] src = BitConverter.GetBytes(val);
			System.Array.Copy(src, 0, _bytes, index, src.Length);

			return this;
		}

		/// <summary>
		/// Relative <c>Get</c> method for reading a float value.
		/// </summary>
		/// <returns>The float value at the buffer's current position</returns>
		public float GetFloat()
		{
			if (_pos > _bytes.Length - 4)
				throw new OverflowException("There are fewer than four bytes remaining in this buffer");

			float ret = BitConverter.ToSingle(_bytes, _pos);
			_pos += 4;

			return ret;
		}

		/// <summary>
		/// Relative <c>Put</c> method for writing a float value.
		/// </summary>
		/// <param name="val">The float value to be written</param>
		/// <returns>This buffer</returns>
		public ByteBuffer PutFloat(float val)
		{
			if (_pos > _bytes.Length - 4)
				throw new OverflowException("There are fewer than four bytes remaining in this buffer");

			byte[] src = BitConverter.GetBytes(val);
			System.Array.Copy(src, 0, _bytes, _pos, src.Length);
			_pos += src.Length;

			return this;
		}

		/// <summary>
		/// Absolute <c>Get</c> method for reading a float value.
		/// </summary>
		/// <param name="index">The index from which the bytes will be read</param>
		/// <returns>The float value at the given index</returns>
		public float GetFloat(int index)
		{
			if (index < 0 || index > _bytes.Length - 4)
				throw new IndexOutOfRangeException("The index is either negative or larger than the buffer's limit.");

			float ret = BitConverter.ToSingle(_bytes, index);

			return ret;
		}

		/// <summary>
		/// Absolute <c>Put</c> method for writing a float value.
		/// </summary>
		/// <param name="index">The index from which the bytes will be written</param>
		/// <param name="val">The float value to be written</param>
		/// <returns>This buffer</returns>
		public ByteBuffer PutFloat(int index, float val)
		{
			if (index < 0 || index > _bytes.Length - 4)
				throw new IndexOutOfRangeException("The index is either negative or larger than the buffer's limit.");

			byte[] src = BitConverter.GetBytes(val);
			System.Array.Copy(src, 0, _bytes, index, src.Length);

			return this;
		}

		/// <summary>
		/// Relative <c>Get</c> method for reading a double value.
		/// </summary>
		/// <returns>The double value at the buffer's current position</returns>
		public double GetDouble()
		{
			if (_pos > _bytes.Length - 8)
				throw new OverflowException("There are fewer than eight bytes remaining in this buffer");

			double ret = BitConverter.ToDouble(_bytes, _pos);
			_pos += 8;

			return ret;
		}

		/// <summary>
		/// Relative <c>Put</c> method for writing a double value.
		/// </summary>
		/// <param name="val">The double value to be written</param>
		/// <returns>This buffer</returns>
		public ByteBuffer PutDouble(double val)
		{
			if (_pos > _bytes.Length - 8)
				throw new OverflowException("There are fewer than eight bytes remaining in this buffer");

			byte[] src = BitConverter.GetBytes(val);
			System.Array.Copy(src, 0, _bytes, _pos, src.Length);
			_pos += src.Length;

			return this;
		}

		/// <summary>
		/// Absolute <c>Get</c> method for reading a double value.
		/// </summary>
		/// <param name="index">The index from which the bytes will be read</param>
		/// <returns>The double value at the given index</returns>
		public double GetDouble(int index)
		{
			if (index < 0 || index > _bytes.Length - 8)
				throw new IndexOutOfRangeException("The index is either negative or larger than the buffer's limit.");

			double ret = BitConverter.ToDouble(_bytes, index);

			return ret;
		}

		/// <summary>
		/// Absolute <c>Put</c> method for writing a double value.
		/// </summary>
		/// <param name="index">The index from which the bytes will be written</param>
		/// <param name="val">The double value to be written</param>
		/// <returns>This buffer</returns>
		public ByteBuffer PutFloat(int index, double val)
		{
			if (index < 0 || index > _bytes.Length - 8)
				throw new IndexOutOfRangeException("The index is either negative or larger than the buffer's limit.");

			byte[] src = BitConverter.GetBytes(val);
			System.Array.Copy(src, 0, _bytes, index, src.Length);

			return this;
		}
	}
}
