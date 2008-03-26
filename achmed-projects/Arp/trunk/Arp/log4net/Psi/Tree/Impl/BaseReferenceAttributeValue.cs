using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;

namespace Arp.log4net.Psi.Tree.Impl
{
    public class BaseReferenceAttributeValue : CompositeElement, IXmlAttributeValueNode, IXmlAttributeValue
    {
        public BaseReferenceAttributeValue(CompositeNodeType _type) : base(_type)
        {
        }

        public IXmlAttributeValueNode ToTreeNode()
        {
            return this;
        }

        public string UnquotedValue
        {
            get
            {
                string text = this.GetText();
                int start = text.StartsWith("\"") ? 1 : 0;
                int end = text.EndsWith("\"") ? 1 : 0;
                int unquotedLen = (text.Length - start) - end;
                if (unquotedLen <= 0)
                {
                    return string.Empty;
                }
                return text.Substring(start, unquotedLen);

            }
        }

        public IXmlValueToken ValueToken
        {
            get
            {
                return null;
            }
        }
    }
}