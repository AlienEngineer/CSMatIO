using System;
using System.IO;

using csmatio.common;
using csmatio.types;

namespace csmatio.io
{
	/// <summary>
	/// MAT-file input stream class.
	/// </summary>
	/// <author>David Zier (david.zier@gmail.com)</author>
	public class MatFileInputStream
	{
		private int _type;
		private BinaryReader _buf;

		/// <summary>
		/// Attach MAT-file input stream to <c>ByteBuffer</c>
		/// </summary>
		/// <param name="buf"><c>BinaryReader</c></param>
		/// <param name="type">Type of data stream</param>
		public MatFileInputStream(BinaryReader buf, int type)
		{
			_type = type;
			_buf = buf;
		}

		/// <summary>
		/// Reads data (number of bytes read is determined by <i>data type</i>)
		/// from the stream to <c>int</c>.
		/// </summary>
		/// <returns><c>int</c></returns>
		/// <exception cref="ArgumentException">If input stream type is not known</exception>
		public int ReadInt()
		{
			switch (_type)
			{
				case MatDataTypes.miUINT8:
					return (int)(_buf.ReadByte() & 0xff);
				case MatDataTypes.miINT8:
					return (int)_buf.ReadByte();
				case MatDataTypes.miUINT16:
					return (int)(_buf.ReadInt16() & 0xFFFF);
				case MatDataTypes.miINT16:
					return (int)_buf.ReadInt16();
				case MatDataTypes.miUINT32:
					return (int)(_buf.ReadInt32() & 0xFFFFFFFF);
				case MatDataTypes.miINT32:
					return (int)_buf.ReadInt32();
				case MatDataTypes.miUINT64:
					return (int)_buf.ReadInt64();
				case MatDataTypes.miINT64:
					return (int)_buf.ReadInt64();
				case MatDataTypes.miSINGLE:
					return (int)_buf.ReadSingle();
				case MatDataTypes.miDOUBLE:
					return (int)_buf.ReadDouble();
				default:
					throw new ArgumentException("Unknown data type: " + _type);
			}
		}

		/// <summary>
		/// Reads data (number of bytes read is determined by <i>data type</i>)
		/// from the stream to <c>char</c>.
		/// </summary>
		/// <returns><c>char</c></returns>
		/// <exception cref="ArgumentException">If input stream type is not known</exception>
		public char ReadChar()
		{
			switch (_type)
			{
				case MatDataTypes.miUINT8:
					return (char)(_buf.ReadByte() & 0xff);
				case MatDataTypes.miINT8:
					return (char)_buf.ReadByte();
				case MatDataTypes.miUINT16:
					return (char)(_buf.ReadInt16() & 0xFFFF);
				case MatDataTypes.miINT16:
					return (char)_buf.ReadInt16();
				case MatDataTypes.miUINT32:
					return (char)(_buf.ReadInt32() & 0xFFFFFFFF);
				case MatDataTypes.miINT32:
					return (char)_buf.ReadInt32();
				case MatDataTypes.miDOUBLE:
					return (char)_buf.ReadDouble();
				case MatDataTypes.miUTF8:
					return (char)_buf.ReadByte();
				default:
					throw new ArgumentException("Unknown data type: " + _type);
			}
		}

		/// <summary>
		/// Reads data (number of bytes read is determined by <i>data type</i>)
		/// from the stream to <c>double</c>.
		/// </summary>
		/// <returns><c>double</c></returns>
		/// <exception cref="ArgumentException">If input stream type is not known</exception>
		public double ReadDouble()
		{
			switch (_type)
			{
				case MatDataTypes.miUINT8:
					return (double)(_buf.ReadByte() & 0xff);
				case MatDataTypes.miINT8:
					return (double)_buf.ReadByte();
				case MatDataTypes.miUINT16:
					return (double)(_buf.ReadInt16() & 0xFFFF);
				case MatDataTypes.miINT16:
					return (double)_buf.ReadInt16();
				case MatDataTypes.miUINT32:
					return (double)(_buf.ReadInt32() & 0xFFFFFFFF);
				case MatDataTypes.miINT32:
					return (double)_buf.ReadInt32();
				case MatDataTypes.miSINGLE:
					return (double)_buf.ReadSingle();
				case MatDataTypes.miDOUBLE:
					return (double)_buf.ReadDouble();
				default:
					throw new ArgumentException("Unknown data type: " + _type);
			}
		}

		/// <summary>
		/// Reads data (number of bytes read is determined by <i>data type</i>)
		/// from the stream to <c>byte</c>.
		/// </summary>
		/// <returns><c>byte</c></returns>
		/// <exception cref="ArgumentException">If input stream type is not known</exception>
		public byte ReadByte()
		{
			switch (_type)
			{
				case MatDataTypes.miUINT8:
					return (byte)(_buf.ReadByte() & 0xff);
				case MatDataTypes.miINT8:
					return (byte)_buf.ReadByte();
				case MatDataTypes.miUINT16:
					return (byte)(_buf.ReadInt16() & 0xFFFF);
				case MatDataTypes.miINT16:
					return (byte)_buf.ReadInt16();
				case MatDataTypes.miUINT32:
					return (byte)(_buf.ReadInt32() & 0xFFFFFFFF);
				case MatDataTypes.miINT32:
					return (byte)_buf.ReadInt32();
				case MatDataTypes.miSINGLE:
					return (byte)_buf.ReadSingle();
				case MatDataTypes.miDOUBLE:
					return (byte)_buf.ReadDouble();
				case MatDataTypes.miUTF8:
					return (byte)_buf.ReadByte();
				default:
					throw new ArgumentException("Unknown data type: " + _type);
			}
		}

		/// <summary>
		/// Reads data (number of bytes read is determined by <i>data type</i>)
		/// from the stream to <c>long</c>.
		/// </summary>
		/// <returns><c>long</c></returns>
		/// <exception cref="ArgumentException">If input stream type is not known</exception>
		public long ReadLong()
		{
			switch (_type)
			{
				case MatDataTypes.miUINT8:
					return (long)(_buf.ReadByte() & 0xff);
				case MatDataTypes.miINT8:
					return (long)_buf.ReadByte();
				case MatDataTypes.miUINT16:
					return (long)(_buf.ReadInt16() & 0xFFFF);
				case MatDataTypes.miINT16:
					return (long)_buf.ReadInt16();
				case MatDataTypes.miUINT32:
					return (long)(_buf.ReadInt32() & 0xFFFFFFFF);
				case MatDataTypes.miINT32:
					return (long)_buf.ReadInt32();
				case MatDataTypes.miUINT64:
					return (long)_buf.ReadInt64();
				case MatDataTypes.miINT64:
					return (long)_buf.ReadInt64();
				case MatDataTypes.miSINGLE:
					return (long)_buf.ReadSingle();
				case MatDataTypes.miDOUBLE:
					return (long)_buf.ReadDouble();
				default:
					throw new ArgumentException("Unknown data type: " + _type);
			}
		}

		/// <summary>
		/// Reads the data into a <c>ByteBuffer</c>.
		/// </summary>
		/// <param name="dest">The destination <c>ByteBuffer</c></param>
		/// <param name="elements">The number of elements to read into a buffer</param>
		/// <param name="storage">The backing <c>ByteStorageSupport</c> that
		/// gives information on how data should be interpreted</param>
		/// <returns>Reference to the destination <c>ByteBuffer</c></returns>
		/// <exception cref="NotSupportedException">When attempting to read an unsupported
		/// class type from the buffer</exception>
		public ByteBuffer ReadToByteBuffer(ByteBuffer dest, int elements,
			IByteStorageSupport storage)
		{
			int bytesAllocated = storage.GetBytesAllocated;
			int size = elements * bytesAllocated;

			// direct buffer copy
			if (MatDataTypes.SizeOf(_type) == bytesAllocated)
			{
				int bufMaxSize = 1024;
				int bufSize = Math.Min((int)(_buf.BaseStream.Length - _buf.BaseStream.Position), bufMaxSize);
				int bufPos = (int)_buf.BaseStream.Position;

				byte[] tmp = new byte[bufSize];

				while (dest.Remaining > 0)
				{
					int length = Math.Min(dest.Remaining, tmp.Length);
					_buf.Read(tmp, 0, length);
					dest.Put(tmp, 0, length);
				}
				_buf.BaseStream.Position = bufPos + size;
			}
			else
			{
				// Because Matlab writes data not respectively to the declared
				// matrix type, the reading is not straight forward (as above)
				Type clazz = storage.GetStorageType;
				while (dest.Remaining > 0)
				{
					if (clazz.Equals(typeof(double)))
					{
						dest.PutDouble(ReadDouble());
						continue;
					}
					if (clazz.Equals(typeof(float)))
					{
						dest.PutFloat((float)ReadDouble()); // QND
						continue;
					}
					if (clazz.Equals(typeof(byte)))
					{
						dest.PutDouble(ReadByte());
						continue;
					}
					if (clazz.Equals(typeof(int)))
					{
						dest.PutDouble(ReadInt());
						continue;
					}
					if (clazz.Equals(typeof(long)))
					{
						dest.PutDouble(ReadLong());
						continue;
					}
					throw new NotSupportedException("Not supported buffer reader for " + clazz);
				}
			}
			dest.Rewind();
			return dest;
		}
	}
}
