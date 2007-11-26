using System;
using System.IO;
using System.Reflection;
using JetBrains.ProjectModel;
using JetBrains.ProjectModel.Platforms;
using JetBrains.Shell.Test;
using JetBrains.Util;
using NUnit.Framework;

namespace Arp.Tests
{
    public class BaseTest
    {
        static BaseTest()
        {
            if (JetBrains.Shell.Shell.Instance == null)
            {
                try
                {
                    new TestShell(typeof (UnusedModulesProcessorTests).Assembly, "Arp.Tests.TestShellConfig.xml");
                }
                catch (ReflectionTypeLoadException ex)
                {
                    Exception[] exceptions = ex.LoaderExceptions;
                    throw;
                }
            }
        }


        // Debug|Release\
        //              bin\
        //                  ProjectName\


        private const string testFilesDirectory = "..\\..\\..\\TestFiles";

        protected Pair<ISolution, IProjectFile> CreateSingleFileSolution(string relativeProjectDirectory, string fileName)
        {
            return CreateSingleFileSolution(relativeProjectDirectory, fileName, null);
        }

        protected Pair<ISolution, IProjectFile> CreateSingleFileSolution(string relativeProjectDirectory, string fileName, string [] systemAssemblies)
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
            
            if(systemAssemblies != null)
            {
                foreach (string systemAssemblyName in systemAssemblies)
                {
                    string assemblyPath = GetSystemAssemblyPath(platformInfo, systemAssemblyName);
                    project.AddAssemblyReference(assemblyPath);
                }
            }

            IProjectFile file = project.CreateFile(filePath);

            return new Pair<ISolution, IProjectFile>(solution, file);
        }

        private string GetSystemAssemblyPath(PlatformInfo platformInfo, string systemAssemblyName)
        {
            foreach (FileSystemPath systemAssemblyFolder in platformInfo.GetSystemAssemblyFolders())
            {
                string pathForTring = Path.Combine(systemAssemblyFolder.ToString(), systemAssemblyName + ".dll");
                if (File.Exists(pathForTring))
                    return pathForTring;
            }

            throw new ArgumentException("Assembly does not exissts in system directories", "systemAssemblyName");
        }
    }
}