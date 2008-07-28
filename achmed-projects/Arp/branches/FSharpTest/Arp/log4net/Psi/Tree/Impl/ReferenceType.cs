using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Resolve;

namespace Arp.log4net.Psi.Tree.Impl
{
    public class ReferenceType : CompositeElement
    {
        private const int REFERENCE_NAME_ROLE = 0x27;

        public ReferenceType()
            : base(L4NElementType.REFERENCE_TYPE)
        {

        }

        public ReferenceName ReferenceName
        {
            get
            {
                return (ReferenceName)FindChildByRole(REFERENCE_NAME_ROLE);
            }
        }

        public override short GetChildRole(TreeElement child)
        {
            if (child.type == L4NElementType.REFERENCE_NAME)
            {
                return REFERENCE_NAME_ROLE;
            }
            return 0;
        }
    }
}