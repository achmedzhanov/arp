using System;
using Arp.Assertions;
using JetBrains.ReSharper.Psi.ExtensionsAPI;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace Arp.log4net.Psi.Tree.Impl
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
    }
}