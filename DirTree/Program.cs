using System;

namespace DirTree
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\Mathias\GoogleDrive\DTU\10-semester\Spectral_element_method";

            var runner = new TreeRunner(path);
            runner.Run();

            Console.WriteLine("Done :)");
        }
    }
}
