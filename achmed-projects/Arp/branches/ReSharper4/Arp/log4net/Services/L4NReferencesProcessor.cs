using Arp.Common.Psi.Daemon.References;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Tree;

namespace Arp.log4net.Services
{
    public class L4NReferencesProcessor : ReferencesProcessor
    {
        protected override bool CheckElement(IElement element)
        {
            var projectFileType = element.GetProjectFile().LanguageType;
            if (projectFileType != L4NProjectFileLanguageService.L4N)
                return false;

            return base.CheckElement(element);
        }
    }
}