using JetBrains.ReSharper.CodeInsight.Services.Lookup;
using JetBrains.ReSharper.Psi;

namespace Arp.Common.Psi.Services.CodeCompletion
{
    public class DeclaredElementWithAliasLookupItem : DeclaredElementLookupItem
    {
        private readonly string alias;

        public DeclaredElementWithAliasLookupItem(string alias, DeclaredElementInstance instance, IElementPointerFactory elementPointerFactory, PsiLanguageType languageType) : base(instance, elementPointerFactory, languageType)
        {
            this.alias = alias;
        }

        protected override string CalculateDisplayName(DeclaredElementInstance<IDeclaredElement> declaredElement,
                                                       out DeclaredElementPresenterMarking marking)
        {
            base.CalculateDisplayName(declaredElement, out marking);
            return alias;
        }

        protected override string CalculateTypeInName(DeclaredElementInstance<IDeclaredElement> declaredElement)
        {
            base.CalculateTypeInName(declaredElement);
            return alias;
        }

    }
}