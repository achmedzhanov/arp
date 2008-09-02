using System;
using Arp.log4net.Psi;
using JetBrains.ReSharper.Daemon;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.Util;

namespace Arp.Common.Psi.Daemon.ReferenceNameSyntaxErrors
{
    public class ReferenceNameSyntaxErrorProcessor : BaseProcessor, IHighlightingProcessor
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
            ProcessSyntaxError(element);            
        }

        public bool ProcessingIsFinished
        {
            get { return false; }
        }

        #endregion

        private void ProcessSyntaxError(IElement element)
        {
            IErrorElementNode errorNode = element as IErrorElementNode;
            if (errorNode == null)
                return;

            DocumentRange range = errorNode.GetDocumentRange();
            if (!range.IsValid)
                return;

            if (range.TextRange.Length == 0)
            {
                int start = range.TextRange.StartOffset;
                int end = range.TextRange.EndOffset + 1;
                ITreeNode nextNode = errorNode.FindNextNode(delegate(ITreeNode treeNode)
                                                                {
                                                                    if (treeNode is ITokenNode)
                                                                        return TreeNodeActionType.ACCEPT;
                                                                    return TreeNodeActionType.CONTINUE;
                                                                });
                if (nextNode != null)
                {
                    XmlToken xmlToken = nextNode as XmlToken;
                    // TODO abstract QUOTE token
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

            JetBrains.ReSharper.Daemon.Impl.SyntaxError errorHighlight = new JetBrains.ReSharper.Daemon.Impl.SyntaxError(errorNode.ErrorDescription);
            highlightings.Add(new HighlightingInfo(range, errorHighlight));
        }

    }
}