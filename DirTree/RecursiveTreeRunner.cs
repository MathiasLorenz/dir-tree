using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("DirTreeTest")]
namespace DirTree
{
    internal class RecursiveTreeRunner
    {
        private readonly TreeRunnerOptions _options;
        private readonly int _level;

        public RecursiveTreeRunner(TreeRunnerOptions options, int level)
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
            var sortedFolderItems = FolderItemSorter.Sort(folderItems, _options.Ordering);
            int fileCount = 0;
            int directoryCount = 0;

            foreach (var folderItem in sortedFolderItems)
            {
                WriteNameToOutput(folderItem.Name);

                if (folderItem.IsDirectory)
                {
                    var runner = new RecursiveTreeRunner(_options with { Path = folderItem.Path }, _level + 1);
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

        private IEnumerable<FolderItem> GetFolderItems(DirectoryInfo directoryInfo)
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
                            LastModification = x.LastWriteTimeUtc,
                            FileSize = x.Length,
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
                        LastModification = x.LastWriteTimeUtc,
                        FileSize = null,
                        IsDirectory = true
                    })
            );

            return folderItems;
        }
    }
}