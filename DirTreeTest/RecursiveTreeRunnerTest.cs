﻿using DirTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.IO;

namespace DirTreeTest
{
    [TestClass]
    public class RecursiveTreeRunnerTest
    {
        [TestMethod]
        public void CanBeConstructed_IsNotNull()
        {
            var options = new TreeRunnerOptions
            {
                Path = "."
            };
            var runner = new RecursiveTreeRunner(options, 0);

            runner.ShouldNotBeNull();
            runner.ShouldBeOfType<RecursiveTreeRunner>();
        }

        [TestMethod]
        public void Run_NoOptionsSet_Runs()
        {
            string basePath = SetUpFolders();

            var recursiveRunner = new RecursiveTreeRunner(new TreeRunnerOptions { Path = basePath }, 0);
            var result = recursiveRunner.Run();

            result.ShouldNotBeNull();
            result.DirectoryCount.ShouldBe(3);
            result.FileCount.ShouldBe(8);
        }

        [TestMethod]
        [DataRow(0, 0, 0)]
        [DataRow(1, 2, 3)]
        [DataRow(2, 3, 5)]
        [DataRow(3, 3, 8)]
        [DataRow(10, 3, 8)]
        public void Run_MaxDepths_Runs(int maxDepth, int expectedDirectoryCount, int expectedFileCount)
        {
            string basePath = SetUpFolders();

            var options = new TreeRunnerOptions
            {
                Path = basePath,
                MaxDepth = maxDepth
            };
            var recursiveRunner = new RecursiveTreeRunner(options, 0);
            var result = recursiveRunner.Run();

            result.ShouldNotBeNull();
            result.DirectoryCount.ShouldBe(expectedDirectoryCount);
            result.FileCount.ShouldBe(expectedFileCount);
        }

        [TestMethod]
        [DataRow(1, 2)]
        [DataRow(2, 3)]
        public void Run_OnlyDirectories_Runs(int maxDepth, int expectedDirectoryCount)
        {
            string basePath = SetUpFolders();

            var options = new TreeRunnerOptions
            {
                Path = basePath,
                OnlyDirectories = true,
                MaxDepth = maxDepth
            };
            var recursiveRunner = new RecursiveTreeRunner(options, 0);
            var result = recursiveRunner.Run();

            result.ShouldNotBeNull();
            result.DirectoryCount.ShouldBe(expectedDirectoryCount);
            result.FileCount.ShouldBe(0);
        }

        private static string SetUpFolders()
        {
            string basePath = "test-dir";
            if (new DirectoryInfo(basePath).Exists)
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

            using (File.Create(subSubDirectory.FullName + @"\f6")) { }
            using (File.Create(subSubDirectory.FullName + @"\f7")) { }
            using (File.Create(subSubDirectory.FullName + @"\f8")) { }

            return basePath;
        }
    }
}
