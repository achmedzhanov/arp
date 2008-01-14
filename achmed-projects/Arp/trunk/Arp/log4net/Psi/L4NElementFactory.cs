using Arp.log4net.Psi.Tree;
using Arp.log4net.Psi.Tree.Impl;
using JetBrains.ReSharper.CodeInsight.Services.ParameterInfo;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Xml.Parsing;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace Arp.log4net.Psi
{
    public class L4NElementFactory : XmlElementFactory
    {

        public static L4NElementFactory instance = new L4NElementFactory(L4NLanguageService.L4N);


        public static L4NElementFactory Instance
        {
            get { return instance; }
        }

        public L4NElementFactory(PsiLanguageType languageType) : base(languageType)
        {

        }

        public override IXmlTag CreateTag(IXmlTagHeaderNode header, IXmlTagContainer parentTag)
        {
            string parentTagName = ((IXmlTag)parentTag).TagName;

            string name = header.Name.GetText();


            if (name == L4NConstants.LOG4NET)
            {
                return new L4NSectionImpl();
            }
            else if (parentTagName == L4NConstants.LOG4NET)
            {
                if (name == L4NConstants.APPENDER)
                {
                    return new AppenderImpl();
                }
                else if (name == L4NConstants.LOGGER || name == L4NConstants.ROOT_LOGGER)
                {
                    return new LoggerImpl();
                }
            }
            else if (parentTagName == L4NConstants.LOGGER || parentTagName == L4NConstants.ROOT_LOGGER)
            {
                if (name == L4NConstants.APPENDER_REF)
                {
                    return new AppenderRefImpl();
                }            
                // TODO other properties
                //else
            }
            else if (parentTag is IElementParametersOwner)
            {
                if (name != L4NConstants.PARAM)
                {
                    return new ParamImpl();
                }                            
            }


            return base.CreateTag(header, parentTag);
        }

        public override IXmlTag CreateRootTag(IXmlTagHeaderNode header)
        {
            return base.CreateRootTag(header);
        }

        public override IXmlAttribute CreateAttribute(IXmlIdentifierNode nameIdentifier,
                                                      IXmlAttributeContainer attributeContainer,
                                                      IXmlTagContainer parentTag)
        {
            return base.CreateAttribute(nameIdentifier, attributeContainer, parentTag);
        }

        public override IXmlAttributeValue CreateAttributeValue(NodeType type, IBuffer buffer, int startOffset,
                                                                int endOffset)
        {
            return base.CreateAttributeValue(type, buffer, startOffset, endOffset);
        }
    }
}