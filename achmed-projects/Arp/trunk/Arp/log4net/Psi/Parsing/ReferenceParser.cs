using System;
using Arp.log4net.Psi.Tree;
using Arp.log4net.Psi.Tree.Impl;
using JetBrains.ReSharper.Psi.CSharp.Parsing;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace Arp.log4net.Psi.Parsing
{
    public class ReferenceParser
    {
        private ILexer lexer = null;

        public IXmlAttributeValue Parse(IXmlAttributeValue xmlAttributeValue)
        {
            ReferenceNameAttributeValue attributeValue = new ReferenceNameAttributeValue();

            CompositeElement result = null;

            string rawValue = xmlAttributeValue.UnquotedValue;

            try
            {
                result = ParseTypeNameOrAttributeValue(rawValue);
            }
            catch (SyntaxError syntaxError)
            {
                result = (CompositeElement)syntaxError.ParsingResult;
                result = handleError(result, syntaxError);
            }

            attributeValue.AddChild(new XmlToken(L4NTokenNodeType.QUOTE, new StringBuffer(new string('\"', 1)), 0, 1));
            attributeValue.AddChild(result);
            int resultLegth = result.GetText().Length;
            if(resultLegth < rawValue.Length)
            {
                string suffix = rawValue.Substring(resultLegth);
                StringBuffer sb = new StringBuffer(suffix);
                XmlToken suffixToken = new XmlToken(L4NTokenNodeType.TEXT , sb, 0, suffix.Length);
                attributeValue.AddChild(suffixToken);
            }
            attributeValue.AddChild(new XmlToken(L4NTokenNodeType.QUOTE, new StringBuffer(new string('\"', 1)), 0, 1));

            return attributeValue;

        }

        private CompositeElement handleError(CompositeElement result, SyntaxError syntaxError)
        {
            CompositeElement errorElement = TreeElementFactory.CreateErrorElement(syntaxError.Message);
            if (result == null)
                return errorElement;
            result.AppendNewChild(errorElement);
            return result;  
        }

        protected ReferenceName ParseTypeNameOrAttributeValue(string text )
        {
            lexer = new CSharpLexer(new StringBuffer(text));
            Start();
            TreeElement firstIdentifier = ParseIdentifier();
            TreeElement result = ParseReferencedName(firstIdentifier);
            return (ReferenceName)result;
        }

        private void Start()
        {
            lexer.Start();
//            this.myTokens.Add(new TokenEntry(this.myLexer));
        }

        private TreeElement ParseReferencedName(TreeElement id)
        {
            TreeElement result = id;
            TokenNodeType tokenType = lexer.TokenType;
            XmlToken xmlToken = id as XmlToken;
            if (xmlToken != null && xmlToken.type == L4NTokenNodeType.IDENTIFIER)
            {
                result = new ReferenceName(id);
                // TODO parse argument list
            }

            while (tokenType == CSharpTokenType.DOT)
            {
//                if (LexerUtil.LookaheadToken(lexer, 1) != CSharpTokenType.IDENTIFIER)
//                {
//                    UnexpectedToken ex = new UnexpectedToken("Expected identifier");
//                    ex.ParsingResult = result;
//                    throw ex;
//                }
                result = ParseReferenceNameInternal(result);
                tokenType = lexer.TokenType;
            }
            return result;
        }

        private TreeElement ParseReferenceNameInternal(TreeElement qualifier)
        {
            CompositeElement result = null;
            TreeElement tempParsingResult = null;
            try
            {
                result = TreeElementFactory.CreateCompositeElement(L4NElementType.REFERENCE_NAME);
                tempParsingResult = qualifier;
                result.AppendNewChild(tempParsingResult);
                TokenNodeType tokenType = lexer.TokenType;
                if (tokenType == CSharpTokenType.DOT)
                {
                    tokenType = lexer.TokenType;
                    if (tokenType == CSharpTokenType.DOT)
                    {
                        tempParsingResult =
                            new XmlToken(L4NTokenNodeType.DOT, lexer.Buffer, lexer.TokenStart, lexer.TokenEnd);
                        lexer.Advance();
                        result.AppendNewChild(tempParsingResult);
                    }
                    else
                    {
                        if (result.firstChild == null)
                        {
                            result = null;
                        }
                        throw new UnexpectedToken("Unexpected token");
                    }

                    tempParsingResult = this.ParseIdentifier();
                    result.AppendNewChild(tempParsingResult);
                }
            }
            catch (SyntaxError e)
            {
                if (((qualifier != null) && (qualifier.Parent == null)) && (qualifier != null))
                {
                    result.AppendNewChild(qualifier);
                }
                if ((e.ParsingResult != null) && (result != null))
                {
                    result.AppendNewChild(e.ParsingResult);
                }
                if (result != null)
                {
                    e.ParsingResult = result;
                }
                throw;
            }
            return result;

        }

        private TreeElement ParseIdentifier()
        {
            if (lexer.TokenType != CSharpTokenType.IDENTIFIER)
            {
                throw new UnexpectedToken("Expected identifier");
            }

            TreeElement result = new XmlToken(L4NTokenNodeType.IDENTIFIER, lexer.Buffer, lexer.TokenStart, lexer.TokenEnd);
            lexer.Advance();
            return result;

        }
    }
}