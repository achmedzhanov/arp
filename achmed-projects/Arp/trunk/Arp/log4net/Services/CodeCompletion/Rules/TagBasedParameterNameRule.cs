using System.Collections.Generic;
using Arp.log4net.Psi.Tree;
using JetBrains.ReSharper.CodeInsight.Services.Lookup;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace Arp.log4net.Services.CodeCompletion.Rules
{
    public class TagBasedParameterNameRule : BaseParametersRule, ICodeCompletionRule
    {
        #region ICodeCompletionRule Members

        public void Apply(CodeCompletionContext context, IList<ILookupItem> result)
        {
            IParameterDescriptorProvider parameterDescriptorProvider = GetParameterDescriptorProvider(context);

            ICollection<IParameterDescriptor> descriptors = parameterDescriptorProvider.GetParameterDescriptors();
            foreach (IParameterDescriptor descriptor in descriptors)
            {
                if (descriptor.IsAttribute)
                    continue;

                LookupItemBase item = CreateItem(descriptor, context);
                item.InsertRange = new TextRange(0);
                item.ReplaceRange = new TextRange(0, context.GetPrefix().Length);
                result.Add(item);
            }
        }

        public bool IsApplicable(CodeCompletionContext context)
        {
            if (!context.IsAvalilableTagCompletion)
                return false;

            IParameterDescriptorProvider parameterDescriptorProvider = GetParameterDescriptorProvider(context);

            if (parameterDescriptorProvider == null)
                return false;

            if (!parameterDescriptorProvider.IsAvailable)
                return false;

            return true;
        }

        private IParameterDescriptorProvider GetParameterDescriptorProvider(CodeCompletionContext context)
        {
            ITokenNode token = context.Token;
            if (token.GetTokenType() == context.GetTokenTypes().SPACE && token.GetPrevToken().GetTokenType() == context.GetTokenTypes().TAG_START)
                return token.Parent as IParameterDescriptorProvider;
            else
                return token.Parent.Parent as IParameterDescriptorProvider;
        }


        #endregion
    }
}