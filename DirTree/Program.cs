using System;

namespace DirTree
{
    class Program
    {
        static void Main(string path)
        {
            var runner = new TreeRunner(path);
            runner.Run();
        }
    }
}
