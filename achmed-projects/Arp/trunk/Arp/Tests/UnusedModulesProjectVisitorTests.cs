using System.Collections.Generic;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;
using NUnit.Framework;

namespace Arp.Tests
{
    [TestFixture]
    public class UnusedModulesProjectVisitorTests : BaseTest
    {
        [Test]
        public void UnusedAssembly()
        {
            Pair<ISolution, IProjectFile> solutionHolder =
                CreateSingleFileSolution("UnusedReferences", "DeclaredVariable.cs", new string[] {"System.Data"});
            try
            {
                IProjectFile projectFile = solutionHolder.Second;
                IProject project = projectFile.GetProject();
                UnusedModulesProjectVisitor visitor = new UnusedModulesProjectVisitor();
                project.GetSolution().Accept(visitor);
                ICollection<IModule> modules = visitor.GetUnusedModules(project);
                Assert.AreEqual(1, modules.Count);
                IModule first = CollectionUtil.GetFirst(modules);
                Assert.AreEqual("System.Data", first.Name);
            }
            finally
            {
                SolutionManager.Instance.CloseSolution(solutionHolder.First);
            }
        }        
    }
}