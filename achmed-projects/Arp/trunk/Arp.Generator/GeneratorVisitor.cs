using System;
using System.Collections.Generic;
using System.Xml.Schema;
using Arp.Common.Assertions;
using Arp.Generator.Generation;
using Arp.Generator.Names;

namespace Arp.Generator
{
    
    public class GeneratorVisitor : XsdVisitor
    {
        private INameConverter nameConverter;
        private string targetNamespace;
        private IEnumAcceptor enumAcceptor;
        private IElementsAcceptor elementsAcceptor;
        private readonly Dictionary<XmlSchemaElement,ElementGenerationInfo> elementsMap = new Dictionary<XmlSchemaElement, ElementGenerationInfo>();
        private readonly Dictionary<XmlSchemaComplexType, TypeGenerationInfo> typesMap = new Dictionary<XmlSchemaComplexType, TypeGenerationInfo>();

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
            ElementGenerationInfo elementGenerationInfo = new ElementGenerationInfo(element.Name, new TypeGenerationInfo(typeName));
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
            ElementGenerationInfo elementGenerationInfo = new ElementGenerationInfo(xmlName, new TypeGenerationInfo(typeName));
            Set(element, elementGenerationInfo);
            base.VisitXmlSchemaElementNested(element);
        }

        public override void VisitXmlSchemaAttribute(XmlSchemaComplexType complexType, XmlSchemaAttribute attribute)
        {
            TypeGenerationInfo typeGenerationInfo = Get(complexType);
            Assert.CheckNotNull(typeGenerationInfo);
            CreateAttributeInfo(typeGenerationInfo, attribute);
            base.VisitXmlSchemaAttribute(complexType, attribute);
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
                Set(xmlSchemaComplexType, new TypeGenerationInfo(typeName));
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


        public override void VisitXmlSchemaChoice(XmlSchemaChoice choice)
        {
            base.VisitXmlSchemaChoice(choice);



        }

        public override void VisitXmlSchemaSequence(XmlSchemaSequence sequence)
        {
            base.VisitXmlSchemaSequence(sequence);
            XmlSchemaComplexType xmlSchemaComplexType = sequence.Parent as XmlSchemaComplexType; 
            if(xmlSchemaComplexType != null)
            {
                TypeGenerationInfo typeGenerationInfo = Get(xmlSchemaComplexType);

                foreach (XmlSchemaObject schemaObject in sequence.Items)
                {
                    XmlSchemaParticle particle = null;

                    if(schemaObject is XmlSchemaElement)
                    {
                        XmlSchemaElement element = (XmlSchemaElement)schemaObject;
                        typeGenerationInfo.FlatNestedElements.Add(new NestedElementGenerationInfo(Get(element)));
                    }
                    else if (schemaObject is XmlSchemaChoice)
                    {
                        XmlSchemaChoice choice = (XmlSchemaChoice)schemaObject;
                        
                        if(IsCollection(choice))
                        {
                            List<ElementGenerationInfo> elementsInfo = new List<ElementGenerationInfo>();

                            foreach (XmlSchemaObject choiceItem in choice.Items)
                            {
                                XmlSchemaElement choiceElement = choiceItem as XmlSchemaElement;
                                Assert.CheckNotNull(choiceElement, "Unexpected items type "  + choiceItem.GetType());
                                Assert.CheckFalse(IsCollection(choiceElement), "Unexpected collection");
                                elementsInfo.Add(Get(choiceElement));
                            }
                            Assert.Check(elementsInfo.Count > 0, "Unexpected empty choice");

                            typeGenerationInfo.FlatNestedElements.Add(new NestedElementGenerationInfo(elementsInfo));

                        }
                        else
                        {
                            foreach (XmlSchemaObject choiceItem in choice.Items)
                            {
                                XmlSchemaElement choiceElement = choiceItem as XmlSchemaElement;
                                Assert.CheckNotNull(choiceElement, "Unexpected items type " + choiceItem.GetType());
                                ElementGenerationInfo info = Get(choiceElement);
                                if(IsCollection(choiceElement))
                                {
                                    List<ElementGenerationInfo> list = new List<ElementGenerationInfo>();
                                    list.Add(info);
                                    typeGenerationInfo.FlatNestedElements.Add(new NestedElementGenerationInfo(list));                            
                                }
                                else
                                {
                                    typeGenerationInfo.FlatNestedElements.Add(new NestedElementGenerationInfo(info));                            
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
        }

        #endregion


        private bool IsCollection(XmlSchemaParticle particle)
        {
            if(particle.MaxOccurs == 1)
                return false;
            else
                return true;
        }

        private void CreateAttributeInfo(TypeGenerationInfo typeGenerationInfo, XmlSchemaAttribute attribute)
        {
            string xmlName = attribute.Name;
            typeGenerationInfo.Attributes.Add(new AttributeGenerationInfo(xmlName, nameConverter.ConvertAttributeName(xmlName)));
        }

        private ElementGenerationInfo Get(XmlSchemaElement element)
        {
            Assert.Check(elementsMap.ContainsKey(element));
            ElementGenerationInfo ret = elementsMap[element];
            Assert.CheckNotNull(ret);
            return ret;
        }

        private void Set(XmlSchemaElement element, ElementGenerationInfo info)
        {
            Assert.CheckFalse(elementsMap.ContainsKey(element));
            elementsMap[element] = info;
        }

        private TypeGenerationInfo Get(XmlSchemaComplexType complexType)
        {
            Assert.Check(typesMap.ContainsKey(complexType));
            TypeGenerationInfo ret = typesMap[complexType];
            Assert.CheckNotNull(ret);
            return ret;
        }

        private void Set(XmlSchemaComplexType complexType, TypeGenerationInfo typeGenerationInfo)
        {
            Assert.CheckFalse(typesMap.ContainsKey(complexType));
            typesMap[complexType] = typeGenerationInfo;
        }

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
            
            nameParts.Add("Type");

            return new TypeName(nameConverter.CreateComplexName(nameParts.ToArray()), targetNamespace); 
        }

    }
}