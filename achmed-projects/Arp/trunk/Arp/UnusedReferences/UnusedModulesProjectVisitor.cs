using System.Collections.Generic;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace Arp
{
    public class UnusedModulesProjectVisitor : RecursiveProjectVisitor
    {
        private HashSet<IModule> candidates = new HashSet<IModule>();
        private Dictionary<IProject, ICollection<IModule>> results = new Dictionary<IProject, ICollection<IModule>>();

        public UnusedModulesProjectVisitor() : base(false)
        {
        }

        public ICollection<IModule> GetUnusedModules(IProject project)
        {
            return results[project];
        }

        
        /// <summary>
        /// only for debug
        /// </summary>
        /// <returns></returns>
        public ICollection<IModule> GetAllUnusedModules()
        {
            List<IModule> ret = new List<IModule>();
            foreach (KeyValuePair<IProject, ICollection<IModule>> pair in results)
            {
                ret.AddRange(pair.Value);
            }
            return ret;            
        }

        public int GetTotalUnusedModules()
        {
            int count = 0;
            foreach (KeyValuePair<IProject, ICollection<IModule>> pair in results)
            {
                count += pair.Value.Count;
            }
            return count;
        }

        public override void VisitProject(IProject project)
        {
            SetupCandidates(project);
            base.VisitProject(project);
            results[project] = new List<IModule>(candidates);
        }

        private void SetupCandidates(IProject project)
        {
            candidates.Clear();

            ICollection<IAssemblyReference> assemblyReferences = project.GetAssemblyReferences();
            foreach (IAssemblyReference assemblyReference in assemblyReferences)
            {
                IModule module = assemblyReference.ResolveReferencedModule();
                if (module != null)
                    candidates.Add(module);
            }

            ICollection<IProjectReference> projectReferences = project.GetProjectReferences();
            foreach (IProjectReference projectReference in projectReferences)
            {
                IModule module = projectReference.ResolveReferencedModule();
                if (module != null)
                    candidates.Add(module);
            }
        }

        public override void VisitProjectFile(IProjectFile projectFile)
        {
            base.VisitProjectFile(projectFile);

            IFile file = PsiManager.GetInstance(projectFile.GetSolution()).GetPsiFile(projectFile);
            if (file != null)
            {
                new AllNonQualifiedReferencesResolver().ProcessFile(file);
                UnusedModulesProcessor processor = new UnusedModulesProcessor(candidates);
                file.ProcessDescendants(processor);
            }
        }
    }
}