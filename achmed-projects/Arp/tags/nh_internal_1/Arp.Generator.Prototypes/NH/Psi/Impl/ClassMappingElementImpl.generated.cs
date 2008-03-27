using System.Collections.Generic;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Xml.Impl.Tree;

namespace Arp.Generator.Prototypes.NH.Psi.Impl
{
    public partial class ClassMappingElementImpl : BaseTag, IClassMappingElement
    {
        private const short ID_MAPPING_ELEMENT_ROLE = 0x1;
        private const short PROPTIY_ELEMENT_ROLE = 0x2;

        public ClassMappingElementImpl()
            : base(ElementType.ClassMappingElementImpl_ELEMENT)
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

        public ICollection<IPropertyMappingElement> Propties
        {
            get
            {

                return base.FindListOfChildrenByRole<IPropertyMappingElement>(PROPTIY_ELEMENT_ROLE);

                /*
 
public override IList<IXamlObjectElement> ResourceDeclarations
{
    get
    {
        IList<XamlPropertyElement> propertyElements = base.FindListOfChildrenByRole<XamlPropertyElement>(10, delegate (XamlPropertyElement obj) {
            return obj.CanContainResourceDeclarations();
        });
        List<IXamlObjectElement> resources = new List<IXamlObjectElement>();
        foreach (XamlPropertyElement element in propertyElements)
        {
            resources.AddRange(element.ResourceDeclarations);
        }
        return resources.ToArray();
    }
}
                  
 
 */

            }
        }

        public IBooleanAttribute Lazy
        {
            get { return (IBooleanAttribute)base.GetAttribute("lazy"); }
        }

        public IReferenceNameAttribute Name
        {
            get { return (IReferenceNameAttribute)base.GetAttribute("name"); }
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