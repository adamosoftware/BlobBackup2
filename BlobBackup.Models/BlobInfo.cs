using System;

namespace BlobBackup.Models
{
	public class BlobInfo
	{
		public string ContainerName { get; set; }
		public Uri Uri { get; set; }
		public DateTimeOffset? LastModified { get; set; }
		public long Length { get; set; }
	}
}
