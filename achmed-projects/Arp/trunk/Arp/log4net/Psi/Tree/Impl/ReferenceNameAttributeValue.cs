using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;

namespace Arp.log4net.Psi.Tree.Impl
{
    public class ReferenceNameAttributeValue : CompositeElement, IXmlAttributeValue, IXmlAttributeValueNode
    {
        public ReferenceNameAttributeValue(): base(L4NElementType.REFERENCE_NAME_ATTRIBUTE_VALUE)
        {
        }

        #region IXmlAttributeValue Members

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

        #endregion

        #region IXmlAttributeValueNode Members

        public IXmlValueToken ValueToken
        {
            get
            {
                return null;
            }
        }

        #endregion
    }
}