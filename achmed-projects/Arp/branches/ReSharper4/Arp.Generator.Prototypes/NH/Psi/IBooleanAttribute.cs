using JetBrains.ReSharper.Psi.Xml.Tree;

namespace Arp.Generator.Prototypes.NH.Psi
{
    public interface IBooleanAttribute : IXmlAttribute
    {
        bool? ParsedValue { get;}
    }
}