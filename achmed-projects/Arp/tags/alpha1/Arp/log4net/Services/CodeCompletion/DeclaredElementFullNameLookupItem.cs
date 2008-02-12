using JetBrains.ReSharper.CodeInsight.Services.Lookup;
using JetBrains.ReSharper.Psi;

namespace Arp.log4net.Services.CodeCompletion
{
    public class DeclaredElementFullNameLookupItem : DeclaredElementLookupItem
    {
        public DeclaredElementFullNameLookupItem(DeclaredElementInstance instance, IElementPointerFactory elementPointerFactory, PsiLanguageType languageType) : base(instance, elementPointerFactory, languageType)
        {
        }


        protected override string CalculateDisplayName(DeclaredElementInstance<IDeclaredElement> declaredElement,
                                                       out DeclaredElementPresenterMarking marking)
        {
            string baseRet = base.CalculateDisplayName(declaredElement, out marking);
            ITypeElement typeElement = declaredElement.Element as ITypeElement;
            if (typeElement != null)
                return typeElement.CLRName;
            else
                return
                    baseRet;
        }

        protected override string CalculateTypeInName(DeclaredElementInstance<IDeclaredElement> declaredElement)
        {
            string baseRet = base.CalculateTypeInName(declaredElement);
            ITypeElement typeElement = declaredElement.Element as ITypeElement;
            if (typeElement != null)
                return typeElement.CLRName;
            else
                return
                    baseRet;
        }
    }
}