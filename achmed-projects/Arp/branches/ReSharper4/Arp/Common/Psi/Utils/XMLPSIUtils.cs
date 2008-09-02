using System;
using Arp.Common.Assertions;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Psi.ExtensionsAPI;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace Arp.Common.Psi.Utils
{
    public class XMLPSIUtils
    {
        public static void SetAttributeValiue(IXmlAttribute attribute, string newValue)
        {
            if (newValue == null) 
                throw new ArgumentNullException("newValue");
            
            Assert.CheckNotNull(attribute.Value, "attribute.Value");
            IXmlAttributeValueNode oldValueNode = attribute.Value.ToTreeNode();
            StringBuffer buffer = new StringBuffer("\"" + newValue + "\"");
            XmlTokenTypes types = XmlTokenTypeFactory.GetTokenTypes(attribute.Language);
            XmlValueToken newValueNode = new XmlValueToken(types.STRING, buffer, 0, buffer.Length);
            LowLevelModificationUtil.ReplaceChildRange(oldValueNode, oldValueNode, new ITreeNode[] { newValueNode });
        }

        public static DocumentRange GetFooterTagRange(IElement element, DocumentRange range)
        {
            IXmlTag tag = element as IXmlTag;
            if (tag == null)
                return DocumentRange.InvalidRange;
            if (tag.ToTreeNode().Header.Name.GetDocumentRange().TextRange == range.TextRange)
            {
                IXmlTagFooterNode footer = tag.ToTreeNode().Footer;
                if (footer != null)
                    return footer.Name.GetDocumentRange();
            }

            return DocumentRange.InvalidRange;
        }

    }
}