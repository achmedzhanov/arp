using System.Collections.Generic;
using Arp.Assertions;
using Arp.log4net.Psi.Tree;
using Arp.log4net.Services.CodeCompletion.Rules;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.CodeInsight.Services.Lookup;
using JetBrains.ReSharper.CodeInsight.Services.Xml.CodeCompletion;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.ReSharper.TextControl;
using JetBrains.Util;

namespace Arp.log4net.Services.CodeCompletion
{
    public class CodeCompletionContext : XmlCodeCompletionContextBase<IXmlFile>, IL4NCodeCompletionContext
    {
        private ITokenNode token;
        private TextRange prefixRange;
        private bool isAvalilableTagCompletion = false;
        private bool isAvalilableAttributeValueCompletion = false;
        private bool isAvalilableAttributeNameCompletion = false;
        private readonly XmlTokenTypes xmlTokenTypes;
        private readonly List<ICodeCompletionRule> rules = new List<ICodeCompletionRule>();


        public CodeCompletionContext(ISolution solution, ITextControl textControl)
            : base(solution, textControl)
        {
            IXmlFile file = this.XmlFile;
            Logger.Assert(file != null, "file != null");
            int offset = this.CaretOffset;
            token = (ITokenNode)file.FindTokenAt(offset);

            xmlTokenTypes = XmlTokenTypeFactory.GetTokenTypes(XmlFile.Language);

            InitilizeRules();

            Initilize();
        }

        private void InitilizeRules()
        {
            rules.Add(new AppenderNameRule());
            rules.Add(new TagBasedParameterNameRule());
            rules.Add(new ParameterAttributeNameRule());
            rules.Add(new AppenderTypeNameRule());
            rules.Add(new ParameterAttributeEnumerableValuesRule());
            rules.Add(new DeclaredParameterAttributesRule());
            rules.Add(new DeclaredParameterTypeNameRule());
            //rules.Add(new LoggerNameRule());
//            rules.Add(new TagBasedParameterNameRule());
        }

        private void Initilize()
        {
            ITokenNode currentToken = token;

            if (!(isAvalilableTagCompletion = CheckTagNameCompletion(currentToken)))
                if (!(isAvalilableAttributeValueCompletion = CheckAttributeValueCompletion(currentToken)))
                    isAvalilableAttributeNameCompletion = CheckAttributeNameCompletion(currentToken);
        }


        public ITokenNode Token
        {
            get { return token; }
        }

        public bool IsAvalilableTagCompletion
        {
            get { return isAvalilableTagCompletion; }
        }

        public bool IsAvalilableAttributeValueCompletion
        {
            get { return isAvalilableAttributeValueCompletion; }
        }

        public bool IsAvalilableAttributeNameCompletion
        {
            get { return isAvalilableAttributeNameCompletion; }
        }

        private bool CheckAttributeNameCompletion(ITokenNode currentToken)
        {
            if (token == null || !token.Parent.IsValid())
                return false;

            if (xmlTokenTypes.SPACE == currentToken.GetTokenType() && 
                (currentToken.Parent.ToTreeNode() is IXmlTagHeaderNode ||
                currentToken.Parent.ToTreeNode() is IXmlTagNode))
            {
                DocumentRange spaceRange = currentToken.GetDocumentRange();

                if (!(CaretOffset > spaceRange.TextRange.StartOffset &&
                      CaretOffset < spaceRange.TextRange.EndOffset))
                    return false;

                prefixRange = new TextRange(CaretOffset);
                return true;
            }

            if (xmlTokenTypes.IDENTIFIER != currentToken.GetTokenType())
                return false;

            if (!(token.Parent is XmlAttribute))
                return false;

            TextRange identifierRange = currentToken.GetNextToken().GetDocumentRange().TextRange;
            prefixRange = new TextRange(identifierRange.StartOffset,identifierRange.EndOffset);

            return true;
        }

        private bool CheckAttributeValueCompletion(ITokenNode token)
        {
            if (token == null || !token.Parent.IsValid())
            {
                return false;
            }

            int offset = CaretOffset;
            token = (ITokenNode)XmlFile.FindTokenAt(offset);
            if ((this.token == null) && (offset > 0))
            {
                this.token = (ITokenNode)XmlFile.FindTokenAt(offset - 1);
            }

            if (token == null)
                return false;

            IXmlAttributeValue attributeValue = token.GetContainingElement<IXmlAttributeValue>(true);

            if (attributeValue == null)
                return false;

            XmlValueToken xmlValueToken = (XmlValueToken)attributeValue;

            Assert.CheckFalse(xmlValueToken.GetDocumentRange().TextRange.IsEmpty);
            Assert.Check(xmlValueToken.GetText().StartsWith("\""));
            int startOffset = xmlValueToken.GetDocumentRange().TextRange.StartOffset + 1;

            this.prefixRange = new TextRange(startOffset, CaretOffset);

            return true;
        }

        private bool CheckTagNameCompletion(ITokenNode currentToken)
        {

            if (token == null || !token.Parent.IsValid())
            {
                return false;
            }

            if (xmlTokenTypes.SPACE == currentToken.GetTokenType())
            {
                if (currentToken.GetDocumentRange().TextRange.StartOffset == this.TextControl.CaretModel.Offset)
                    currentToken = currentToken.GetPrevToken();
                else
                {
                    return false;
                }
            }

            if (xmlTokenTypes.IDENTIFIER == currentToken.GetTokenType())
            {
                currentToken = currentToken.GetPrevToken();
            }

            if (xmlTokenTypes.TAG_START == currentToken.GetTokenType())
            {
                prefixRange = new TextRange(currentToken.GetNextToken().GetDocumentRange().TextRange.StartOffset, TextControl.CaretModel.Offset);
            }
            else
            {
                return false;
            }
            return true;
        }

        public TextRange PrefixRange
        {
            get { return prefixRange; }
        }

        public bool IsAvailable()
        {
            return true;
        }

        #region IL4NCodeCompletionContext Members

        public IList<ILookupItem> EvaluateLookupItems()
        {
            List<ILookupItem> result = new List<ILookupItem>();

            foreach (ICodeCompletionRule rule in rules)
            {
                if(rule.IsApplicable(this))
                    rule.Apply(this, result);
            }

            return result;
        }

        #endregion

        public XmlTokenTypes GetTokenTypes()
        {
            return XmlTokenTypeFactory.GetTokenTypes(XmlFile.Language);
        }

        public string GetPrefix()
        {
            if (!PrefixRange.IsValid)
            {
                return string.Empty;
            }
            return TextControl.Document.GetText(PrefixRange);
            
        }
            
    }
}