using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Arp.log4net.Psi.Tree;
using Arp.log4net.Psi.Tree.Impl.SchemaSupport;
using JetBrains.Util;
using NUnit.Framework;

namespace Arp.Tests
{
    [TestFixture]
    public class SchemaParameterDescriptorProviderTests
    {
        private XmlSchema xmlSchema;
        private SchemaParameterDescriptorProvider provider;
        
        private const string SECTION_TAG =  "log4net";

        [SetUp]
        public void SetUpFixture()
        {
            string relativePath = "../../../TestFiles/log4net.xsd";
            Assert.IsTrue(File.Exists(relativePath), string.Format("File does not exist {0}", relativePath)); 
            xmlSchema = XmlSchema.Read(new XmlTextReader(relativePath), null);
            xmlSchema.Compile(null);
            provider = new SchemaParameterDescriptorProvider(xmlSchema);
        }

        [Test]
        public void SimpleSequense()
        {
            ICollection<IParameterDescriptor> descriptors = provider.GetParameterDescriptors(SECTION_TAG);
            IParameterDescriptor appenderDescriptor = GetWithCheck(descriptors, "appender");
            IParameterDescriptor loggerDescriptor = GetWithCheck(descriptors, "logger");
        }

        protected IParameterDescriptor GetWithCheck(ICollection<IParameterDescriptor> descriptors, string name)
        {
            IList<IParameterDescriptor> found = CollectionUtil.FindAll(descriptors, delegate(IParameterDescriptor obj)
                                                                  {
                                                                      return obj.Name == name;
                                                                  });
            if(found.Count == 0)
                Assert.Fail(string.Format("descriptor with {0} does not exist", name));

            if (found.Count > 1)
                Assert.Fail(string.Format("descriptors with {0} are more then one - {1}", name, found.Count));

            return found[0];
        }
    }
}