using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;

namespace Arp.NH.Psi.Tree
{
    public class RefrenceTable_ELEMENT_TYPE : NHConpositeElementType
    {
        public RefrenceTable_ELEMENT_TYPE(string s) : base(s)
        {

        }

        public override CompositeElement Create()
        {
            return new ReferenceTableAttributeValue();
        }
    }
}