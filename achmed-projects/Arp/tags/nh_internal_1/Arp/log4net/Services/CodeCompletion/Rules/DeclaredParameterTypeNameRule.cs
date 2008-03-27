using Arp.Common.Assertions;
using Arp.Common.Psi.Services.CodeCompletion;
using Arp.log4net.Psi;
using Arp.log4net.Psi.Tree;
using JetBrains.ReSharper.CodeInsight.Services.Xml.CodeCompletion;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Xml.Tree;

namespace Arp.log4net.Services.CodeCompletion.Rules
{
    public class DeclaredParameterTypeNameRule : TypeNameRuleBase
    {
        protected override ITypeElement GetBaseType(BaseCodeCompletionContext context)
        {
            IDeclaredParameter declaredParameter = GetAttributeTag<IDeclaredParameter>(context);
            Assert.CheckNotNull(declaredParameter);
            IParameterDescriptor descriptor = declaredParameter.ParameterDescriptorProvider.GetParameterDescriptor(declaredParameter.Name);

            if(descriptor.RequredType)
            {
                return ((IDeclaredType) descriptor.Type).GetTypeElement();
            }

            return null;
        }

        public override bool IsApplicable(BaseCodeCompletionContext context)
        {
            if (!context.IsAvalilableAttributeValueCompletion)
                return false;

            if (!IsAttributeOf<IDeclaredParameter>(L4NConstants.TYPE, context))
                return false;

            return true;
        }
    }
}