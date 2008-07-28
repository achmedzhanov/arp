using Arp.NH.Services;
using JetBrains.ComponentModel;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;

namespace Arp.NH.Psi
{
    [BuildPsiProvider(ProgramConfigurations.ALL)]
    public class NHSupportedByPSIChecker : ISupportedByPSIChecker
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
            if (projectFile.LanguageType != NHProjectFileLanguageService.NH)
            {
                return BuildPsiResult.UNDEF;
            }
            return BuildPsiResult.DO_BUILD;

        }        
    }
}