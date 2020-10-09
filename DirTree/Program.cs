namespace DirTree
{
    class Program
    {
        static void Main(string path, int maxDepth = 10)
        {
            var runner = new TreeRunner(path, maxDepth);
            runner.Run();
        }
    }
}
