using System;
using System.IO;

namespace DirTree
{
    class Program
    {
        static void Main(string[] args)
        {
            var dirInfo = new DirectoryInfo(".");

            Console.WriteLine(dirInfo.ToString());
        }
    }
}
