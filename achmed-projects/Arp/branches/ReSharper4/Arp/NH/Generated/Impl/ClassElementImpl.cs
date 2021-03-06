
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
    public partial class ClassElementImpl : XmlTag, IClassElement, IQualifier, IQualifierOwner
    {
        public IQualifier GetQualifier(QualifierType qualifierType)
        {
            return ClassLikeElementImplUtils.GetQualifier(this, qualifierType);
        }

        #region IQualifier Members

        public ISymbolTable GetSymbolTable(params string[] referenceNames)
        {
            return ClassLikeElementImplUtils.GetSymbolTable(this, referenceNames);
        }

        public QualifierKind GetKind()
        {
            return ClassLikeElementImplUtils.GetKind(this);
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
                return ClassLikeElementImplUtils.Resolved(this);
            }
        }

        #endregion
    }
}
