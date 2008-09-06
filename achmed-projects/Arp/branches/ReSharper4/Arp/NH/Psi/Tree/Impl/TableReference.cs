using System.Collections.Generic;
using Arp.Common.Assertions;
using Arp.Databases;
using Arp.Databases.Meta;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.Util;

namespace Arp.NH.Psi.Tree
{
    public class TableReference : CheckedReferenceBase<ReferenceTable>, ICompleteableReference
    {
        private ISymbolTable completionSymbolTable;

        public TableReference(ReferenceTable owner) : base(owner)
        {
        }

        public override ISymbolFilter[] GetSymbolFilters(out int mustRun)
        {
            mustRun = 0;
            return EmptyArray<ISymbolFilter>.Instance;
        }

        public override ResolveResult ResolveWithoutCache(out IResolveInfo resolveInfo)
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
            IDatabase database = DatabaseManager.Instance().GetDatabaseForSolution(myOwner.GetProject().GetSolution());

            var symbolTable = new SymbolTable(database.CaseSensitive);
            var tableLikeObjects = new List<ITableLike>();
            ICollection<ITable> tables = database.GetTables();
            if (tables != null)
                tableLikeObjects.AddRange(tables);
            ICollection<IView> views = database.GetViews();
            if (views != null)
                tableLikeObjects.AddRange(views);

            foreach (var table in tableLikeObjects)
            {
                Assert.Check(!string.IsNullOrEmpty(table.ShortName), "!string.IsNullOrEmpty(table.ShortName)");
                Assert.Check(!string.IsNullOrEmpty(table.QualifiedName), "!string.IsNullOrEmpty(table.QualifiedName)");
                symbolTable.AddSymbol(table.ShortName, new TableDeclaredElement(table, database, myOwner.GetProject()), EmptySubstitution.INSTANCE, null,0);
// TODO change table name perser to support "."
//                if (table.ShortName != table.QualifiedName)
//                    symbolTable.AddSymbol(table.QualifiedName, new TableDeclaredElement(table, database, myOwner.GetProject()), EmptySubstitution.INSTANCE, null,0);
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