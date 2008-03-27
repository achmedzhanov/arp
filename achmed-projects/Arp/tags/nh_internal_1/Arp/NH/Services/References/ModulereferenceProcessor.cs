using Arp.Common.Psi.Daemon.References;
using Arp.log4net.Psi.Tree.Impl;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi;

namespace Arp.NH.Services.References
{
    public class ModulereferenceProcessor : ReferencesProcessor
    {
        protected override string GetHighlightAttributeForReference(IDeclaredElement element)
        {
            if (element is ModuleDeclaredElement)
                return HighlightingAttributeIds.NAMESPACE_IDENTIFIER_ATTRIBUTE;
            else
                return null;
        }
    }
}