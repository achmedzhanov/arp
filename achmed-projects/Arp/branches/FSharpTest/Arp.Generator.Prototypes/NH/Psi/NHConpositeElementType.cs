using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Xml.Tree;

namespace Arp.Generator.Prototypes.NH.Psi
{
    public abstract class NHConpositeElementType : XmlElementTypes.XmlCompositeNodeType
    {

        protected NHConpositeElementType(string s)
            : base(s, PsiLanguageType.UNKNOWN)
        {
        }
    }
}