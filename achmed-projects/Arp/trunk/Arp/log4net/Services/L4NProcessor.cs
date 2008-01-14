using System;
using System.Collections.Generic;
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

            if(element is IAppender)
            {
                IAppender appender = (IAppender)element;
                // TODO use IAppenderElement to get header range
                IXmlTagNode treeNode = ((IXmlTag)appender).ToTreeNode();
                Highlight(treeNode.Header.Name.GetDocumentRange(), HighlightingAttributeIds.OPERATOR_IDENTIFIER_ATTRIBUTE);
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
            IElementParametersOwner elementParametersOwner = element as IElementParametersOwner;

            // TODO use get reference

            if(elementParametersOwner != null)
            {
                IParameterDescriptorProvider parameterDescriptorProvider = elementParametersOwner as IParameterDescriptorProvider;

                if (parameterDescriptorProvider == null)
                    return;

                if(!parameterDescriptorProvider.IsAvailable)
                    return;

                ICollection<IParameterDescriptor> infos = parameterDescriptorProvider.GetParameterDescriptors();

                foreach (IParam param in elementParametersOwner.GetParams())
                {
                    IList<IParameterDescriptor> filteredInfos = CollectionUtil.FindAll(infos, delegate(IParameterDescriptor obj)
                                                                                                  {
                                                                                                      return param.Name == obj.Name;
                                                                                                  });

                    if(filteredInfos.Count == 0)
                    {
                        highlightings.Add(new HighlightingInfo(param.NameDocumentRange, new InvalidPropertyHighlighting()));
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