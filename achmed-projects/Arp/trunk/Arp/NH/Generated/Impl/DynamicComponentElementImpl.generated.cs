
/*
This file was generated by Arp.Generator
*/
#region Imports
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using System.Collections.Generic;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
#endregion

namespace Arp.NH.Psi.Tree.Impl
{
    public partial class DynamicComponentElementImpl : XmlTag,  IDynamicComponentElement
    {

        #region Role ids

        private const int Properties_ROLE_ID = 20;

        private const int ManyToOnes_ROLE_ID = 21;

        private const int OneToOnes_ROLE_ID = 22;

        private const int Components_ROLE_ID = 23;

        private const int DynamicComponents_ROLE_ID = 24;

        private const int Anys_ROLE_ID = 25;

        private const int Maps_ROLE_ID = 26;

        private const int Sets_ROLE_ID = 27;

        private const int Lists_ROLE_ID = 28;

        private const int Bags_ROLE_ID = 29;

        private const int Arrays_ROLE_ID = 30;

        private const int PrimitiveArrays_ROLE_ID = 31;

        #endregion

        #region Constructor 

        public DynamicComponentElementImpl() : base(ElementType.DynamicComponentElementImpl_ELEMENT) {}

        #endregion

        #region Attributes


        public IXmlAttribute NameAttribute 
        {
            get
            {
                return base.GetAttribute("name");
            }
        }

        public IXmlAttribute AccessAttribute 
        {
            get
            {
                return base.GetAttribute("access");
            }
        }

        public IXmlAttribute UpdateAttribute 
        {
            get
            {
                return base.GetAttribute("update");
            }
        }

        public IXmlAttribute InsertAttribute 
        {
            get
            {
                return base.GetAttribute("insert");
            }
        }

        #endregion

        #region Elements


        public ICollection<IPropertyElement> Properties
        { 
            get
            { 
                return base.FindListOfChildrenByRole<IPropertyElement>(Properties_ROLE_ID);
            } 
        }

        public ICollection<IManyToOneElement> ManyToOnes
        { 
            get
            { 
                return base.FindListOfChildrenByRole<IManyToOneElement>(ManyToOnes_ROLE_ID);
            } 
        }

        public ICollection<IOneToOneElement> OneToOnes
        { 
            get
            { 
                return base.FindListOfChildrenByRole<IOneToOneElement>(OneToOnes_ROLE_ID);
            } 
        }

        public ICollection<IComponentElement> Components
        { 
            get
            { 
                return base.FindListOfChildrenByRole<IComponentElement>(Components_ROLE_ID);
            } 
        }

        public ICollection<IDynamicComponentElement> DynamicComponents
        { 
            get
            { 
                return base.FindListOfChildrenByRole<IDynamicComponentElement>(DynamicComponents_ROLE_ID);
            } 
        }

        public ICollection<IAnyElement> Anys
        { 
            get
            { 
                return base.FindListOfChildrenByRole<IAnyElement>(Anys_ROLE_ID);
            } 
        }

        public ICollection<IMapElement> Maps
        { 
            get
            { 
                return base.FindListOfChildrenByRole<IMapElement>(Maps_ROLE_ID);
            } 
        }

        public ICollection<ISetElement> Sets
        { 
            get
            { 
                return base.FindListOfChildrenByRole<ISetElement>(Sets_ROLE_ID);
            } 
        }

        public ICollection<IListElement> Lists
        { 
            get
            { 
                return base.FindListOfChildrenByRole<IListElement>(Lists_ROLE_ID);
            } 
        }

        public ICollection<IBagElement> Bags
        { 
            get
            { 
                return base.FindListOfChildrenByRole<IBagElement>(Bags_ROLE_ID);
            } 
        }

        public ICollection<IArrayElement> Arrays
        { 
            get
            { 
                return base.FindListOfChildrenByRole<IArrayElement>(Arrays_ROLE_ID);
            } 
        }

        public ICollection<IPrimitiveArrayElement> PrimitiveArrays
        { 
            get
            { 
                return base.FindListOfChildrenByRole<IPrimitiveArrayElement>(PrimitiveArrays_ROLE_ID);
            } 
        }


        #endregion

 

        public override short GetChildRole(TreeElement child)
        {

                if (child is IPropertyElement && ((XmlTag)child).TagName == "property")
                    return Properties_ROLE_ID;
                else 
                
                if (child is IManyToOneElement && ((XmlTag)child).TagName == "many-to-one")
                    return ManyToOnes_ROLE_ID;
                else 
                
                if (child is IOneToOneElement && ((XmlTag)child).TagName == "one-to-one")
                    return OneToOnes_ROLE_ID;
                else 
                
                if (child is IComponentElement && ((XmlTag)child).TagName == "component")
                    return Components_ROLE_ID;
                else 
                
                if (child is IDynamicComponentElement && ((XmlTag)child).TagName == "dynamic-component")
                    return DynamicComponents_ROLE_ID;
                else 
                
                if (child is IAnyElement && ((XmlTag)child).TagName == "any")
                    return Anys_ROLE_ID;
                else 
                
                if (child is IMapElement && ((XmlTag)child).TagName == "map")
                    return Maps_ROLE_ID;
                else 
                
                if (child is ISetElement && ((XmlTag)child).TagName == "set")
                    return Sets_ROLE_ID;
                else 
                
                if (child is IListElement && ((XmlTag)child).TagName == "list")
                    return Lists_ROLE_ID;
                else 
                
                if (child is IBagElement && ((XmlTag)child).TagName == "bag")
                    return Bags_ROLE_ID;
                else 
                
                if (child is IArrayElement && ((XmlTag)child).TagName == "array")
                    return Arrays_ROLE_ID;
                else 
                
                if (child is IPrimitiveArrayElement && ((XmlTag)child).TagName == "primitive-array")
                    return PrimitiveArrays_ROLE_ID;
                else 
                
        return base.GetChildRole(child);
        }
    }
}
