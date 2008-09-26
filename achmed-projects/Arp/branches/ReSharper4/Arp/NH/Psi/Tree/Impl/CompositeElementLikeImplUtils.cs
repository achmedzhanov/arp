using Arp.log4net.Psi.Tree.Impl;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Xml.Tree;
using ReferenceType=Arp.log4net.Psi.Tree.Impl.ReferenceType;

namespace Arp.NH.Psi.Tree.Impl
{
    public class CompositeElementLikeImplUtils
    {
        public static IQualifier GetQualifier(INHElement @this, QualifierType qualifierType)
        {
            if (qualifierType == QualifierType.Type)
                return GetNameReferenceName(@this);

            return null;
        }

        private static ReferenceName GetNameReferenceName(INHElement @this)
        {
            var tag = @this as IXmlTag;
            var xmlAttribute = tag.GetAttribute(attribute => attribute.AttributeName == "class");

            if (xmlAttribute == null)
                return null;

            ReferenceTypeAttributeValue attributeValue = (ReferenceTypeAttributeValue)xmlAttribute.Value;
            if (attributeValue == null)
                return null;

            ReferenceType referenceType = attributeValue.GetReferenceType();
            if (referenceType == null)
                return null;

            return referenceType.ReferenceName;
        }
    }
}