using System.Collections.Generic;
using JetBrains.ReSharper.CodeInsight.Services.Lookup;
using JetBrains.Util;

namespace Arp.log4net.Services.CodeCompletion
{
    public interface IL4NCodeCompletionContext
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