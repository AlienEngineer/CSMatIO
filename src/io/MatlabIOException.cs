using System.IO;

namespace csmatio.io
{
	/// <summary>
	/// MAT-file reader/writer exception
	/// </summary>
	/// <author>David Zier (david.zier@gmail.com)</author>
	public class MatlabIOException : IOException
	{
		/// <summary>
		/// Construct a new <c>MatlabIOException</c>.
		/// </summary>
		/// <param name="s">A string containing the error information.</param>
		public MatlabIOException( string s ) :
			base( s ) {}
	}
}
