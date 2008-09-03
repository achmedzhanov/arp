using System.Collections.Generic;
using Arp.Common.Assertions;
using Arp.Common.Utils;
using Arp.NH.Services;
using JetBrains.Application;
using JetBrains.ComponentModel;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;

namespace Arp.NH.Psi
{
    [BuildPsiProvider(ProgramConfigurations.ALL)]
    public class NHSupportedByPSIChecker : ISupportedByPSIChecker, IShellComponent
    {
        private bool fixedLangServiceOrder;

        ///<summary>
        ///
        ///            If any of the registered checked answer 
        ///<c>true</c> for the given project file, then PSI will be built
        ///            
        ///</summary>
        ///
        public BuildPsiResult Check(IProjectFile projectFile)
        {
            FixLangServiceOrder();
            
            
            if (projectFile.Name.EndsWith(NHProjectFileLanguageService.HBM_SUFFIX))
                return BuildPsiResult.DO_BUILD;

            if (projectFile.LanguageType != NHProjectFileLanguageService.NH)
            {
                return BuildPsiResult.UNDEF;
            }
            return BuildPsiResult.DO_BUILD;

        }

        private void FixLangServiceOrder()
        {
            if(fixedLangServiceOrder)
                return;

            IList<IProjectFileLanguageService> services = ProjectFileLanguageServiceManager.Instance.Services;
            NHProjectFileLanguageService projectFileLanguageService =
                (NHProjectFileLanguageService) CollectionsUtils.Find(services, t => t is NHProjectFileLanguageService);

            Assert.CheckNotNull(projectFileLanguageService);

            services.Remove(projectFileLanguageService);
            services.Insert(0,projectFileLanguageService);

            fixedLangServiceOrder = true;
        }


        public void Dispose()
        {
            
        }

        public void Init()
        {

        }
    }
}