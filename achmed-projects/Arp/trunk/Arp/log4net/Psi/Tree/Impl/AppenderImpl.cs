using System;
using System.Collections.Generic;
using System.Xml;
using Arp.Assertions;
using Arp.Utils;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Caches;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Impl;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace Arp.log4net.Psi.Tree.Impl
{
    public class AppenderImpl : TypedParametersOwner, IAppender, IDeclaration, IDeclaredElement, ITypeOwner, IParameterDescriptorProvider
    {
        private IDeclaration[] declarations;
        private ICollection<IParameterDescriptor> elementParametrInfos = null;

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

        public ICollection<IParam> GetParams()
        {
            return GetTagsByType<IParam>();
        }

        #region ITypeOwner

        #region IParameterDescriptorProvider

        public ICollection<IParameterDescriptor> GetParameterDescriptors()
        {
            if (elementParametrInfos == null)
            {
                if (!((IParameterDescriptorProvider)this).IsAvailable)
                    elementParametrInfos =  EmptyArray<IParameterDescriptor>.Instance;                
                else
                {
                    elementParametrInfos = ElementParametrInfoImplUtil.GetParameters(appenderCLRType);
                }
            }

            return elementParametrInfos;
        }


        public IParameterDescriptor GetElementParametrInfo(string name)
        {
            IList<IParameterDescriptor> found = CollectionUtil.FindAll(GetParameterDescriptors(), delegate(IParameterDescriptor obj)
                                                                                {
                                                                                    return obj.Name == name;
                                                                                });

            Assert.Check(found.Count < 2);

            return found.Count == 0 ? null : found[0];
        }

        bool IParameterDescriptorProvider.IsAvailable
        {
            get
            {
                return TypeFromAttribute != null;
            }
        }

        #endregion

        public IType Type
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region IDeclaration

        public void SetName(string name)
        {
            throw new NotImplementedException();
        }

        public TextRange GetNameRange()
        {
            TextRange treeTextRange = GetAttributeValueElement(L4NConstants.NAME).ToTreeNode().GetTreeTextRange();
            TextRange narrowed = RangeUtils.Narrow(treeTextRange, 1);
            return narrowed;
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

//        IDeclaredElement и IDeclaration не дожны быть оджнин и тем же?
//
//        м.б. ideclaration это appender-der ?

        #region IDeclaredElement

        public IDeclaration[] GetDeclarations()
        {
            if (declarations == null)
            {
                declarations = new IDeclaration[] { this };
            }
            return this.declarations;
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
            return SearchDomainFactory.Instance.CreateSearchDomain(this.GetProjectFile());
            
//            // TODO change method ?
//            ISearchDomain domain = SearchDomainFactory.Instance.CreateSearchDomain(this);
//            return domain;
        }

        public DeclaredElementType GetElementType()
        {
            return L4NDeclaredElementType.Appender;
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