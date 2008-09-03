using System.Collections.Generic;
using Arp.NH.Psi;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Refactorings;
using JetBrains.ReSharper.Refactorings.Conflicts;
using JetBrains.ReSharper.Refactorings.Rename;
using JetBrains.ReSharper.Refactorings.RenameModel;
using JetBrains.ReSharper.Refactorings.Workflow;
using JetBrains.Util;

namespace Arp.NH.Services.Refactorings
{
    public class NHRename : RenameBase
    {
        public NHRename(RenameWorkflow workflow, ISolution solution, IRefactoringDriver driver) : base(workflow, solution, driver)
        {
        }

        // Methods
        public override IList<IConflictSearcher> AdditionalConflictsSearchers(IDeclaredElement element, string newName)
        {
            return EmptyArray<IConflictSearcher>.Instance;
        }

        public override void AdditionalReferenceProcessing(IDeclaredElement newTarget, IReference reference, ICollection<IReference> newReferences)
        {
        }


        public override string[] GetPossibleReferenceNames(IDeclaredElement element, string newName)
        {
            return new[] { element.ShortName };
        }


        public override bool DoNotProcess(IDeclaredElement element)
        {
            return false;
        }

        public override IReference TransformAnonymousType(IReference reference)
        {
            return reference;
        }

        public IDeclaredElement ReplaceDeclaredElement(IDeclaredElement element)
        {
            return element;
        }
    }
}