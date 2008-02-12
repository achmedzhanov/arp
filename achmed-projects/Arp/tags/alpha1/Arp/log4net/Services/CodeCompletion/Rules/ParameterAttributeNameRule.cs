using System.Collections.Generic;
using Arp.Assertions;
using Arp.log4net.Psi.Tree;
using JetBrains.ReSharper.CodeInsight.Services.Lookup;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace Arp.log4net.Services.CodeCompletion.Rules
{
    public class ParameterAttributeNameRule : BaseParametersRule, ICodeCompletionRule
    {
        #region ICodeCompletionRule Members

        public void Apply(CodeCompletionContext context, IList<ILookupItem> result)
        {
            IParameterDescriptorProvider provider = GetParameterDescriptorProvider(context);
            
            ICollection<IParameterDescriptor> descriptors = provider.GetParameterDescriptors();

            // it's parameters conception hak, TODO fix conception
            IXmlTag tag = context.GetAttributeTag();
            Assert.CheckNotNull(tag);

            foreach (IParameterDescriptor descriptor in descriptors)
            {
                if (!descriptor.IsAttribute)
                    continue;

                // scipr declared attribjutes
                // it's parameters conception hak, TODO fix conception
                if(tag.GetAttribute(descriptor.Name) != null)
                    continue;

                LookupItemBase item = CreateItem(descriptor, context);
                item.InsertRange = new TextRange(0);
                item.ReplaceRange = new TextRange(0, context.GetPrefix().Length);
                result.Add(item);
            }
        }

        public bool IsApplicable(CodeCompletionContext context)
        {
            if(!context.IsAvalilableAttributeNameCompletion)
                return false;

            IParameterDescriptorProvider provider = GetParameterDescriptorProvider(context);
            if(provider == null)
                return false;

//            if(!provider.IsAvailable)
//                return false;

            return true;
        }

        private IParameterDescriptorProvider GetParameterDescriptorProvider(CodeCompletionContext context)
        {
            return context.GetAttributeTag() as IParameterDescriptorProvider;
        }

        #endregion
    }
}