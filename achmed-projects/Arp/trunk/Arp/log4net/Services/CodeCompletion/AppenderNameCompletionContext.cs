using System.Collections.Generic;
using Arp.Assertions;
using Arp.log4net.Psi;
using Arp.log4net.Psi.Tree;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.CodeInsight.Services.CodeCompletion;
using JetBrains.ReSharper.CodeInsight.Services.Lookup;
using JetBrains.ReSharper.CodeInsight.Services.Xml.CodeCompletion;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.ReSharper.TextControl;
using JetBrains.Util;

namespace Arp.log4net.Services.CodeCompletion
{
    public class AppenderNameCompletionContext : XmlCodeCompletionContextBase<IXmlFile>, IL4NCodeCompletionContext
    {
        private ITokenNode token = null;
        private IXmlAttributeValue attributeValue;
        private TextRange prefixRange;

        public AppenderNameCompletionContext(ISolution solution, ITextControl textControl) : base(solution, textControl)
        {
            int offset = CaretOffset;
            token = (ITokenNode)XmlFile.FindTokenAt(offset);
            if ((this.token == null) && (offset > 0))
            {
                this.token = (ITokenNode) XmlFile.FindTokenAt(offset - 1);
            }

            if(token == null)
                return;

            attributeValue = token.GetContainingElement<IXmlAttributeValue>(true);

            if (attributeValue == null)
                return;

            XmlValueToken xmlValueToken = (XmlValueToken)attributeValue;

            Assert.CheckFalse(xmlValueToken.GetDocumentRange().TextRange.IsEmpty);
            Assert.Check(xmlValueToken.GetText().StartsWith("\""));
            int startOffset = xmlValueToken.GetDocumentRange().TextRange.StartOffset + 1;
            
            this.prefixRange = new TextRange(startOffset, CaretOffset);
        }

        public bool IsAvailable()
        {
            if (token == null)
                return false;

            if (attributeValue == null)
                return false;

            Assert.Check(attributeValue.GetDocumentRange().TextRange.Contains(CaretOffset));

            return true;
        }

        public IList<ILookupItem> EvaluateLookupItems()
        {
            IL4NSection l4nSection = token.GetContainingElement<IL4NSection>(false);
            Assert.CheckNotNull(l4nSection);

            ICollection<IAppender> appenders = l4nSection.GetAppenders();

            List<ILookupItem> result = new List<ILookupItem>();
            foreach (IAppender appender in appenders)
            {
                DeclaredElementLookupItem item =
                    new DeclaredElementLookupItem(new DeclaredElementInstance((IDeclaredElement)appender), new DeclaredElementLookupItemCreationContext(ProjectFile), L4NLanguageService.L4N);
                item.InsertRange = new TextRange(0);
                item.ReplaceRange = new TextRange(0, GetPrefix().Length);
                result.Add(item);
            }
            
            return result;
        }


        public string GetPrefix()
        {
            if (!PrefixRange.IsValid)
            {
                return string.Empty;
            }
            return TextControl.Document.GetText(PrefixRange);
        }

        public TextRange PrefixRange
        {
            get
            {
                return prefixRange;
            }
        }
    }
}