using System.Collections.Generic;
using JetBrains.ReSharper.CodeInsight.Services.Lookup;

namespace Arp.log4net.Services.CodeCompletion.Rules
{
    public class NamespaceorTypeNameRule : ICodeCompletionRule
    {
        #region ICodeCompletionRule Members

        public void Apply(CodeCompletionContext context, IList<ILookupItem> result)
        {
            throw new System.NotImplementedException();
        }

        public bool IsApplicable(CodeCompletionContext context)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}