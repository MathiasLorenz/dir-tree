namespace DirTree
{
	internal record DirTreeInfo
	{
		public string Path { get; init; } = "";
		public string Name { get; init; } = "";
		public string DisplayName { get; init; } = "";
		public bool IsDirectory { get; init; }
	}
}