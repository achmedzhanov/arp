using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi.Xml.Tree;

namespace Arp.log4net.Psi.Tree.Impl
{
    public class AttributeParamImpl : IDeclaredParameter
    {
        private readonly string name;
        private readonly DocumentRange nameDocumentRange;
        private readonly string stringValue;
        private readonly IParameterDescriptorProvider parameterDescriptorProvider;


        public AttributeParamImpl(IXmlAttribute xmlAttribute,IParameterDescriptorProvider parameterDescriptorProvider)
        {
            name = xmlAttribute.AttributeName;
            nameDocumentRange = xmlAttribute.ToTreeNode().NameNode.GetDocumentRange();
            stringValue = xmlAttribute.UnquotedValue;
            this.parameterDescriptorProvider = parameterDescriptorProvider;
        }

        #region IDeclaredParameter Members

        public string Name
        {
            get { return name; }
        }

        public DocumentRange NameDocumentRange
        {
            get { return nameDocumentRange; }
        }

        public string StringValue
        {
            get { return stringValue; }
        }

        public IParameterDescriptorProvider ParameterDescriptorProvider
        {
            get { return parameterDescriptorProvider; }
        }

        #endregion
    }
}