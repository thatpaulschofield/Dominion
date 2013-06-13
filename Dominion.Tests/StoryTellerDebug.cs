using System;
using System.IO;
using NUnit.Framework;
using StoryTeller.Execution;
using StoryTeller.Workspace;

namespace Dominion.Tests
{
    [TestFixture]
    public class Template
    {
        private ProjectTestRunner runner;

        [TestFixtureSetUp]
        public void SetupRunner()
        {
            var project = new Project("DominionProject.xml");
            project.BinaryFolder = ".";
            runner = new ProjectTestRunner(project);
            var results = runner.GetResults();
            Console.WriteLine(results);
        }



        [TestFixtureTearDown]
        public void TeardownRunner()
        {
            try
            {
                runner.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}