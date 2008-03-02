using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Schema;
using Arp.Generator.Acceptors;
using Arp.Generator.Names;
using Arp.Generator.Preprocessing.Impl;

namespace Arp.Generator.Tests
{
    public class BaseTestFixture
    {
        protected string BASE_NAMESPACE = "Arp.NH.Psi.Tree";

        protected XmlSchema CreateXmlSchema(string xsd)
        {
            XmlSchema xmlSchema = XmlSchema.Read(new StringReader(xsd), ValidationEventHandler);
            xmlSchema.Compile(ValidationEventHandler);
            return xmlSchema;
        }

        private void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Error)
                throw new Exception("", e.Exception);
        }

        protected class EnumGenerator : IEnumAcceptor
        {
            
            List<EnumGenerationInfo> enumGenerationInfos = new List<EnumGenerationInfo>();

            #region IEnumAcceptor Members

            public void Accept(EnumGenerationInfo enumGenerationInfo)
            {
                enumGenerationInfos.Add(enumGenerationInfo);
            }

            #endregion

            public List<EnumGenerationInfo> EnumGenerationInfos
            {
                get { return enumGenerationInfos; }
            }
        }

        protected class ElementGenerator : IElementsAcceptor
        {

            List<ElementGenerationInfo> elementGenerationInfos = new List<ElementGenerationInfo>();

            #region IElementsAcceptor Members

            public void Accept(ElementGenerationInfo elementGenerationInfo)
            {
                elementGenerationInfos.Add(elementGenerationInfo);
            }

            #endregion

            public List<ElementGenerationInfo> ElementGenerationInfos
            {
                get { return elementGenerationInfos; }
            }
        }

        protected PreprocesingVisitor CreateGeneratorVisitor()
        {
            PreprocesingVisitor preprocesingVisitor = new PreprocesingVisitor();
            preprocesingVisitor.NameConverter = new CamelNameConverter();
            preprocesingVisitor.TargetNamespace = BASE_NAMESPACE;
            preprocesingVisitor.EnumAcceptor = new EnumGenerator();
            preprocesingVisitor.ElementsAcceptor = new ElementGenerator();
            return preprocesingVisitor;
        }
    }
}