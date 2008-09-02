using System.Collections.Generic;
using JetBrains.ProjectModel;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.Util;

namespace Arp.log4net.Psi.Tree.Impl
{
    public class ModuleReference : CheckedReferenceBase<ReferenceModule>, ICompleteableReference
    {
        private ISymbolTable completionSymbolTable;

        public ModuleReference(ReferenceModule owner) : base(owner)
        {
        }



        public override ISymbolFilter[] GetSymbolFilters(out int mustRun)
        {
            mustRun = 0;
            return EmptyArray<ISymbolFilter>.Instance;
        }

        public override ResolveResult ResolveWithoutCache(out ResolveInfo resolveInfo)
        {
            ISymbolTable table = this.GetReferenceSymbolTable(true);
            IList<ISymbolInfo> infos = table.GetAllSymbolInfos(GetName());
            ResolveResult result = base.ResolveInTable(table, out resolveInfo);
            return result;
        }

        public override string GetName()
        {
            return myOwner.GetText();
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
            if (completionSymbolTable == null)
                completionSymbolTable = CreateCompletionSymboltavble();
            return completionSymbolTable;
        }

        private ISymbolTable CreateCompletionSymboltavble()
        {
            IProject project = myOwner.GetProject();
            ICollection<IModuleReference> references = project.GetModuleReferences();
//            NameSymbolTable symbolTable = new NameSymbolTable(this.GetName(),true);
            SymbolTable symbolTable = new SymbolTable(true);
            foreach (IModuleReference reference in references)
            {
                IModule module = reference.ResolveReferencedModule();
                if (module != null)
                    symbolTable.AddSymbol(new ModuleDeclaredElement(module),EmptySubstitution.INSTANCE, null, 0);
            }
            return symbolTable;
        }

        #endregion



        public override ISymbolTable GetReferenceSymbolTable(bool useReferenceName)
        {
            return GetCompletionSymbolTable();
        }

        public override IReference BindTo(IDeclaredElement element)
        {
            throw new System.NotImplementedException();
        }

        public override IReference BindTo(IDeclaredElement element, ISubstitution substitution)
        {
            throw new System.NotImplementedException();
        }

        public override IAccessContext GetAccessContext()
        {
            return null;
        }

        public override TextRange GetTreeTextRange()
        {
            return myOwner.GetDocumentRange().TextRange;
        }

        public override DocumentRange GetDocumentRange()
        {
            return myOwner.GetDocumentRange();
        }

        public override JetBrains.ReSharper.Psi.Resolve.ReferenceType ReferenceType
        {
            get { return JetBrains.ReSharper.Psi.Resolve.ReferenceType.TEXT; }
        }


        ///<summary>
        ///
        ///            Checks whether the reference can provide a meaningful symbol table for completion
        ///            
        ///</summary>
        ///
        public bool CanComplete
        {
            get { return true; }
        }
    }
}