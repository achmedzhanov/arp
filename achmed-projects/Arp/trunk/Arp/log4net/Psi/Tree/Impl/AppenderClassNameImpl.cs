using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.Util;

namespace Arp.log4net.Psi.Tree.Impl
{
    public class AppenderClassNameImpl : XmlAttribute, IAppenderTypeName
    {
        public AppenderClassNameImpl(CompositeNodeType _type) : base(_type)
        {
        }
    }
}