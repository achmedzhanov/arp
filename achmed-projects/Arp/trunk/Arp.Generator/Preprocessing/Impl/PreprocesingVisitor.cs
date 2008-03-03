using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml.Schema;
using Arp.Common.Assertions;
using Arp.Common.Utils;
using Arp.Generator.Acceptors;
using Arp.Generator.Names;
using Arp.Generator.Preprocessing.Impl;
using Arp.Generator.Xsd;

namespace Arp.Generator.Preprocessing.Impl
{
    public class PreprocesingVisitor : XsdVisitor, IRegistry<GroupGenerationInfo>, IRegistry<AttributesGroupGenerationInfo>
    {
        private INameConverter nameConverter;
        private string targetNamespace;
        private IEnumAcceptor enumAcceptor;
        private IElementsAcceptor elementsAcceptor;
        private readonly Dictionary<XmlSchemaElement,ElementGenerationInfo> elementsMap = new Dictionary<XmlSchemaElement, ElementGenerationInfo>();
        private readonly Dictionary<XmlSchemaComplexType, TypeGenerationInfo> typesMap = new Dictionary<XmlSchemaComplexType, TypeGenerationInfo>();
        private readonly Dictionary<XmlSchemaGroup, GroupGenerationInfo> groupsMap = new Dictionary<XmlSchemaGroup, GroupGenerationInfo>();
        private readonly Dictionary<XmlSchemaAttributeGroup, AttributesGroupGenerationInfo> attributeGroupsMap = new Dictionary<XmlSchemaAttributeGroup, AttributesGroupGenerationInfo>();

        public INameConverter NameConverter
        {
            get { return nameConverter; }
            set { nameConverter = value; }
        }


        public string TargetNamespace
        {
            get { return targetNamespace; }
            set { targetNamespace = value; }
        }


        public IEnumAcceptor EnumAcceptor
        {
            get { return enumAcceptor; }
            set { enumAcceptor = value; }
        }


        public IElementsAcceptor ElementsAcceptor
        {
            get { return elementsAcceptor; }
            set { elementsAcceptor = value; }
        }

        #region Visitor overrides

        public override void VisitXmlSchemaElementNamed(XmlSchemaElement element)
        {
            TypeName typeName = GetElementShortTypeName(element);
            ElementGenerationInfo elementGenerationInfo = new ElementGenerationInfo(element.Name, new TypeGenerationInfo(typeName, element.Name));
            Set(element, elementGenerationInfo);
            base.VisitXmlSchemaElementNamed(element);
            elementsAcceptor.Accept(elementGenerationInfo);
        }

        public override void VisitXmlSchemaElementNested(XmlSchemaElement element)
        {
            TypeName typeName = GetElementShortTypeName(element);
            string xmlName = element.Name;
            if(string.IsNullOrEmpty(xmlName))
            {
                Assert.CheckNotNull(element.RefName);
                Assert.CheckFalse(string.IsNullOrEmpty(element.RefName.Name));
                xmlName = element.RefName.Name;
            }
            ElementGenerationInfo elementGenerationInfo = new ElementGenerationInfo(xmlName, new TypeGenerationInfo(typeName, xmlName   ));
            Set(element, elementGenerationInfo);
            base.VisitXmlSchemaElementNested(element);
        }

        public override void VisitXmlSchemaAttribute(XmlSchemaComplexType complexType, XmlSchemaAttribute attribute)
        {
            base.VisitXmlSchemaAttribute(complexType, attribute);
            TypeGenerationInfo typeGenerationInfo = Get(complexType);
            Assert.CheckNotNull(typeGenerationInfo);
            CreateAttributeInfo(typeGenerationInfo.Attributes, attribute);
        }

        public override void VisitXmlSchemaAttribute(XmlSchemaAttributeGroup xmlSchemaAttributeGroup,
                                                     XmlSchemaAttribute attribute)
        {
            base.VisitXmlSchemaAttribute(xmlSchemaAttributeGroup, attribute);
            AttributesGroupGenerationInfo attributesGroupGenerationInfo = Get(xmlSchemaAttributeGroup);
            Assert.CheckNotNull(attributesGroupGenerationInfo);
            CreateAttributeInfo(attributesGroupGenerationInfo.Attributes, attribute);
        }

        public override void VisitComplexType(XmlSchemaComplexType xmlSchemaComplexType)
        {
            if (xmlSchemaComplexType.Parent is XmlSchemaElement )
            {
                ElementGenerationInfo elementGenerationInfo = Get((XmlSchemaElement)xmlSchemaComplexType.Parent);
                Set(xmlSchemaComplexType, elementGenerationInfo.TypeGenerationInfo);
            }
            else
            {
                Assert.CheckNotNull(xmlSchemaComplexType.Name);
                TypeName typeName = new TypeName(nameConverter.ConvertElementName(xmlSchemaComplexType.Name), targetNamespace);
                Set(xmlSchemaComplexType, new TypeGenerationInfo(typeName, xmlSchemaComplexType.Name));
            }

            base.VisitComplexType(xmlSchemaComplexType);
        }

        public override void VisitXmlSchemaSimpleTypeRestrictionNotEmpty(XmlSchemaSimpleTypeRestriction restriction)
        {
            base.VisitXmlSchemaSimpleTypeRestrictionNotEmpty(restriction);

            if (restriction.BaseTypeName.Name == "string")
            {
                TypeName typeName = GetRestrictionShortTypeName(restriction);
                Dictionary<string, string> namesValues = new Dictionary<string, string>();
                Dictionary<string, string> namesXmlValues = new Dictionary<string, string>();
                int valueCount = 0;
                foreach (XmlSchemaFacet facet in restriction.Facets)
                {
                    string filedName = nameConverter.ConvertFacetName(facet.Value);
                    namesValues.Add(filedName, (valueCount++).ToString());
                    namesXmlValues.Add(filedName, facet.Value);
                }

                enumAcceptor.Accept(new EnumGenerationInfo(typeName, namesValues, namesXmlValues));

            }
            else
                Assert.Fail(" unexpected base type name: " + restriction.BaseTypeName.Name);
        }

        public override void VisitXmlSchemaSequence(XmlSchemaSequence sequence)
        {
            base.VisitXmlSchemaSequence(sequence);
            List<NestedElementGenerationInfo> flatElements = null;
            
            if(sequence.Parent is XmlSchemaComplexType)
            {
                XmlSchemaComplexType xmlSchemaComplexType = (XmlSchemaComplexType) sequence.Parent; 
                TypeGenerationInfo typeGenerationInfo = Get(xmlSchemaComplexType);
                flatElements = typeGenerationInfo.FlatNestedElements;                
            }
            else if(sequence.Parent is XmlSchemaGroup)
            {
                XmlSchemaGroup schemaGroup = (XmlSchemaGroup)sequence.Parent; 
                GroupGenerationInfo groupGenerationInfo = Get(schemaGroup);
                flatElements = groupGenerationInfo.FlatNestedElements;                
            }
            else
                Assert.Fail("Unexpected type " + sequence.Parent);

            ProcessSequence(sequence, flatElements);

        }

        protected override void VisitXmlSchemaAttributeGroup(XmlSchemaAttributeGroup group)
        {
            Set(group, new AttributesGroupGenerationInfo(group.Name));
            base.VisitXmlSchemaAttributeGroup(group);
        }

        public override void VisitXmlSchemaGroup(XmlSchemaGroup group)
        {
            Set(group, new GroupGenerationInfo(group.Name));
            base.VisitXmlSchemaGroup(group);
        }


        public override void VisitXmlSchemaGroupRef(XmlSchemaGroupRef groupRef)
        {
            base.VisitXmlSchemaGroupRef(groupRef);

            if (groupRef.Parent is XmlSchemaSequence && groupRef.Parent.Parent is XmlSchemaComplexType)
            {
                XmlSchemaComplexType complexType = (XmlSchemaComplexType) groupRef.Parent.Parent;
                TypeGenerationInfo typeGenerationInfo = Get(complexType);
                typeGenerationInfo.Groups.Add(new RegistryInfoRef<GroupGenerationInfo>(this, groupRef.RefName.Name));
            }
            else
                Assert.Fail("Unexpected element type " + groupRef.Parent + " ; " + groupRef.Parent.Parent);

        }

        protected override void VisitXmlSchemaAttributeGroupRef(XmlSchemaAttributeGroupRef groupRef)
        {
            base.VisitXmlSchemaAttributeGroupRef(groupRef);

            if (groupRef.Parent is XmlSchemaComplexType)
            {
                XmlSchemaComplexType xmlSchemaComplexType = (XmlSchemaComplexType)groupRef.Parent;
                TypeGenerationInfo typeGenerationInfo = Get(xmlSchemaComplexType);
                typeGenerationInfo.AttributeGroups.Add(new RegistryInfoRef<AttributesGroupGenerationInfo>(this, groupRef.RefName.Name));
            }
            else
                    Assert.Fail("Unexpected element type " + groupRef.Parent);

        }

        #endregion

        private void ProcessSequence(XmlSchemaSequence sequence, List<NestedElementGenerationInfo> flatElements)
        {
            foreach (XmlSchemaObject schemaObject in sequence.Items)
            {
                XmlSchemaParticle particle = null;

                if (schemaObject is XmlSchemaElement)
                {
                    XmlSchemaElement element = (XmlSchemaElement)schemaObject;
                    flatElements.Add(new NestedElementGenerationInfo(Get(element)));
                }
                else if (schemaObject is XmlSchemaChoice)
                {
                    XmlSchemaChoice choice = (XmlSchemaChoice)schemaObject;

                    if (IsCollection(choice))
                    {
                        List<ElementGenerationInfo> elementsInfo = new List<ElementGenerationInfo>();

                        foreach (XmlSchemaObject choiceItem in choice.Items)
                        {
                            XmlSchemaElement choiceElement = choiceItem as XmlSchemaElement;
                            Assert.CheckNotNull(choiceElement, "Unexpected items type " + choiceItem.GetType());
                            Assert.CheckFalse(IsCollection(choiceElement), "Unexpected collection");
                            elementsInfo.Add(Get(choiceElement));
                        }
                        Assert.Check(elementsInfo.Count > 0, "Unexpected empty choice");

                        flatElements.Add(new NestedElementGenerationInfo(elementsInfo));

                    }
                    else
                    {
                        foreach (XmlSchemaObject choiceItem in choice.Items)
                        {
                            XmlSchemaElement choiceElement = choiceItem as XmlSchemaElement;
                            Assert.CheckNotNull(choiceElement, "Unexpected items type " + choiceItem.GetType());
                            ElementGenerationInfo info = Get(choiceElement);
                            if (IsCollection(choiceElement))
                            {
                                List<ElementGenerationInfo> list = new List<ElementGenerationInfo>();
                                list.Add(info);
                                flatElements.Add(new NestedElementGenerationInfo(list));
                            }
                            else
                            {
                                flatElements.Add(new NestedElementGenerationInfo(info));
                            }
                        }
                    }
                }
                else if (schemaObject is XmlSchemaGroupRef)
                {
                    // TODO
                }
                else if (schemaObject is XmlSchemaAttributeGroupRef)
                {
                    // TODO
                }
                else
                    Assert.Fail("Unexpected type element " + schemaObject);
            }
        }

        private bool IsCollection(XmlSchemaParticle particle)
        {
            if(particle.MaxOccurs == 1)
                return false;
            else
                return true;
        }

        private void CreateAttributeInfo(List<AttributeGenerationInfo> attributes, XmlSchemaAttribute attribute)
        {
            string xmlName = attribute.Name;
            attributes.Add(new AttributeGenerationInfo(xmlName, nameConverter.ConvertAttributeName(xmlName)));
        }

        #region Get/Set generics

        private static ET Get<KT,ET>(IDictionary<KT, ET> dictionary, KT element)
        {
            Assert.Check(dictionary.ContainsKey(element));
            ET ret = dictionary[element];
            Assert.CheckNotNull(ret);
            return ret;            
        }

        private static void Set<KT, ET>(IDictionary<KT, ET> dictionary, KT element, ET info)
        {
            Assert.CheckFalse(dictionary.ContainsKey(element));
            dictionary[element] = info;
        }

        #endregion

        #region Get/Set XmlSchemaElement

        private ElementGenerationInfo Get(XmlSchemaElement element)
        {
            return Get(elementsMap, element);
        }

        private void Set(XmlSchemaElement element, ElementGenerationInfo info)
        {
            Set(elementsMap, element, info);
        }

        #endregion

        #region Get/Set XmlSchemaComplexType

        private TypeGenerationInfo Get(XmlSchemaComplexType complexType)
        {
            return Get(typesMap, complexType);
        }

        private void Set(XmlSchemaComplexType complexType, TypeGenerationInfo typeGenerationInfo)
        {
            Set(typesMap, complexType, typeGenerationInfo);
        }

        #endregion

        #region Get/Set XmlSchemaGroup

        private GroupGenerationInfo Get(XmlSchemaGroup xmlSchemaGroup)
        {
            return Get(groupsMap, xmlSchemaGroup);
        }

        private void Set(XmlSchemaGroup xmlSchemaGroup, GroupGenerationInfo groupGenerationInfo)
        {
            Set(groupsMap, xmlSchemaGroup, groupGenerationInfo);
        }

        #endregion

        #region Get/Set XmlSchemaAttributesGroup

        private AttributesGroupGenerationInfo Get(XmlSchemaAttributeGroup xmlSchemaAttributeGroup)
        {
            return Get(attributeGroupsMap, xmlSchemaAttributeGroup);
        }

        private void Set(XmlSchemaAttributeGroup xmlSchemaAttributeGroup, AttributesGroupGenerationInfo attributesGroupGenerationInfo)
        {
            Set(attributeGroupsMap, xmlSchemaAttributeGroup, attributesGroupGenerationInfo);
        }

        #endregion

        #region IRegistry<GroupGenerationInfo> Members

        public GroupGenerationInfo Get(string name)
        {
            GroupGenerationInfo found = CollectionsUtils.Find<GroupGenerationInfo>(groupsMap.Values, delegate(GroupGenerationInfo obj)
                                                                                                         {
                                                                                                             return obj.Name == name;
                                                                                                         });
            Assert.CheckNotNull(found);
            return found;
        }

        #endregion

        #region IRegistry<AttributesGroupGenerationInfo> Members

        AttributesGroupGenerationInfo IRegistry<AttributesGroupGenerationInfo>.Get(string name)
        {
            AttributesGroupGenerationInfo found = CollectionsUtils.Find<AttributesGroupGenerationInfo>(attributeGroupsMap.Values, delegate(AttributesGroupGenerationInfo obj)
                                                                                             {
                                                                                                 return obj.Name == name;
                                                                                             });
            Assert.CheckNotNull(found);
            return found;
        }

        #endregion

        private TypeName GetElementShortTypeName(XmlSchemaElement element)
        {
            string baseXmlName = null;
            if (!String.IsNullOrEmpty(element.Name))
                baseXmlName = element.Name;
            else if (!String.IsNullOrEmpty(element.RefName.Name))
            {
                baseXmlName = element.RefName.Name;
            }
            else 
                Assert.Fail();

            return new TypeName(nameConverter.ConvertSimpleTypeRestrictionName(baseXmlName), targetNamespace); 
        }

        
        private TypeName GetRestrictionShortTypeName(XmlSchemaSimpleTypeRestriction restriction)
        {
            XmlSchemaObject parent = restriction.Parent;
            XmlSchemaSimpleType simpleType = (XmlSchemaSimpleType) parent;
            if(simpleType.Name != null)
            {
                return new TypeName(nameConverter.ConvertSimpleTypeRestrictionName(simpleType.Name), targetNamespace); 
            }

            parent = simpleType.Parent;
            List<string> nameParts = new List<string>();
            XmlSchemaAttribute attribute = (XmlSchemaAttribute)parent;

            nameParts.Insert(0,attribute.Name);


            XmlSchemaComplexType xmlSchemaComplexType = attribute.Parent as XmlSchemaComplexType;
            if(xmlSchemaComplexType != null)
            {
                if(xmlSchemaComplexType.Name != null)
                {
                    nameParts.Insert(0, xmlSchemaComplexType.Name);
                }
                else
                {
                    XmlSchemaElement element = (XmlSchemaElement)xmlSchemaComplexType.Parent;
                    Assert.CheckNotNull(element.Name);
                    nameParts.Insert(0, element.Name);
                }
            }
            
            nameParts.Add("TypeInfo");

            return new TypeName(nameConverter.CreateComplexName(nameParts.ToArray()), targetNamespace); 
        }

    }
}