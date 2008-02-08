using System;
using Arp.Assertions;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Impl.Resolve;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Util;
using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.Util;

namespace Arp.log4net.Psi.Tree.Impl
{
    public class ReferenceName : CompositeElement, ICompleteableReference, IQualifier, IQualifiableReference, IAccessContext, ICheckedReference
    {
        private ResolveInfo resolveInfo;

        public ReferenceName(TreeElement identifier)
            : base(L4NElementType.REFERENCE_NAME)
        {
            AppendNewChild(identifier);
        }


        public ReferenceName(): base(L4NElementType.REFERENCE_NAME)
        {
        }

        public XmlToken NameToken
        {
            get
            {
                return (XmlToken)this.FindChildByRole(0x1a);
            }
        }


        public override short GetChildRole(TreeElement child)
        {
            if (child.type == L4NTokenNodeType.DOT)
            {
                return 14;
            }
            if (child.type == L4NTokenNodeType.IDENTIFIER)
            {
                return 0x1a;
            }
            if (child.type == L4NElementType.REFERENCE_NAME)
            {
                return 0x27;
            }
            return 0;
        }

        #region ICompleteableReference Members

        ///<summary>
        ///
        ///            Returns symbol table for code completion
        ///            
        ///</summary>
        ///
        public ISymbolTable GetCompletionSymbolTable()
        {
            ISymbolTable symbolTable = this.GetReferenceSymbolTable(false);
            // TODO check this code
            //return CompletionUtil.ApplyReferenceNameFilters(this, symbolTable);
            return symbolTable;
        }

        #endregion

        #region IReference Members

        ///<summary>
        ///
        ///            Returns "owner" of reference.
        ///            
        ///</summary>
        ///
        public IElement GetElement()
        {
            return this;
        }

        ///<summary>
        ///
        ///            Returns reference name.
        ///            This name usually coincides with short name of corresponding DeclaredElement.
        ///            (Only known exception is constructor initializer, its name is "this" or "base".)
        ///            
        ///</summary>
        ///
        public string GetName()
        {
            XmlToken token = this.NameToken;
            if (token == null)
            {
                return SharedImplUtil.MISSING_DECLARATION_NAME;
            }
            return ResolveUtil.ReferenceName(token.GetText());
        }

        ///<summary>
        ///
        ///            Returns all reference names (i.e., all possible short names of corresponding DeclaredElement)
        ///            
        ///</summary>
        ///
        public string[] GetAllNames()
        {
            string name = this.GetName();
            return new string[] { name };
        }

        ///<summary>
        ///
        ///            Returns full symbol table for the reference
        ///            
        ///</summary>
        ///
        public ISymbolTable GetReferenceSymbolTable(bool useReferenceName)
        {
            return ResolveUtil.GetSymbolTableByReference(this, useReferenceName);
        }

        ///<summary>
        ///
        ///            todo[dsl]: must write
        ///            
        ///</summary>
        ///
        public ResolveResult Resolve()
        {
            return ResolveUtil.ResolveWithCache(this);
        }

        ///<summary>
        ///
        ///</summary>
        ///
        public ResolveErrorType CheckResolveResult()
        {
            return CheckedReferenceImplUtil.CheckResolveResult(this);
        }

        ///<summary>
        ///
        ///            Bind this reference to given Declared Element. May insert using directive.
        ///            
        ///</summary>
        ///
        ///<returns>
        ///new "this"
        ///</returns>
        ///
        public IReference BindTo(IDeclaredElement element)
        {
            // TODO
            throw new NotImplementedException();
        }

        ///<summary>
        ///
        ///            Bind this reference to given Declared Element and substitution. May insert using directive.
        ///            
        ///</summary>
        ///
        ///<returns>
        ///new "this"
        ///</returns>
        ///
        public IReference BindTo(IDeclaredElement element, ISubstitution substitution)
        {
            throw new System.NotImplementedException();
        }

        ///<summary>
        ///
        ///            Returns access context containing reference. It is useful to define, if context
        ///            is static or to determine access rights of context.
        ///            
        ///</summary>
        ///
        public IAccessContext GetAccessContext()
        {
            return this;
        }

        ///<summary>
        ///
        ///</summary>
        ///
        public ReferenceType ReferenceType
        {
            get
            {
                return ReferenceType.REFERENCE_NAME;
            }
        }

        ///<summary>
        ///
        ///            Returns true iff reference could be resolved in declared elements with different names.
        ///            E.g., attribute [XXX] could be resolved into class with name 'XXX' or 'XXXAttribute' 
        ///            
        ///</summary>
        ///
        public bool HasMultipleNames
        {
            get
            {
                // TODO check this code
                return false;
            }
        }


        DocumentRange IReference.GetDocumentRange()
        {
            XmlToken identifier = this.NameToken;
            if (identifier != null)
            {
                return identifier.GetDocumentRange();
            }
            return DocumentRange.InvalidRange;
        }

        #endregion

        #region IQualifier Members

        public ISymbolTable GetSymbolTable()
        {
            ResolveResult result = this.Resolve();
            IDeclaredElement element = result.DeclaredElement;
            IProject project = this.GetProject();

            if (element == null)
                return EmptySymbolTable.INSTANCE;

            if (element is INamespace)
            {
                return ResolveUtil.GetSymbolTableByNamespace((INamespace) element, project, true);
            }
            else if(element is ITypeElement)
            {
                ISymbolTable table = ResolveUtil.GetQualifierSymbolTableByTypeElement((ITypeElement)element);
                if (result.Substitution is EmptySubstitution)
                {
                    return table;
                }
                return new SubstitutedSymbolTable(table, result.Substitution);
            }
            else
                throw new AssertException("Fail " + (element == null ? "'null'" : element.GetType().ToString()) + "");

        }

        public QualifierKind GetKind()
        {
            if (this.Reference.Resolve().DeclaredElement is INamespace)
            {
                return QualifierKind.NAMESPACE;
            }
            return QualifierKind.TYPE;

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
                IDeclaredElement element = this.Reference.Resolve().DeclaredElement;
                if (element == null)
                {
                    return false;
                }
                return element is INamespace;

            }
        }

        private IReference Reference
        {
            get { return this; }
        }

        #endregion

        #region IQualifiableReference Members

        public ResolveResult Resolve(ISymbolTable table, IAccessContext context, out ResolveInfo resolveInfo)
        {
            // TODO check this code
            if (table == EmptySymbolTable.INSTANCE)
            {
                resolveInfo = ResolveErrorType.NOT_RESOLVED;
                return ResolveResult.EMPTY;
            }
            return CheckedReferenceImplUtil.Resolve(this, table, out resolveInfo);
        }

        public IQualifier GetQualifier()
        {
            return this.Qualifier;
        }

        private ReferenceName Qualifier
        {
            get
            {
                return (ReferenceName)FindChildByRole(0x27);
            }
        }

        public bool IsQualified
        {
            get
            {
                return (this.GetQualifier() != null);
            }
        }

        public bool IsCaseSensitive
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region ITreeReference Members

        public void SetResolveError(ResolveInfo resolveInfo)
        {
            this.resolveInfo = resolveInfo;
        }

        public ResolveResult ResolveWithoutCache(out ResolveInfo resolveInfo)
        {
            // TODO check this code
            return ResolveUtil.ResolveQualifiableReference(this, this, out resolveInfo);
        }

        public ISymbolTable AdaptedSymbolTable(ISymbolTable originalTable)
        {
            return originalTable;
        }

        #endregion

        #region IAccessContext Members

        public ITypeElement GetAccessContainingTypeElement()
        {
            return ResolveUtil.GetAccessContainingTypeElement(this);
        }

        ///<summary>
        ///
        ///            Determines whether static members are accessible in this context.
        ///            
        ///</summary>
        ///
        ///            Staticness should be consistent with qualifier kind of access context.
        ///            All qualifier 
        ///            
        ///<returns>
        ///
        ///</returns>
        ///
        public Staticness GetStaticness()
        {
            // TODO check this code
            return AccessUtil.GetQualifierStaticness(this.GetQualifier());
        }

        public QualifierKind GetQualifierKind()
        {
            if (!this.IsQualified)
            {
                return QualifierKind.NONE;
            }
            return this.GetQualifier().GetKind();

        }

        public ITypeElement GetQualifierTypeElement()
        {
            ReferenceName qualifier = this.Qualifier;
            if (qualifier == null)
            {
                return null;
            }
            return (qualifier.Reference.Resolve().DeclaredElement as ITypeElement);
        }


        #endregion

        #region ICheckedReference Members

        public ISymbolFilter[] GetSymbolFilters(out int mustRun)
        {
//            string name = this.GetName();
//            TypeParameterNumberFilter filter = new TypeParameterNumberFilter(this.TypeArgumentsNumber);
//            ExactNameFilter nameFilter = new ExactNameFilter(name);
//            return new ISymbolFilter[] { nameFilter, TypeOrNamespaceFilter.INSTANCE, filter, PreImportedElementsFilter.INSTANCE, new AccessRightsFilter(this), ImportedElementsFilter.INSTANCE, OverriddenFilter.INSTANCE, new ExplicitSubstitutionProvider(this.TypeArguments), new LocalNamesFilter(this.GetProject(), true) };

            // TODO check this code
            mustRun = 0;
            return EmptyArray<ISymbolFilter>.Instance;
        }

        public ResolveInfo CurrentResolveInfo
        {
            get
            {
                return this.resolveInfo;
            }
            set
            {
                this.resolveInfo = value;
            }

        }

        #endregion

        public override ITreeReference[] GetInternalReferences()
        {
            return SharedImplUtil.CreateReferencesArray(this, base.GetInternalReferences());
        }
    }
}