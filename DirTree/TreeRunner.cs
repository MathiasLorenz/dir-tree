using System;
using System.IO;
using System.Text;

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

            PrintFolderCounts(folderCounts);
        }

        private void PrintFolderCounts(FolderCounts folderCounts)
        {
            var (fileCount, directoryCount) = folderCounts;
            var stringBuilder = new StringBuilder();
            stringBuilder.Append('\n');

            stringBuilder.Append($"{ directoryCount } director{ (directoryCount > 1 ? "ies" : "y")}");
            if (_options.OnlyDirectories == false)
            {
                stringBuilder.Append($", { fileCount } file{ (fileCount > 1 ? "s" : "")}");
            }

            Console.WriteLine(stringBuilder.ToString());
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

    internal record FolderCounts(int FileCount, int DirectoryCount);
}
