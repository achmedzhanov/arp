using Arp.log4net.Psi.Parsing;
using Arp.log4net.Psi.Tree;
using Arp.NH.Psi.Tree;
using Arp.NH.Psi.Tree.Impl;
using Arp.NH.Psi.Tree.Parsing;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Resolve;
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
            {
                HibernateMappingElementImpl ret = new HibernateMappingElementImpl();
                HandleCreateTagElement((INHElement)ret, header, null);
                return ret;
            }

            return base.CreateRootTag(header);
        }

        private void HandleCreateTagElement(INHElement element, IXmlTagHeaderNode header, IXmlTagContainer tag)
        {
            foreach (IXmlAttribute attribute in header.Attributes)
            {
                ProcessAttribute(element, attribute, tag);
            }
        }

        private void ProcessAttribute(INHElement element, IXmlAttribute attribute, IXmlTagContainer parent)
        {
            IClassElement classElement = element as IClassElement;
            if (classElement != null)
            {
                if (attribute.XmlName == "name" || attribute.XmlName == "proxy")
                {
                    CreateTypeReferenceAttributeValue(classElement, attribute);
                    return;
                }
                
            }

            IHibernateMappingElement hibernateMappingElement = element as IHibernateMappingElement;
            if (hibernateMappingElement != null)
            {
                if (attribute.XmlName == "assembly")
                {
                    CreateModuleReferenceAttributeValue(hibernateMappingElement, attribute);
                }
                else if (attribute.XmlName == "namespace")
                {
                    CreateNamespaceReferenceAttributeValue(hibernateMappingElement, attribute);
                    return;
                }

                
            }


            if (element is IAnyElement
                 || element is IArrayElement
                 || element is IBagElement
                 || element is IIdbagElement
                 || element is IIdElement
                 || element is IKeyManyToOneElement
                 || element is IKeyPropertyElement
                 || element is IListElement
                 || element is IManyToOneElement
                 || element is IMapElement
                 || element is IOneToOneElement
                 || element is IPrimitiveArrayElement
                 || element is IPropertyElement
                 || element is IResultsetElement
                 || element is IReturnColumnElement
                 || element is IReturnPropertyElement
                 || element is ISetElement
                 || element is ITimestampElement
                 || element is IVersionElement)
            {
                if (attribute.XmlName == "name")
                {
                    CreateMappingMemberReferenceAttributeValue(element, attribute, parent);
                    return;
                }
                
            }
            
             if (element is IComponentElement
              || element is ICompositeElementElement
              || element is ICompositeIdElement
              || element is ICompositeIndexElement
              || element is IImportElement
              || element is IIndexManyToManyElement
              || element is IKeyManyToOneElement
              || element is IManyToManyElement
              || element is IManyToOneElement
              || element is IMetaValueElement
              || element is INestedCompositeElementElement
              || element is IOneToManyElement
              || element is IOneToOneElement
              || element is IReturnElement)
             {
                 if (attribute.XmlName == "class")
                 {
                     CreateTypeReferenceAttributeValue(element, attribute);
                     return;
                 }                 
             }
        }


        private void CreateTypeReferenceAttributeValue(INHElement element, IXmlAttribute attribute)
        {
            if (attribute.Value == null)
                return;

            ReferenceParser parser = new ReferenceParser();
            IXmlAttributeValue newElement = parser.ParseReferenceType(attribute.Value);
            ReplaceAttributeValue(attribute, newElement);
        }
        
        private void CreateModuleReferenceAttributeValue(INHElement element, IXmlAttribute attribute)
        {
            if (attribute.Value == null)
                return;

            ReferenceParser parser = new ReferenceParser();
            IXmlAttributeValue newElement = parser.ParseReferenceModule(attribute.Value);
            ReplaceAttributeValue(attribute, newElement);
        }
        
        private void CreateNamespaceReferenceAttributeValue(INHElement element, IXmlAttribute attribute)
        {
            if (attribute.Value == null)
                return;

            ReferenceParser parser = new ReferenceParser();
            IXmlAttributeValue newElement = parser.ParseReferenceName(attribute.Value);
            ReplaceAttributeValue(attribute, newElement);
        }

        private void CreateMappingMemberReferenceAttributeValue(INHElement element, IXmlAttribute attribute, IXmlTagContainer parent)
        {
            if (attribute.Value == null)
                return;

            ReferenceParser parser = new ReferenceParser();
            IQualifier qualifier = GetElementQualifier(element, parent);
            IXmlAttributeValue newElement = parser.ParseReferenceIdentifier(attribute.Value, qualifier);
            ReplaceAttributeValue(attribute, newElement);
        }

        private IQualifier GetElementQualifier(INHElement element, IXmlTagContainer parent)
        {
            ITreeNode node = (ITreeNode)parent;
            while(node != null)
            {
                if (node is IQualifier)
                    return (IQualifier)node;
                node = node.Parent;
            }

            return null;
        }

        private void ReplaceAttributeValue(IXmlAttribute attribute, IXmlAttributeValue newElement)
        {
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
                    CreateTypeReferenceAttributeValue(element, attribute);
                }

                return;
            }

            IDeclaredParameter declaredParameter = element as IDeclaredParameter;
            if(declaredParameter != null)
            {
                if (attribute.XmlName == L4NConstants.TYPE)
                {
                    CreateTypeReferenceAttributeValue(element, attribute);
                }
                return;
            }

            IAppender appender = element as IAppender;
            if (appender != null)
            {
                if (attribute.XmlName == L4NConstants.TYPE)
                {
                    CreateTypeReferenceAttributeValue(element, attribute);
                }
                return;
            }

        }

        private void CreateTypeReferenceAttributeValue(IL4NElement element, IXmlAttribute attribute)
        {
            if(attribute.Value == null)
                return;
            
            ReferenceParser parser = new ReferenceParser();
            IXmlAttributeValue newElement = parser.ParseReferenceName(attribute.Value);
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