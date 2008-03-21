using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Resolve;

namespace Arp.log4net.Psi.Tree.Impl
{
    public class ReferenceType : CompositeElement
    {
        public ReferenceType()
            : base(L4NElementType.REFERENCE_TYPE)
        {

        }
    }
}