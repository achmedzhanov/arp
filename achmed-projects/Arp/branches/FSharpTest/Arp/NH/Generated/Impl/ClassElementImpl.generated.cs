
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
    public partial class ClassElementImpl : XmlTag,  IClassElement
    {

        #region Role ids

        private const int Meta_ROLE_ID = 20;

        private const int JcsCache_ROLE_ID = 21;

        private const int Cache_ROLE_ID = 22;

        private const int Id_ROLE_ID = 23;

        private const int CompositeId_ROLE_ID = 24;

        private const int Discriminator_ROLE_ID = 25;

        private const int Version_ROLE_ID = 26;

        private const int Timestamp_ROLE_ID = 27;

        private const int Properties_ROLE_ID = 28;

        private const int ManyToOnes_ROLE_ID = 29;

        private const int OneToOnes_ROLE_ID = 30;

        private const int Components_ROLE_ID = 31;

        private const int DynamicComponents_ROLE_ID = 32;

        private const int Anys_ROLE_ID = 33;

        private const int Maps_ROLE_ID = 34;

        private const int Sets_ROLE_ID = 35;

        private const int Lists_ROLE_ID = 36;

        private const int Bags_ROLE_ID = 37;

        private const int Idbags_ROLE_ID = 38;

        private const int Arrays_ROLE_ID = 39;

        private const int PrimitiveArrays_ROLE_ID = 40;

        private const int Subclasss_ROLE_ID = 41;

        private const int JoinedSubclasss_ROLE_ID = 42;

        private const int Filter_ROLE_ID = 43;

        private const int Loader_ROLE_ID = 44;

        private const int SqlInsert_ROLE_ID = 45;

        private const int SqlUpdate_ROLE_ID = 46;

        private const int SqlDelete_ROLE_ID = 47;

        #endregion

        #region Constructor 

        public ClassElementImpl() : base(ElementType.ClassElementImpl_ELEMENT) {}

        #endregion

        #region Attributes


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

        public IXmlAttribute DiscriminatorValueAttribute 
        {
            get
            {
                return base.GetAttribute("discriminator-value");
            }
        }

        public IXmlAttribute MutableAttribute 
        {
            get
            {
                return base.GetAttribute("mutable");
            }
        }

        public IXmlAttribute PolymorphismAttribute 
        {
            get
            {
                return base.GetAttribute("polymorphism");
            }
        }

        public IXmlAttribute PersisterAttribute 
        {
            get
            {
                return base.GetAttribute("persister");
            }
        }

        public IXmlAttribute WhereAttribute 
        {
            get
            {
                return base.GetAttribute("where");
            }
        }

        public IXmlAttribute BatchSizeAttribute 
        {
            get
            {
                return base.GetAttribute("batch-size");
            }
        }

        public IXmlAttribute OptimisticLockAttribute 
        {
            get
            {
                return base.GetAttribute("optimistic-lock");
            }
        }

        public IXmlAttribute CheckAttribute 
        {
            get
            {
                return base.GetAttribute("check");
            }
        }

        public IXmlAttribute NameAttribute 
        {
            get
            {
                return base.GetAttribute("name");
            }
        }

        public IXmlAttribute ProxyAttribute 
        {
            get
            {
                return base.GetAttribute("proxy");
            }
        }

        public IXmlAttribute LazyAttribute 
        {
            get
            {
                return base.GetAttribute("lazy");
            }
        }

        public IXmlAttribute DynamicUpdateAttribute 
        {
            get
            {
                return base.GetAttribute("dynamic-update");
            }
        }

        public IXmlAttribute DynamicInsertAttribute 
        {
            get
            {
                return base.GetAttribute("dynamic-insert");
            }
        }

        public IXmlAttribute SelectBeforeUpdateAttribute 
        {
            get
            {
                return base.GetAttribute("select-before-update");
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

        public IIdElement Id
        {
            get
            {
                return (IIdElement)FindChildByRole(Id_ROLE_ID);
            }
        }

        public ICompositeIdElement CompositeId
        {
            get
            {
                return (ICompositeIdElement)FindChildByRole(CompositeId_ROLE_ID);
            }
        }

        public IDiscriminatorElement Discriminator
        {
            get
            {
                return (IDiscriminatorElement)FindChildByRole(Discriminator_ROLE_ID);
            }
        }

        public IVersionElement Version
        {
            get
            {
                return (IVersionElement)FindChildByRole(Version_ROLE_ID);
            }
        }

        public ITimestampElement Timestamp
        {
            get
            {
                return (ITimestampElement)FindChildByRole(Timestamp_ROLE_ID);
            }
        }

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

        public ICollection<IIdbagElement> Idbags
        { 
            get
            { 
                return base.FindListOfChildrenByRole<IIdbagElement>(Idbags_ROLE_ID);
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

        public ICollection<ISubclassElement> Subclasss
        { 
            get
            { 
                return base.FindListOfChildrenByRole<ISubclassElement>(Subclasss_ROLE_ID);
            } 
        }

        public ICollection<IJoinedSubclassElement> JoinedSubclasss
        { 
            get
            { 
                return base.FindListOfChildrenByRole<IJoinedSubclassElement>(JoinedSubclasss_ROLE_ID);
            } 
        }

        public IFilterElement Filter
        {
            get
            {
                return (IFilterElement)FindChildByRole(Filter_ROLE_ID);
            }
        }

        public ILoaderElement Loader
        {
            get
            {
                return (ILoaderElement)FindChildByRole(Loader_ROLE_ID);
            }
        }

        public ISqlInsertElement SqlInsert
        {
            get
            {
                return (ISqlInsertElement)FindChildByRole(SqlInsert_ROLE_ID);
            }
        }

        public ISqlUpdateElement SqlUpdate
        {
            get
            {
                return (ISqlUpdateElement)FindChildByRole(SqlUpdate_ROLE_ID);
            }
        }

        public ISqlDeleteElement SqlDelete
        {
            get
            {
                return (ISqlDeleteElement)FindChildByRole(SqlDelete_ROLE_ID);
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
                
                if (child is IIdElement && ((XmlTag)child).TagName == "id")
                    return Id_ROLE_ID;
                else 
                
                if (child is ICompositeIdElement && ((XmlTag)child).TagName == "composite-id")
                    return CompositeId_ROLE_ID;
                else 
                
                if (child is IDiscriminatorElement && ((XmlTag)child).TagName == "discriminator")
                    return Discriminator_ROLE_ID;
                else 
                
                if (child is IVersionElement && ((XmlTag)child).TagName == "version")
                    return Version_ROLE_ID;
                else 
                
                if (child is ITimestampElement && ((XmlTag)child).TagName == "timestamp")
                    return Timestamp_ROLE_ID;
                else 
                
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
                
                if (child is IIdbagElement && ((XmlTag)child).TagName == "idbag")
                    return Idbags_ROLE_ID;
                else 
                
                if (child is IArrayElement && ((XmlTag)child).TagName == "array")
                    return Arrays_ROLE_ID;
                else 
                
                if (child is IPrimitiveArrayElement && ((XmlTag)child).TagName == "primitive-array")
                    return PrimitiveArrays_ROLE_ID;
                else 
                
                if (child is ISubclassElement && ((XmlTag)child).TagName == "subclass")
                    return Subclasss_ROLE_ID;
                else 
                
                if (child is IJoinedSubclassElement && ((XmlTag)child).TagName == "joined-subclass")
                    return JoinedSubclasss_ROLE_ID;
                else 
                
                if (child is IFilterElement && ((XmlTag)child).TagName == "filter")
                    return Filter_ROLE_ID;
                else 
                
                if (child is ILoaderElement && ((XmlTag)child).TagName == "loader")
                    return Loader_ROLE_ID;
                else 
                
                if (child is ISqlInsertElement && ((XmlTag)child).TagName == "sql-insert")
                    return SqlInsert_ROLE_ID;
                else 
                
                if (child is ISqlUpdateElement && ((XmlTag)child).TagName == "sql-update")
                    return SqlUpdate_ROLE_ID;
                else 
                
                if (child is ISqlDeleteElement && ((XmlTag)child).TagName == "sql-delete")
                    return SqlDelete_ROLE_ID;
                else 
                
        return base.GetChildRole(child);
        }
    }
}
