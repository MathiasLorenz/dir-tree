namespace DirTree
{
    class Program
    {
        static void Main(string path, int maxDepth = 10, bool onlyDirectories = false)
        {
            var runner = new TreeRunner(new TreeRunnerOptions 
                { 
                    Path = path,
                    MaxDepth = maxDepth,
                    OnlyDirectories = onlyDirectories
                });
            runner.Run();
        }
    }
}
