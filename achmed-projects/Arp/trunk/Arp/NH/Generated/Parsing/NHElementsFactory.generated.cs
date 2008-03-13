
using Arp.NH.Psi.Tree.Impl;
using JetBrains.ReSharper.Psi.Xml.Tree;

namespace Arp.NH.Psi.Tree.Parsing
{
    public partial class NHElementsFactory
    {
        protected IXmlTag CreateTagGenerated(IXmlTagHeaderNode header, IXmlTagContainer parentTag)
        {

            string name = header.Name.GetText();

            if(parentTag is IFilterElement)
            {
                
                    return null;
            }
            else

            if(parentTag is ISqlInsertElement)
            {
                
                    return null;
            }
            else

            if(parentTag is ISqlUpdateElement)
            {
                
                    return null;
            }
            else

            if(parentTag is ISqlDeleteElement)
            {
                
                    return null;
            }
            else

            if(parentTag is ISqlDeleteAllElement)
            {
                
                    return null;
            }
            else

            if(parentTag is ILoaderElement)
            {
                
                    return null;
            }
            else

            if(parentTag is IResultsetElement)
            {
                
                    return null;
            }
            else

            if(parentTag is IHibernateMappingElement)
            {

                if (name == "meta")
                {
                    return new MetaElementImpl();
                }
                else

                if (name == "import")
                {
                    return new ImportElementImpl();
                }
                else


                if (name == "class")
                {
                    return new ClassElementImpl();
                }

                if (name == "subclass")
                {
                    return new SubclassElementImpl();
                }

                if (name == "joined-subclass")
                {
                    return new JoinedSubclassElementImpl();
                }

                else

                if (name == "resultset")
                {
                    return new ResultsetElementImpl();
                }
                else


                if (name == "query")
                {
                    return new QueryElementImpl();
                }

                if (name == "sql-query")
                {
                    return new SqlQueryElementImpl();
                }

                else

                if (name == "filter-def")
                {
                    return new FilterDefElementImpl();
                }
                else

                if (name == "database-object")
                {
                    return new DatabaseObjectElementImpl();
                }
                else
                
                    return null;
            }
            else

            if(parentTag is IFilterParamElement)
            {
                
                    return null;
            }
            else

            if(parentTag is IFilterDefElement)
            {

                if (name == "filter-param")
                {
                    return new FilterParamElementImpl();
                }
                else
                
                    return null;
            }
            else

            if(parentTag is ICreateElement)
            {
                
                    return null;
            }
            else

            if(parentTag is IDropElement)
            {
                
                    return null;
            }
            else

            if(parentTag is IDialectScopeElement)
            {
                
                    return null;
            }
            else

            if(parentTag is IDatabaseObjectElement)
            {

                if (name == "create")
                {
                    return new CreateElementImpl();
                }
                else

                if (name == "drop")
                {
                    return new DropElementImpl();
                }
                else

                if (name == "dialect-scope")
                {
                    return new DialectScopeElementImpl();
                }
                else
                
                    return null;
            }
            else

            if(parentTag is IImportElement)
            {
                
                    return null;
            }
            else

            if(parentTag is IClassElement)
            {

                if (name == "meta")
                {
                    return new MetaElementImpl();
                }
                else

                if (name == "jcs-cache")
                {
                    return new JcsCacheElementImpl();
                }
                else

                if (name == "cache")
                {
                    return new CacheElementImpl();
                }
                else

                if (name == "id")
                {
                    return new IdElementImpl();
                }
                else

                if (name == "composite-id")
                {
                    return new CompositeIdElementImpl();
                }
                else

                if (name == "discriminator")
                {
                    return new DiscriminatorElementImpl();
                }
                else

                if (name == "version")
                {
                    return new VersionElementImpl();
                }
                else

                if (name == "timestamp")
                {
                    return new TimestampElementImpl();
                }
                else


                if (name == "property")
                {
                    return new PropertyElementImpl();
                }

                if (name == "many-to-one")
                {
                    return new ManyToOneElementImpl();
                }

                if (name == "one-to-one")
                {
                    return new OneToOneElementImpl();
                }

                if (name == "component")
                {
                    return new ComponentElementImpl();
                }

                if (name == "dynamic-component")
                {
                    return new DynamicComponentElementImpl();
                }

                if (name == "any")
                {
                    return new AnyElementImpl();
                }

                if (name == "map")
                {
                    return new MapElementImpl();
                }

                if (name == "set")
                {
                    return new SetElementImpl();
                }

                if (name == "list")
                {
                    return new ListElementImpl();
                }

                if (name == "bag")
                {
                    return new BagElementImpl();
                }

                if (name == "idbag")
                {
                    return new IdbagElementImpl();
                }

                if (name == "array")
                {
                    return new ArrayElementImpl();
                }

                if (name == "primitive-array")
                {
                    return new PrimitiveArrayElementImpl();
                }

                else


                if (name == "subclass")
                {
                    return new SubclassElementImpl();
                }

                else


                if (name == "joined-subclass")
                {
                    return new JoinedSubclassElementImpl();
                }

                else

                if (name == "filter")
                {
                    return new FilterElementImpl();
                }
                else

                if (name == "loader")
                {
                    return new LoaderElementImpl();
                }
                else

                if (name == "sql-insert")
                {
                    return new SqlInsertElementImpl();
                }
                else

                if (name == "sql-update")
                {
                    return new SqlUpdateElementImpl();
                }
                else

                if (name == "sql-delete")
                {
                    return new SqlDeleteElementImpl();
                }
                else
                
                    return null;
            }
            else

            if(parentTag is IIdElement)
            {

                if (name == "meta")
                {
                    return new MetaElementImpl();
                }
                else

                if (name == "column")
                {
                    return new ColumnElementImpl();
                }
                else

                if (name == "generator")
                {
                    return new GeneratorElementImpl();
                }
                else
                
                    return null;
            }
            else

            if(parentTag is ICompositeIdElement)
            {


                if (name == "key-property")
                {
                    return new KeyPropertyElementImpl();
                }

                if (name == "key-many-to-one")
                {
                    return new KeyManyToOneElementImpl();
                }

                else
                
                    return null;
            }
            else

            if(parentTag is IDiscriminatorElement)
            {

                if (name == "column")
                {
                    return new ColumnElementImpl();
                }
                else
                
                    return null;
            }
            else

            if(parentTag is IVersionElement)
            {
                
                    return null;
            }
            else

            if(parentTag is ITimestampElement)
            {
                
                    return null;
            }
            else

            if(parentTag is ISubclassElement)
            {

                if (name == "meta")
                {
                    return new MetaElementImpl();
                }
                else


                if (name == "property")
                {
                    return new PropertyElementImpl();
                }

                if (name == "many-to-one")
                {
                    return new ManyToOneElementImpl();
                }

                if (name == "one-to-one")
                {
                    return new OneToOneElementImpl();
                }

                if (name == "component")
                {
                    return new ComponentElementImpl();
                }

                if (name == "dynamic-component")
                {
                    return new DynamicComponentElementImpl();
                }

                if (name == "any")
                {
                    return new AnyElementImpl();
                }

                if (name == "map")
                {
                    return new MapElementImpl();
                }

                if (name == "set")
                {
                    return new SetElementImpl();
                }

                if (name == "list")
                {
                    return new ListElementImpl();
                }

                if (name == "bag")
                {
                    return new BagElementImpl();
                }

                if (name == "idbag")
                {
                    return new IdbagElementImpl();
                }

                if (name == "array")
                {
                    return new ArrayElementImpl();
                }

                if (name == "primitive-array")
                {
                    return new PrimitiveArrayElementImpl();
                }

                else

                if (name == "subclass")
                {
                    return new SubclassElementImpl();
                }
                else

                if (name == "loader")
                {
                    return new LoaderElementImpl();
                }
                else

                if (name == "sql-insert")
                {
                    return new SqlInsertElementImpl();
                }
                else

                if (name == "sql-update")
                {
                    return new SqlUpdateElementImpl();
                }
                else

                if (name == "sql-delete")
                {
                    return new SqlDeleteElementImpl();
                }
                else
                
                    return null;
            }
            else

            if(parentTag is IJoinedSubclassElement)
            {

                if (name == "meta")
                {
                    return new MetaElementImpl();
                }
                else

                if (name == "key")
                {
                    return new KeyElementImpl();
                }
                else


                if (name == "property")
                {
                    return new PropertyElementImpl();
                }

                if (name == "many-to-one")
                {
                    return new ManyToOneElementImpl();
                }

                if (name == "one-to-one")
                {
                    return new OneToOneElementImpl();
                }

                if (name == "component")
                {
                    return new ComponentElementImpl();
                }

                if (name == "dynamic-component")
                {
                    return new DynamicComponentElementImpl();
                }

                if (name == "any")
                {
                    return new AnyElementImpl();
                }

                if (name == "map")
                {
                    return new MapElementImpl();
                }

                if (name == "set")
                {
                    return new SetElementImpl();
                }

                if (name == "list")
                {
                    return new ListElementImpl();
                }

                if (name == "bag")
                {
                    return new BagElementImpl();
                }

                if (name == "idbag")
                {
                    return new IdbagElementImpl();
                }

                if (name == "array")
                {
                    return new ArrayElementImpl();
                }

                if (name == "primitive-array")
                {
                    return new PrimitiveArrayElementImpl();
                }

                else

                if (name == "joined-subclass")
                {
                    return new JoinedSubclassElementImpl();
                }
                else

                if (name == "loader")
                {
                    return new LoaderElementImpl();
                }
                else

                if (name == "sql-insert")
                {
                    return new SqlInsertElementImpl();
                }
                else

                if (name == "sql-update")
                {
                    return new SqlUpdateElementImpl();
                }
                else

                if (name == "sql-delete")
                {
                    return new SqlDeleteElementImpl();
                }
                else
                
                    return null;
            }
            else

            if(parentTag is IPropertyElement)
            {

                if (name == "meta")
                {
                    return new MetaElementImpl();
                }
                else

                if (name == "column")
                {
                    return new ColumnElementImpl();
                }
                else

                if (name == "type")
                {
                    return new TypeElementImpl();
                }
                else
                
                    return null;
            }
            else

            if(parentTag is IMetaValueElement)
            {
                
                    return null;
            }
            else

            if(parentTag is IAnyElement)
            {

                if (name == "meta")
                {
                    return new MetaElementImpl();
                }
                else

                if (name == "meta-value")
                {
                    return new MetaValueElementImpl();
                }
                else

                if (name == "column")
                {
                    return new ColumnElementImpl();
                }
                else
                
                    return null;
            }
            else

            if(parentTag is IArrayElement)
            {

                if (name == "meta")
                {
                    return new MetaElementImpl();
                }
                else

                if (name == "jcs-cache")
                {
                    return new JcsCacheElementImpl();
                }
                else

                if (name == "cache")
                {
                    return new CacheElementImpl();
                }
                else

                if (name == "key")
                {
                    return new KeyElementImpl();
                }
                else

                if (name == "index")
                {
                    return new IndexElementImpl();
                }
                else

                if (name == "element")
                {
                    return new ElementElementImpl();
                }
                else

                if (name == "one-to-many")
                {
                    return new OneToManyElementImpl();
                }
                else

                if (name == "many-to-many")
                {
                    return new ManyToManyElementImpl();
                }
                else

                if (name == "composite-element")
                {
                    return new CompositeElementElementImpl();
                }
                else

                if (name == "many-to-any")
                {
                    return new ManyToAnyElementImpl();
                }
                else
                
                    return null;
            }
            else

            if(parentTag is ICacheElement)
            {
                
                    return null;
            }
            else

            if(parentTag is ICollectionIdElement)
            {

                if (name == "meta")
                {
                    return new MetaElementImpl();
                }
                else

                if (name == "column")
                {
                    return new ColumnElementImpl();
                }
                else

                if (name == "generator")
                {
                    return new GeneratorElementImpl();
                }
                else
                
                    return null;
            }
            else

            if(parentTag is IColumnElement)
            {
                
                    return null;
            }
            else

            if(parentTag is IComponentElement)
            {

                if (name == "parent")
                {
                    return new ParentElementImpl();
                }
                else


                if (name == "property")
                {
                    return new PropertyElementImpl();
                }

                if (name == "many-to-one")
                {
                    return new ManyToOneElementImpl();
                }

                if (name == "one-to-one")
                {
                    return new OneToOneElementImpl();
                }

                if (name == "component")
                {
                    return new ComponentElementImpl();
                }

                if (name == "dynamic-component")
                {
                    return new DynamicComponentElementImpl();
                }

                if (name == "any")
                {
                    return new AnyElementImpl();
                }

                if (name == "map")
                {
                    return new MapElementImpl();
                }

                if (name == "set")
                {
                    return new SetElementImpl();
                }

                if (name == "list")
                {
                    return new ListElementImpl();
                }

                if (name == "bag")
                {
                    return new BagElementImpl();
                }

                if (name == "array")
                {
                    return new ArrayElementImpl();
                }

                if (name == "primitive-array")
                {
                    return new PrimitiveArrayElementImpl();
                }

                else
                
                    return null;
            }
            else

            if(parentTag is IDynamicComponentElement)
            {


                if (name == "property")
                {
                    return new PropertyElementImpl();
                }

                if (name == "many-to-one")
                {
                    return new ManyToOneElementImpl();
                }

                if (name == "one-to-one")
                {
                    return new OneToOneElementImpl();
                }

                if (name == "component")
                {
                    return new ComponentElementImpl();
                }

                if (name == "dynamic-component")
                {
                    return new DynamicComponentElementImpl();
                }

                if (name == "any")
                {
                    return new AnyElementImpl();
                }

                if (name == "map")
                {
                    return new MapElementImpl();
                }

                if (name == "set")
                {
                    return new SetElementImpl();
                }

                if (name == "list")
                {
                    return new ListElementImpl();
                }

                if (name == "bag")
                {
                    return new BagElementImpl();
                }

                if (name == "array")
                {
                    return new ArrayElementImpl();
                }

                if (name == "primitive-array")
                {
                    return new PrimitiveArrayElementImpl();
                }

                else
                
                    return null;
            }
            else

            if(parentTag is ICompositeElementElement)
            {

                if (name == "parent")
                {
                    return new ParentElementImpl();
                }
                else


                if (name == "property")
                {
                    return new PropertyElementImpl();
                }

                if (name == "many-to-one")
                {
                    return new ManyToOneElementImpl();
                }

                if (name == "nested-composite-element")
                {
                    return new NestedCompositeElementElementImpl();
                }

                else
                
                    return null;
            }
            else

            if(parentTag is ICompositeIndexElement)
            {


                if (name == "key-property")
                {
                    return new KeyPropertyElementImpl();
                }

                if (name == "key-many-to-one")
                {
                    return new KeyManyToOneElementImpl();
                }

                else
                
                    return null;
            }
            else

            if(parentTag is IElementElement)
            {

                if (name == "column")
                {
                    return new ColumnElementImpl();
                }
                else
                
                    return null;
            }
            else

            if(parentTag is IGeneratorElement)
            {

                if (name == "param")
                {
                    return new ParamElementImpl();
                }
                else
                
                    return null;
            }
            else

            if(parentTag is IIdbagElement)
            {

                if (name == "meta")
                {
                    return new MetaElementImpl();
                }
                else

                if (name == "jcs-cache")
                {
                    return new JcsCacheElementImpl();
                }
                else

                if (name == "cache")
                {
                    return new CacheElementImpl();
                }
                else

                if (name == "collection-id")
                {
                    return new CollectionIdElementImpl();
                }
                else

                if (name == "key")
                {
                    return new KeyElementImpl();
                }
                else

                if (name == "element")
                {
                    return new ElementElementImpl();
                }
                else

                if (name == "many-to-many")
                {
                    return new ManyToManyElementImpl();
                }
                else

                if (name == "composite-element")
                {
                    return new CompositeElementElementImpl();
                }
                else

                if (name == "many-to-any")
                {
                    return new ManyToAnyElementImpl();
                }
                else

                if (name == "filter")
                {
                    return new FilterElementImpl();
                }
                else

                if (name == "loader")
                {
                    return new LoaderElementImpl();
                }
                else

                if (name == "sql-insert")
                {
                    return new SqlInsertElementImpl();
                }
                else

                if (name == "sql-update")
                {
                    return new SqlUpdateElementImpl();
                }
                else

                if (name == "sql-delete")
                {
                    return new SqlDeleteElementImpl();
                }
                else

                if (name == "sql-delete-all")
                {
                    return new SqlDeleteAllElementImpl();
                }
                else
                
                    return null;
            }
            else

            if(parentTag is IIndexElement)
            {

                if (name == "column")
                {
                    return new ColumnElementImpl();
                }
                else
                
                    return null;
            }
            else

            if(parentTag is IIndexManyToAnyElement)
            {

                if (name == "column")
                {
                    return new ColumnElementImpl();
                }
                else
                
                    return null;
            }
            else

            if(parentTag is IIndexManyToManyElement)
            {

                if (name == "column")
                {
                    return new ColumnElementImpl();
                }
                else
                
                    return null;
            }
            else

            if(parentTag is IJcsCacheElement)
            {
                
                    return null;
            }
            else

            if(parentTag is IKeyElement)
            {

                if (name == "column")
                {
                    return new ColumnElementImpl();
                }
                else
                
                    return null;
            }
            else

            if(parentTag is IKeyManyToOneElement)
            {

                if (name == "column")
                {
                    return new ColumnElementImpl();
                }
                else
                
                    return null;
            }
            else

            if(parentTag is IKeyPropertyElement)
            {

                if (name == "column")
                {
                    return new ColumnElementImpl();
                }
                else
                
                    return null;
            }
            else

            if(parentTag is IManyToAnyElement)
            {

                if (name == "meta-value")
                {
                    return new MetaValueElementImpl();
                }
                else

                if (name == "column")
                {
                    return new ColumnElementImpl();
                }
                else
                
                    return null;
            }
            else

            if(parentTag is IManyToManyElement)
            {

                if (name == "meta")
                {
                    return new MetaElementImpl();
                }
                else

                if (name == "column")
                {
                    return new ColumnElementImpl();
                }
                else

                if (name == "filter")
                {
                    return new FilterElementImpl();
                }
                else
                
                    return null;
            }
            else

            if(parentTag is IManyToOneElement)
            {

                if (name == "meta")
                {
                    return new MetaElementImpl();
                }
                else

                if (name == "column")
                {
                    return new ColumnElementImpl();
                }
                else
                
                    return null;
            }
            else

            if(parentTag is IMetaElement)
            {
                
                    return null;
            }
            else

            if(parentTag is INestedCompositeElementElement)
            {

                if (name == "parent")
                {
                    return new ParentElementImpl();
                }
                else


                if (name == "property")
                {
                    return new PropertyElementImpl();
                }

                if (name == "many-to-one")
                {
                    return new ManyToOneElementImpl();
                }

                if (name == "nested-composite-element")
                {
                    return new NestedCompositeElementElementImpl();
                }

                else
                
                    return null;
            }
            else

            if(parentTag is IOneToManyElement)
            {
                
                    return null;
            }
            else

            if(parentTag is IOneToOneElement)
            {

                if (name == "meta")
                {
                    return new MetaElementImpl();
                }
                else
                
                    return null;
            }
            else

            if(parentTag is IParamElement)
            {
                
                    return null;
            }
            else

            if(parentTag is IParentElement)
            {
                
                    return null;
            }
            else

            if(parentTag is IPrimitiveArrayElement)
            {

                if (name == "meta")
                {
                    return new MetaElementImpl();
                }
                else

                if (name == "jcs-cache")
                {
                    return new JcsCacheElementImpl();
                }
                else

                if (name == "cache")
                {
                    return new CacheElementImpl();
                }
                else

                if (name == "key")
                {
                    return new KeyElementImpl();
                }
                else

                if (name == "index")
                {
                    return new IndexElementImpl();
                }
                else

                if (name == "element")
                {
                    return new ElementElementImpl();
                }
                else
                
                    return null;
            }
            else

            if(parentTag is IQueryElement)
            {
                
                    return null;
            }
            else

            if(parentTag is IReturnElement)
            {

                if (name == "return-discriminator")
                {
                    return new ReturnDiscriminatorElementImpl();
                }
                else

                if (name == "return-property")
                {
                    return new ReturnPropertyElementImpl();
                }
                else
                
                    return null;
            }
            else

            if(parentTag is IReturnDiscriminatorElement)
            {
                
                    return null;
            }
            else

            if(parentTag is IReturnScalarElement)
            {
                
                    return null;
            }
            else

            if(parentTag is IReturnColumnElement)
            {
                
                    return null;
            }
            else

            if(parentTag is IReturnPropertyElement)
            {

                if (name == "return-column")
                {
                    return new ReturnColumnElementImpl();
                }
                else
                
                    return null;
            }
            else

            if(parentTag is IReturnJoinElement)
            {
                
                    return null;
            }
            else

            if(parentTag is ILoadCollectionElement)
            {
                
                    return null;
            }
            else

            if(parentTag is ISynchronizeElement)
            {
                
                    return null;
            }
            else

            if(parentTag is ISqlQueryElement)
            {
                
                    return null;
            }
            else

            if(parentTag is IListElement)
            {

                if (name == "meta")
                {
                    return new MetaElementImpl();
                }
                else

                if (name == "jcs-cache")
                {
                    return new JcsCacheElementImpl();
                }
                else

                if (name == "cache")
                {
                    return new CacheElementImpl();
                }
                else

                if (name == "key")
                {
                    return new KeyElementImpl();
                }
                else

                if (name == "index")
                {
                    return new IndexElementImpl();
                }
                else

                if (name == "element")
                {
                    return new ElementElementImpl();
                }
                else

                if (name == "one-to-many")
                {
                    return new OneToManyElementImpl();
                }
                else

                if (name == "many-to-many")
                {
                    return new ManyToManyElementImpl();
                }
                else

                if (name == "composite-element")
                {
                    return new CompositeElementElementImpl();
                }
                else

                if (name == "many-to-any")
                {
                    return new ManyToAnyElementImpl();
                }
                else

                if (name == "filter")
                {
                    return new FilterElementImpl();
                }
                else
                
                    return null;
            }
            else

            if(parentTag is IBagElement)
            {

                if (name == "meta")
                {
                    return new MetaElementImpl();
                }
                else

                if (name == "jcs-cache")
                {
                    return new JcsCacheElementImpl();
                }
                else

                if (name == "cache")
                {
                    return new CacheElementImpl();
                }
                else

                if (name == "key")
                {
                    return new KeyElementImpl();
                }
                else

                if (name == "element")
                {
                    return new ElementElementImpl();
                }
                else

                if (name == "one-to-many")
                {
                    return new OneToManyElementImpl();
                }
                else

                if (name == "many-to-many")
                {
                    return new ManyToManyElementImpl();
                }
                else

                if (name == "composite-element")
                {
                    return new CompositeElementElementImpl();
                }
                else

                if (name == "many-to-any")
                {
                    return new ManyToAnyElementImpl();
                }
                else

                if (name == "filter")
                {
                    return new FilterElementImpl();
                }
                else

                if (name == "loader")
                {
                    return new LoaderElementImpl();
                }
                else

                if (name == "sql-insert")
                {
                    return new SqlInsertElementImpl();
                }
                else

                if (name == "sql-update")
                {
                    return new SqlUpdateElementImpl();
                }
                else

                if (name == "sql-delete")
                {
                    return new SqlDeleteElementImpl();
                }
                else

                if (name == "sql-delete-all")
                {
                    return new SqlDeleteAllElementImpl();
                }
                else
                
                    return null;
            }
            else

            if(parentTag is ISetElement)
            {

                if (name == "meta")
                {
                    return new MetaElementImpl();
                }
                else

                if (name == "jcs-cache")
                {
                    return new JcsCacheElementImpl();
                }
                else

                if (name == "cache")
                {
                    return new CacheElementImpl();
                }
                else

                if (name == "key")
                {
                    return new KeyElementImpl();
                }
                else

                if (name == "element")
                {
                    return new ElementElementImpl();
                }
                else

                if (name == "one-to-many")
                {
                    return new OneToManyElementImpl();
                }
                else

                if (name == "many-to-many")
                {
                    return new ManyToManyElementImpl();
                }
                else

                if (name == "composite-element")
                {
                    return new CompositeElementElementImpl();
                }
                else

                if (name == "many-to-any")
                {
                    return new ManyToAnyElementImpl();
                }
                else

                if (name == "filter")
                {
                    return new FilterElementImpl();
                }
                else

                if (name == "loader")
                {
                    return new LoaderElementImpl();
                }
                else

                if (name == "sql-insert")
                {
                    return new SqlInsertElementImpl();
                }
                else

                if (name == "sql-update")
                {
                    return new SqlUpdateElementImpl();
                }
                else

                if (name == "sql-delete")
                {
                    return new SqlDeleteElementImpl();
                }
                else

                if (name == "sql-delete-all")
                {
                    return new SqlDeleteAllElementImpl();
                }
                else
                
                    return null;
            }
            else

            if(parentTag is IMapElement)
            {

                if (name == "meta")
                {
                    return new MetaElementImpl();
                }
                else

                if (name == "jcs-cache")
                {
                    return new JcsCacheElementImpl();
                }
                else

                if (name == "cache")
                {
                    return new CacheElementImpl();
                }
                else

                if (name == "key")
                {
                    return new KeyElementImpl();
                }
                else

                if (name == "index")
                {
                    return new IndexElementImpl();
                }
                else

                if (name == "composite-index")
                {
                    return new CompositeIndexElementImpl();
                }
                else

                if (name == "index-many-to-many")
                {
                    return new IndexManyToManyElementImpl();
                }
                else

                if (name == "index-many-to-any")
                {
                    return new IndexManyToAnyElementImpl();
                }
                else

                if (name == "element")
                {
                    return new ElementElementImpl();
                }
                else

                if (name == "one-to-many")
                {
                    return new OneToManyElementImpl();
                }
                else

                if (name == "many-to-many")
                {
                    return new ManyToManyElementImpl();
                }
                else

                if (name == "composite-element")
                {
                    return new CompositeElementElementImpl();
                }
                else

                if (name == "many-to-any")
                {
                    return new ManyToAnyElementImpl();
                }
                else

                if (name == "filter")
                {
                    return new FilterElementImpl();
                }
                else

                if (name == "loader")
                {
                    return new LoaderElementImpl();
                }
                else

                if (name == "sql-insert")
                {
                    return new SqlInsertElementImpl();
                }
                else

                if (name == "sql-update")
                {
                    return new SqlUpdateElementImpl();
                }
                else

                if (name == "sql-delete")
                {
                    return new SqlDeleteElementImpl();
                }
                else

                if (name == "sql-delete-all")
                {
                    return new SqlDeleteAllElementImpl();
                }
                else
                
                    return null;
            }
            else

            if(parentTag is ITypeElement)
            {

                if (name == "param")
                {
                    return new ParamElementImpl();
                }
                else
                
                    return null;
            }
            else

                return null;
            }        
    }
}