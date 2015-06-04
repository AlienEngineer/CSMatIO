using System;

namespace csmatio.io
{
	/// <summary>
	/// MAT-file header
	/// </summary>
	/// <remarks>
	/// Level 5 MAT-files begin with a 128-byte header made up of a 124 byte text field
	/// and two, 16-bit flag fields
	/// </remarks>
	/// <author>David Zier (david.zier@gmail.com)</author>
	public class MatFileHeader
	{
		private static string DEFAULT_DESCRIPTIVE_TEXT = "MATLAB 5.0 MAT-file, Platform: " +
													Environment.OSVersion.Platform +
													", CREATED on: ";
		private static int DEFAULT_VERSION = 0x0100;
		private static byte[] DEFAULT_ENDIAN_INDICATOR = new byte[] {(byte)'I', (byte)'M'};

		private int _version;
		private string _description;
		private byte[] _endianIndicator;

		/// <summary>
		/// Create a new MAT-file header.
		/// </summary>
		/// <param name="Description">Descriptive text (no longer than 116 characters)</param>
		/// <param name="Version">By default, is set to 0x0100</param>
		/// <param name="EndianIndicator">Byte array size of 2 indicating byte-swapping requirements</param>
		public MatFileHeader( string Description, int Version, byte[] EndianIndicator )
		{
			_description = Description;
			_version = Version;
			_endianIndicator = EndianIndicator;
		}

		/// <summary>
		/// Gets the descriptive text.
		/// </summary>
		public string Description
		{
			get{ return _description; }
		}

		/// <summary>
		/// Gets the endian indicator.  Bytes written as "MI" suggest that byte-swapping operation is required
		/// in order to interpret data correctly.  If value is set to "IM", byte-swapping is not needed.
		/// </summary>
		public byte[] EndianIndicator
		{
			get{ return _endianIndicator; }
		}

		/// <summary>
		/// When creating a MAT-file, set version to 0x0100
		/// </summary>
		public int Version
		{
			get{ return _version; }
		}

		/// <summary>
		/// A static factory that creates a new <c>MatFileHeader</c> instance with default header values:
		/// <list type="bullet">
		/// <item>Mat-file is 5.0 version</item>
		/// <item>Version is set to 0x0100</item>
		/// <item>No byte-swapping</item>
		/// </list>
		/// </summary>
		/// <returns>A new <c>MatFileHeader</c> instance</returns>
		public static MatFileHeader CreateHeader()
		{
			return new MatFileHeader( DEFAULT_DESCRIPTIVE_TEXT + (DateTime.Now.ToString("R")),
				DEFAULT_VERSION, DEFAULT_ENDIAN_INDICATOR );
		}

		/// <summary>
		/// <see cref="object.ToString()"/>
		/// </summary>
		public override string ToString()
		{
			return "[descriptive text: " + _description + ", version: 0x" + _version.ToString("x04") + 
				", endianIndicator: " + (char)_endianIndicator[0] + (char)_endianIndicator[1] + "]";
		}
	}
}
