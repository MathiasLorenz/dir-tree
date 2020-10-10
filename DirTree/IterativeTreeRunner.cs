using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DirTree
{
    internal class IterativeTreeRunner
    {
        private readonly TreeRunnerOptions _options;
        private readonly int _level;

        public IterativeTreeRunner(TreeRunnerOptions options, int level)
        {
            _options = options;
            _level = level;
        }

        public FolderCounts Run()
        {
            if (_level == _options.MaxDepth)
            {
                return new FolderCounts(0, 0);
            }

            var directoryInfo = new DirectoryInfo(_options.Path);
            var folderItems = GetFolderItems(directoryInfo);
            int fileCount = 0;
            int directoryCount = 0;

            foreach (var info in folderItems.OrderBy(x => x.Path))
            {
                WriteNameToOutput(info.Name);

                if (info.IsDirectory)
                {
                    var runner = new IterativeTreeRunner(_options with { Path = info.Path }, _level + 1);
                    var subFolderCounts = runner.Run();

                    fileCount += subFolderCounts.FileCount;
                    directoryCount += subFolderCounts.DirectoryCount + 1;
                }
                else
                {
                    fileCount++;
                }
            }

            return new FolderCounts(fileCount, directoryCount);
        }

        private void WriteNameToOutput(string name)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < _level; i++)
            {
                sb.Append("    ");
            }
            sb.Append("|-- ");
            sb.Append(name);
            Console.WriteLine(sb.ToString());
        }

        private List<FolderItem> GetFolderItems(DirectoryInfo directoryInfo)
        {
            var filesInDirectory = directoryInfo.GetFiles();
            var directoriesInDirectory = directoryInfo.GetDirectories();
            var capacity = directoriesInDirectory.Length + (_options.OnlyDirectories == true ? 0 : filesInDirectory.Length);
            var folderItems = new List<FolderItem>(capacity);

            if (_options.OnlyDirectories == false)
            {
                folderItems.AddRange(filesInDirectory
                    .Select(x =>
                        new FolderItem
                        {
                            Path = x.FullName,
                            Name = x.Name,
                            IsDirectory = false
                        })
                );
            }
            
            folderItems.AddRange(directoriesInDirectory
                .Select(x =>
                    new FolderItem
                    {
                        Path = x.FullName,
                        Name = x.Name,
                        IsDirectory = true
                    })
            );

            return folderItems;
        }
    }
}