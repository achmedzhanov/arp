
/*
This file was generated by Arp.Generator
*/
#region Imports

using Arp.log4net.Psi.Tree.Impl;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using System.Collections.Generic;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using ReferenceType=Arp.log4net.Psi.Tree.Impl.ReferenceType;

#endregion

namespace Arp.NH.Psi.Tree.Impl
{
    public partial class ClassElementImpl : XmlTag,  IClassElement, IQualifier
    {
        #region IQualifier Members

        public ISymbolTable GetSymbolTable()
        {
            ReferenceName name = GetNameReferenceName();
            if (name != null)
                return name.GetSymbolTable();
            else
                return EmptySymbolTable.INSTANCE;
        }

        public QualifierKind GetKind()
        {
            return QualifierKind.TYPE;
        }

        protected ReferenceName GetNameReferenceName()
        {
            if(NameAttribute == null)
                return null;

            ReferenceTypeAttributeValue attributeValue = (ReferenceTypeAttributeValue)NameAttribute.Value;
            if (attributeValue == null)
                return null;

            ReferenceType referenceType = attributeValue.GetReferenceType();
            if (referenceType == null)
                return null;

            return referenceType.ReferenceName;
        }

        ///<summary>
        ///
        ///            True means failed to resolve
        ///            
        ///</summary>
        ///
        public bool Resolved
        {
            get
            {
                ReferenceName name = GetNameReferenceName();
                if (name != null)
                    return name.Resolved;
                else
                    return false;
            }
        }

        #endregion
    }
}
