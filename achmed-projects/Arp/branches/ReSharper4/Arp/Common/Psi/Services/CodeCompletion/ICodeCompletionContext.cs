using System.Collections.Generic;
using JetBrains.ReSharper.CodeInsight.Services.Lookup;
using JetBrains.Util;

namespace Arp.Common.Psi.Services.CodeCompletion
{
    public interface ICodeCompletionContext
    {
        bool IsAvailable();
        IList<ILookupItem> EvaluateLookupItems();

        TextRange PrefixRange
        {
            get;
        }

        string GetPrefix();
    }
}