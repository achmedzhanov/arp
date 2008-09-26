using System;
using Arp.log4net.Psi.Tree.Impl;
using Arp.NH.Psi.Tree.Impl;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Xml.Tree;
using ReferenceType=Arp.log4net.Psi.Tree.Impl.ReferenceType;


namespace Arp.NH.Psi.Tree
{
    public static class ClassLikeElementImplUtils
    {
        public static IQualifier GetQualifier(INHElement @this, QualifierType qualifierType)
        {
            if (qualifierType == QualifierType.Type)
                return (IQualifier)@this;
            
            if(qualifierType == QualifierType.Table)
            {
                var table = GetNameReferenceTable(@this);
                return table;
            }

            throw new NotImplementedException();
        }

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

        private static ReferenceTable GetNameReferenceTable(INHElement @this)
        {
            var tag = @this as IXmlTag;
            var xmlAttribute = tag.GetAttribute(attribute => attribute.AttributeName == "table");

            if (xmlAttribute == null)
                return null;

            ReferenceTableAttributeValue attributeValue = (ReferenceTableAttributeValue)xmlAttribute.Value;
            if (attributeValue == null)
                return null;

            ReferenceTable referenceType = attributeValue.GetReferenceTable();
            return referenceType;
        }
    }
}