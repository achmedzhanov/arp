using Arp.Common.Psi.Daemon.References;
using Arp.Databases;
using Arp.NH.Psi.Tree;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Tree;

namespace Arp.NH.Services.References
{
    public class NHReferencesProcessor : ReferencesProcessor
    {
        protected override bool CheckResolve(IReference reference, IElement element)
        {
            if (reference is TableReference && !DatabaseManager.GetInstance(element.GetManager().Solution).Enabled)
                return false;
            
            return base.CheckResolve(reference, element);
        }

        protected override bool CheckElement(IElement element)
        {
            if (!NHProjectFileLanguageService.IsNHFile(element.GetProjectFile()))
                return false;            

            return base.CheckElement(element);
        }
    }
}