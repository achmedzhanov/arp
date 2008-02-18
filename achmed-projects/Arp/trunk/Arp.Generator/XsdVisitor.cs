using System;
using System.Xml.Schema;
using Arp.Common.Assertions;

namespace Arp.Generator
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

            Dispatch(xmlSchemaComplexType.AttributeUses);

        }

        public virtual void VisitSequense(XmlSchemaSequence xmlSchemaSequence)
        {
            throw new NotImplementedException();
        }

        public virtual void VisitXmlSchemaElement(XmlSchemaElement element)
        {
            if (element.Name != null)
                VisitXmlSchemaElementNamed(element);
            else
                VisitXmlSchemaElementNested(element);
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
            else
                Assert.Fail("Unexpected " + o);
        }

    }
}