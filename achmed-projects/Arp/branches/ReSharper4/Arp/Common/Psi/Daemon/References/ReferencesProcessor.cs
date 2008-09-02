using Arp.Common.Psi.Daemon.References;
using Arp.Common.Psi.Utils;
using JetBrains.ReSharper.Daemon;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Tree;

namespace Arp.Common.Psi.Daemon.References
{
    public class ReferencesProcessor : BaseProcessor, IHighlightingProcessor
    {
        #region IRecursiveElementProcessor Members

        public bool InteriorShouldBeProcessed(IElement element)
        {
            return true;
        }

        public void ProcessBeforeInterior(IElement element)
        {
            // do nothing
        }

        public void ProcessAfterInterior(IElement element)
        {
            ProcessReferences(element);
        }

        public bool ProcessingIsFinished
        {
            get { return false; }
        }

        #endregion

        protected void ProcessReferences(IElement element)
        {
            IReference[] references = element.GetReferences();
            foreach (IReference reference in references)
            {
                CheckForResolveProblems(reference);

                if (!reference.IsValid())
                {
                    //ResolveErrorType result = reference.CheckResolveResult();
                    // TODO hightlight "can not resolve symbol 'blahblah' "
                    //if( reference.CheckResolveResult() == ResolveErrorType. ...)
                }
                else
                {
                    HighlightReference(element, reference);                
                }

            }
        }

        protected virtual void HighlightReference(IElement element, IReference reference)
        {
            if (reference.ReferenceType == ReferenceType.TEXT
                || reference.ReferenceType == ReferenceType.REFERENCE_NAME
                || reference.ReferenceType == ReferenceType.LABEL)
            {
                ResolveResult resolveResult = reference.Resolve();
                
                
//                TODO resolveResult.DeclaredElement == null property@name !!!

                if (resolveResult.DeclaredElement != null)
                {
                    DocumentRange range = reference.GetDocumentRange();
                    DocumentRange footerRange = XMLPSIUtils.GetFooterTagRange(element, range);
                    Highlight(range, resolveResult.DeclaredElement);
                    if (footerRange != DocumentRange.InvalidRange)
                        Highlight(footerRange, resolveResult.DeclaredElement);
                }
            }
        }

        protected virtual void CheckForResolveProblems(IReference reference)
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

        private void Highlight(DocumentRange range, IDeclaredElement declaredElement)
        {
            string attribute = GetHighlightAttributeForReference(declaredElement);
            if (attribute != null)
            {
                // TODO highlight closed tag
                highlightings.Add(new HighlightingInfo(range, new IdentifierHighlighting(attribute)));
            }
        }

        protected virtual string GetHighlightAttributeForReference(IDeclaredElement element)
        {
            if (element is IProperty)
                return HighlightingAttributeIds.FIELD_IDENTIFIER_ATTRIBUTE;
            else if (element is ITypeElement)
            {
                return HighlightingAttributeIds.TYPE_IDENTIFIER_ATTRIBUTE;
            }
            else if (element is INamespace)
            {
                return HighlightingAttributeIds.NAMESPACE_IDENTIFIER_ATTRIBUTE;
            }
            else if (element is IProperty || element is IField)
            {
                return HighlightingAttributeIds.FIELD_IDENTIFIER_ATTRIBUTE;
            }
            else if (element is IMethod)
            {
                return HighlightingAttributeIds.METHOD_IDENTIFIER_ATTRIBUTE;
            }
            else
                return null;
        }

    }
}