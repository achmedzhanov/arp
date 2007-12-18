using System;
using System.Collections.Generic;
using System.Xml;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace Arp.log4net.Psi.Tree.Impl
{
    public class AppenderImpl : BaseL4NTag, IAppender, IDeclaration, IDeclaredElement
    {
        public AppenderImpl()
            : base(L4NElementType.APPENDER_ELEMENT)
        {
        }

        public IAppenderTypeName AppenderTypeName
        {
            get { throw new System.NotImplementedException(); }
        }
        
        public string Name
        {
            get
            {
                return GetAttributeValue(L4NConstants.NAME, string.Empty);
            }
        }


        public IXmlAttributeValue NameValue
        {
            get { return GetAttributeValueElement(L4NConstants.NAME); }
        }

        public new IL4NSection Parent
        {
            get { return (IL4NSection)base.Parent; }
        }

        #region IDeclaration

        public void SetName(string name)
        {
            throw new NotImplementedException();
        }

        public TextRange GetNameRange()
        {
            return GetAttributeValueElement(L4NConstants.NAME).ToTreeNode().GetTreeTextRange();
        }

        public DocumentRange GetNameDocumentRange()
        {
            return GetAttributeValueElement(L4NConstants.NAME).GetDocumentRange();
        }

        public XmlNode GetXMLDoc(bool inherit)
        {
            return null;
        }

        public XmlNode GetXMLDescriptionSummary(bool inherit)
        {
            return null;
        }

        public bool IsSynthetic()
        {
            return false;
        }

        public IDeclaredElement DeclaredElement
        {
            get { return this; }
        }

        public string DeclaredName
        {
            get { return Name; }
        }

        #endregion

        #region IDeclaredElement

        public IDeclaration[] GetDeclarations()
        {
            return new IDeclaration[] {this};
        }

        public IList<IDeclaration> GetDeclarationsIn(IProjectFile projectFile)
        {
            if (this.GetProjectFile() == projectFile)
            {
                return GetDeclarations();
            }
            else
                // todo use one instance
                return new List<IDeclaration>();
        }

        public ISearchDomain GetAccessibilityDomain()
        {
            throw new NotImplementedException();
        }

        public DeclaredElementType GetElementType()
        {
            // TODO get logger type
            return null;
        }

        public ITypeElement GetContainingType()
        {
            // TODO get logger type
            return null;
        }

        public ITypeMember GetContainingTypeMember()
        {
            // TODO get logger type
            return null;
        }

        public string ShortName
        {
            get
            {
                return Name;
            }
        }

        public bool CaseSensistiveName
        {
            get
            {
                return true;
            }
        }

        public string XMLDocId
        {
            get
            {
                return null;
            }
        }

        public IModule Module
        {
            get
            {
                // TODO null ??
                return null;
            }
        }

        public ISubstitution IdSubstitution
        {
            get { return EmptySubstitution.INSTANCE; }
        }

        #endregion
    }
}