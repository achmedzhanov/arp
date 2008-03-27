using System.Collections.Generic;
using JetBrains.ReSharper.CodeInsight.Services.Lookup;
using JetBrains.ReSharper.CodeInsight.Services.Xml.CodeCompletion;
using JetBrains.ReSharper.Psi.Xml.Tree;

namespace Arp.Common.Psi.Services.CodeCompletion
{
    public interface ICodeCompletionRule
    {
        void Apply(BaseCodeCompletionContext context, IList<ILookupItem> result);
        bool IsApplicable(BaseCodeCompletionContext context);
    }
}