using System.Collections.Generic;
using System.Xml;
using Arp.log4net.Psi.Tree.Impl;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace Arp.log4net.Psi.Tree.Impl
{
    public class ModuleDeclaredElement : IDeclaredElement
    {
        private readonly IModule module;


        public ModuleDeclaredElement(IModule module)
        {
            this.module = module;
        }

        #region IDeclaredElement Members

        public IDeclaration[] GetDeclarations()
        {
            return EmptyArray<IDeclaration>.Instance;
        }

        public IList<IDeclaration> GetDeclarationsIn(IProjectFile projectFile)
        {
            return EmptyArray<IDeclaration>.Instance;
        }

        public ISearchDomain GetAccessibilityDomain()
        {
            return SearchDomainFactory.Instance.CreateSearchDomain(module);
        }

        public DeclaredElementType GetElementType()
        {
            return ModuleDeclaredElementType.INSTANCE;
        }

        public PsiManager GetManager()
        {
            return PsiManager.GetInstance(module.GetSolution());
        }

        public ITypeElement GetContainingType()
        {
            return null;
        }

        public ITypeMember GetContainingTypeMember()
        {
            return null;
        }

        public XmlNode GetXMLDoc(bool inherit)
        {
            return null;
        }

        public XmlNode GetXMLDescriptionSummary(bool inherit)
        {
            return null;
        }

        public bool IsValid()
        {
            return true;
        }

        public bool IsSynthetic()
        {
            return false;
        }

        ///<summary>
        ///
        ///            Get the set of project files which contains the declaration of this element
        ///            
        ///</summary>
        ///
        public IProjectFile[] GetProjectFiles()
        {
            return EmptyArray<IProjectFile>.Instance;
        }

        ///<summary>
        ///
        ///            Checks if there are declarations of this element in given <paramref name="projectFile" />
        ///</summary>
        ///
        public bool HasDeclarationsInProjectFile(IProjectFile projectFile)
        {
            return false;
        }

        public string ShortName
        {
            get { return module.Name; }
        }

        public bool CaseSensistiveName
        {
            get { return true; }
        }

        public string XMLDocId
        {
            get { return null; }
        }

        ///<summary>
        ///
        ///            Get the language on which this element is declared
        ///            
        ///</summary>
        ///
        public PsiLanguageType Language
        {
            get { return  L4NLanguageService.L4N; }
        }

        public IModule Module
        {
            get { return module; }
        }

        public ISubstitution IdSubstitution
        {
            get { return EmptySubstitution.INSTANCE; }
        }

        #endregion
    }
}