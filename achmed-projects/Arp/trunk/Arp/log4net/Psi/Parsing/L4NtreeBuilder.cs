using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Xml.Parsing;
using JetBrains.Shell;

namespace Arp.log4net.Psi.Parsing
{
    public class L4NTreeBuilder : XmlTreeBuilder
    {
        public L4NTreeBuilder(XmlElementFactory elementFactory, CheckForInterrupt checkForInterrupt)
            : base(elementFactory, checkForInterrupt, L4NLanguageService.L4N)
        {
        }



    }
}