using System;
using System.Drawing;
using Arp.log4net.Psi;
using Arp.log4net.Psi.Tree;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.CodeInsight.Services.Lookup;
using JetBrains.ReSharper.CodeInsight.Services.ParameterInfo;
using JetBrains.ReSharper.Psi;
using JetBrains.UI.RichText;

namespace Arp.log4net.Services
{
    // TODO IParameterInfoProvidingLookupItem we need to implement .PreferredDeclaredElement to show Parameter XML Doc (!)
    // TODO implement GetReferences -> reference to property
    // TODO see TooltipManager 
    public class ParameterLookupItem : DeclaredElementLookupItem 
    {
        private readonly IParameterDescriptor parameterDescriptor;


        public ParameterLookupItem(IParameterDescriptor parameterDescriptor, IElementPointerFactory elementInstancePointer)
            : 
            base(new DeclaredElementInstance(parameterDescriptor.DecraedElement), 
            elementInstancePointer, 
            L4NLanguageService.L4N )
        {
            if (parameterDescriptor == null) throw new ArgumentNullException("parameterDescriptor");
            this.parameterDescriptor = parameterDescriptor;


        }


        protected override string CalculateDisplayName(DeclaredElementInstance<IDeclaredElement> declaredElement,
                                                       out DeclaredElementPresenterMarking marking)
        {
            base.CalculateDisplayName(declaredElement, out marking);
            return parameterDescriptor.Name; 
        }

        protected override string CalculateTypeInName(DeclaredElementInstance<IDeclaredElement> declaredElement)
        {
            return parameterDescriptor.Name; 
        }
    }
}