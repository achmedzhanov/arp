using System.Collections.Generic;
using System.Xml;
using Arp.Databases.Meta;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Impl.Search.SearchDomain;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace Arp.NH.Psi.Tree
{
    public class TableDeclaredElement : IDeclaredElement, IDatabaseObjectDeclaredElement
    {
        private ITableLike table;
        private IDatabase database;
        private IProject project;

        public TableDeclaredElement(ITableLike table, IDatabase database, IProject project)
        {
            this.table = table;
            this.database = database;
            this.project = project;
        }

        #region IDeclaredElement Members

        public IList<IDeclaration> GetDeclarations()
        {
            return EmptyArray<IDeclaration>.Instance;
        }

        public IList<IDeclaration> GetDeclarationsIn(IProjectFile projectFile)
        {
            return EmptyArray<IDeclaration>.Instance;
        }

        public ISearchDomain GetAccessibilityDomain()
        {
            return EmptySearchDomain.Instance;
        }

        public DeclaredElementType GetElementType()
        {
            return TableDeclaredElementType.INSTANCE;
        }

        public PsiManager GetManager()
        {
            return PsiManager.GetInstance(project.GetSolution());
        }

        public JetBrains.ReSharper.Psi.ITypeElement GetContainingType()
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
        public IList<IProjectFile> GetProjectFiles()
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
            get { return table.ShortName; }
        }

        public bool CaseSensistiveName
        {
            get { return database.CaseSensitive; }
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
            get { return  NHLanguageService.NH; }
        }

        public IModule Module
        {
            get { return project; }
        }

        public ISubstitution IdSubstitution
        {
            get { return EmptySubstitution.INSTANCE; }
        }

        #endregion
    }
}