using Arp.log4net.Psi.Tree.Impl;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Xml.Tree;
using ReferenceType=Arp.log4net.Psi.Tree.Impl.ReferenceType;


namespace Arp.NH.Psi.Tree
{
    public static class ClassLikeElementImplUtils
    {
        public static ISymbolTable GetSymbolTable(INHElement @this, params string[] referenceNames)
        {
            ReferenceName name = GetNameReferenceName(@this);
            if (name != null)
                return name.GetSymbolTable();
            else
                return EmptySymbolTable.INSTANCE;
        }

        public static QualifierKind GetKind(INHElement @this)
        {
            return QualifierKind.TYPE;
        }

        public static bool Resolved(INHElement @this)
        {

                ReferenceName name = GetNameReferenceName(@this);
                if (name != null)
                    return name.Resolved;
                else
                    return false;
        }

        private static ReferenceName GetNameReferenceName(INHElement @this)
        {
            var tag = @this as IXmlTag;
            var xmlAttribute = tag.GetAttribute(attribute => attribute.AttributeName == "name");

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