using Arp.log4net.Services;
using JetBrains.ComponentModel;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;

namespace Arp.log4net.Psi
{

    [BuildPsiProvider(ProgramConfigurations.ALL)]
    public class L4NSupportedByPSIChecker : ISupportedByPSIChecker
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
            if (projectFile.LanguageType != L4NProjectFileLanguageService.L4N)
            {
                return BuildPsiResult.UNDEF;
            }
            return BuildPsiResult.DO_BUILD;

        }
    }
}