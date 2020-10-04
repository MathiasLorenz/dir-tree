using System;
using System.IO;

namespace DirTree
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\Mathias\GoogleDrive\DTU\11-semester\Master_things";

            var runner = new TreeRunner(path, 0);
            runner.Run();

            Console.WriteLine("Done :)");
        }
    }
}
