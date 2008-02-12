using System;
using System.Collections.Generic;
using Arp.Assertions;
using Arp.log4net.Psi;
using Arp.log4net.Services.CodeCompletion.Rules;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.CodeInsight.Services.Lookup;
using JetBrains.ReSharper.CodeInsight.Services.Xml.CodeCompletion;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Resolve;
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
        private const string REFERENCE_SUFFIX = "__";

        public CodeCompletionContext(ISolution solution, ITextControl textControl)
            : base(solution, textControl)
        {
            IXmlFile file = XmlFile;
            Logger.Assert(file != null, "file != null");
            int offset = CaretOffset;
            token = (ITokenNode) file.FindTokenAt(offset);

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
            rules.Add(new LoggerNameRule());
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

        public IXmlTag GetAttributeTag()
        {
            if (!IsAvalilableAttributeNameCompletion && !IsAvalilableAttributeValueCompletion)
                throw new InvalidOperationException();

            IXmlTag tag = Token.PrevSibling as IXmlTag;
            if (tag == null)
                tag = Token.GetContainingElement<IXmlTag>(false);
            return tag;
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

                if(CaretOffset == spaceRange.TextRange.StartOffset)
                {
                    ITokenNode prevToken = currentToken.GetPrevToken();
                    XmlToken xmlToken = prevToken as XmlToken;
                    if(xmlToken.type != xmlTokenTypes.IDENTIFIER)
                        return false;

                    TextRange attrIdentifierRange = xmlToken.GetDocumentRange().TextRange;
                    prefixRange = new TextRange(attrIdentifierRange.StartOffset, attrIdentifierRange.EndOffset);                    

                }
                else
                {
                    if (!(CaretOffset > spaceRange.TextRange.StartOffset &&
                          CaretOffset < spaceRange.TextRange.EndOffset))
                        return false;

                    prefixRange = new TextRange(CaretOffset);                    
                }

                return true;
            }

            if (xmlTokenTypes.IDENTIFIER != currentToken.GetTokenType())
                return false;

            if (!(token.Parent is XmlAttribute))
                return false;

            TextRange identifierRange = currentToken.GetNextToken().GetDocumentRange().TextRange;
            prefixRange = new TextRange(identifierRange.StartOffset, identifierRange.EndOffset);

            return true;
        }

        private bool CheckAttributeValueCompletion(ITokenNode token)
        {
            if (token == null || !token.Parent.IsValid())
            {
                return false;
            }

            int offset = CaretOffset;
            token = (ITokenNode) XmlFile.FindTokenAt(offset);
            if ((this.token == null) && (offset > 0))
            {
                this.token = (ITokenNode) XmlFile.FindTokenAt(offset - 1);
            }

            if (token == null)
                return false;

            IXmlAttributeValue attributeValue = token.GetContainingElement<IXmlAttributeValue>(true);

            if (attributeValue == null)
                return false;


            Assert.CheckFalse(attributeValue.GetDocumentRange().TextRange.IsEmpty);
            Assert.Check(attributeValue.GetText().StartsWith("\""));
            int startOffset = attributeValue.GetDocumentRange().TextRange.StartOffset + 1;

            prefixRange = new TextRange(startOffset, CaretOffset);

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
                if (currentToken.GetDocumentRange().TextRange.StartOffset == TextControl.CaretModel.Offset)
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
                prefixRange =
                    new TextRange(currentToken.GetNextToken().GetDocumentRange().TextRange.StartOffset,
                                  TextControl.CaretModel.Offset);
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

            PreEvaluteItems(result);

            foreach (ICodeCompletionRule rule in rules)
            {
                if (rule.IsApplicable(this))
                    rule.Apply(this, result);
            }

            return result;
        }

        #endregion

        private ICompleteableReference GetComplatebleReference()
        {
//            ICompleteableReference reference = FindCompleteableReference(XmlFile.FindReferencesAt(PrefixRange));
            ICompleteableReference reference = FindCompleteableReference(XmlFile.FindReferencesAt(new TextRange(TextControl.CaretModel.Offset)));
            if (reference != null && reference.GetDocumentRange().IsValid)
                return reference;

            if (!TextControl.SelectionModel.Range.IsEmpty)
                return null;

//            создавать в дереве новую ссылку или достать таблицу по qualifier ?
//            2 ситуации: после "." и после "

            using (
                TemporaryChangeCookie temporaryChangeCookie =
                    new TemporaryChangeCookie(PsiManager.GetInstance(this.ProjectFile.GetSolution())))
            {
                IDocument document = TextControl.Document;
                int offset = TextControl.CaretModel.Offset;
                TextRange reparseRange = new TextRange(offset);
                TextRange searchRange = new TextRange(reparseRange.StartOffset + 1);
                IFile unterminatedFile = XmlFile.ReParse(reparseRange, REFERENCE_SUFFIX);
                IFile terminatedFile = unterminatedFile.ReParse(new TextRange(reparseRange.StartOffset, reparseRange.StartOffset + REFERENCE_SUFFIX.Length), REFERENCE_SUFFIX);

                reference = FindCompleteableReference(terminatedFile.FindReferencesAt(searchRange));
                PsiManager.GetInstance(this.Solution).UpdateCaches();
                return reference;
            }
        }

        private void PreEvaluteItems(List<ILookupItem> result)
        {
            ICompleteableReference reference = GetComplatebleReference();

            if (reference == null)
                return;

            if(!reference.GetDocumentRange().IsValid)
                return;

            TextRange referenceRange = reference.GetDocumentRange().TextRange;
            if (reference.GetName().EndsWith(REFERENCE_SUFFIX))
            {
                Assert.Check(referenceRange.Length >= REFERENCE_SUFFIX.Length);
                referenceRange = new TextRange(referenceRange.StartOffset, referenceRange.EndOffset - REFERENCE_SUFFIX.Length);
                
            }
            prefixRange = referenceRange;

            ISymbolTable table = reference.GetCompletionSymbolTable();
            //          TODO
            string[] names = table.Names("", true, delegate { return false; });
            List<IDeclaredElement> toAdd = new List<IDeclaredElement>();
            foreach (string name in names)
            {
                foreach (ISymbolInfo info in table.GetAllSymbolInfos(name))
                {
                    IDeclaredElement declaredElement = info.GetDeclaredElement();
                    if ((declaredElement != null) && !declaredElement.IsSynthetic() &&
                        (declaredElement is ITypeElement || declaredElement is INamespace))
                    {
                        toAdd.Add(declaredElement);
                    }
                }
            }

            foreach (IDeclaredElement element in toAdd)
            {
                DeclaredElementLookupItem item =
                    new DeclaredElementLookupItem(new DeclaredElementInstance(element),
                                                  new DeclaredElementLookupItemCreationContext(ProjectFile),
                                                  L4NLanguageService.L4N);
                item.InsertRange = new TextRange(0);
                item.ReplaceRange = new TextRange(0, /* TODO */ GetPrefix().Length);
                result.Add(item);
            }
        }

        private static ICompleteableReference FindCompleteableReference(IEnumerable<IReference> references)
        {
            foreach (IReference reference in references)
            {
                ICompleteableReference completeableReference = reference as ICompleteableReference;
                if (completeableReference != null)
                {
                    return completeableReference;
                }
            }
            return null;
        }

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