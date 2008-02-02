using Arp.log4net.Psi.Tree;
using JetBrains.ReSharper.CodeInsight.Services.Lookup;

namespace Arp.log4net.Services.CodeCompletion.Rules
{
    public class BaseParametersRule
    {
        protected LookupItemBase CreateItem(IParameterDescriptor descriptor, CodeCompletionContext context)
        {
            if (descriptor.DecraedElement != null)
                return new ParameterLookupItem(descriptor, new DeclaredElementLookupItemCreationContext(context.ProjectFile));
            else
                return new StaticParameterLookupItem(descriptor.Name);
        }
    }
}