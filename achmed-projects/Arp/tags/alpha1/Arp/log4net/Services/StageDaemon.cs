using Arp.Xml;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi.Xml.Tree;

namespace Arp.log4net.Services
{

//TODO add daemon for C# code
//
//- quick fix "add if(log.IsXXXX)"
//- quick fix "change if(log.IsXXXX)" and "change log.XXX)"

    [DaemonStage()]
    public class L4NDaemonStage : BaseXmlDaemonStage
    {
        protected override IDaemonStageProcess CreateProcess(IDaemonProcess process, IXmlFile file)
        {
            return new L4NDaemonStageProcess(file, process);
        }

        ///<summary>
        ///
        ///            Check the error stripe indicator necessity for this stage after processing given <paramref name="projectFile" />
        ///</summary>
        ///
        public override ErrorStripeRequest NeedsErrorStripe(IProjectFile projectFile)
        {
            return ErrorStripeRequest.STRIPE_AND_ERRORS;
        }
    }
}