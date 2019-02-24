using System;
using System.Collections.Generic;
using System.Text;

namespace BlobBackup.Models
{
	/// <summary>
	/// Names of API resources that sources must support and clients need to know
	/// </summary>
	public static class SourceAPIResources
	{
		public const string ListContainers = "ListContainers";
		public const string ListBlobs = "ListBlobs";
	}
}
