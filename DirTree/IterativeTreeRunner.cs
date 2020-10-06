using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DirTree
{
    internal class IterativeTreeRunner
    {
        private readonly int _level;
        private readonly string _path;

        public IterativeTreeRunner(string path, int level)
        {
            _path = path;
            _level = level;
        }

        public FolderCounts Run()
        {
            var directoryInfo = new DirectoryInfo(_path);
            var folderItems = GetFolderItems(directoryInfo);
            int fileCount = 0;
            int directoryCount = 0;

            foreach (var info in folderItems.OrderBy(x => x.Path))
            {
                WriteNameToOutput(info);

                if (info.IsDirectory)
                {
                    var runner = new IterativeTreeRunner(info.Path, _level + 1);
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

        private void WriteNameToOutput(FolderItem info)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < _level; i++)
            {
                sb.Append("    ");
            }
            sb.Append("|-- ");
            sb.Append(info.Name);
            Console.WriteLine(sb.ToString());
        }

        private List<FolderItem> GetFolderItems(DirectoryInfo directoryInfo)
        {
            var filesInDirectory = directoryInfo.GetFiles();
            var directoriesInDirectory = directoryInfo.GetDirectories();
            var folderItems = new List<FolderItem>(filesInDirectory.Length + directoriesInDirectory.Length);

            folderItems.AddRange(filesInDirectory
                .Select(x =>
                    new FolderItem
                    {
                        Path = x.FullName,
                        Name = x.Name,
                        IsDirectory = false
                    })
            );
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