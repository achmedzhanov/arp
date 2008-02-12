using JetBrains.ProjectModel;
using JetBrains.ReSharper.CodeInsight.Services.Lookup;
using JetBrains.ReSharper.Psi;

namespace Arp.log4net.Services.CodeCompletion
{
    public class DeclaredElementLookupItemCreationContext : IElementPointerFactory
    {
        protected readonly IProjectFile projectFile;

        public DeclaredElementLookupItemCreationContext(IProjectFile projectFile)
        {
            this.projectFile = projectFile;
        }

        public IElementInstancePointer<T> CreateElementPointer<T>(DeclaredElementInstance<T> instance) where T : IDeclaredElement
        {
            if (instance.Element.HasDeclarationsInProjectFile(this.projectFile))
            {
                return PsiManager.CreateElementInstancePointer<T>(instance);
            }
            return new TrivialElementInstancePointer<T>(instance);
        }
    }
}