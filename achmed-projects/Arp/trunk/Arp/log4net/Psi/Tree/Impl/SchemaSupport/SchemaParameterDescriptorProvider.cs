using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using Arp.Assertions;
using Arp.log4net.Psi.Tree.Impl.SchemaSupport;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace Arp.log4net.Psi.Tree.Impl.SchemaSupport
{
    public class SchemaParameterDescriptorProvider
    {
        private XmlSchema xmlSchema;

        public SchemaParameterDescriptorProvider(XmlSchema xmlSchema)
        {
            this.xmlSchema = xmlSchema;
        }

        public ICollection<IParameterDescriptor> GetParameterDescriptors(string [] elements)
        {
            if (elements == null) throw new ArgumentNullException("elements");

            Assert.Check(elements.Length > 0);

            // TODO check valid element path 

            string elementName = elements[elements.Length - 1 ];

            return GetParameterDescriptors(elementName);
        }

        
        // TODO create absract visitor
        class DescriptrosCollector
        {
            XmlSchemaElement element;

            List<IParameterDescriptor> descriptors = new List<IParameterDescriptor>();

            public DescriptrosCollector(XmlSchemaElement element)
            {
                if (element == null) throw new ArgumentNullException("element");
                this.element = element;

                Collect();
            }

            protected void Collect()
            {
                XmlSchemaComplexType complexType = element.ElementSchemaType as XmlSchemaComplexType;

                List<IParameterDescriptor> ret = new List<IParameterDescriptor>();

                if (complexType != null)
                {
                    // elements
                    XmlSchemaSequence sequence = complexType.ContentTypeParticle as XmlSchemaSequence;
                    
                    if (sequence != null)
                    {
                        ProcessSequense(sequence, ret);
                    }
                    else
                    {
                        Assert.Fail("complexType.ContentTypeParticle " + complexType.ContentTypeParticle.GetType());
                    }
                    // attributes
                }
                else
                {
                    XmlSchemaSimpleType simpleType = (XmlSchemaSimpleType)element.ElementSchemaType;
                    Assert.Fail();
                }                
            }

            private void ProcessSequense(XmlSchemaSequence sequence, List<IParameterDescriptor> ret)
            {
                foreach (XmlSchemaObject item in sequence.Items)
                {
                    // TODO check for recursion

                    XmlSchemaElement itemElement = item as XmlSchemaElement;
                    if (itemElement != null)
                    {
                        ret.Add(new SchemaElementParameterDescriptor(itemElement));
                        continue;
                    }

//TODO assert for nested elements without ref

//                    XmlSchemaChoice itemChose = item as XmlSchemaChoice;
//                    if (itemChose != null)
//                    {
//                        ret.Add(new SchemaElementParameterDescriptor(itemElement));
//                        continue;
//                    }

                    Assert.Fail("item typw is " + item.GetType());

                }
            }

            public ICollection<IParameterDescriptor> Descriptors
            {
                get
                {
                    return descriptors;
                }
            }

        }


        public ICollection<IParameterDescriptor> GetParameterDescriptors(string elementName)
        {
            XmlQualifiedName qualifiedName = new XmlQualifiedName(elementName);

            if(!xmlSchema.Elements.Contains(qualifiedName))
                return EmptyArray<IParameterDescriptor>.Instance;

            XmlSchemaElement element = (XmlSchemaElement)xmlSchema.Elements[qualifiedName];

            DescriptrosCollector collector = new DescriptrosCollector(element);
            return collector.Descriptors;
            
        }
    }
}