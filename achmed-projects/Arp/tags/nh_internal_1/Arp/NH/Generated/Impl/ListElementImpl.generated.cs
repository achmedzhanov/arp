
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
    public partial class ListElementImpl : XmlTag,  IListElement
    {

        #region Role ids

        private const int Meta_ROLE_ID = 20;

        private const int JcsCache_ROLE_ID = 21;

        private const int Cache_ROLE_ID = 22;

        private const int Key_ROLE_ID = 23;

        private const int Index_ROLE_ID = 24;

        private const int Element_ROLE_ID = 25;

        private const int OneToMany_ROLE_ID = 26;

        private const int ManyToMany_ROLE_ID = 27;

        private const int CompositeElement_ROLE_ID = 28;

        private const int ManyToAny_ROLE_ID = 29;

        private const int Filter_ROLE_ID = 30;

        #endregion

        #region Constructor 

        public ListElementImpl() : base(ElementType.ListElementImpl_ELEMENT) {}

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

        public IXmlAttribute TableAttribute 
        {
            get
            {
                return base.GetAttribute("table");
            }
        }

        public IXmlAttribute SchemaAttribute 
        {
            get
            {
                return base.GetAttribute("schema");
            }
        }

        public IXmlAttribute LazyAttribute 
        {
            get
            {
                return base.GetAttribute("lazy");
            }
        }

        public IXmlAttribute OuterJoinAttribute 
        {
            get
            {
                return base.GetAttribute("outer-join");
            }
        }

        public IXmlAttribute FetchAttribute 
        {
            get
            {
                return base.GetAttribute("fetch");
            }
        }

        public IXmlAttribute CascadeAttribute 
        {
            get
            {
                return base.GetAttribute("cascade");
            }
        }

        public IXmlAttribute WhereAttribute 
        {
            get
            {
                return base.GetAttribute("where");
            }
        }

        public IXmlAttribute InverseAttribute 
        {
            get
            {
                return base.GetAttribute("inverse");
            }
        }

        public IXmlAttribute PersisterAttribute 
        {
            get
            {
                return base.GetAttribute("persister");
            }
        }

        public IXmlAttribute BatchSizeAttribute 
        {
            get
            {
                return base.GetAttribute("batch-size");
            }
        }

        public IXmlAttribute CheckAttribute 
        {
            get
            {
                return base.GetAttribute("check");
            }
        }

        public IXmlAttribute CollectionTypeAttribute 
        {
            get
            {
                return base.GetAttribute("collection-type");
            }
        }

        public IXmlAttribute OptimisticLockAttribute 
        {
            get
            {
                return base.GetAttribute("optimistic-lock");
            }
        }

        public IXmlAttribute GenericAttribute 
        {
            get
            {
                return base.GetAttribute("generic");
            }
        }

        #endregion

        #region Elements


        public IMetaElement Meta
        {
            get
            {
                return (IMetaElement)FindChildByRole(Meta_ROLE_ID);
            }
        }

        public IJcsCacheElement JcsCache
        {
            get
            {
                return (IJcsCacheElement)FindChildByRole(JcsCache_ROLE_ID);
            }
        }

        public ICacheElement Cache
        {
            get
            {
                return (ICacheElement)FindChildByRole(Cache_ROLE_ID);
            }
        }

        public IKeyElement Key
        {
            get
            {
                return (IKeyElement)FindChildByRole(Key_ROLE_ID);
            }
        }

        public IIndexElement Index
        {
            get
            {
                return (IIndexElement)FindChildByRole(Index_ROLE_ID);
            }
        }

        public IElementElement Element
        {
            get
            {
                return (IElementElement)FindChildByRole(Element_ROLE_ID);
            }
        }

        public IOneToManyElement OneToMany
        {
            get
            {
                return (IOneToManyElement)FindChildByRole(OneToMany_ROLE_ID);
            }
        }

        public IManyToManyElement ManyToMany
        {
            get
            {
                return (IManyToManyElement)FindChildByRole(ManyToMany_ROLE_ID);
            }
        }

        public ICompositeElementElement CompositeElement
        {
            get
            {
                return (ICompositeElementElement)FindChildByRole(CompositeElement_ROLE_ID);
            }
        }

        public IManyToAnyElement ManyToAny
        {
            get
            {
                return (IManyToAnyElement)FindChildByRole(ManyToAny_ROLE_ID);
            }
        }

        public IFilterElement Filter
        {
            get
            {
                return (IFilterElement)FindChildByRole(Filter_ROLE_ID);
            }
        }


        #endregion

 

        public override short GetChildRole(TreeElement child)
        {

                if (child is IMetaElement && ((XmlTag)child).TagName == "meta")
                    return Meta_ROLE_ID;
                else 
                
                if (child is IJcsCacheElement && ((XmlTag)child).TagName == "jcs-cache")
                    return JcsCache_ROLE_ID;
                else 
                
                if (child is ICacheElement && ((XmlTag)child).TagName == "cache")
                    return Cache_ROLE_ID;
                else 
                
                if (child is IKeyElement && ((XmlTag)child).TagName == "key")
                    return Key_ROLE_ID;
                else 
                
                if (child is IIndexElement && ((XmlTag)child).TagName == "index")
                    return Index_ROLE_ID;
                else 
                
                if (child is IElementElement && ((XmlTag)child).TagName == "element")
                    return Element_ROLE_ID;
                else 
                
                if (child is IOneToManyElement && ((XmlTag)child).TagName == "one-to-many")
                    return OneToMany_ROLE_ID;
                else 
                
                if (child is IManyToManyElement && ((XmlTag)child).TagName == "many-to-many")
                    return ManyToMany_ROLE_ID;
                else 
                
                if (child is ICompositeElementElement && ((XmlTag)child).TagName == "composite-element")
                    return CompositeElement_ROLE_ID;
                else 
                
                if (child is IManyToAnyElement && ((XmlTag)child).TagName == "many-to-any")
                    return ManyToAny_ROLE_ID;
                else 
                
                if (child is IFilterElement && ((XmlTag)child).TagName == "filter")
                    return Filter_ROLE_ID;
                else 
                
        return base.GetChildRole(child);
        }
    }
}
