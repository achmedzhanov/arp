using System;
using System.Collections.Generic;
using System.IO;
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
    public class UnusedModulesProcessorTests
    {
        static UnusedModulesProcessorTests()
        {
            if (JetBrains.ReSharper.Shell.Shell.Instance == null)
            {
                new TestShell(typeof (UnusedModulesProcessorTests).Assembly, "Arp.Tests.TestShellConfig.xml");
            }
        }

        private const string testFilesDirectory = "..\\..\\..\\TestFiles";
        // Debug|Release\
        //              bin\
        //                  ProjectName\

        [Test]
        public void ShellEnviroment()
        {
            Pair<ISolution, IProjectFile> solutionHolder =
                CreateSingleFileSolution("UnusedReferences", "DeclaredVariable.cs");
            SolutionManager.Instance.CloseSolution(solutionHolder.First);
        }


        private Pair<ISolution, IProjectFile> CreateSingleFileSolution(string relativeProjectDirectory, string fileName)
        {
            PlatformInfo platformInfo = PlatformManager.Instance.GetPlatformInfo("Standard", Environment.Version);
            string projectDirectory = Path.Combine(testFilesDirectory, relativeProjectDirectory);

            FileSystemPath filePath = new FileSystemPath(Path.Combine(projectDirectory, fileName));
            if(!File.Exists(filePath.ToString()))
                Assert.Fail("File does not exists {0}", filePath);

            ISolution solution =
                SolutionManager.Instance.CreateSolution(
                    new FileSystemPath(Path.Combine(projectDirectory, "TestSolution.sln")));
            IProject project =
                solution.CreateProject(new FileSystemPath(Path.Combine(projectDirectory, "TestProject.csproj")),
                                       ProjectFileType.CSHARP,
                                       platformInfo.PlatformID);

            Arp.Assertions.Assert.CheckNotNull(platformInfo);
            project.AddAssemblyReference(platformInfo.MscorlibPath.ToString());
            
            IProjectFile file = project.CreateFile(filePath);

            return new Pair<ISolution, IProjectFile>(solution, file);
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
                List<IModule> modules = new List<IModule>();
                foreach (IModuleReference reference in references)
                {
                    IModule module = reference.ResolveReferencedModule();
                    modules.Add(module);
                }

                IFile file = PsiManager.GetInstance(projectFile.GetSolution()).GetPsiFile(projectFile);
                Assert.IsNotNull(file);

//                new AllNonQualifiedReferencesResolver().ProcessFile(file);
                UnusedModulesProcessor processor = new UnusedModulesProcessor(modules);
                Assert.AreEqual(1, processor.Candidates.Count);
                file.ProcessDescendants(processor);
                Assert.AreEqual(0, processor.Candidates.Count);
            }
            finally
            {
                SolutionManager.Instance.CloseSolution(solutionHolder.First);
            }
        }
    }
}