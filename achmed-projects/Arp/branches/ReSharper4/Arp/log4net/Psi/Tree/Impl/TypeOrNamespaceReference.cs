using System.Collections.Generic;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace Arp.log4net.Psi.Tree.Impl
{
    public class TypeOrNamespaceReference : BaseDeclaredElementReference<IElement>, ICompleteableReference
    {
        public TypeOrNamespaceReference(IElement owner) : base(owner)
        {
        }

        #region ICompleteableReference Members

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

        #endregion

        protected override ICollection<IDeclaredElement> GetDeclaredElements()
        {
            throw new System.NotImplementedException();
        }


        public override string GetName()
        {
            throw new System.NotImplementedException();
        }

        public override IReference BindTo(IDeclaredElement element)
        {
            // TODO implement for refactoring
            throw new System.NotImplementedException();
        }

        public override IReference BindTo(IDeclaredElement element, ISubstitution substitution)
        {
            // TODO implement for refactoring
            throw new System.NotImplementedException();
        }

        public override TextRange GetTreeTextRange()
        {
            return GetDocumentRange().TextRange;
        }

        public override DocumentRange GetDocumentRange()
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        #endregion
    }
}