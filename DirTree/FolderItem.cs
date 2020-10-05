namespace DirTree
{
	internal record FolderItem
	{
		public string Name { get; init; } = "";
		public string Path { get; init; } = "";
		public bool IsDirectory { get; init; }
	}
}