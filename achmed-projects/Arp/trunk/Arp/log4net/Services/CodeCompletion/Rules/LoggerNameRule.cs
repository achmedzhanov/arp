using System.Collections.Generic;
using Arp.log4net.Psi;
using Arp.log4net.Psi.Tree;
using JetBrains.ReSharper.CodeInsight.Services.Lookup;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Caches;
using JetBrains.Util;

namespace Arp.log4net.Services.CodeCompletion.Rules
{
    // TODO use ICompleteableReference and IQualifier in CondeCompletionContext ?
    public class LoggerNameRule : ICodeCompletionRule
    {
        #region ICodeCompletionRule Members
        
        public void Apply(CodeCompletionContext context, IList<ILookupItem> result)
        {
            List<ILookupItem> toRemove = new List<ILookupItem>();
            foreach (ILookupItem item in result)
            {
                DeclaredElementLookupItem declaredElementLookupItem = item as DeclaredElementLookupItem;
                if(declaredElementLookupItem == null)
                    continue;

                IDeclaredElement element = declaredElementLookupItem.PreferredDeclaredElement.Element;
                if(!(element is IClass) &&!(element is INamespace))
                    toRemove.Add(declaredElementLookupItem);
            }

            foreach (ILookupItem item in toRemove)
            {
                result.Remove(item);
            }
        }

        public bool IsApplicable(CodeCompletionContext context)
        {
            if(!context.IsAvalilableAttributeValueCompletion)
                return false;

            if(GetLogger(context) == null)
                return false;

            return true;

        }

        private ILogger GetLogger(CodeCompletionContext context)
        {
            return context.GetAttributeTag() as ILogger;
        }

        #endregion
    }
}