using System.Collections.Generic;
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
        ///<summary>
        ///
        ///            If any of the registered checked answer 
        ///<c>true</c> for the given project file, then PSI will be built
        ///            
        ///</summary>
        ///
        public BuildPsiResult Check(IProjectFile projectFile)
        {
            if (projectFile.Name.EndsWith(NHProjectFileLanguageService.HBM_SUFFIX))
                return BuildPsiResult.DO_BUILD;

            if (projectFile.LanguageType != NHProjectFileLanguageService.NH)
            {
                return BuildPsiResult.UNDEF;
            }
            return BuildPsiResult.DO_BUILD;

        }


        public void Dispose()
        {
            
        }

        public void Init()
        {

        }
    }
}