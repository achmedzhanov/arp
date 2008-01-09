using System.Collections.Generic;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace Arp.log4net.Psi.Tree.Impl
{
    public abstract class BaseDeclaredElementReference<T> : CheckedReferenceBase<T>
        where T: IElement
    {
        protected BaseDeclaredElementReference(T owner) : base(owner)
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
        // TODO return correct resolve error code (now we return empty result only)
        public override ISymbolTable GetReferenceSymbolTable(bool useReferenceName)
        {
            ICollection<IDeclaredElement> candicates = GetDeclaredElements();

            if (candicates.Count == 0)
                return EmptySymbolTable.INSTANCE;


            WritableSymbolTableBase table = useReferenceName
                                                ? ((WritableSymbolTableBase) new NameSymbolTable(this.GetName(), true))
                                                : ((WritableSymbolTableBase) new SymbolTable(true));
            foreach (IDeclaredElement condidate in candicates)
            {
                table.AddSymbol(condidate, EmptySubstitution.INSTANCE, null, 0);
            }

            return table;
        }

        protected abstract ICollection<IDeclaredElement> GetDeclaredElements();

        public override IReference BindTo(IDeclaredElement element)
        {
            throw new System.NotImplementedException();
        }

        public override IReference BindTo(IDeclaredElement element, ISubstitution substitution)
        {
            throw new System.NotImplementedException();
        }

        public override ReferenceType ReferenceType
        {
            get { return ReferenceType.TEXT; }
        }

        public override IAccessContext GetAccessContext()
        {
            return null;
        }
    }
}