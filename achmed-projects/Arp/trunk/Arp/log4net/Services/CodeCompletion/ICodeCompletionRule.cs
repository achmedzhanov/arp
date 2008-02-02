using System.Collections.Generic;
using JetBrains.ReSharper.CodeInsight.Services.Lookup;

namespace Arp.log4net.Services.CodeCompletion
{
    public interface ICodeCompletionRule
    {
        void Apply(CodeCompletionContext context, IList<ILookupItem> result);
        bool IsApplicable(CodeCompletionContext context);
    }
}