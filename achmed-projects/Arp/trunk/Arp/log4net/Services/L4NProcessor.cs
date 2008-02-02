using System;
using System.Collections.Generic;
using Arp.Assertions;
using Arp.log4net.Psi;
using Arp.log4net.Psi.Tree;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace Arp.log4net.Services
{


//TODO required attributes - static and dynamics aka name, vale, type . We need quick fix 'add attribute|element <name>', highlithihg 'required attribute <name>', autocompletion with attribute <name>, insert requred attribute after autocompletion list closed
//TODO requred element
//TODO element order (requred after, requred before)
//TODO required zero or one, zero or more
//
//TODO interface oriented model (backends: cuctom implementation, xsd based implementation)
//
//
//TODO add to IParamDescriptor properties Description, requred Before, CountRestriction, Required, TextValueRestriction
//TODO introduce interface IParamDescriptorXmlType Element or Attribute
//TODO IEnumerableParamValueDescription
//
//NOTE do we need to use DTD instead XSD ? 
//XSD is better! Try to implement only part

    public class L4NProcessor : IRecursiveElementProcessor
    {
        
        List<HighlightingInfo> highlightings = new List<HighlightingInfo>();

        public bool InteriorShouldBeProcessed(IElement element)
        {
//            IL4NElement l4nElement = element as IL4NElement;
//            if (l4nElement == null)
//                return false;
//
//            return true;

            return true;
        }

        public void ProcessBeforeInterior(IElement element)
        {
            // do nothing
        }


        public HighlightingInfo[] Highlightings
        {
            get { return highlightings.ToArray(); }
        }

        public void ProcessAfterInterior(IElement element)
        {
            ProcessElementParametersOwner(element);
            ProcessBackground(element);
            ProcessIdentifiers(element);

//            TODO  use for paratemetrs values OptionConverter

        }

        private void ProcessBackground(IElement element)
        {
            if(element is IL4NSection)
            {
//                Highlight(element.GetDocumentRange(), L4NHighlightingAttributeIds.XML_BACKGROUND);
//                Highlight(element.GetDocumentRange(), "ReSharper Highlight Target");
            }
        }

        private void ProcessIdentifiers(IElement element)
        {
            IReference[] references = element.GetReferences();
            foreach (IReference reference in references)
            {
                if(!reference.IsValid())
                {
                    // TODO hightlight "can not resolve symbol 'blahblah' "
                    //if( reference.CheckResolveResult() == ResolveErrorType. ...)
                }
                else if(reference.ReferenceType == ReferenceType.TEXT)
                {
                    ResolveResult resolveResult = reference.Resolve();
                    if(resolveResult.DeclaredElement != null)
                        Highlight(reference.GetDocumentRange(), resolveResult.DeclaredElement);
                }
            }

            if (element is IAppenderRef)
            {
                Highlight(((IXmlTag)element).ToTreeNode().Header.Name, HighlightingAttributeIds.FIELD_IDENTIFIER_ATTRIBUTE);
            }

            // TODO remove double code
            if(element is ILogger &&  ((ILogger)element).TagName == L4NConstants.ROOT_LOGGER)
            {
                IXmlTagNode treeNode = ((IXmlTag)element).ToTreeNode();
                // TODO use ILoggerElement to get header range
                Highlight(treeNode.Header.Name, HighlightingAttributeIds.CONSTANT_IDENTIFIER_ATTRIBUTE);                
            }
            else if(element is IAppender || element is ILogger)
            {
                // TODO use IAppenderElement to get header range
                IXmlTagNode treeNode = ((IXmlTag)element).ToTreeNode();
                Highlight(treeNode.Header.Name, HighlightingAttributeIds.TYPE_IDENTIFIER_ATTRIBUTE);
            }

            // TODO tooltips for predefined tags appender, appender-ref, logger, level etc
        }

        private void Highlight(IXmlIdentifierNode node, string attributeId)
        {
            Highlight(node.GetDocumentRange(), attributeId);

            IXmlTagHeaderNode header = node.Parent as IXmlTagHeaderNode;
            if(header != null)
            {
                IXmlTagNode tag = header.Parent as IXmlTagNode;
                Assert.CheckNotNull(tag);
                if(tag.Footer != null)
                    Highlight(tag.Footer.Name.GetDocumentRange(), attributeId);
            }
        }

        private void Highlight(DocumentRange range, IDeclaredElement declaredElement)
        {
            string attribute = GetHighlightAttributeForReference(declaredElement);
            if (attribute != null)
            {
                // TODO highlight closed tag
                // if (treeNode.Footer != null && treeNode.Footer.Name != null)
                highlightings.Add(new HighlightingInfo(range, new L4NIdentifierHighlighting(attribute)));
            }
        }

        private void Highlight(DocumentRange range, string attribute)
        {
            highlightings.Add(new HighlightingInfo(range, new L4NIdentifierHighlighting(attribute)));
        }


        private string GetHighlightAttributeForReference(IDeclaredElement element)
        {
            if (element is IProperty)
                return HighlightingAttributeIds.FIELD_IDENTIFIER_ATTRIBUTE;
//                return HighlightingAttributeIds.CONSTANT_IDENTIFIER_ATTRIBUTE;
            else if(element is IAppender)
            {
                return HighlightingAttributeIds.LOCAL_VARIABLE_IDENTIFIER_ATTRIBUTE;
            }
            else
                return null;
        }


        private void ProcessElementParametersOwner(IElement element)
        {
            IDeclaredParametersOwner declaredParametersOwner = element as IDeclaredParametersOwner;

            // TODO use get reference

            if(declaredParametersOwner != null)
            {
                IParameterDescriptorProvider parameterDescriptorProvider = declaredParametersOwner as IParameterDescriptorProvider;

                if (parameterDescriptorProvider == null)
                    return;

                if(!parameterDescriptorProvider.IsAvailable)
                    return;

                ICollection<IParameterDescriptor> infos = parameterDescriptorProvider.GetParameterDescriptors();

                ICollection<IDeclaredParameter> declaredParameters = declaredParametersOwner.GetParams();

                // Highlight invalid names
                foreach (IDeclaredParameter param in declaredParameters)
                {
                    if(ParametersUtil.GetByName(infos, param.Name) == null)
                    {
                        highlightings.Add(new HighlightingInfo(param.NameDocumentRange, new InvalidPropertyHighlighting()));
                    }
                }

                // Highlight requred attributes
                IXmlTag tag = element as IXmlTag;
                foreach (IParameterDescriptor descriptor in infos)
                {
                    if(descriptor.IsRequired)
                    {
                        if(ParametersUtil.GetByName(declaredParameters, descriptor.Name) != null
                            // it is conxeption hak, TODO fix parameters conception
                            || (tag != null && tag.GetAttribute(descriptor.Name) != null))
                            continue;

                        DocumentRange range;
                        if (tag != null)
                            range = tag.ToTreeNode().Header.GetDocumentRange();
                        else
                            range = element.ToTreeNode().GetDocumentRange();

                        highlightings.Add(new HighlightingInfo(range, new MissedParameterError(descriptor, element)));

                    }
                }
            }
        }

        public bool ProcessingIsFinished
        {
            get { return false; }
        }
    }
}