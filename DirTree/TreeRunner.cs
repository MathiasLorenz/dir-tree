using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DirTree
{
    internal class TreeRunner
    {
		private readonly int _level;
        private readonly string _path;

        public TreeRunner(string path, int level)
        {
            _path = path;
            _level = level;
		}

        // Could return an object to reflect number of directories and files analyzed.
        public void Run()
        {
            var directoryInfo = new DirectoryInfo(_path);
            var folderItems = GetFolderItems(directoryInfo);

            foreach (var info in folderItems.OrderBy(x => x.Path))
            {
                WriteNameToOutput(info);

                if (info.IsDirectory)
                {
                    var runner = new TreeRunner(info.Path, _level + 1);
                    runner.Run();
                }
            }
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
            var folderItems = new List<FolderItem>();

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