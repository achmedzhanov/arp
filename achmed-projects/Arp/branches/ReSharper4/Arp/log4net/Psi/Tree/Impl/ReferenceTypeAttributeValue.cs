using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;

namespace Arp.log4net.Psi.Tree.Impl
{
    public class ReferenceTypeAttributeValue : BaseReferenceAttributeValue
    {
        public ReferenceTypeAttributeValue()
            : base(L4NElementType.REFERENCE_TYPE)
        {
        }

        public ReferenceType GetReferenceType()
        {
            return (ReferenceType)FindChildByRole(0x27);
        }

        public override short GetChildRole(TreeElement child)
        {
            if (child is ReferenceType)
                return 0x27;

            return base.GetChildRole(child);
        }

    }
}