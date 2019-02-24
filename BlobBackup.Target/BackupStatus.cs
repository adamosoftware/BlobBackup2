namespace BlobBackup.Target
{
	/// <summary>
	/// What to do with blob?
	/// </summary>
	internal enum Action
	{
		Backup,
		Skip
	}

	internal class BackupStatus
	{
		public BackupStatus(Action action, int version)
		{
			Action = action;
			Version = version;
		}

		public Action Action { get; }
		public int Version { get; }
	}
}