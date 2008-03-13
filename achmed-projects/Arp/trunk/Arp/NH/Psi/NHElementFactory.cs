using Arp.log4net.Psi.Parsing;
using Arp.log4net.Psi.Tree;
using Arp.NH.Psi.Tree;
using Arp.NH.Psi.Tree.Impl;
using Arp.NH.Psi.Tree.Parsing;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Parsing;
using JetBrains.ReSharper.Psi.Xml.Tree;

namespace Arp.NH.Psi
{
    public class NHElementFactory : NHElementsFactory
    {

        public static NHElementFactory instance = new NHElementFactory(NHLanguageService.NH);


        public static NHElementFactory Instance
        {
            get { return instance; }
        }


        public NHElementFactory(PsiLanguageType languageType) : base(languageType)
        {
        }


        public override IXmlTag CreateTag(IXmlTagHeaderNode header, IXmlTagContainer parentTag)
        {
            IXmlTag created = base.CreateTagGenerated(header, parentTag);
            if (created != null)
            {
                HandleCreateTagElement((INHElement)created,header, parentTag);
                return created;
            }

            return base.CreateTag(header, parentTag);
        }


        public override IXmlTag CreateRootTag(IXmlTagHeaderNode header)
        {
            if (header.Name.GetText() == "hibernate-mapping")
                return new HibernateMappingElementImpl();
            
            return base.CreateRootTag(header);
        }

        private void HandleCreateTagElement(INHElement element, IXmlTagHeaderNode header, IXmlTagContainer tag)
        {
            foreach (IXmlAttribute attribute in header.Attributes)
            {
                ProcessAttribute(element, attribute);
            }

        }

        private void ProcessAttribute(INHElement element, IXmlAttribute attribute)
        {
            IClassElement classElement = element as IClassElement;
            if (classElement != null)
            {
                if (attribute.XmlName == "name")
                {
                    CreateReferenceAttributeValue(classElement, attribute);
                }
            }
        }


        private void CreateReferenceAttributeValue(INHElement element, IXmlAttribute attribute)
        {
            if (attribute.Value == null)
                return;

            ReferenceParser parser = new ReferenceParser();
            IXmlAttributeValue newElement = parser.Parse(attribute.Value);
            IXmlAttributeValueNode childNode = attribute.Value.ToTreeNode();
            ITreeNode parent = attribute.ToTreeNode();
            ((CompositeElement)parent).AddChildAfter(newElement.ToTreeNode(), childNode);
            ((CompositeElement)parent).DeleteChildRange(childNode, childNode);
        }



        /*

        public override IXmlTag CreateTag(IXmlTagHeaderNode header, IXmlTagContainer parentTag)
        {
            IXmlTag tag = CreateTagInternal(parentTag, header);
            IL4NElement element = tag as IL4NElement;
            if (element != null)
                HandleCreateTagElement(element, header, parentTag);
            return tag;
        }

        private void HandleCreateTagElement(IL4NElement element, IXmlTagHeaderNode header, IXmlTagContainer tag)
        {
            foreach (IXmlAttribute attribute in header.Attributes)
            {
                ProcessAttribute(element, attribute);
            }
            
        }

        private void ProcessAttribute(IL4NElement element, IXmlAttribute attribute)
        {
            ILogger logger = element as ILogger;

            if (logger != null)
            {
                if (attribute.XmlName == L4NConstants.NAME)
                {
                    CreateReferenceAttributeValue(element, attribute);
                }

                return;
            }

            IDeclaredParameter declaredParameter = element as IDeclaredParameter;
            if(declaredParameter != null)
            {
                if (attribute.XmlName == L4NConstants.TYPE)
                {
                    CreateReferenceAttributeValue(element, attribute);
                }
                return;
            }

            IAppender appender = element as IAppender;
            if (appender != null)
            {
                if (attribute.XmlName == L4NConstants.TYPE)
                {
                    CreateReferenceAttributeValue(element, attribute);
                }
                return;
            }

        }

        private void CreateReferenceAttributeValue(IL4NElement element, IXmlAttribute attribute)
        {
            if(attribute.Value == null)
                return;
            
            ReferenceParser parser = new ReferenceParser();
            IXmlAttributeValue newElement = parser.Parse(attribute.Value);
            IXmlAttributeValueNode childNode = attribute.Value.ToTreeNode();
            ITreeNode parent = attribute.ToTreeNode();
            ((CompositeElement)parent).AddChildAfter(newElement.ToTreeNode(), childNode);
            ((CompositeElement)parent).DeleteChildRange(childNode, childNode);
        }


        private IXmlTag CreateTagInternal(IXmlTagContainer parentTag, IXmlTagHeaderNode header)
        {
            string parentTagName = ((IXmlTag)parentTag).TagName;

            string name = header.Name.GetText();

            if (name == L4NConstants.LOG4NET)
            {
                return new L4NSectionImpl();
            }
            else if (parentTag is IL4NSection)
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
            else if (parentTag is ILogger)
            {
                if (name == L4NConstants.APPENDER_REF)
                {
                    return new AppenderRefImpl();
                }            
                // TODO other properties
                //else
            }
            else if (parentTag is IDeclaredParametersOwner)
            {
                if (name != L4NConstants.PARAM)
                {
                    return new PropertyParamImpl();
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
            IXmlAttribute baseRet = base.CreateAttribute(nameIdentifier, attributeContainer, parentTag);

            return baseRet;
        }

        public override IXmlAttributeValue CreateAttributeValue(NodeType type, IBuffer buffer, int startOffset,
                                                                int endOffset)
        {
            return base.CreateAttributeValue(type, buffer, startOffset, endOffset);
        }
         * */
    }
}