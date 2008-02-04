using System.Collections.Generic;
using Arp.Assertions;
using Arp.log4net.Psi;
using Arp.log4net.Psi.Tree;
using JetBrains.ReSharper.CodeInsight.Services.Lookup;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace Arp.log4net.Services.CodeCompletion.Rules
{
    public class ParameterAttributeEnumerableValuesRule : ICodeCompletionRule
    {
        #region ICodeCompletionRule Members

        public void Apply(CodeCompletionContext context, IList<ILookupItem> result)
        {
            IXmlTag tag = context.GetAttributeTag();
            Assert.CheckNotNull(tag);
            IXmlAttribute attribute = context.Token.GetContainingElement<IXmlAttribute>(false);
            Assert.CheckNotNull(attribute);

            IParameterDescriptor descriptor = null;
            IDeclaredParameter declaredParameter = tag as IDeclaredParameter;
            if (declaredParameter != null && attribute.AttributeName == L4NConstants.VALUE)
            {
                descriptor = declaredParameter.ParameterDescriptorProvider.GetParameterDescriptor(declaredParameter.Name);
            }
            else
            {
                IParameterDescriptorProvider parameterDescriptorProvider = tag as IParameterDescriptorProvider;
                if (parameterDescriptorProvider == null)
                    return;
                descriptor = parameterDescriptorProvider.GetParameterDescriptor(attribute.AttributeName);
            }

            if(descriptor == null)
                return;

            if(!descriptor.IsEnumerable)
                return;

            string[] values = descriptor.PossibleValues;
            if(values == null)
                return;

            

            foreach (string v in values)
            {
                TextLookupItem item = new TextLookupItem(v);
                item.InsertRange = new TextRange(0);
                item.ReplaceRange = new TextRange(0, context.GetPrefix().Length);
                result.Add(item);
            }
        }

        public bool IsApplicable(CodeCompletionContext context)
        {
            if(!context.IsAvalilableAttributeValueCompletion)
                return false;

            return true;
        }

        #endregion
    }
}