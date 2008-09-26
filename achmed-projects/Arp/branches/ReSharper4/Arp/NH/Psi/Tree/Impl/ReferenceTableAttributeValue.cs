using Arp.log4net.Psi.Tree.Impl;
using Arp.NH.Psi.Tree.Impl;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;

namespace Arp.NH.Psi.Tree
{
    public class ReferenceTableAttributeValue : BaseReferenceAttributeValue
    {
        public ReferenceTableAttributeValue()
            : base(ElementType.RefrenceTable_ELEMENT_TYPE)
        {
        }

        public ReferenceTable GetReferenceTable()
        {
            return (ReferenceTable)FindChildByRole(0x27);
        }

        public override short GetChildRole(TreeElement child)
        {
            if (child is ReferenceTable)
                return 0x27;

            return base.GetChildRole(child);
        }

    }
}