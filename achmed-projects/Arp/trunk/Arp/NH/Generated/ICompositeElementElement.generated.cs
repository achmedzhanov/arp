
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
    public partial interface ICompositeElementElement : IElement 
    {
        #region Attributes


        IXmlAttribute ClassAttribute {get;}

        #endregion

        #region Elements


        IParentElement Parent {get;}
        ICollection<IPropertyElement> Properties {get;}
        ICollection<IManyToOneElement> ManyToOnes {get;}
        ICollection<INestedCompositeElementElement> NestedCompositeElements {get;}
        #endregion

 
    }
}
