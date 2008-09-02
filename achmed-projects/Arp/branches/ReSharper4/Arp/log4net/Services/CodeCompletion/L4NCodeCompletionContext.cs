using System;
using System.Collections.Generic;
using Arp.Common.Assertions;
using Arp.Common.Psi.Services.CodeCompletion;
using Arp.log4net.Psi;
using Arp.log4net.Services.CodeCompletion.Rules;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.CodeInsight.Services.Lookup;
using JetBrains.ReSharper.CodeInsight.Services.Xml.CodeCompletion;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.TextControl;
using JetBrains.Util;

namespace Arp.log4net.Services.CodeCompletion
{
    public class L4NCodeCompletionContext : BaseCodeCompletionContext
    {
        public L4NCodeCompletionContext(ISolution solution, ITextControl textControl)
            : base(solution, textControl)
        {
        }

        protected override void InitilizeRules()
        {
            rules.Add(new AppenderNameRule());
            rules.Add(new TagBasedParameterNameRule());
            rules.Add(new ParameterAttributeNameRule());
            rules.Add(new AppenderTypeNameRule());
            rules.Add(new ParameterAttributeEnumerableValuesRule());
            rules.Add(new DeclaredParameterAttributesRule());
            rules.Add(new DeclaredParameterTypeNameRule());
            rules.Add(new LoggerNameRule());
//            rules.Add(new TagBasedParameterNameRule());
        }

    }
}