using Arp.Common.Psi.Daemon.References;
using Arp.Databases;
using Arp.NH.Psi.Tree;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Tree;

namespace Arp.NH.Services.References
{
    public class DatabaseObjectsReferenceProcessor : NHReferencesProcessor
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