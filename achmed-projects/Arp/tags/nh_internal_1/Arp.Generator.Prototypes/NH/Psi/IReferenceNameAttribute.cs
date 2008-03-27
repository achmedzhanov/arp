using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;

namespace Arp.Generator.Prototypes.NH.Psi
{
    public interface IReferenceNameAttribute : IXmlAttribute
    {
        IRefenceName ReferenceName { get;}
    }
}