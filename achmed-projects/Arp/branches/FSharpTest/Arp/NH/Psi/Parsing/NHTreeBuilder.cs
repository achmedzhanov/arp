using JetBrains.ReSharper.Psi.Xml.Parsing;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Shell;

namespace Arp.NH.Psi.Parsing
{
    public class NHTreeBuilder : XmlTreeBuilder
    {
        public NHTreeBuilder(XmlElementFactory elementFactory, CheckForInterrupt checkForInterrupt)
            : base(elementFactory, checkForInterrupt, NHLanguageService.NH)
        {
        }

        protected override void AppendNewChild(IXmlAttribute parent, IXmlAttributeValue child)
        {
            base.AppendNewChild(parent, child);
        }
    }
}