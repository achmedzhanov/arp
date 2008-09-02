using Arp.Common.Psi.Services.CodeCompletion;
using Arp.log4net.Psi.Tree;
using JetBrains.ReSharper.CodeInsight.Services.Lookup;
using JetBrains.ReSharper.CodeInsight.Services.Xml.CodeCompletion;
using JetBrains.ReSharper.CodeInsight.Services.Xml.CodeCompletion.Impl;
using JetBrains.ReSharper.Psi.Xml.Tree;

namespace Arp.log4net.Services.CodeCompletion.Rules
{
    public class BaseParametersRule
    {
        protected LookupItemBase CreateItem(IParameterDescriptor descriptor, XmlCodeCompletionContextBase<IXmlFile> context)
        {
            if (descriptor.DecraedElement != null)
                return new ParameterLookupItem(descriptor, new DeclaredElementLookupItemCreationContext(context.ProjectFile));
            else
                return new StaticParameterLookupItem(descriptor.Name);
        }
    }
}