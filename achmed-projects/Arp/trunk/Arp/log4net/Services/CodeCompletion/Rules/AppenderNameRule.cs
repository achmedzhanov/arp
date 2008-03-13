using System.Collections.Generic;
using Arp.Common.Psi.Services.CodeCompletion;
using Arp.log4net.Psi;
using Arp.log4net.Psi.Tree;
using JetBrains.ReSharper.CodeInsight.Services.Lookup;
using JetBrains.ReSharper.CodeInsight.Services.Xml.CodeCompletion;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Xml.Tree;
using Arp.Common.Assertions;
using JetBrains.Util;

namespace Arp.log4net.Services.CodeCompletion.Rules
{
    public class AppenderNameRule : ICodeCompletionRule
    {
        #region ICodeCompletionRule Members

        public void Apply(BaseCodeCompletionContext context, IList<ILookupItem> result)
        {
            IAppenderRef appenderRef = GetAppenderRef(context);
            Assert.CheckNotNull(appenderRef);

            IL4NSection l4nSection = appenderRef.GetContainingElement<IL4NSection>(false);
            Assert.CheckNotNull(l4nSection);

            ICollection<IAppender> appenders = l4nSection.GetAppenders();

            foreach (IAppender appender in appenders)
            {
                DeclaredElementLookupItem item =
                    new DeclaredElementLookupItem(new DeclaredElementInstance((IDeclaredElement)appender), new DeclaredElementLookupItemCreationContext(context.ProjectFile), L4NLanguageService.L4N);
                item.InsertRange = new TextRange(0);
                item.ReplaceRange = new TextRange(0, context.GetPrefix().Length);
                result.Add(item);
            }
        }

        public bool IsApplicable(BaseCodeCompletionContext context)
        {
            if(!context.IsAvalilableAttributeValueCompletion)
                return false;

            return GetAppenderRef(context) != null;
        }

        private IAppenderRef GetAppenderRef(BaseCodeCompletionContext context)
        {
            return context.Token.GetContainingElement<IXmlTag>(false) as IAppenderRef;
        }

        #endregion
    }
}