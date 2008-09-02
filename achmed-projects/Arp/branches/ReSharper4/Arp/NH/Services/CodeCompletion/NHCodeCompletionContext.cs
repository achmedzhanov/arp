using Arp.Common.Psi.Services.CodeCompletion;
using Arp.log4net.Psi.Tree.Impl;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.TextControl;

namespace Arp.NH.Services.CodeCompletion
{
    public class NHCodeCompletionContext : BaseCodeCompletionContext
    {
        public NHCodeCompletionContext(ISolution solution, ITextControl textControl) : base(solution, textControl)
        {
        }

        protected override void InitilizeRules()
        {
            // there is no rules
        }


        protected override bool IncludeDeclaredElement(IDeclaredElement declaredElement)
        {
//            return base.IncludeDeclaredElement(declaredElement) || declaredElement is ModuleDeclaredElement;
            return true;
        }
    }
}