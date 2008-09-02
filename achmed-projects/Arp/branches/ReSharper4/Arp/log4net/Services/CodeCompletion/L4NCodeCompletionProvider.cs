
using System.Collections.Generic;
using Arp.Common.Assertions;
using Arp.Common.Psi.Services.CodeCompletion;
using Arp.log4net.Services.CodeCompletion;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.CodeInsight.Services.CodeCompletion;
using JetBrains.ReSharper.CodeInsight.Services.Lookup;
using JetBrains.ReSharper.CodeInsight.Services.Xml.CodeCompletion;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.TextControl;
using JetBrains.Util;

namespace Arp.log4net.Services.CodeCompletion
{
    [LanguageSpecificImplementation("L4N", typeof(ICodeCompletionServiceProvider))]
    public class L4NCodeCompletionProvider : BaseCodeCompletionProvider
    {
        protected override ICodeCompletionContext CreateContexts(ISolution solution, ITextControl textControl, CodeCompletionType codeCompletionType)
        {
            if (codeCompletionType == CodeCompletionType.BasicCompletion)
                return new L4NCodeCompletionContext(solution, textControl);
            return null;
        }
    }
}