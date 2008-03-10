using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Xml.Tree;

namespace Arp.NH.Psi
{
    public abstract class NHConpositeElementType : XmlElementTypes.XmlCompositeNodeType
    {
        protected NHConpositeElementType(string s)
            : base(s, NHLanguageService.NH)
        {
        }
    }
}