using System;
using System.Collections.Generic;
using System.Text;
using Arp.Common.Psi.Resolve.Filters;
using Arp.log4net.Psi.Tree;
using Arp.log4net.Psi.Tree.Impl;
using JetBrains.ReSharper.Psi.CSharp.Parsing;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve.Filters;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;
using ReferenceType=Arp.log4net.Psi.Tree.Impl.ReferenceType;

namespace Arp.log4net.Psi.Parsing
{
    public class ReferenceParser
    {
        protected ILexer lexer = null;

        public IXmlAttributeValue ParseReferenceName(IXmlAttributeValue xmlAttributeValue)
        {
            ReferenceNameAttributeValue attributeValue = new ReferenceNameAttributeValue();

            return ParseAttributeValueAspect(xmlAttributeValue, attributeValue, text =>  ParseTypeNameOrAttributeValue(text, null));
        }

        public IXmlAttributeValue ParseReferenceType(IXmlAttributeValue xmlAttributeValue, IQualifier qualifier)
        {
            ReferenceTypeAttributeValue attributeValue = new ReferenceTypeAttributeValue();

            return ParseAttributeValueAspect(xmlAttributeValue, attributeValue, text => ParseTypeReference(text, qualifier));
        }

        public IXmlAttributeValue ParseReferenceModule(IXmlAttributeValue xmlAttributeValue)
        {
            ReferenceModuleAttributeValue attributeValue = new ReferenceModuleAttributeValue();

            return ParseAttributeValueAspect(xmlAttributeValue, attributeValue, ParseModule);
        }

        public IXmlAttributeValue ParseReferenceIdentifier(IXmlAttributeValue xmlAttributeValue, IQualifier qualifier)
        {
            ReferenceModuleAttributeValue attributeValue = new ReferenceModuleAttributeValue();

            return ParseAttributeValueAspect(xmlAttributeValue, attributeValue, delegate(string text) {
                                                                                                          return ParseMemberIdentifier
                                                                                                              (text,
                                                                                                               qualifier); });
        }


        protected delegate CompositeElement StringParse(string text);

        protected IXmlAttributeValue ParseAttributeValueAspect(IXmlAttributeValue xmlAttributeValue, CompositeElement newAttributeValue, StringParse stringParse)
        {
            if (xmlAttributeValue == null) throw new ArgumentNullException("xmlAttributeValue");
            if (newAttributeValue == null) throw new ArgumentNullException("newAttributeValue");
            if (stringParse == null) throw new ArgumentNullException("stringParse");
            CompositeElement result = null;

            string rawValue = xmlAttributeValue.UnquotedValue;

            try
            {
                result = stringParse(rawValue);
            }
            catch (SyntaxError syntaxError)
            {
                result = (CompositeElement)syntaxError.ParsingResult;
                result = handleError(result, syntaxError);
            }

            newAttributeValue.AddChild(new XmlToken(L4NTokenNodeType.QUOTE, new StringBuffer(new string('\"', 1)), 0, 1));
            newAttributeValue.AddChild(result);
            int resultLegth = result.GetText().Length;
            if(resultLegth < rawValue.Length)
            {
                string suffix = rawValue.Substring(resultLegth);
                StringBuffer sb = new StringBuffer(suffix);
                XmlToken suffixToken = new XmlToken(L4NTokenNodeType.TEXT , sb, 0, suffix.Length);
                newAttributeValue.AddChild(suffixToken);
            }
            newAttributeValue.AddChild(new XmlToken(L4NTokenNodeType.QUOTE, new StringBuffer(new string('\"', 1)), 0, 1));

            return (IXmlAttributeValue)newAttributeValue;
        }

        public ReferenceName ParseReferenceName(string text, IQualifier qualifier)
        {
            return ParseTypeNameOrAttributeValue(text, qualifier);
        }


        public ReferenceType ParseTypeReference(string text, IQualifier qualifier)
        {
            // it's stub
            try
            {
                ReferenceName referenceName = ParseReferenceName(text, qualifier);
                ReferenceType referenceType = new ReferenceType();
                referenceType.AddChild(referenceName);
                return referenceType;
            }
            catch (UnexpectedToken ex)
            {
                ReferenceName referenceName = ex.ParsingResult as ReferenceName;
                if(referenceName != null)
                {
                    ReferenceType referenceType = new ReferenceType();
                    referenceType.AddChild(referenceName);
                    UnexpectedToken newEx = new UnexpectedToken("Unexpected token");
                    newEx.ParsingResult = referenceType;
                    throw newEx;
                }

                throw;
            }
        }

        public ReferenceModule ParseModule(string text)
        {
            lexer = new CSharpLexer(new StringBuffer(text));
            Start();
            
            XmlToken token = null;

            try
            {
                token = ParseModuleName();
            }
            catch (UnexpectedToken ex)
            {
                token = ex.ParsingResult as XmlToken;
                if(token != null)
                {
                    ReferenceModule @ref = CreateReferenceModule(token);
                    UnexpectedToken th = new UnexpectedToken("Unexpected token");
                    th.ParsingResult = @ref;
                    throw th;
                }

                throw;
            }
            
            return CreateReferenceModule(token);
        }

        public ReferenceName ParseMemberIdentifier(string text, IQualifier qualifier)
        {
            lexer = new CSharpLexer(new StringBuffer(text));
            Start();
            TreeElement firstIdentifier = ParseIdentifier();
            ReferenceName referenceName = CreateMemeberIdentifier(firstIdentifier, qualifier);
            if(lexer.TokenType != null)
            {
                UnexpectedToken ex = new UnexpectedToken("Unexpected token");
                ex.ParsingResult = referenceName;
                throw ex;
            }

            return referenceName;
        }

        private XmlToken ParseModuleName()
        {
            StringBuilder tokenTexts = new StringBuilder();

            int start = lexer.TokenStart;
            int end = lexer.TokenStart;

            bool expectedIdentifier = true;

            while (lexer.TokenType != null)
            {
                TokenNodeType tokenType = lexer.TokenType;

                if(expectedIdentifier)
                {
                    if (IsIdentifier(tokenType))
                    {
                        tokenTexts.Append(lexer.TokenText);
                        expectedIdentifier = false;
                        end = lexer.TokenEnd;
                    }
                    else
                    {
                        CreateModuleNameToken(start, end, true);
                    }
                }
                else
                {
                    if (tokenType == CSharpTokenType.DOT)
                    {
                        tokenTexts.Append(lexer.TokenText);
                        expectedIdentifier = true;
                        end = lexer.TokenEnd;

                        if (!IsIdentifier(LexerUtil.LookaheadToken(lexer, 1)))
                            return CreateModuleNameToken(start, end, true);

                    }
                    else
                    {
                        return CreateModuleNameToken(start, end, false);
                    }                    
                }

                lexer.Advance();
            }

            return CreateModuleNameToken(start, end, false);
        }

        private bool IsIdentifier(TokenNodeType tokenType)
        {
            return tokenType == CSharpTokenType.IDENTIFIER || CSharpTokenType.KEYWORDS.Exists(type => type == tokenType);
        }

        private XmlToken CreateModuleNameToken(int start, int end, bool unexpectedToken)
        {
            if(start == 0 && end == 0)
                throw new UnexpectedToken("Unexpected token");
            
            XmlToken ret = new XmlToken(L4NTokenNodeType.IDENTIFIER, lexer.Buffer, start, end);
            if(unexpectedToken)
            {
                UnexpectedToken ex = new UnexpectedToken("Unexpected token");
                ex.ParsingResult = ret;
                throw ex;
            }

            return ret;
        }

        private CompositeElement handleError(CompositeElement result, SyntaxError syntaxError)
        {
            CompositeElement errorElement = TreeElementFactory.CreateErrorElement(syntaxError.Message);
            if (result == null)
                return errorElement;
            result.AppendNewChild(errorElement);
            return result;  
        }

        protected ReferenceName ParseTypeNameOrAttributeValue(string text, IQualifier qualifier)
        {
            lexer = new CSharpLexer(new StringBuffer(text));
            Start();
            TreeElement firstIdentifier = ParseIdentifier();
            TreeElement result = ParseReferencedName(firstIdentifier, qualifier);
            return (ReferenceName)result;
        }

        protected void Start()
        {
            lexer.Start();
//            this.myTokens.Add(new TokenEntry(this.myLexer));
        }

        private TreeElement ParseReferencedName(TreeElement id, IQualifier qualifier)
        {
            TreeElement result = id;
            TokenNodeType tokenType = lexer.TokenType;
            XmlToken xmlToken = id as XmlToken;
            if (xmlToken != null && xmlToken.type == L4NTokenNodeType.IDENTIFIER)
            {
                result = new ReferenceName(id, qualifier);
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
                result = CreateReferenceName();
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
        
        protected TreeElement ParseIdentifier()
        {
            if (!IsIdentifier(lexer.TokenType))
            {
                throw new UnexpectedToken("Expected identifier");
            }

            TreeElement result = new XmlToken(L4NTokenNodeType.IDENTIFIER, lexer.Buffer, lexer.TokenStart, lexer.TokenEnd);
            lexer.Advance();
            return result;

        }
        

        
        protected virtual ReferenceName CreateReferenceName()
        {
            return new FilteredReferenceName(new FiltersArray( new ISymbolFilter[] {TypeOrNamespaceFilter.INSTANCE}));
        }

        protected virtual ReferenceModule CreateReferenceModule(XmlToken token)
        {
            ReferenceModule referenceModule = new ReferenceModule();
            referenceModule.AppendNewChild(token);
            return referenceModule;
        }

        private ReferenceName CreateMemeberIdentifier(TreeElement firstIdentifier, IQualifier qualifier)
        {
            return new FilteredReferenceName(firstIdentifier, qualifier, new FiltersArray(new ISymbolFilter[] { PropertyOrFiledFilter.INSTANCE, PublicOrProtectedFilter.INSTANCE }));
        }


    }
}