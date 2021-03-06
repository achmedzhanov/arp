using System;
using System.Collections.Generic;
using Arp.Assertions;
using Arp.log4net.Psi;
using Arp.log4net.Psi.Tree;
using Arp.log4net.Psi.Tree.Impl;
using Arp.log4net.Psi.Tree.Impl.Validators;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Editor;
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

    public class L4NProcessor : IRecursiveElementProcessor
    {
        readonly List<HighlightingInfo> highlightings = new List<HighlightingInfo>();

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
            ProcessSyntaxError(element);
            ProcessElementParametersOwner(element);
            ProcessBackground(element);
            ProcessIdentifiers(element);

//            TODO  use for paratemetrs values OptionConverter

        }

        private void ProcessSyntaxError(IElement element)
        {
            IErrorElementNode errorNode = element as IErrorElementNode;
            if(errorNode == null)
                return;

            DocumentRange range = errorNode.GetDocumentRange();
            if(!range.IsValid)
                return;

            if(range.TextRange.Length == 0)
            {
                int start = range.TextRange.StartOffset;
                int end = range.TextRange.EndOffset + 1;
                ITreeNode nextNode = errorNode.FindNextNode(delegate(ITreeNode treeNode)
                                                                {
                                                                    if(treeNode is ITokenNode)
                                                                        return TreeNodeActionType.ACCEPT;
                                                                    return TreeNodeActionType.CONTINUE;
                                                                });
                if(nextNode != null)
                {
                    XmlToken xmlToken = nextNode as XmlToken;
                    if (xmlToken != null && xmlToken.type == L4NTokenNodeType.QUOTE)
                        start--;
                }
                else
                {
                    start--;
                    end--;
                }

                int documentLength = range.Document.GetTextLength();

                start = Math.Max(0, start);
                start = Math.Min(documentLength, start);
                end = Math.Max(0, end);
                end = Math.Min(documentLength, end);
                range = new DocumentRange(range.Document, new TextRange(start, end));

            }

            SyntaxError errorHighlight = new JetBrains.ReSharper.Daemon.Impl.SyntaxError(errorNode.ErrorDescription);
            highlightings.Add(new HighlightingInfo(range, errorHighlight));
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
                CheckForResolveProblems(reference);
                
                if(!reference.IsValid())
                {
                    //ResolveErrorType result = reference.CheckResolveResult();
                    // TODO hightlight "can not resolve symbol 'blahblah' "
                    //if( reference.CheckResolveResult() == ResolveErrorType. ...)
                }
                else if(reference.ReferenceType == ReferenceType.TEXT
                    || reference.ReferenceType == ReferenceType.REFERENCE_NAME)
                {
                    ResolveResult resolveResult = reference.Resolve();
                    if(resolveResult.DeclaredElement != null)
                    {
                        DocumentRange range = reference.GetDocumentRange();
                        DocumentRange footerRange = GetFooterTagRange(element, range);
                        Highlight(range, resolveResult.DeclaredElement);
                        if (footerRange != DocumentRange.InvalidRange)
                            Highlight(footerRange, resolveResult.DeclaredElement);
                    }
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

        private void CheckForResolveProblems(IReference reference)
        {
            IQualifiableReference qualifiableReference = reference as IQualifiableReference;
            if (((qualifiableReference == null) || !qualifiableReference.IsQualified) || qualifiableReference.GetQualifier().Resolved)
            {
                ResolveErrorType result = reference.CheckResolveResult();
                if (result != ResolveErrorType.OK)
                {
                    NotResolvedError notResolvedError = new NotResolvedError(reference);
                    highlightings.Add(new HighlightingInfo(notResolvedError.Range, notResolvedError));
                }
            }
        }

        private DocumentRange GetFooterTagRange(IElement element, DocumentRange range)
        {
            IXmlTag tag = element as IXmlTag;
            if (tag == null)
                return DocumentRange.InvalidRange;
            if(tag.ToTreeNode().Header.Name.GetDocumentRange().TextRange == range.TextRange)
            {
                IXmlTagFooterNode footer = tag.ToTreeNode().Footer;
                if (footer != null)
                    return footer.Name.GetDocumentRange();
            }

            return DocumentRange.InvalidRange;
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
                return HighlightingAttributeIds.FIELD_IDENTIFIER_ATTRIBUTE;
            }
            else if(element is ITypeElement)
            {
                return HighlightingAttributeIds.TYPE_IDENTIFIER_ATTRIBUTE;
            }
            else if (element is INamespace)
            {
                return HighlightingAttributeIds.NAMESPACE_IDENTIFIER_ATTRIBUTE;
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