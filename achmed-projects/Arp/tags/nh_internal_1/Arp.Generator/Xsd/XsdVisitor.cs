using System;
using System.Xml;
using System.Xml.Schema;
using Arp.Common.Assertions;

namespace Arp.Generator.Xsd
{
    public class XsdVisitor
    {

        public virtual void VisitSchema(XmlSchema xmlSchema)
        {
            foreach (XmlSchemaObject o in xmlSchema.Items)
            {
                Dispatch(o);
            }
        }

        public virtual void VisitXmlSchemaSimpleTypeRestriction(XmlSchemaSimpleTypeRestriction restriction)
        {
            if(restriction.BaseType != null  || restriction.Facets.Count > 0)
                VisitXmlSchemaSimpleTypeRestrictionNotEmpty(restriction);
        }

        public virtual void VisitXmlSchemaSimpleTypeRestrictionNotEmpty(XmlSchemaSimpleTypeRestriction restriction)
        {
            
        }

        public virtual void VisitXmlSchemaAttribute(XmlSchemaAttribute attribute)
        {
            Dispatch(attribute.AttributeSchemaType);

            // why there is no pattern mathing in C# ???

            if(attribute.Parent is XmlSchemaComplexType)
            {
                XmlSchemaComplexType complexType = (XmlSchemaComplexType) attribute.Parent;

                VisitXmlSchemaAttribute(complexType, attribute);
            }
            else if (attribute.Parent is XmlSchemaAttributeGroup)
            {
                XmlSchemaAttributeGroup attributeGroup = (XmlSchemaAttributeGroup)attribute.Parent;

                VisitXmlSchemaAttribute(attributeGroup, attribute);
            }
            else
            {
                Assert.Fail(attribute.Parent.GetType().ToString());
            }
                
        }

        public virtual void VisitXmlSchemaAttribute(XmlSchemaComplexType xmlSchemaComplexType , XmlSchemaAttribute attribute)
        {

        }

        public virtual void VisitXmlSchemaAttribute(XmlSchemaAttributeGroup xmlSchemaAttributeGroup, XmlSchemaAttribute attribute)
        {

        }

        public virtual void VisitSimpleType(XmlSchemaSimpleType xmlSchemaSimpleType)
        {
            XmlSchemaSimpleTypeContent content = xmlSchemaSimpleType.Content;
            Dispatch(content);
        }

        public virtual void VisitComplexType(XmlSchemaComplexType xmlSchemaComplexType)
        {
//            XmlSchemaParticle particle = xmlSchemaComplexType.ContentTypeParticle;
//            VisitParticle(particle);

            foreach (XmlSchemaObject attribute in xmlSchemaComplexType.Attributes)
            {
                Dispatch(attribute);
            }
            Dispatch(xmlSchemaComplexType.Particle);
        }

        public virtual void VisitXmlSchemaElement(XmlSchemaElement element)
        {
            if (element.Name != null)
                VisitXmlSchemaElementNamed(element);
            else
            {
                VisitXmlSchemaElementNested(element);
            }
            
        }

        public virtual void VisitXmlSchemaElementNested(XmlSchemaElement element)
        {
            Dispatch(element.SchemaType);
        }

        public virtual void VisitXmlSchemaElementNamed(XmlSchemaElement element)
        {
            Dispatch(element.SchemaType);
        }

        private void VisitParticle(XmlSchemaParticle particle)
        {
            Dispatch(particle);
        }

        private void Dispatch(XmlSchemaObjectTable table)
        {
            foreach (XmlSchemaObject o in table.Values)
            {
                Dispatch(o);
            }
        }

        private void Dispatch(XmlSchemaObject o)
        {
            if (o == null)
                return;

            if (o is XmlSchemaSimpleType)
            {
                VisitSimpleType((XmlSchemaSimpleType)o);
            }
            else if (o is XmlSchemaComplexType)
            {
                VisitComplexType((XmlSchemaComplexType)o);
            }
            else if (o is XmlSchemaAttribute)
            {
                VisitXmlSchemaAttribute((XmlSchemaAttribute)o);
            }
            else if (o is XmlSchemaElement)
            {
                VisitXmlSchemaElement((XmlSchemaElement)o);
            }
            else if (o is XmlSchemaSimpleTypeRestriction)
            {
                VisitXmlSchemaSimpleTypeRestriction((XmlSchemaSimpleTypeRestriction)o);
            }
            else if (o is XmlSchemaAttributeGroup)
            {
                VisitXmlSchemaAttributeGroup((XmlSchemaAttributeGroup)o);
            }
            else if (o is XmlSchemaAttributeGroupRef)
            {
                VisitXmlSchemaAttributeGroupRef((XmlSchemaAttributeGroupRef)o);
            }
            else if (o is XmlSchemaSequence)
            {
                VisitXmlSchemaSequence((XmlSchemaSequence)o);
            }
            else if (o is XmlSchemaChoice)
            {
                VisitXmlSchemaChoice((XmlSchemaChoice)o);
            }
            else if (o is XmlSchemaGroup)
            {
                VisitXmlSchemaGroup((XmlSchemaGroup)o);
            }
            else if (o is XmlSchemaGroupRef)
            {
                VisitXmlSchemaGroupRef((XmlSchemaGroupRef)o);
            }
            else
                Assert.Fail("Unexpected " + o + " " + o.GetType());
        }

        public virtual void VisitXmlSchemaGroupRef(XmlSchemaGroupRef groupRef)
        {
            
        }

        public virtual void VisitXmlSchemaGroup(XmlSchemaGroup group)
        {
            Dispatch(group.Particle);
        }

        public virtual void VisitXmlSchemaChoice(XmlSchemaChoice choice)
        {
            foreach (XmlSchemaObject o in choice.Items)
            {
                Dispatch(o);
            }
        }

        public virtual void VisitXmlSchemaSequence(XmlSchemaSequence sequence)
        {
            foreach (XmlSchemaObject o in sequence.Items)
            {
                Dispatch(o);
            }
        }

        protected virtual void VisitXmlSchemaAttributeGroupRef(XmlSchemaAttributeGroupRef groupRef)
        {
            // TODO
        }

        protected virtual void VisitXmlSchemaAttributeGroup(XmlSchemaAttributeGroup group)
        {
            foreach (XmlSchemaObject attribute in group.Attributes)
            {
                Dispatch(attribute);
            }
        }
    }
}