using System;
using Arp.log4net.Psi.Parsing;
using Arp.log4net.Psi.Tree.Impl;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Refactorings.Conflicts.New;
using NUnit.Framework;

namespace Arp.Tests.Parsing
{
    [TestFixture]
    public class ReferencesNameTests
    {
        [Test]
        public void ParseReferenceNameSuccess()
        {
            ReferenceParser parser = new ReferenceParser();
            log4net.Psi.Tree.Impl.ReferenceName referenceName = parser.ParseReferenceName("TestFiles.Parsing.ClassForReferencesTests");
            
            Check(referenceName);
        }

        private void Check(ReferenceName referenceName)
        {
            Assert.AreEqual("ClassForReferencesTests", referenceName.GetName());
            Assert.AreEqual("TestFiles.Parsing.ClassForReferencesTests", referenceName.GetText());


            AssertChildsList(referenceName, 
                             delegate(IElement obj)

                                 {
                                     ReferenceName @ref = (ReferenceName)obj;
                                     Assert.AreEqual("TestFiles.Parsing", @ref.GetText());
                                 },
                             delegate(IElement obj)
                                 {
                                     XmlToken token = (XmlToken)obj;
                                     Assert.AreEqual(".", token.GetText());
                                 },
                             delegate(IElement obj)
                                 {
                                     XmlToken token = (XmlToken)obj;
                                     Assert.AreEqual("ClassForReferencesTests", token.GetText());
                                 });
        }

        [Test]
        public void ParseReferenceNameWithUnnecessaryEnd()
        {
            ReferenceParser parser = new ReferenceParser();
            Check(parser.ParseReferenceName("TestFiles.Parsing.ClassForReferencesTests ")); 
            Check(parser.ParseReferenceName("TestFiles.Parsing.ClassForReferencesTests;")); 
            Check(parser.ParseReferenceName("TestFiles.Parsing.ClassForReferencesTests\"")); 
            Check(parser.ParseReferenceName("TestFiles.Parsing.ClassForReferencesTests&%"));
        }

        [Test]
        public void ParseReferenceNameExpectedIdentifier()
        {
            ReferenceParser parser = new ReferenceParser();
            ParseWithoutIdentifier(parser, "TestFiles.Parsing.ClassForReferencesTests.");
            ParseWithoutIdentifier(parser, "TestFiles.Parsing.ClassForReferencesTests.%%");
            ParseWithoutIdentifier(parser, "TestFiles.Parsing.ClassForReferencesTests.-");
        }

        private void ParseWithoutIdentifier(ReferenceParser parser, string text)
        {
            try
            {
                
                parser.ParseReferenceName(text);
            }
            catch (UnexpectedToken ex)
            {
                ReferenceName referenceName = (ReferenceName) ex.ParsingResult;
                
                CheckWithoutIdentifier(referenceName);
                return;
            }

            Assert.Fail("Expected UnexpectedToken exception");
        }

        private void CheckWithoutIdentifier(ReferenceName referenceName)
        {
            Assert.AreEqual("TestFiles.Parsing.ClassForReferencesTests.", referenceName.GetText());

            AssertChildsList(referenceName,
                             delegate(IElement obj)
                                 {
                                     ReferenceName @ref = (ReferenceName)obj;
                                     Check(@ref);
                                 },
                             delegate(IElement obj)
                                 {
                                     XmlToken token = (XmlToken)obj;
                                     Assert.AreEqual(".", token.GetText());
                                 });
        }


        protected void AssertChildsList(CompositeElement compositeElement, params Action<IElement> [] actions)
        {
            int index = 0;
            for (TreeElement child = compositeElement.firstChild; 
                child != compositeElement.lastChild; child = child.nextSibling, index++)
            {
                if(index >= actions.Length)
                {
                    Assert.Fail("There is child unnecessary with index " + index + " " + child);
                }

                actions[index](child);
            }

            if(index < actions.Length - 1)
                Assert.Fail("There is no element with index  " + index);

        }
    }
}