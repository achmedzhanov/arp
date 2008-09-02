using System;
using System.Collections.Generic;
using Arp.Common.Assertions;
using Arp.Common.Psi.Daemon;
using Arp.Common.Psi.Daemon.References;
using Arp.Common.Psi.Utils;
using Arp.log4net.Psi;
using Arp.log4net.Psi.Tree;
using Arp.log4net.Psi.Tree.Impl;
using Arp.log4net.Psi.Tree.Impl.Validators;
using JetBrains.ReSharper.Daemon;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;
using SyntaxError=JetBrains.ReSharper.Daemon.Impl.SyntaxError;

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

    public class L4NProcessor : BaseProcessor, IHighlightingProcessor
    {
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


        private void Highlight(DocumentRange range, string attribute)
        {
            highlightings.Add(new HighlightingInfo(range, new IdentifierHighlighting(attribute)));
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
                            range = GetHighlightRange(tag.ToTreeNode().Header);
                        else
                            range = element.ToTreeNode().GetDocumentRange();

                        highlightings.Add(new HighlightingInfo(range, new MissedParameterError(descriptor, element)));

                    }
                }

                // highlight invalid values
                foreach (IDeclaredParameter param in declaredParameters)
                {
                    IParameterDescriptor descriptor = ParametersUtil.GetByName(infos, param.Name);
                    if (descriptor == null)
                        continue;

                    IParameterStringValueValidator validator = ValidatorsManager.Instance().GetValidator(descriptor);
                    if (validator == null)
                        continue;

                    // TODO introduce interface marker to determine tag pased parameters
                    PropertyParamImpl propertyParam = param as PropertyParamImpl;
                    IXmlAttributeValue attributeValue = null;
                    if (propertyParam != null)
                    {
                        attributeValue = propertyParam.Value;
                    }
                    else // attribute
                    {
                        IXmlAttribute attribute = ((IXmlTag)declaredParametersOwner).GetAttribute(param.Name);
                        if (attribute != null)
                            attributeValue = attribute.Value;
                    }

                    if (attributeValue == null)
                        continue;

                    string unquotedValue = attributeValue.UnquotedValue;
                    ValidationResult validateResult = validator.Validate(unquotedValue);
                    if (validateResult == ValidationResult.Ok)
                        continue;
                    
                    if(validateResult.Range == TextRange.InvalidRange)
                        continue;

                    int valueStart = attributeValue.ToTreeNode().GetDocumentRange().TextRange.StartOffset + 1;
                    TextRange valueRange = new TextRange(valueStart, valueStart + unquotedValue.Length);
                    int errorStart = valueRange.StartOffset + validateResult.Range.StartOffset;
                    TextRange errorRange = new TextRange(errorStart, errorStart + validateResult.Range.Length);
                    errorRange = valueRange.Intersect(errorRange);

                    if (errorRange == TextRange.InvalidRange)
                        continue;

                    if(errorRange.IsEmpty)
                    {
                        if (attributeValue.UnquotedValue.Length  == 0)
                        {
                            errorRange = new TextRange(errorRange.StartOffset - 1, errorRange.StartOffset + 1);
                        }
                        else
                            continue;
                    }
                    
                    highlightings.Add(new HighlightingInfo( new DocumentRange(element.GetDocumentRange().Document, errorRange),
                        new InvalidValue(validateResult.Message)));
                }
            }
        }

        private DocumentRange GetHighlightRange(IXmlTagHeaderNode header)
        {
            DocumentRange ret = DocumentRange.InvalidRange;
            XmlToken token = header.FirstChild as XmlToken;
            if (token == null)
                return ret;

            XmlTokenTypes types = XmlTokenTypeFactory.GetTokenTypes(L4NLanguageService.L4N);
            Assert.Check(token.type == types.TAG_START);
            ret = token.GetDocumentRange();
            
            token = token.NextSibling as XmlToken;
            if(token == null || token.type != types.IDENTIFIER)
                return ret;

            ret = token.GetDocumentRange();
            return ret;
        }

        public bool ProcessingIsFinished
        {
            get { return false; }
        }
    }
}