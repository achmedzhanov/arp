using Arp.log4net.Psi.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Xml.Impl.Tree;

namespace Arp.NH.Psi.Tree.Impl
{
    public class ReferenceTable : XmlCompositeElement, IQualifier
    {
        public ReferenceTable()
            : base(L4NElementType.REFERENCE_MODULE)
        {
            
        }

        #region IQualifier Members

        public ISymbolTable GetSymbolTable(params string[] referenceNames)
        {
            // TODO get columns set here
            
            IReference[] references = this.GetReferences();
            if (references.Length > 0)
                return ResolveUtil.GetSymbolTableByReference((IQualifiableReference)references[0], true);
            return EmptySymbolTable.INSTANCE;
        }

        public QualifierKind GetKind()
        {
            return QualifierKind.UNKNOWN;
        }

        ///<summary>
        ///
        ///            True means failed to resolve
        ///            
        ///</summary>
        ///
        public bool Resolved
        {
            get { throw new System.NotImplementedException(); }
        }

        #endregion

        protected override IReferenceImpl[] CreateReferences()
        {
            return new IReferenceImpl[] { new TableReference(this) };
        }

        public object GetName()
        {
            return GetText();
        }
    }
}