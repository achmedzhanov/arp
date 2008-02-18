using System;
using System.IO;
using System.Xml.Schema;

namespace Arp.Generator.Tests
{
    public class BaseTestFixture
    {
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
    }
}