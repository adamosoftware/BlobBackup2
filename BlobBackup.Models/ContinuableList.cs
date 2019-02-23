using System.Collections.Generic;

namespace BlobBackup.Models
{
	/// <summary>
	/// List that indicates whether there are more items to retrieve
	/// </summary>
	public class ContinuableList<T>
	{
		public ContinuableList(IEnumerable<T> items, bool isContinued)
		{
			Items = items;
			IsContinued = isContinued;
		}

		public IEnumerable<T> Items { get; }

		/// <summary>
		/// Indicates what there are more items
		/// </summary>
		public bool IsContinued { get; }
	}
}