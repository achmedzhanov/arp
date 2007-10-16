using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Tree;

namespace Arp
{
    public class UnusedModulesProjectVisitor : RecursiveProjectVisitor
    {
        // TODO
        
        public override void VisitProjectFile(IProjectFile projectFile)
        {
            base.VisitProjectFile(projectFile);

//            IFile file = PsiManager.GetInstance(projectFile.GetSolution()).GetPsiFile(projectFile);
//            if (file != null)
//            {
//                new AllNonQualifiedReferencesResolver().ProcessFile(file);
//                UnusedModulesProcessor processor = new UnusedModulesProcessor();
//                file.ProcessDescendants(processor);
//            }

        }
    }
}