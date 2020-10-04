using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DirTree
{
    internal class TreeRunner
    {
		private readonly int _level;

		public string Path { get; init; }

        public TreeRunner(string path, int level)
        {
            Path = path;
			_level = level;
		}

        // Could return an object to reflect number of directories and files analyzed.
        public void Run()
        {
            var directoryInfo = new DirectoryInfo(Path);
            var directoryTreeInfos = GetDirectoryTreeInfo(directoryInfo);

            Console.WriteLine(Path);

            foreach (var info in directoryTreeInfos.OrderBy(x => x.Path))
            {
                if (info.IsDirectory)
                {
                    var runner = new TreeRunner(info.Path, _level + 1);
                    runner.Run();
                }
                else
                {
                    Console.WriteLine($"\t { info.Name }");
                }
            }
        }

        private List<DirTreeInfo> GetDirectoryTreeInfo(DirectoryInfo directoryInfo)
        {
            var filesInDirectory = directoryInfo.GetFiles();
            var subDirectoriesInDirectory = directoryInfo.GetDirectories();
            var directoryTreeInfos = new List<DirTreeInfo>();

            directoryTreeInfos.AddRange(filesInDirectory
                .Select(x =>
                    new DirTreeInfo
                    {
                        Path = x.FullName,
                        Name = x.Name,
                        IsDirectory = false
                    })
            );
            directoryTreeInfos.AddRange(subDirectoriesInDirectory
                .Select(x =>
                    new DirTreeInfo
                    {
                        Path = x.FullName,
                        Name = x.Name,
                        IsDirectory = true
                    })
            );

            return directoryTreeInfos;
        }
    }
}