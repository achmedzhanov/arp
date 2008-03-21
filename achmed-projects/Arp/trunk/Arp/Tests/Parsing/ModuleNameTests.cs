using System;
using Arp.log4net.Psi.Parsing;
using Arp.log4net.Psi.Tree.Impl;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using NUnit.Framework;

namespace Arp.Tests.Parsing
{
    [TestFixture]
    public class ModuleNameTests : BaseParsingTest
    {
        [Test]
        public void ParseReferenceNameSuccess()
        {
            ReferenceParser parser = new ReferenceParser();
            ReferenceModule referenceName = parser.ParseModule("Arp.Common");
            Check(referenceName);
        }

        private void Check(ReferenceModule referenceName)
        {
            Assert.AreEqual("Arp.Common", referenceName.GetName());

            AssertChildsList(referenceName,
                             delegate(IElement obj)
                             {
                                 XmlToken token = (XmlToken)obj;
                                 Assert.AreEqual("Arp.Common", token.GetText());
                             });
        }

        [Test]
        public void ParseReferenceNameWithUnnecessaryEnd()
        {
            ReferenceParser parser = new ReferenceParser();
            Check(parser.ParseModule("Arp.Common "));
            Check(parser.ParseModule("Arp.Common;"));
            Check(parser.ParseModule("Arp.Common\""));
            Check(parser.ParseModule("Arp.Common&%"));
        }

        [Test]
        public void ParseReferenceNameExpectedIdentifier()
        {
            ReferenceParser parser = new ReferenceParser();
            ParseWithoutIdentifier(parser, "Arp.Common.");
            ParseWithoutIdentifier(parser, "Arp.Common.%%");
            ParseWithoutIdentifier(parser, "Arp.Common.-");
        }

        private void ParseWithoutIdentifier(ReferenceParser parser, string text)
        {
            try
            {

                parser.ParseModule(text);
            }
            catch (UnexpectedToken ex)
            {
                ReferenceModule referenceName = (ReferenceModule)ex.ParsingResult;

                CheckWithoutIdentifier(referenceName);
                return;
            }

            Assert.Fail("Expected UnexpectedToken exception");
        }

        private void CheckWithoutIdentifier(ReferenceModule referenceName)
        {
            Assert.AreEqual("Arp.Common.", referenceName.GetText());

            AssertChildsList(referenceName,
                             delegate(IElement obj)
                             {
                                 XmlToken token = (XmlToken)obj;
                                 Assert.AreEqual("Arp.Common", token.GetText());
                             });
        }

    }
}