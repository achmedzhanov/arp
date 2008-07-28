using Arp.Common.Psi.Services.CodeInside;
using Arp.NH.Psi;
using JetBrains.ReSharper.CodeInsight.Services;
using JetBrains.ReSharper.Psi;

namespace Arp.NH.Services.CodeInside
{
    [CodeInsightService]
    public class NHCodeInsightService : BaseCodeInsightService
    {
        public override PsiLanguageType LanguageType
        {
            get { return NHLanguageService.NH; }
        }        
    }
}