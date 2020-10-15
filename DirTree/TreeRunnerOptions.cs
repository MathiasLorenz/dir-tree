namespace DirTree
{
    public record TreeRunnerOptions
    {
        public string Path { get; init; } = "";
        public int? MaxDepth { get; init; }
        public bool? OnlyDirectories { get; init; } = false;
        public TreeRunnerOrdering Ordering { get; init; } = TreeRunnerOrdering.Alphabetically;
    }
}
