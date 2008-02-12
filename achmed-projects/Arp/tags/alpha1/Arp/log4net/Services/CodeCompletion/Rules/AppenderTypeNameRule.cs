using System.Collections.Generic;
using Arp.Assertions;
using Arp.log4net.Psi;
using Arp.log4net.Psi.Tree;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.CodeInsight.Services.Lookup;
using JetBrains.ReSharper.CodeView.Occurences;
using JetBrains.ReSharper.CodeView.Search;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Caches;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace Arp.log4net.Services.CodeCompletion.Rules
{
    public class AppenderTypeNameRule : TypeNameRuleBase
    {
        #region ICodeCompletionRule Members

        protected override ITypeElement GetBaseType(CodeCompletionContext context)
        {
            return GetTypeElement(context, L4NConstants.log4net.IAppender);
        }

        public override bool IsApplicable(CodeCompletionContext context)
        {
            if(!context.IsAvalilableAttributeValueCompletion)
                return false;

            if(!IsAttributeOf<IAppender>(L4NConstants.TYPE, context))
                return false;

//            TODO introduce base class for <dynamic parameter>@type parameter@type and renderer@type

            return true;
        }

        #endregion
    }
}