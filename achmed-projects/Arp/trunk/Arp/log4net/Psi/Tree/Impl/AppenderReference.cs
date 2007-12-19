using System.Collections.Generic;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.Util;

namespace Arp.log4net.Psi.Tree.Impl
{
    //TODO use CheckedReferenceBase or TreeReferenceBase?
    internal class AppenderReference : CheckedReferenceBase<AppenderRefImpl>
    {
        public AppenderReference(AppenderRefImpl owner)
            : base(owner)
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
            return myOwner.Ref;
        }

        public override ISymbolTable GetReferenceSymbolTable(bool useReferenceName)
        {
            ICollection<IAppender> appenders = myOwner.Parent.Parent.GetAppenders();

            IList<IAppender> candicates =
                CollectionUtil.FindAll(appenders, delegate(IAppender obj) { return (obj.Name == myOwner.Ref); });
            if (candicates.Count == 0)
                return EmptySymbolTable.INSTANCE;


            WritableSymbolTableBase table = useReferenceName
                                                ? ((WritableSymbolTableBase) new NameSymbolTable(this.GetName(), true))
                                                : ((WritableSymbolTableBase) new SymbolTable(true));
            foreach (IAppender appender in candicates)
            {
                table.AddSymbol((IDeclaredElement) appender, EmptySubstitution.INSTANCE, null, 0);
            }

            return table;
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
            return myOwner.RefValue.ToTreeNode().GetTreeTextRange();
        }

        public override DocumentRange GetDocumentRange()
        {
            return myOwner.RefValue.GetDocumentRange();
        }

        public override ReferenceType ReferenceType
        {
            get { return ReferenceType.TEXT; }
        }
    }
}