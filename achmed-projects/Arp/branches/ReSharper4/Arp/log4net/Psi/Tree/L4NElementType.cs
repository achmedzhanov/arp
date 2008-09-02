using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;

namespace Arp.log4net.Psi.Tree
{
    public class L4NElementType
    {
        public static readonly CompositeNodeType APPENDER_ELEMENT = new APPENDER_ELEMENT_TYPE("APPENDER_ELEMENT_TYPE");
        public static readonly CompositeNodeType APPENDER_PARAM_ELEMENT = new APPENDER_PARAM_ELEMENT_TYPE("APPENDER_PARAM_ELEMENT_TYPE");
        public static readonly CompositeNodeType APPENDER_REF_ELEMENT = new APPENDER_ELEMENT_REF_TYPE("APPENDER_ELEMENT_REF_TYPE");
        public static readonly CompositeNodeType LOGGER_ELEMENT = new LOGGER_ELEMENT_TYPE("LOGGER_ELEMENT_TYPE");
        public static readonly CompositeNodeType L4NSECTION_ELEMENT = new L4NSECTION_ELEMENT_TYPE("L4NSECTION_ELEMENT_TYPE");
        public static readonly CompositeNodeType REFERENCE_NAME = new REFERENCE_NAME("REFERENCE_NAME");
        public static readonly CompositeNodeType REFERENCE_TYPE = new REFERENCE_TYPE("REFERENCE_TYPE");
        public static readonly CompositeNodeType REFERENCE_MODULE = new REFERENCE_MODULE("REFERENCE_MODULE");
        public static readonly CompositeNodeType REFERENCE_NAME_ATTRIBUTE_VALUE = new REFERENCE_NAME_ATTRIBUTE_VALUE("REFERENCE_REFERENCE_NAME_ATTRIBUTE_VALUE");
    }
}