using JetBrains.ReSharper.CodeView.Descriptors;
using JetBrains.ReSharper.CodeView.Occurences;
using JetBrains.ReSharper.CodeView.Search;

namespace Arp.UnusedReferences
{
    public class UnusedReferencesSearchDescriptor : SearchDescriptor
    {
        public UnusedReferencesSearchDescriptor(SearchRequest request) : base(request)
        {
        }

        public override string GetResultsTitle(OccurenceSection section)
        {
            return "#GetResultsTitle";
        }
    }
}