
/*
This file was generated by Arp.Generator
*/
#region Imports
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.ReSharper.Psi.Tree;
using System.Collections.Generic;
#endregion

namespace Arp.NH.Psi.Tree
{
    public partial interface IComponentElement : IElement 
    {
        #region Attributes


        IXmlAttribute NameAttribute {get;}

        IXmlAttribute AccessAttribute {get;}

        IXmlAttribute ClassAttribute {get;}

        IXmlAttribute UpdateAttribute {get;}

        IXmlAttribute InsertAttribute {get;}

        #endregion

        #region Elements


        IParentElement Parent {get;}
        ICollection<IPropertyElement> Properties {get;}
        ICollection<IManyToOneElement> ManyToOnes {get;}
        ICollection<IOneToOneElement> OneToOnes {get;}
        ICollection<IComponentElement> Components {get;}
        ICollection<IDynamicComponentElement> DynamicComponents {get;}
        ICollection<IAnyElement> Anys {get;}
        ICollection<IMapElement> Maps {get;}
        ICollection<ISetElement> Sets {get;}
        ICollection<IListElement> Lists {get;}
        ICollection<IBagElement> Bags {get;}
        ICollection<IArrayElement> Arrays {get;}
        ICollection<IPrimitiveArrayElement> PrimitiveArrays {get;}
        #endregion

 
    }
}
