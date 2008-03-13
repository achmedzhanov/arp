
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
    public partial interface IListElement : IElement 
    {
        #region Attributes


        IXmlAttribute NameAttribute {get;}

        IXmlAttribute AccessAttribute {get;}

        IXmlAttribute TableAttribute {get;}

        IXmlAttribute SchemaAttribute {get;}

        IXmlAttribute LazyAttribute {get;}

        IXmlAttribute OuterJoinAttribute {get;}

        IXmlAttribute FetchAttribute {get;}

        IXmlAttribute CascadeAttribute {get;}

        IXmlAttribute WhereAttribute {get;}

        IXmlAttribute InverseAttribute {get;}

        IXmlAttribute PersisterAttribute {get;}

        IXmlAttribute BatchSizeAttribute {get;}

        IXmlAttribute CheckAttribute {get;}

        IXmlAttribute CollectionTypeAttribute {get;}

        IXmlAttribute OptimisticLockAttribute {get;}

        IXmlAttribute GenericAttribute {get;}

        #endregion

        #region Elements


        IMetaElement Meta {get;}
        IJcsCacheElement JcsCache {get;}
        ICacheElement Cache {get;}
        IKeyElement Key {get;}
        IIndexElement Index {get;}
        IElementElement Element {get;}
        IOneToManyElement OneToMany {get;}
        IManyToManyElement ManyToMany {get;}
        ICompositeElementElement CompositeElement {get;}
        IManyToAnyElement ManyToAny {get;}
        IFilterElement Filter {get;}
        #endregion

 
    }
}
