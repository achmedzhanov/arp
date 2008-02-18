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

        public override void VisitXmlSchemaElementNamed(XmlSchemaElement element)
        {
            base.VisitXmlSchemaElementNamed(element);


            TypeName typeName = GetElementShortTypeName(element);

            List<AttributeGenerationInfo> attributes = null;
            List<NestedElementGenerationInfo> nestedElements = null;

            ElementGenerationInfo elementGenerationInfo = new ElementGenerationInfo(element.Name, typeName, attributes, nestedElements);
            elementsAcceptor.Accept(elementGenerationInfo);

        }

        private TypeName GetElementShortTypeName(XmlSchemaElement element)
        {
            return new TypeName(nameConverter.ConvertSimpleTypeRestrictionName(element.Name), targetNamespace); 
        }

        public override void VisitXmlSchemaSimpleTypeRestrictionNotEmpty(XmlSchemaSimpleTypeRestriction restriction)
        {
            base.VisitXmlSchemaSimpleTypeRestrictionNotEmpty(restriction);

            if(restriction.BaseTypeName.Name == "string")
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