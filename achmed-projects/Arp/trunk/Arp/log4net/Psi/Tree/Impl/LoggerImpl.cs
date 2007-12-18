using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Xml.Impl.Tree;

namespace Arp.log4net.Psi.Tree.Impl
{
    public class LoggerImpl : BaseL4NTag,  ILogger
    {

        public LoggerImpl()
            : base(L4NElementType.LOGGER_ELEMENT)
        {
        }

        public string Name
        {
            get
            {
                return GetAttributeValue(L4NConstants.NAME, string.Empty);
            }
        }

        public new IL4NSection Parent
        {
            get { return (IL4NSection)base.Parent; }
        }
    }
}