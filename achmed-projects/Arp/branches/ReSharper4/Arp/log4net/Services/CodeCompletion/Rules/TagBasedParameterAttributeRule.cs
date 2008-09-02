using System.Collections.Generic;
using Arp.Common.Psi.Services.CodeCompletion;
using JetBrains.ReSharper.CodeInsight.Services.Lookup;
using JetBrains.ReSharper.CodeInsight.Services.Xml.CodeCompletion;
using JetBrains.ReSharper.Psi.Xml.Tree;

namespace Arp.log4net.Services.CodeCompletion.Rules
{
    public class TagBasedParameterAttributeRule : ICodeCompletionRule
    {
        #region ICodeCompletionRule Members

        public void Apply(BaseCodeCompletionContext context, IList<ILookupItem> result)
        {
            throw new System.NotImplementedException();
        }

        public bool IsApplicable(BaseCodeCompletionContext context)
        {
            if(context.IsAvalilableAttributeNameCompletion)
                return false;

            throw new System.NotImplementedException();
        }

        #endregion
    }
}