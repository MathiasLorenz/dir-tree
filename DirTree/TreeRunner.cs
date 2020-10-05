using System;

namespace DirTree
{
    public class TreeRunner
    {
        private readonly string _path;

        public TreeRunner(string path)
        {
            _path = path;
        }

        public void Run()
        {
            Console.WriteLine(_path);
            var iterativeRunner = new IterativeTreeRunner(_path, 0);
            iterativeRunner.Run();
        }
    }
}
