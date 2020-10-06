using System;

namespace DirTree
{
    public class TreeRunner
    {
        private readonly string _path;

        public TreeRunner(string path)
        {
            _path = path;
        }

        public void Run()
        {
            Console.WriteLine(_path);

            var iterativeRunner = new IterativeTreeRunner(_path, 0);
            var folderCounts = iterativeRunner.Run();
            var (fileCount, directoryCount) = folderCounts; // You can do this too. You can also deconstruct in the line above.

            Console.WriteLine();
            Console.WriteLine($"{ directoryCount } director{ (directoryCount > 1 ? "ies" : "y")}, { fileCount } file{ (fileCount > 1 ? "s" : "" )}");
        }
    }
}
