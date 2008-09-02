using Arp.Common.Psi.Services.CodeCompletion;
using Arp.log4net.Services.CodeCompletion;
using Arp.NH.Services.CodeCompletion;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.CodeInsight.Services.CodeCompletion;
using JetBrains.ReSharper.TextControl;

namespace Arp.NH.Services
{
    [LanguageSpecificImplementation("NH", typeof(ICodeCompletionServiceProvider))]
    public class NHCodeCompletionProvider : BaseCodeCompletionProvider
    {
        protected override ICodeCompletionContext CreateContexts(ISolution solution, ITextControl textControl, CodeCompletionType codeCompletionType)
        {
            if (codeCompletionType == CodeCompletionType.BasicCompletion)
                return new NHCodeCompletionContext(solution, textControl);
            return null;
        }

    }
}