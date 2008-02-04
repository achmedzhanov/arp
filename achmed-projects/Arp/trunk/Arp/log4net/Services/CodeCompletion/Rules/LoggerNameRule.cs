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
            IDeclarationsCache cache = PsiManager.GetInstance(context.Solution).GetDeclarationsCache(DeclarationsCacheScope.SolutionScope(context.Solution, true),true);
            Queue<IDeclaredElement> queue = new Queue<IDeclaredElement>(cache.GetElementsAtQualifiedName(""));
            
            while(queue.Count > 0)
            {
                IDeclaredElement current = queue.Peek();

                INamespace @namespace = current as INamespace;
                if (@namespace != null)
                {
// it's too large
//                    foreach (IDeclaredElement namespaceElement in @namespace.GetNestedElements(cache))
//                    {
//                        queue.Enqueue(namespaceElement); 
//                    }
                }
                else if (!(current is IClass))
                    continue;

                DeclaredElementFullNameLookupItem item = new DeclaredElementFullNameLookupItem(new DeclaredElementInstance(current),
                                                                               new DeclaredElementLookupItemCreationContext(context.ProjectFile),
                                                                               L4NLanguageService.L4N);

                item.InsertRange = new TextRange(0);
                item.ReplaceRange = new TextRange(0, context.GetPrefix().Length);
                result.Add(item);
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