using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;

namespace Arp.log4net.Psi.Tree
{
    public interface IAppenderRef : IXmlTag, IL4NElement
    {
        string Ref { get;}

        IXmlAttributeValue RefValue
        { get;}

        ILogger Parent
        { get;}

        void SetRef(string name);

    }
}