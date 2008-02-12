using System.Collections.Generic;
using Arp.log4net.Psi;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Refactorings;
using JetBrains.ReSharper.Refactorings.Conflicts;
using JetBrains.ReSharper.Refactorings.Rename;
using JetBrains.Util;

namespace Arp.log4net.Services.Refactorings
{
    [LanguageSpecificImplementation(L4NLanguageService.L4N_STRING, typeof(IRename))]
    public class L4NRename : IRename, ILanguageSpecificRefactoringService
    {
        // Methods
        public IList<IConflictSearcher> AdditionalConflictsSearchers(IDeclaredElement element, string newName)
        {
            return EmptyArray<IConflictSearcher>.Instance;
        }

        public void AdditionalReferenceProcessing(IDeclaredElement newTarget, IReference reference, ICollection<IReference> newReferences)
        {
        }

        public RenameAvailabilityCheckResult CheckAvailability(IDeclaredElement element)
        {
            return RenameAvailabilityCheckResult.CanBeRenamed;
        }

        public string[] GetPossibleReferenceNames(IDeclaredElement element, string newName)
        {
            return new string[] { element.ShortName };
        }

        public IDeclaredElement ReplaceDeclaredElement(IDeclaredElement element)
        {
            return element;
        }
    }

}