# BlobBackup2

A couple years ago I made a desktop app for Azure blob storage backup (private repo). As a desktop app, it does everything on the client side. It logs activity to a database, which can be anywhere. There are things I like about this, but the main discomfort I have is that the client needs to know the storage account details. It connects directly to the storage account and lists the blobs in it.

I'd like to rework this so the info about blobs to backup are surfaced through a web app's backup API, which can be setup with its own authentication requirements. Some blobs may be more or less public, while some are definitely private. This repo, therefore, represents steps in this direction. There are two main pieces:

- [BackupSource](https://github.com/adamosoftware/BlobBackup2/blob/master/BlobBackup.Source/BackupSource.cs) would be used in a web app to surface info about blobs that it wants backed up. As an abstract class, you must implement its [GetAccount](https://github.com/adamosoftware/BlobBackup2/blob/master/BlobBackup.Source/BackupSource.cs#L12) method. This is where you'd provide your own storage account credentials.

- [BackupTarget](https://github.com/adamosoftware/BlobBackup2/blob/master/BlobBackup.Target/BackupTarget.cs) would be used by a backup client (either on the desktop or another web app, or perhaps even the web app you started with) to request blob info from a source. It's here that you define the kind of authentication expected by overriding the [GetAuthenticator](https://github.com/adamosoftware/BlobBackup2/blob/master/BlobBackup.Target/BackupTarget.cs#L22) mnethod.

At this point I've made just baby steps, and I'm not sure how much time I'll be able to put into this.
