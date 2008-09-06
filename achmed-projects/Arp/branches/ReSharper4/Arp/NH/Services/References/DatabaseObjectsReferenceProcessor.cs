using Arp.Common.Psi.Daemon.References;
using Arp.NH.Psi.Tree;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi;

namespace Arp.NH.Services.References
{
    public class DatabaseObjectsReferenceProcessor : ReferencesProcessor
    {
        protected override string GetHighlightAttributeForReference(IDeclaredElement element)
        {
            if (element is IDatabaseObjectDeclaredElement)
                return HighlightingAttributeIds.CONSTANT_IDENTIFIER_ATTRIBUTE;
            else
                return null;
        }
    }
}