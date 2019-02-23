using BlobBackup.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlobBackup.Server
{
	public abstract class BackupServer
	{
		public abstract CloudStorageAccount GetAccount();

		public async Task<IEnumerable<string>> ListContainersAsync(string prefix = null)
		{
			var account = GetAccount();
			var client = account.CreateCloudBlobClient();

			BlobContinuationToken token = null;
			List<string> results = new List<string>();
			do
			{
				var response = await client.ListContainersSegmentedAsync(prefix, token);
				token = response.ContinuationToken;
				results.AddRange(response.Results.Select(c => c.Name));
			}
			while (token != null);

			return results;
		}

		public async Task<IEnumerable<BlobInfo>> ListBlobsAsync(string containerName, string prefix = null)
		{
			var account = GetAccount();
			var client = account.CreateCloudBlobClient();
			var container = client.GetContainerReference(containerName);

			BlobContinuationToken token = null;
			List<BlobInfo> results = new List<BlobInfo>();
			do
			{
				var response = await container.ListBlobsSegmentedAsync(prefix, token);
				token = response.ContinuationToken;
				results.AddRange(response.Results.OfType<CloudBlockBlob>().Select(b => new BlobInfo()
				{
					Uri = b.Uri,
					LastModified = b.Properties.LastModified,
					Length = b.Properties.Length
				}));
			} while (token != null);

			return results;
		}
	}
}