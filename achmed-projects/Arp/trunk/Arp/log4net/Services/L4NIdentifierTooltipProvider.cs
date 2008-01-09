using JetBrains.ComponentModel;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;

namespace Arp.log4net.Services
{
//    [SolutionComponentImplementation, SolutionComponentInterface(ProgramConfigurations.ALL)]
    public class L4NIdentifierTooltipProvider : IdentifierTooltipProvider, ISolutionComponent
    {
        public L4NIdentifierTooltipProvider(ISolution solution) : base(solution)
        {
        }


        public void AfterSolutionOpened()
        {
            // do nothing
        }

        public void BeforeSolutionClosed()
        {
            // do nothing
        }

        public void Init()
        {
            // do nothing
        }

        ///<summary>
        ///Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ///</summary>
        ///<filterpriority>2</filterpriority>
        public void Dispose()
        {
            // do nothing
        }

        public static L4NIdentifierTooltipProvider GetInstance(ISolution solution)
        {
            return solution.GetComponent<L4NIdentifierTooltipProvider>();
        }

    }
}