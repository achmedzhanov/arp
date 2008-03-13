using Arp.Common.Psi.Services.CodeInside;
using Arp.log4net.Psi;
using JetBrains.ReSharper.CodeInsight.Services;
using JetBrains.ReSharper.CodeInsight.Services.MatchingBraceHighlighting;
using JetBrains.ReSharper.CodeInsight.Services.Xml;
using JetBrains.ReSharper.Psi;

namespace Arp.log4net.Services
{
    [CodeInsightService]
    public class L4NCodeInsightService : BaseCodeInsightService
    {
        public override PsiLanguageType LanguageType
        {
            get { return L4NLanguageService.L4N; }
        }
    }
}