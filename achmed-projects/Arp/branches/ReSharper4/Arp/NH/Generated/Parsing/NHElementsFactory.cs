using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Xml.Parsing;

namespace Arp.NH.Psi.Tree.Parsing
{
    public partial class NHElementsFactory : XmlElementFactory
    {
        public NHElementsFactory(PsiLanguageType languageType) : base(languageType)
        {
        }
    }
}