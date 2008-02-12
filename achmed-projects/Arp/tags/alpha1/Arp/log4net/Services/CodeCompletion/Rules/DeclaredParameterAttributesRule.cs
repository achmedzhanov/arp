using System.Collections.Generic;
using Arp.Assertions;
using Arp.log4net.Psi.Tree;
using JetBrains.ReSharper.CodeInsight.Services.Lookup;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace Arp.log4net.Services.CodeCompletion.Rules
{
    public class DeclaredParameterAttributesRule : ICodeCompletionRule
    {
        #region ICodeCompletionRule Members

        public void Apply(CodeCompletionContext context, IList<ILookupItem> result)
        {
            IDeclaredParameter declaredParameter = GetDeclaredParameter(context);
            Assert.CheckNotNull(declaredParameter);

            IParameterDescriptor descriptor =
                declaredParameter.ParameterDescriptorProvider.GetParameterDescriptor(declaredParameter.Name);

            LookupItemBase item = null;
            if (!descriptor.RequredType)
                item = new TextLookupItem("value");
            else
                item = new TextLookupItem("type");

            item.InsertRange = new TextRange(0);
            item.ReplaceRange = new TextRange(0, context.GetPrefix().Length);

            result.Add(item);
        }

        public bool IsApplicable(CodeCompletionContext context)
        {
            if (!context.IsAvalilableAttributeNameCompletion)
                return false;

            IDeclaredParameter declaredParameter = GetDeclaredParameter(context);
            if (declaredParameter == null)
                return false;

            return true;
        }

        private IDeclaredParameter GetDeclaredParameter(CodeCompletionContext context)
        {
            return context.GetAttributeTag() as IDeclaredParameter;
        }

        #endregion
    }
}