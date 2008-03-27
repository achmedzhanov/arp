using System;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;

namespace Arp.log4net.Psi.Tree.Impl
{
    public class ReferenceNameAttributeValue : BaseReferenceAttributeValue
    {
        public ReferenceNameAttributeValue()
            : base(L4NElementType.REFERENCE_NAME_ATTRIBUTE_VALUE)
        {
        }

        public ReferenceName GetReferenceName()
        {
            return (ReferenceName)FindChildByRole(0x27);
        }

        public override short GetChildRole(TreeElement child)
        {
            if (child is ReferenceName)
                return 0x27;

            return base.GetChildRole(child);
        }
    }
}