using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;

namespace Arp.log4net.Psi.Tree
{
    public interface IAppender : IXmlTag /*, IDeclaredElement*/
    {
        IAppenderTypeName AppenderTypeName
        { get;}

        string Name
        { get;}

        IXmlAttributeValue NameValue
        { get;}

        IL4NSection Parent
        { get;}

    }
}