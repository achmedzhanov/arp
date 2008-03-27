using System;
using System.Collections.Generic;
using System.IO;
using Arp.UnusedReferences;
using JetBrains.ProjectModel;
using JetBrains.ProjectModel.Platforms;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Shell.Test;
using JetBrains.Util;
using NUnit.Framework;
using PlatformID=JetBrains.ProjectModel.Platforms.PlatformID;

namespace Arp.Tests
{
    [TestFixture]
    public class UnusedModulesProcessorTests : BaseTest
    {

        [Test]
        public void ShellEnviroment()
        {
            Pair<ISolution, IProjectFile> solutionHolder =
                CreateSingleFileSolution("UnusedReferences", "DeclaredVariable.cs");
            SolutionManager.Instance.CloseSolution(solutionHolder.First);
        }


        [Test]
        public void DeclaredVariable()
        {
            Pair<ISolution, IProjectFile> solutionHolder =
                CreateSingleFileSolution("UnusedReferences", "DeclaredVariable.cs");
            try
            {
                IProjectFile projectFile = solutionHolder.Second;
                ICollection<IModuleReference> references = projectFile.GetProject().GetModuleReferences();
                HashSet<IModule> modules = new HashSet<IModule>();
                foreach (IModuleReference reference in references)
                {
                    IModule module = reference.ResolveReferencedModule();
                    modules.Add(module);
                }

                IFile file = PsiManager.GetInstance(projectFile.GetSolution()).GetPsiFile(projectFile);
                Assert.IsNotNull(file);

//                new AllNonQualifiedReferencesResolver().ProcessFile(file);
                UnusedModulesProcessor processor = new UnusedModulesProcessor(modules);
                Assert.AreEqual(1, modules.Count);
                file.ProcessDescendants(processor);
                Assert.AreEqual(0, modules.Count);
            }
            finally
            {
                SolutionManager.Instance.CloseSolution(solutionHolder.First);
            }
        }
    }
}