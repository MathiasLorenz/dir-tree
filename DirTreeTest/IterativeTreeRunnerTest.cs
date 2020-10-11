using DirTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

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
    }
}
