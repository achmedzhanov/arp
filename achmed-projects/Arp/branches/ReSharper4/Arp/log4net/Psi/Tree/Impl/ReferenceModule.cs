using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Impl.Tree;

namespace Arp.log4net.Psi.Tree.Impl
{
    public class ReferenceModule : XmlCompositeElement, IQualifier
    {
        public ReferenceModule()
            : base(L4NElementType.REFERENCE_MODULE)
        {
            
        }

        #region IQualifier Members

        public ISymbolTable GetSymbolTable(params string[] referenceNames)
        {
            throw new System.NotImplementedException();
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
            return new IReferenceImpl[] { new ModuleReference(this) };
        }

        public object GetName()
        {
            return GetText();
        }
    }
}