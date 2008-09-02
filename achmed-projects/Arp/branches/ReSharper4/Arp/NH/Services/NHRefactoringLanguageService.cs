using Arp.NH.Services.Refactorings;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Refactorings;
using JetBrains.ReSharper.Refactorings.Rename;
using JetBrains.ReSharper.Refactorings.RenameModel;
using JetBrains.ReSharper.Refactorings.Workflow;

namespace Arp.NH.Services
{
    [LanguageSpecificImplementation("NH", typeof(RefactoringLanguageService))]
    public class NHRefactoringLanguageService : RefactoringLanguageService
    {

        public override RenameBase CreateRename(RenameWorkflow workflow, ISolution solution, IRefactoringDriver driver)
        {
            return new NHRename(workflow, solution, driver);
        }

        public override bool RenameAvailable()
        {
            return true;
        }
        
    }
}