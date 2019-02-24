using BlobBackup.Models;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlobBackup.Target
{
	public abstract class BackupTarget
	{
		public BackupTarget(string sourceUrl)
		{
			SourceUrl = sourceUrl;
		}

		/// <summary>
		/// Location of backup source API
		/// </summary>
		public string SourceUrl { get; }

		protected virtual IAuthenticator GetAuthenticator()
		{
			return null;
		}

		public async Task BackupAsync()
		{
			var client = new RestClient(SourceUrl);
			client.Authenticator = GetAuthenticator();

			var containerRequest = new RestRequest(SourceAPIResources.ListContainers, DataFormat.Json);
			var containerHandle = client.ExecuteAsync<List<string>>(containerRequest, (response) =>
			{
				foreach (string containerName in response.Data)
				{
					var blobRequest = new RestRequest($"{SourceAPIResources.ListBlobs}/{containerName}", DataFormat.Json);					

					var blobHandle = client.ExecuteAsync<List<BlobInfo>>(blobRequest, (blobResponse) =>
					{
						foreach (var blob in blobResponse.Data)
						{
							BackupStatus status = GetBackupStatus(blob);
							switch (status.Action)
							{
								case Action.Backup:
									break;

								case Action.Skip:
									// log that we skipped it?
									break;
							}
						}
					});
				}
			});
		}

		/// <summary>
		/// Determine what to do with a blob when given info about it from the source
		/// </summary>
		private BackupStatus GetBackupStatus(BlobInfo blob)
		{
			throw new NotImplementedException();
		}
	}
}