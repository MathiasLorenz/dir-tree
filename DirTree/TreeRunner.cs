using System;
using System.IO;

namespace DirTree
{
    public class TreeRunner
    {
        private readonly TreeRunnerOptions _options;

        public TreeRunner(TreeRunnerOptions options)
        {
            _options = options;
        }

        public void Run()
        {
            if (!EnsurePath())
            {
                return;
            }

            Console.WriteLine(_options.Path);

            var iterativeRunner = new IterativeTreeRunner(_options, 0);
            var folderCounts = iterativeRunner.Run();
            var (fileCount, directoryCount) = folderCounts; // You can do this too. You can also deconstruct in the line above.

            Console.WriteLine();
            Console.WriteLine($"{ directoryCount } director{ (directoryCount > 1 ? "ies" : "y")}, { fileCount } file{ (fileCount > 1 ? "s" : "" )}");
        }

        private bool EnsurePath()
        {
            try
            {
                var directoryInfo = new DirectoryInfo(_options.Path);
                if (directoryInfo.Exists == false)
                {
                    Console.WriteLine("The specified path was not a folder. Please supply a proper folder.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not open specified directory.");
                Console.WriteLine("Exception thrown was:");
                Console.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }
    }

    public record TreeRunnerOptions
    {
        public string Path { get; init; } = "";
        public int MaxDepth { get; set; } = 10;
    }

    internal record FolderCounts(int FileCount, int DirectoryCount);
}
