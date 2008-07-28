using Arp.log4net.Psi;
using JetBrains.ReSharper.CodeInsight.Services.MatchingBraceHighlighting;
using JetBrains.ReSharper.CodeInsight.Services.Xml;

namespace Arp.Common.Psi.Services.CodeInside
{
    public class BaseCodeInsightService : XmlCodeInsightService
    {
        private readonly XmlMatchingBraceHighlightingProvider provider = new XmlMatchingBraceHighlightingProvider(L4NLanguageService.L4N);

        public override IMatchingBraceHighlightingProvider MatchingBraceHighlightingProvider
        {
            get
            {
                return provider;
            }
        }
    }
}