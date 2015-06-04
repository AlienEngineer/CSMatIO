using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using csmatio.common;
using csmatio.types;

#if !NET20 && !NET40 && !NET45
#error .NET-Version undefiniert
#endif

#if NET20
using zlib = ComponentAce.Compression.Libs.ZLib;
#endif

#if NET40 || NET45
using System.IO.Compression;
#endif

namespace csmatio.io
{
	/// <summary>
	/// MAT-file writer.
	/// </summary>
	/// <example> Usage:
	/// <code>
	///		//1. First create example arrays
	///		double[] src = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0 };
	///		MLDouble mlDouble = new MLDouble( "double_arr", src, 3 );
	///		MLChar mlChar = new MLChar( "char_arr", "I am dummy" );
	///		
	///		//2. Write arrays to file
	///		List&lt;MLArray&gt; list = new List&lt;MLArray&gt;();
	///		list.Add( mlDouble );
	///		list.Add( mlChar );
	///		
	///		new MatFileWriter( "mat_file.mat", list, true );
	/// </code>
	/// 
	/// This is "equal" to the Matlab commands:
	/// <code>
	/// >> double_arr = [ 1 2; 3 4; 5 6 ];
	/// >> char_arr = 'I am dummy';
	/// >>
	/// >> save('mat_file.mat', 'double_arr', 'char_arr');
	/// </code>
	/// </example>
	/// <author>David Zier (<a href="mailto:david.zier@gmail.com">david.zier@gmail.com</a>)</author>
	public class MatFileWriter
	{
		/// <summary>
		/// Create a MatFileWriter by specifying a filename.
		/// </summary>
		/// <param name="fileName">The name of the file to create.</param>
		/// <param name="data">The <c>MLArray</c> data to be written to the file.</param>
		/// <param name="compress">Use data compression?</param>
		public MatFileWriter(string fileName, List<MLArray> data, bool compress) :
			this(new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Read), data, compress) { }

		/// <summary>
		/// Create a MatFileWriter by specifying a Filestream.
		/// </summary>
		/// <param name="file">The <c>FileStream</c> to write the MAT-file to.</param>
		/// <param name="data">The <c>MLArray</c> data that is to be written.</param>
		/// <param name="compress">Use data compression?</param>
		public MatFileWriter(FileStream file, List<MLArray> data, bool compress)
			: this(new BinaryWriter(file), data, compress) { }

		/// <summary>
		/// Writes MLArrays into <c>OutputStream</c>
		/// </summary>
		/// <remarks>
		/// Writes MAT-file header and compressed data (<c>miCompressed</c>).
		/// </remarks>
		/// <param name="stream"><c>Stream</c></param>
		/// <param name="data"><c>Collection</c> of <c>MLArray</c> elements.</param>
		/// <param name="compress">Use data compression?</param>
		public MatFileWriter(BinaryWriter stream, ICollection data, bool compress)
		{
			// Write header
			WriteHeader(stream);

			foreach (MLArray matrix in data)
			{
				if (compress)
				{
					// Prepare buffer for MATRIX data
					MemoryStream memstrm = new MemoryStream();
					BinaryWriter bw = new BinaryWriter(memstrm);
					WriteMatrix(bw, matrix); // Write MATRIX bytes into buffer
					memstrm.Position = 0; // Rewind the stream

					// Compress data to save storage
					MemoryStream compressed = new MemoryStream();
#if NET20
                    zlib.ZOutputStream zos = new zlib.ZOutputStream(compressed, -1);
                    byte[] input = new byte[128];
                    int len = 0;
                    while ((len = memstrm.Read(input, 0, input.Length)) > 0)
                        zos.Write(input, 0, len);
                    zos.Flush();
                    zos.Close();  // important to note that the zlib.ZOutputStream needs to close before writting out the data to the Base.stream

					byte[] compressedBytes = compressed.ToArray();
#endif
#if NET40 || NET45
					uint s1 = 1, s2 = 0, crc = 0; // Adler-32 CRC
					using (DeflateStream df = new DeflateStream(compressed, CompressionMode.Compress, true))
					{
						int readByte;
						do
						{
							readByte = memstrm.ReadByte();
							if (readByte != -1)
							{
								df.WriteByte((byte)readByte);
								s1 = (s1 + (byte)readByte) % 0xFFF1;
								s2 = (s2 + s1) % 0xFFF1;
							}
						}
						while (readByte != -1);
						crc = (s2 << 16) | s1;
					}

					compressed.Position = 0;

					// zip RFC 1950
					byte[] compressedBytes = new byte[compressed.Length + 6];
					compressedBytes[0] = 0x78;
					compressedBytes[1] = 0x9c;
					for (int i = 2; i < compressedBytes.Length - 4; i++)
					{
						compressedBytes[i] = (byte)compressed.ReadByte();
					}
					BitConverter.GetBytes(crc).CopyTo(compressedBytes, compressedBytes.Length - 4);
#endif

					// write COMPRESSED tag and compressed data into output channel
					ByteBuffer buf = new ByteBuffer(2 * 4 + compressedBytes.Length);
					buf.PutInt(MatDataTypes.miCOMPRESSED);
					buf.PutInt(compressedBytes.Length);
					buf.Put(compressedBytes, 0, compressedBytes.Length);

					stream.Write(buf.Array());

					compressed.Close();
				}
				else
				{
					// Write MATRIX bytes into buffer
					WriteMatrix(stream, matrix);
				}

			}

			stream.Close();
		}

		/// <summary>
		/// Writes MAT-file header into <c>Stream</c>
		/// </summary>
		/// <param name="stream">The output stream</param>
		private void WriteHeader(BinaryWriter stream)
		{
			//write descriptive text
			MatFileHeader header = MatFileHeader.CreateHeader();
			char[] dest = new char[116];
			char[] src = header.Description.ToCharArray();
			Array.Copy(src, 0, dest, 0, src.Length);

			byte[] endianIndicator = header.EndianIndicator;

			//ByteBuffer buf = new ByteBuffer( dest.Length * 2 + /* char size */ + 2 + endianIndicator.Length );
			ByteBuffer buf = new ByteBuffer(128); // The header is always a 128-byte header

			for (int i = 0; i < dest.Length; i++)
			{
				buf.Put((byte)dest[i]);
			}
			//write subsyst data offset
			buf.Position(buf.Position() + 8);

			// write version
			int version = header.Version;
			//buf.Put( (byte)(version >> 8) );
			//buf.Put( (byte)version );
			buf.PutShort((short)version);

			buf.Put(endianIndicator);

			stream.Write(buf.Array());
		}

		/// <summary>
		/// Writes MATRIX into <c>BinaryWriter</c> stream
		/// </summary>
		/// <param name="output">The <c>BinaryWriter</c> stream</param>
		/// <param name="array">The <c>MLArray</c> matrix</param>
		private void WriteMatrix(BinaryWriter output, MLArray array)
		{
			OSArrayTag tag;
			MemoryStream buffer;
			BinaryWriter bufferBW;
			MemoryStream ms = new MemoryStream();
			BinaryWriter bw = new BinaryWriter(ms);

			//flags
			WriteFlags(bw, array);

			//dimensions
			WriteDimensions(bw, array);

			// array  name
			WriteName(bw, array);

			switch (array.Type)
			{
				case MLArray.mxCHAR_CLASS:
					// write char data
					buffer = new MemoryStream();
					bufferBW = new BinaryWriter(buffer);
					char[] ac = ((MLChar)array).ExportChar();
					for (int i = 0; i < ac.Length; i++)
					{
						bufferBW.Write((ushort)ac[i]);
					}
					tag = new OSArrayTag(MatDataTypes.miUINT16, buffer.ToArray());
					tag.WriteTo(bw);

					break;
				case MLArray.mxDOUBLE_CLASS:
					// TODO: Exception occurs here when trying to write a MLEmptyArray.
					// See https://sourceforge.net/p/csmatio/tickets/5/
					tag = new OSArrayTag(MatDataTypes.miDOUBLE,
						((MLNumericArray<double>)array).RealByteBuffer);
					tag.WriteTo(bw);
					if (array.IsComplex)
					{
						tag = new OSArrayTag(MatDataTypes.miDOUBLE,
							((MLNumericArray<double>)array).ImaginaryByteBuffer);
						tag.WriteTo(bw);
					}
					break;
				case MLArray.mxSINGLE_CLASS:
					tag = new OSArrayTag(MatDataTypes.miSINGLE,
						((MLNumericArray<float>)array).RealByteBuffer);
					tag.WriteTo(bw);
					if (array.IsComplex)
					{
						tag = new OSArrayTag(MatDataTypes.miSINGLE,
							((MLNumericArray<float>)array).ImaginaryByteBuffer);
						tag.WriteTo(bw);
					}
					break;
				case MLArray.mxUINT8_CLASS:
					tag = new OSArrayTag(MatDataTypes.miUINT8,
						((MLNumericArray<byte>)array).RealByteBuffer);
					tag.WriteTo(bw);
					if (array.IsComplex)
					{
						tag = new OSArrayTag(MatDataTypes.miUINT8,
							((MLNumericArray<byte>)array).ImaginaryByteBuffer);
						tag.WriteTo(bw);
					}
					break;
				case MLArray.mxINT8_CLASS:
					tag = new OSArrayTag(MatDataTypes.miINT8, 
						((MLNumericArray<sbyte>)array).RealByteBuffer );
					tag.WriteTo(bw);
					if (array.IsComplex)
					{
                        tag = new OSArrayTag(MatDataTypes.miINT8,
                            ((MLNumericArray<sbyte>)array).ImaginaryByteBuffer);
						tag.WriteTo(bw);
					}
					break;
				case MLArray.mxUINT16_CLASS:
					tag = new OSArrayTag(MatDataTypes.miUINT16,
						((MLNumericArray<ushort>)array).RealByteBuffer);
					tag.WriteTo(bw);
					if (array.IsComplex)
					{
						tag = new OSArrayTag(MatDataTypes.miUINT16,
							((MLNumericArray<ushort>)array).ImaginaryByteBuffer);
						tag.WriteTo(bw);
					}
					break;
				case MLArray.mxINT16_CLASS:
					tag = new OSArrayTag(MatDataTypes.miINT16,
						((MLNumericArray<short>)array).RealByteBuffer);
					tag.WriteTo(bw);
					if (array.IsComplex)
					{
						tag = new OSArrayTag(MatDataTypes.miINT16,
							((MLNumericArray<short>)array).ImaginaryByteBuffer);
						tag.WriteTo(bw);
					}
					break;
				case MLArray.mxUINT32_CLASS:
					tag = new OSArrayTag(MatDataTypes.miUINT32,
						((MLNumericArray<uint>)array).RealByteBuffer);
					tag.WriteTo(bw);
					if (array.IsComplex)
					{
						tag = new OSArrayTag(MatDataTypes.miUINT32,
							((MLNumericArray<uint>)array).ImaginaryByteBuffer);
						tag.WriteTo(bw);
					}
					break;
				case MLArray.mxINT32_CLASS:
					tag = new OSArrayTag(MatDataTypes.miINT32,
						((MLNumericArray<int>)array).RealByteBuffer);
					tag.WriteTo(bw);
					if (array.IsComplex)
					{
						tag = new OSArrayTag(MatDataTypes.miINT32,
							((MLNumericArray<int>)array).ImaginaryByteBuffer);
						tag.WriteTo(bw);
					}
					break;
				case MLArray.mxUINT64_CLASS:
					tag = new OSArrayTag(MatDataTypes.miUINT64,
						((MLNumericArray<ulong>)array).RealByteBuffer);
					tag.WriteTo(bw);
					if (array.IsComplex)
					{
						tag = new OSArrayTag(MatDataTypes.miUINT64,
							((MLNumericArray<ulong>)array).ImaginaryByteBuffer);
						tag.WriteTo(bw);
					}
					break;
				case MLArray.mxINT64_CLASS:
					tag = new OSArrayTag(MatDataTypes.miINT64,
						((MLNumericArray<long>)array).RealByteBuffer);
					tag.WriteTo(bw);
					if (array.IsComplex)
					{
						tag = new OSArrayTag(MatDataTypes.miINT64,
							((MLNumericArray<long>)array).ImaginaryByteBuffer);
						tag.WriteTo(bw);
					}
					break;
				case MLArray.mxSTRUCT_CLASS:
					// field name length
					int itag = 4 << 16 | MatDataTypes.miINT32 & 0xffff;
					bw.Write(itag);
					bw.Write(((MLStructure)array).MaxFieldLength);

					// Get Field Names
					tag = new OSArrayTag(MatDataTypes.miINT8, ((MLStructure)array).GetKeySetToByteArray());
					tag.WriteTo(bw);

					foreach (MLArray a in ((MLStructure)array).AllFields)
					{
						WriteMatrix(bw, a);
					}
					break;
				case MLArray.mxCELL_CLASS:
					foreach (MLArray a in ((MLCell)array).Cells)
					{
						WriteMatrix(bw, a);
					}
					break;
				case MLArray.mxSPARSE_CLASS:
					int[] ai;

					//write ir
					buffer = new MemoryStream();
					bufferBW = new BinaryWriter(buffer);
					ai = ((MLSparse)array).IR;
					foreach (int i in ai)
					{
						bufferBW.Write(i);
					}
					tag = new OSArrayTag(MatDataTypes.miINT32, buffer.ToArray());
					tag.WriteTo(bw);

					// write jc
					buffer = new MemoryStream();
					bufferBW = new BinaryWriter(buffer);
					ai = ((MLSparse)array).JC;
					foreach (int i in ai)
					{
						bufferBW.Write(i);
					}
					tag = new OSArrayTag(MatDataTypes.miINT32, buffer.ToArray());
					tag.WriteTo(bw);

					//write real
					buffer = new MemoryStream();
					bufferBW = new BinaryWriter(buffer);
					double[] ad = ((MLSparse)array).ExportReal();
					for (int i = 0; i < ad.Length; i++)
					{
						bufferBW.Write(ad[i]);
					}
					tag = new OSArrayTag(MatDataTypes.miDOUBLE, buffer.ToArray());
					tag.WriteTo(bw);

					//write imaginary
					if (array.IsComplex)
					{
						buffer = new MemoryStream();
						bufferBW = new BinaryWriter(buffer);
						ad = ((MLSparse)array).ExportImaginary();
						for (int i = 0; i < ad.Length; i++)
						{
							bufferBW.Write(ad[i]);
						}
						tag = new OSArrayTag(MatDataTypes.miDOUBLE, buffer.ToArray());
						tag.WriteTo(bw);
					}
					break;
				default:
					throw new MatlabIOException("Cannot write matrix of type: " + MLArray.TypeToString(array.Type));
			}

			// Write matrix
			output.Write(MatDataTypes.miMATRIX);  //matrix tag
			output.Write((int)ms.Length);  // size of the matrix
			output.Write(ms.ToArray());  // matrix data
		}

		/// <summary>
		/// Write the flag values from the <c>MLArray</c> to the <c>BinaryWriter</c> stream.
		/// </summary>
		/// <param name="bw"><c>BinaryWriter</c> stream</param>
		/// <param name="array"><c>MLArray</c> matrix</param>
		private void WriteFlags(BinaryWriter bw, MLArray array)
		{
			MemoryStream buffer = new MemoryStream();
			BinaryWriter bufferBW = new BinaryWriter(buffer);

			bufferBW.Write(array.Flags);

			if (array.IsSparse)
				bufferBW.Write(((MLSparse)array).MaxNZ);
			else
				bufferBW.Write((int)0);
			OSArrayTag tag = new OSArrayTag(MatDataTypes.miUINT32, buffer.ToArray());
			tag.WriteTo(bw);
		}

		/// <summary>
		/// Write the Dimensions values from the <c>MLArray</c> to the <c>BinaryWriter</c> stream.
		/// </summary>
		/// <param name="bw"><c>BinaryWriter</c> stream</param>
		/// <param name="array"><c>MLArray</c> matrix</param>
		private void WriteDimensions(BinaryWriter bw, MLArray array)
		{
			MemoryStream buffer = new MemoryStream();
			BinaryWriter bufferBW = new BinaryWriter(buffer);

			int[] dims = array.Dimensions;

			for (int i = 0; i < dims.Length; i++)
				bufferBW.Write(dims[i]);

			OSArrayTag tag = new OSArrayTag(MatDataTypes.miINT32, buffer.ToArray());
			tag.WriteTo(bw);

		}

		/// <summary>
		/// Write the Name values from the <c>MLArray</c> to the <c>BinaryWriter</c> stream.
		/// </summary>
		/// <param name="bw"><c>BinaryWriter</c> stream</param>
		/// <param name="array"><c>MLArray</c> matrix</param>
		private void WriteName(BinaryWriter bw, MLArray array)
		{
			//MemoryStream buffer = new MemoryStream();
			//BinaryWriter bufferBW = new BinaryWriter( buffer );

			byte[] nameByteArray = array.GetNameToByteArray();
			//bufferBW.Write( nameByteArray );

			OSArrayTag tag = new OSArrayTag(MatDataTypes.miINT8, nameByteArray/*buffer.ToArray()*/ );
			tag.WriteTo(bw);
		}

		/// <summary>
		/// Tiny class that represents MAT-file TAG
		/// It simplifies writing data.  Automates writing padding for instance.
		/// </summary>
		private class OSArrayTag : MatTag
		{
			private ByteBuffer _data;
			private int _padding;
			private bool _compressed;

			/// <summary>
			/// Creates TAG and sets its <c>size</c> as a size of byte array
			/// </summary>
			/// <param name="Type">The Matlab array type</param>
			/// <param name="Data"><c>byte[]</c></param>
			public OSArrayTag(int Type, byte[] Data) :
				this(Type, new ByteBuffer(Data)) { }

			/// <summary>
			/// Creates TAG and sets its <c>size</c> as a size of byte array
			/// </summary>
			/// <param name="Type">The Matlab array type</param>
			/// <param name="Data"><c>ByteBuffer</c></param>
			public OSArrayTag(int Type, ByteBuffer Data) :
				base(Type, Data.Limit)
			{
				_data = Data;
				_data.Rewind();
				_compressed = (Data.Limit >= 1) && (Data.Limit <= 4);
				_padding = GetPadding(Data.Limit, _compressed);
			}

			/// <summary>
			/// Writes tag and data to <c>BinaryWriter</c> output stream.  Writes padding if neccesary.
			/// </summary>
			/// <param name="os"><c>BinaryWriter</c> output stream</param>
			public void WriteTo(BinaryWriter os)
			{
				if (_compressed)
				{
					// Write out a compressed header
					uint tag = (uint)_size << 16 | (uint)_type;
					os.Write(tag);
					byte[] data = new byte[_data.Limit];
					_data.Get(ref data, 0, data.Length);
					os.Write(data, 0, data.Length);
					if (_padding > 0)
					{
						os.Write(new byte[_padding]);
					}
				}
				else
				{
					// Write out an uncompressed header
					os.Write(_type);
					os.Write(_size);

					int maxBuffSize = 1024;
                    int writeBuffSize = _data.Remaining < maxBuffSize ? _data.Remaining : maxBuffSize;
					byte[] tmp = new byte[writeBuffSize];
                    while (_data.Remaining > 0)
					{
                        int length = _data.Remaining > tmp.Length ? tmp.Length : _data.Remaining;
						_data.Get(ref tmp, 0, length);
						os.Write(tmp, 0, length);
					}

					if (_padding > 0)
					{
						os.Write(new byte[_padding]);
					}
				}
			}
		}
	}
}
