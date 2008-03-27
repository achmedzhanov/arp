using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve;
using JetBrains.ReSharper.Psi.Tree;

namespace Arp.Generator.Prototypes.NH.Psi.Impl
{
    public partial class ClassMappingElementImpl : IScope
    {
        #region IScope Members

        public IScope GetParentScope()
        {
            throw new System.NotImplementedException();
        }

        public void AddGlobalDeclarations(IWritableSymbolTable table, int level)
        {
            throw new System.NotImplementedException();
        }

        public void AddDeclarationsBeforeElement(IWritableSymbolTable table, IElement element, int level)
        {
            throw new System.NotImplementedException();
        }

        public void AddDeclarationsAfterElement(IWritableSymbolTable table, IElement element, int level)
        {
            throw new System.NotImplementedException();
        }

        public int LevelDelta
        {
            get { throw new System.NotImplementedException(); }
        }

        #endregion
    }
}