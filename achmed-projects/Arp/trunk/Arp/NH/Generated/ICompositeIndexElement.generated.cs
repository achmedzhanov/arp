
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
    public partial interface ICompositeIndexElement : IElement 
    {
        #region Attributes


        IXmlAttribute ClassAttribute {get;}

        #endregion

        #region Elements


        ICollection<IKeyPropertyElement> KeyPropertys {get;}
        ICollection<IKeyManyToOneElement> KeyManyToOnes {get;}
        #endregion

 
    }
}
