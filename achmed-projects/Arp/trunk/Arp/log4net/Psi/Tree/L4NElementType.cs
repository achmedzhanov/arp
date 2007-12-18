using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;

namespace Arp.log4net.Psi.Tree
{
    public class L4NElementType
    {
        public static readonly CompositeNodeType APPENDER_ELEMENT = new APPENDER_ELEMENT_TYPE("APPENDER_ELEMENT_TYPE");
        public static readonly CompositeNodeType APPENDER_REF_ELEMENT = new APPENDER_ELEMENT_REF_TYPE("APPENDER_ELEMENT_REF_TYPE");
        public static readonly CompositeNodeType LOGGER_ELEMENT = new LOGGER_ELEMENT_TYPE("LOGGER_ELEMENT_TYPE");
        public static readonly CompositeNodeType L4NSECTION_ELEMENT = new L4NSECTION_ELEMENT_TYPE("L4NSECTION_ELEMENT_TYPE");
    }
}