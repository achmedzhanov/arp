using System.Collections.Generic;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Xml.Impl.Tree;

namespace Arp.Generator.Prototypes.NH.Psi.Impl
{
    public partial class ClassMappingElementImpl : BaseTag, IClassMappingElement
    {
        private const short ID_MAPPING_ELEMENT_ROLE = 0x1;
        private const short PROPTIY_ELEMENT_ROLE = 0x2;
        
        public ClassMappingElementImpl(CompositeNodeType _type) : base(_type)
        {
        }

        #region IClassMappingElement Members

        public IIdMappingElement Id
        {
            get
            {
                return (IIdMappingElement)FindChildByRole(ID_MAPPING_ELEMENT_ROLE);
            }
        }

        public IList<IPropertyMappingElement> Propties
        {
            get
            {
                return new List<IPropertyMappingElement>(
                    (IPropertyMappingElement[])FindChildrenByRole(PROPTIY_ELEMENT_ROLE, new IPropertyMappingElement[] { })
                    );
            }
        }

        public IBooleanAttribute Lazy
        {
            get { throw new System.NotImplementedException(); }
        }

        public IReferenceNameAttribute Name
        {
            get { throw new System.NotImplementedException(); }
        }

        #endregion


        public override short GetChildRole(TreeElement child)
        {

            if (child is IIdMappingElement && ((XmlTag)child).TagName == "id")
                return ID_MAPPING_ELEMENT_ROLE;
            else if (child is IPropertyMappingElement && ((XmlTag)child).TagName == "property")
                return PROPTIY_ELEMENT_ROLE;
            else
                return base.GetChildRole(child);
        }
    }
}