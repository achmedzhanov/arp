using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Xml.Parsing;
using JetBrains.ReSharper.Psi.Xml.Tree;

namespace Arp.Generator.Prototypes.NH.Psi.Parsing
{
    public partial class NHElementsFactory : XmlElementFactory
    {
        public NHElementsFactory(PsiLanguageType languageType) : base(languageType)
        {
        }

        public override IXmlTag CreateTag(IXmlTagHeaderNode header, IXmlTagContainer parentTag)
        {
            return base.CreateTag(header, parentTag);
        }
    }
}