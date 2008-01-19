using System.Collections.Generic;
using Arp.log4net.Psi.Tree;
using Arp.log4net.Services.CodeCompletion;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.CodeInsight.Services.CodeCompletion;
using JetBrains.ReSharper.CodeInsight.Services.Lookup;
using JetBrains.ReSharper.CodeInsight.Services.Xml.CodeCompletion;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.ReSharper.TextControl;
using JetBrains.Util;

namespace Arp.log4net.Services.CodeCompletion
{
    public class ParamNameCompletionContext : XmlCodeCompletionContextBase<IXmlFile>, IL4NCodeCompletionContext
    {
        private ITokenNode token;
        private TextRange prefixRange;
        private bool isAvalilableTagCompletion = true;

        public ParamNameCompletionContext(ISolution solution, ITextControl textControl) : base(solution, textControl)
        {
            IXmlFile file = this.XmlFile;
            Logger.Assert(file != null, "file != null");
            int offset = this.CaretOffset;
            token = (ITokenNode)file.FindTokenAt(offset);

            Initilize();
        }

        private void Initilize()
        {
            if (token == null || !token.Parent.IsValid())
            {
                isAvalilableTagCompletion = false;
                return;
            }
            
            XmlTokenTypes xmlTokenTypes = GetTokenTypes();

            // shift to token '<'
            ITokenNode currentToken = token;
            if (xmlTokenTypes.SPACE == currentToken.GetTokenType())
            {
                if (currentToken.GetDocumentRange().TextRange.StartOffset == this.TextControl.CaretModel.Offset)
                    currentToken = currentToken.GetPrevToken();
                else
                {
                    isAvalilableTagCompletion = false;
                    return;
                }
            }

            if (xmlTokenTypes.IDENTIFIER == currentToken.GetTokenType())
            {
                currentToken = currentToken.GetPrevToken();
            }

            if (xmlTokenTypes.TAG_START == currentToken.GetTokenType())
            {
                this.prefixRange = new TextRange(currentToken.GetNextToken().GetDocumentRange().TextRange.StartOffset, TextControl.CaretModel.Offset);
            }
            else
            {
                isAvalilableTagCompletion = false;
                return;
            }
        }

        public TextRange PrefixRange
        {
            get { return prefixRange; }
        }

        public bool IsAvailable()
        {
            if (!isAvalilableTagCompletion)
                return false;

            IParameterDescriptorProvider parameterDescriptorProvider = GetParameterDescriptorProvider();

            if (parameterDescriptorProvider == null)
                return false;

            if (!parameterDescriptorProvider.IsAvailable)
                return false;
                
            return true;

        }

        private IParameterDescriptorProvider GetParameterDescriptorProvider()
        {
            if(token.GetTokenType() == GetTokenTypes().SPACE &&  token.GetPrevToken().GetTokenType() == GetTokenTypes().TAG_START)
                return token.Parent as IParameterDescriptorProvider;
            else
                return  token.Parent.Parent as IParameterDescriptorProvider;
        }

        private XmlTokenTypes GetTokenTypes()
        {
            return XmlTokenTypeFactory.GetTokenTypes(XmlFile.Language);
        }

        public IList<ILookupItem> EvaluateLookupItems()
        {
            IParameterDescriptorProvider parameterDescriptorProvider = GetParameterDescriptorProvider();

            List<ILookupItem> result = new List<ILookupItem>();

            ICollection<IParameterDescriptor> descriptors = parameterDescriptorProvider.GetParameterDescriptors();
            foreach (IParameterDescriptor descriptor in descriptors)
            {
                ParameterLookupItem item = new ParameterLookupItem(descriptor, new DeclaredElementLookupItemCreationContext(ProjectFile));
                item.InsertRange = new TextRange(0);
                item.ReplaceRange = new TextRange(0,GetPrefix().Length);
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
            
    }
}