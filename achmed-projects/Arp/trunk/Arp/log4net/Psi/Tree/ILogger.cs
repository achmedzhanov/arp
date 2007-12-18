using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;

namespace Arp.log4net.Psi.Tree
{
    public interface ILogger : IXmlTag
    {
        string Name { get;}

        IL4NSection Parent
        { get;}

    }
}