using Arp.Generator.Prototypes.NH.Psi.Impl;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;

namespace Arp.Generator.Prototypes.NH.Psi.Impl
{
    public partial class ClassMappingElementImpl_ELEMENT_TYPE : NHConpositeElementType
    {
        public ClassMappingElementImpl_ELEMENT_TYPE(string s)
            : base(s)
        {
        }

        public override CompositeElement Create()
        {
            return new ClassMappingElementImpl();
        }
    }
}