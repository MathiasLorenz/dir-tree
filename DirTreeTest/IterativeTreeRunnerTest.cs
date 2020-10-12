using DirTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.IO;

namespace DirTreeTest
{
    [TestClass]
    public class IterativeTreeRunnerTest
    {
        [TestMethod]
        public void CanBeConstructed_IsNotNull()
        {
            var options = new TreeRunnerOptions
            {
                Path = "."
            };
            var runner = new IterativeTreeRunner(options, 0);

            runner.ShouldNotBeNull();
            runner.ShouldBeOfType<IterativeTreeRunner>();
        }

        [TestMethod]
        public void Run_NoOptionsSet_Runs()
        {
            string basePath = "test-dir";
            SetUpFolders(basePath);

            var iterativeRunner = new IterativeTreeRunner(new TreeRunnerOptions { Path = basePath }, 0);
            var result = iterativeRunner.Run();

            result.ShouldNotBeNull();
            result.DirectoryCount.ShouldBe(3);
            result.FileCount.ShouldBe(8);
        }

        private void SetUpFolders(string basePath)
        {
            var directoryInfo = new DirectoryInfo(basePath);
            if (directoryInfo.Exists)
            {
                Directory.Delete(basePath, true);
            }

            var newDirectory = Directory.CreateDirectory(basePath);
            using (File.Create(newDirectory.FullName + @"\file1.txt")) { }
            using (File.Create(newDirectory.FullName + @"\file2")) { }
            using (File.Create(newDirectory.FullName + @"\file3.md")) { }

            newDirectory.CreateSubdirectory("sub-dir1");
            var subDirectory = newDirectory.CreateSubdirectory("sub-dir2");
            using (File.Create(subDirectory.FullName + @"\file4.log")) { }
            using (File.Create(subDirectory.FullName + @"\file5")) { }

            var subSubDirectory = subDirectory.CreateSubdirectory("sub");
            using (File.Create(subDirectory.FullName + @"\f6")) { }
            using (File.Create(subDirectory.FullName + @"\f7")) { }
            using (File.Create(subDirectory.FullName + @"\f8")) { }
        }
    }
}
