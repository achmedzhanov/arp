using System.Collections.Generic;
using JetBrains.ReSharper.CodeInsight.Services.CSharp.CodeCompletion;
using JetBrains.ReSharper.CodeInsight.Services.Lookup;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Resolve;

namespace Arp.log4net.Services.CSharp
{
    [CodeCompletionListTransformationRule]
    public class TestCodeCompletionListTransformationRule : ICodeCompletionListTransformationRule
    {
        #region ICodeCompletionListTransformationRule Members

        ///<summary>
        ///
        ///            Determines whether the rule is applicable in the given context
        ///            
        ///</summary>
        ///
        public bool IsApplicable(CodeCompletionContext context)
        {
            return true;
        }

        ///<summary>
        ///
        ///            Applies the rule
        ///            
        ///</summary>
        ///
        public void Apply(CodeCompletionContext context, IList<ILookupItem> result)
        {
            // do nothing

            IUsingNamespaceDirective usingNamespaceDirective = context.Element.GetContainingElement<IUsingNamespaceDirective>(true);

            if(context.Reference == null)
                return;

            ISymbolTable table = context.Reference.GetCompletionSymbolTable();
            string[] names = table.Names("", true, delegate () { return false;});
        }

        ///<summary>
        ///
        ///            Gets flag indicating whether the rule can generate members
        ///            
        ///</summary>
        ///
        public bool CanGenerate
        {
            get { return true; }
        }

        #endregion
    }
}