using System;
using System.Collections;

namespace csmatio.io
{
	/// <summary>
	/// File filter.
	/// </summary>
	/// <remarks>
	/// This class is used to tell <c>MatFileReader</c> which matrices
	/// should be processed.  This is useful when operating on big MAT-files,
	/// when there's no need to load arrays into memory.
	/// </remarks>
	/// <example>
	/// <code>
	/// // Create new filter instance
	/// MatFileFilter filter = new MatFileFilter();
	/// // add a needle
	/// filter.AddArrayName( "your_array_name" );
	/// 
	/// // Read array from file (haystack) looking _only_ for a specific array (needle)
	/// MatFileReader mfr = new MatFileReader( fileName, filter );
	/// </code>
	/// </example>
	/// <author>David Zier (david.zier@gmail.com)</author>
	public class MatFileFilter
	{
		private ArrayList _filter;

		/// <summary>
		/// Creates an empty filter instance.
		/// 
		/// <i>Note: Empty filter accepts all results.</i>
		/// </summary>
		public MatFileFilter()
		{
			_filter = new ArrayList();
		}

		/// <summary>
		/// Creates a filter instance and add the array of names.
		/// </summary>
		/// <param name="Names">Array of names (needles)</param>
		public MatFileFilter( string[] Names ) :
			this()
		{
			foreach( string name in Names )
			{
				AddArrayName( name );
			}
		}

		/// <summary>
		/// Add an array name to the filter.  This array will be processed
		/// while crawling through the MAT-file.
		/// </summary>
		/// <param name="Name">Array name (needle)</param>
		public void AddArrayName( string Name )
		{
			_filter.Add( Name );
		}

		/// <summary>
		/// Test if given name matches the filter.
		/// </summary>
		/// <param name="Name">Array name to be tested</param>
		/// <returns><c>True</c> if array (matrix) of this name should be processed.</returns>
		public bool Matches( string Name )
		{
			if( _filter.Count == 0 )
				return true;
			return _filter.Contains( Name );
		}
	}
}
