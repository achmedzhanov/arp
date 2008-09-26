using Arp.log4net.Psi.Parsing;
using Arp.log4net.Psi.Tree.Impl;
using Arp.NH.Psi.Tree.Impl;
using JetBrains.ReSharper.Psi.CSharp.Parsing;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace Arp.NH.Psi.Tree.Parsing
{
    public class NHReferenceParser : ReferenceParser
    {
        public IXmlAttributeValue ParseReferenceTable(IXmlAttributeValue xmlAttributeValue)
        {
            ReferenceTableAttributeValue attributeValue = new ReferenceTableAttributeValue();

            return ParseAttributeValueAspect(xmlAttributeValue, attributeValue, ParseTable);
        }

        public ReferenceTable ParseTable(string text)
        {
            lexer = new CSharpLexer(new StringBuffer(text));
            Start();

            XmlToken token = null;

            try
            {
                token = ParseTableName();
            }
            catch (UnexpectedToken ex)
            {
                token = ex.ParsingResult as XmlToken;
                if (token != null)
                {
                    var @ref = CreateReferenceTable(token);
                    var th = new UnexpectedToken("Unexpected token");
                    th.ParsingResult = @ref;
                    throw th;
                }

                throw;
            }

            return CreateReferenceTable(token);
        }

        private ReferenceTable CreateReferenceTable(XmlToken token)
        {
            ReferenceTable referenceTable = new ReferenceTable();
            referenceTable.AppendNewChild(token);
            return referenceTable;
        }

        private XmlToken ParseTableName()
        {
            return (XmlToken) ParseIdentifier();
        }
    }
}